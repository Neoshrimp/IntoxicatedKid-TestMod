using LBoL.ConfigData;
using LBoL.Core.Cards;
using JetBrains.Annotations;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using static test.BepinexPlugin;
using UnityEngine;
using LBoL.Core;
using LBoL.Base;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Base.Extensions;
using System.Collections;
using LBoL.Presentation;
using LBoL.EntityLib.Cards.Neutral.Blue;
using Mono.Cecil;
using LBoL.Core.StatusEffects;
using System.Linq;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Randoms;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Other.Misfortune;
using static UnityEngine.TouchScreenKeyboard;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Cirno.Friend;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.MultiColor;

using LBoL.Presentation.UI.Panels;
using UnityEngine.InputSystem.Controls;
using LBoL.EntityLib.EnemyUnits.Normal;
using LBoL.Presentation.UI;
using LBoL.EntityLib.StatusEffects.Cirno;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoL.EntityLib.Cards.Other.Enemy;

namespace test
{
    public sealed class TASBotDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(TASBot);
        }
        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.ExhibitsEn.yaml");
            return locFiles;
        }
        public override ExhibitSprites LoadSprite()
        {
            var folder = "";
            var exhibitSprites = new ExhibitSprites();
            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite((folder + GetId() + s + ".png"), embeddedSource);
            exhibitSprites.main = wrap("");
            return exhibitSprites;
        }
        public override ExhibitConfig MakeConfig()
        {
            var exhibitConfig = new ExhibitConfig(
                Index: sequenceTable.Next(typeof(ExhibitConfig)),
                Id: "",
                Order: 10,
                IsDebug: false,
                IsPooled: false,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.Nowhere,
                Owner: "",
                LosableType: ExhibitLosableType.CantLose,
                Rarity: Rarity.Rare,
                Value1: 15,
                Value2: null,
                Value3: null,
                Mana: new ManaGroup() { Any = 0 },
                BaseManaRequirement: null,
                BaseManaColor: null,
                BaseManaAmount: 0,
                HasCounter: false,
                InitialCounter: 0,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { "Invincible" },
                RelativeCards: new List<string>() { }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(TASBotDef))]
        [UsedImplicitly]
        public sealed class TASBot : Exhibit
        {
            protected override void OnEnterBattle()
            {
                base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
            }
            private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
            {
                base.NotifyActivating();
                //Exhibit[] array = base.GameRun.Player.Exhibits.Where((Exhibit e) => e.Config.Rarity == Rarity.Shining).ToArray<Exhibit>();
                //int exhibitbonus = (int)(base.Value1 + decimal.Round(array.Length, MidpointRounding.ToEven));
                yield return new ApplyStatusEffectAction<TASBotSeDef.TASBotSe>(base.Owner, null, null, null, base.Value1, 0f, true);
                yield break;
            }
        }
    }
    public sealed class TASBotSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(TASBotSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.TASBotSe.png", embeddedSource);
        }
        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                Id: "",
                Order: 70,
                Type: StatusEffectType.Special,
                IsVerbose: false,
                IsStackable: true,
                StackActionTriggerLevel: null,
                HasLevel: false,
                LevelStackType: StackType.Add,
                HasDuration: false,
                DurationStackType: StackType.Add,
                DurationDecreaseTiming: DurationDecreaseTiming.Custom,
                HasCount: true,
                CountStackType: StackType.Keep,
                LimitStackType: StackType.Keep,
                ShowPlusByLimit: false,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                VFX: "Default",
                VFXloop: "Default",
                SFX: "Default"
            );
            return statusEffectConfig;
        }
        [EntityLogic(typeof(TASBotSeDef))]
        public sealed class TASBotSe : StatusEffect
        {
            private bool LastSpell;
            [UsedImplicitly]
            public ManaGroup Mana
            {
                get
                {
                    return ManaGroup.Anys(0);
                }
            }
            protected override void OnAdded(Unit unit)
            {
                this.LastSpell = false;
                foreach (Card card in base.Battle.HandZone.Where((Card card) => card.CardType == CardType.Status))
                {
                    this.React(new ExileCardAction(card));
                }
                base.HandleOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarting, delegate (UnitEventArgs _)
                {
                    base.Count = base.Limit;
                });
                base.HandleOwnerEvent<CardEventArgs>(base.Battle.Predraw, new GameEventHandler<CardEventArgs>(this.OnPredraw));
                base.HandleOwnerEvent<DieEventArgs>(base.Owner.Dying, new GameEventHandler<DieEventArgs>(this.OnDying));
                base.HandleOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageReceived, new GameEventHandler<DamageEventArgs>(this.OnPlayerDamageReceived));
                base.HandleOwnerEvent<StatusEffectApplyEventArgs>(base.Owner.StatusEffectAdding, new GameEventHandler<StatusEffectApplyEventArgs>(this.OnStatusEffectAdding));
                base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarted));
                base.ReactOwnerEvent<CardEventArgs>(base.Battle.CardDrawn, new EventSequencedReactor<CardEventArgs>(this.OnCardDrawn));
                base.ReactOwnerEvent<CardMovingEventArgs>(base.Battle.CardMoved, new EventSequencedReactor<CardMovingEventArgs>(this.OnCardMoved));
                base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(this.OnCardsAddedToHand));
            }
            private void OnPredraw(CardEventArgs args)
            {
                if (base.Count > 0)
                {
                    int num = base.Count - 1;
                    base.Count = num;
                    return;
                }
                base.NotifyActivating();
                args.CancelBy(this);
            }
            /*public override bool ShouldPreventCardUsage(Card card)
            {
                return true;
            }
            public override string PreventCardUsageMessage
            {
                get
                {
                    return "Auto battle in progress.";
                }
            }*/
            private void OnDying(DieEventArgs args)
            {
                if (base.GameRun.Battle != null && base.Battle.Player.Power >= base.Battle.Player.Us.PowerCost)
                {
                    base.NotifyActivating();
                    int num = ((double)(args.Unit.MaxHp * 20) / 100.0).RoundToInt();
                    base.GameRun.SetHpAndMaxHp(num, args.Unit.MaxHp, false);
                    args.CancelBy(this);
                    this.LastSpell = true;
                }
            }
            private void OnPlayerDamageReceived(DamageEventArgs args)
            {
                if (base.GameRun.Battle != null && this.LastSpell)
                {
                    this.LastSpell = false;
                    base.NotifyActivating();
                    this.React(new ApplyStatusEffectAction<Invincible>(base.Owner, null, 1, null, null, 0f, true));
                    EnemyUnit attacker = args.Source is EnemyUnit unit ? unit : base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                    UnitSelector unitSelector;
                    switch (base.Battle.Player.Us.TargetType)
                    {
                        case TargetType.Nobody:
                            unitSelector = UnitSelector.Nobody;
                            goto next;
                        case TargetType.SingleEnemy:
                            unitSelector = new UnitSelector(attacker);
                            goto next;
                        case TargetType.AllEnemies:
                            unitSelector = UnitSelector.AllEnemies;
                            goto next;
                        case TargetType.RandomEnemy:
                            unitSelector = UnitSelector.RandomEnemy;
                            goto next;
                        case TargetType.Self:
                            unitSelector = UnitSelector.Self;
                            goto next;
                        case TargetType.All:
                            unitSelector = UnitSelector.All;
                            goto next;
                    }
                    throw new ArgumentOutOfRangeException();
                next:
                    UnitSelector unitSelector2 = unitSelector;
                    this.React(new UseUsAction(base.Battle.Player.Us, unitSelector2, base.Battle.Player.Us.PowerCost).SetCause(ActionCause.Player));
                    base.NotifyActivating();
                    base.NotifyChanged();
                }
            }
            private void OnStatusEffectAdding(StatusEffectApplyEventArgs args)
            {
                if (args.Effect is FoxCharm)
                {
                    args.CancelBy(this);
                    base.NotifyActivating();
                }
            }
            private IEnumerable<BattleAction> OnPlayerTurnStarted(GameEventArgs args)
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                base.NotifyActivating();
                base.GameRun.SynergyAdditionalCount += 1;
                //Card usedcard = null;
                List<Card> list = base.Battle.HandZone.ToList<Card>();
                foreach (Card card in list)
                {
                    /*if (usedcard.CardType == CardType.Friend && usedcard.Loyalty >= usedcard.UltimateCost && card.CardType == CardType.Friend)
                    {
                        GameMaster.Instance.StartCoroutine(Wait());
                    }*/
                    if (base.Battle.BattleShouldEnd)
                    {
                        base.GameRun.SynergyAdditionalCount -= 1;
                        yield break;
                    }
                    else if ((card.Config.TargetType == TargetType.Nobody) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.SingleEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.AllEnemies) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.RandomEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.Self) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.All) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, this.Mana);
                        }
                    }
                    //usedcard = card;
                }
                base.GameRun.SynergyAdditionalCount -= 1;
                yield return new RequestEndPlayerTurnAction();
                yield break;
            }
            private IEnumerable<BattleAction> OnCardDrawn(CardEventArgs args)
            {
                Card card = args.Card;
                if (args.Cause != ActionCause.TurnStart && args.Card.Zone == CardZone.Hand)
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    else if ((card.Config.TargetType == TargetType.Nobody) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.SingleEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.AllEnemies) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.RandomEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.Self) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.All) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, this.Mana);
                        }
                    }
                }
                yield break;
            }
            private IEnumerable<BattleAction> OnCardMoved(CardMovingEventArgs args)
            {
                Card card = args.Card;
                if (args.Cause != ActionCause.TurnStart && card.Zone == CardZone.Hand && !(args.SourceZone == CardZone.PlayArea && args.DestinationZone == CardZone.Hand))
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    else if ((card.Config.TargetType == TargetType.Nobody) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.SingleEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.AllEnemies) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.RandomEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.Self) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.All) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, this.Mana);
                        }
                    }
                }
                yield break;
            }
            private IEnumerable<BattleAction> OnCardsAddedToHand(CardsEventArgs args)
            {
                List<Card> list = args.Cards.Where((Card card) => card.Zone == CardZone.Hand && !card.Summoned).ToList<Card>();
                foreach (Card card in list)
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    else if (card is Bribery || card is Payment)
                    {
                        yield return new ExileCardAction(card);
                    }
                    else if ((card.Config.TargetType == TargetType.Nobody) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.SingleEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.AllEnemies) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.RandomEnemy) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.Self) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, this.Mana);
                        }
                    }
                    else if ((card.Config.TargetType == TargetType.All) && card.Zone == CardZone.Hand && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, base.Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, this.Mana);
                        }
                    }
                }
                yield break;
            }
            /*private IEnumerator Wait()
            {
                yield return new WaitForSeconds(0.2f);
            }*/
        }
    }
}