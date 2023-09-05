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
            // embedded resource folders are separated by a dot
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
                Rarity: Rarity.Common,
                Value1: 4,
                Value2: null,
                Value3: null,
                Mana: null,
                BaseManaRequirement: null,
                BaseManaColor: null,
                BaseManaAmount: 0,
                HasCounter: false,
                InitialCounter: 0,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                // example of referring to UniqueId of an entity without calling MakeConfig
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
                Exhibit[] array = base.GameRun.Player.Exhibits.Where((Exhibit e) => e.Config.Rarity == Rarity.Shining).ToArray<Exhibit>();
                int exhibitbonus = (int)(base.Value1 + decimal.Round(array.Length, MidpointRounding.ToEven));
                yield return new ApplyStatusEffectAction<TASBotSeDef.TASBotSe>(base.Owner, new int?(exhibitbonus), null, null, null, 0f, true);
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
                Order: 11,
                Type: StatusEffectType.Special,
                IsVerbose: false,
                IsStackable: true,
                StackActionTriggerLevel: null,
                HasLevel: true,
                LevelStackType: StackType.Add,
                HasDuration: false,
                DurationStackType: StackType.Add,
                DurationDecreaseTiming: DurationDecreaseTiming.Custom,
                HasCount: false,
                CountStackType: StackType.Add,
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
                base.HandleOwnerEvent<CardEventArgs>(base.Battle.Predraw, new GameEventHandler<CardEventArgs>(this.OnPredraw));
                base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarted));
            }
            private void OnPredraw(CardEventArgs args)
            {
                if (args.Cause == ActionCause.TurnStart)
                {
                    base.NotifyActivating();
                    args.CancelBy(this);
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
                int max = (this.Level);
                int count = 1;
                while (count <= max)
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        base.GameRun.SynergyAdditionalCount -= 1;
                        yield break;
                    }
                    List<Card> list = base.Battle.DrawZone.ToList<Card>();
                    List<Card> list2 = base.Battle.DiscardZone.ToList<Card>();
                    if (list.Count == 0 && list2.Count > 0)
                    {
                        yield return new ReshuffleAction();
                        count -= 1;
                    }
                    if (list.Count > 0)
                    {
                        Card card = list.First();
                        yield return new MoveCardAction(card, CardZone.Hand);
                        card.PendingManaUsage = new ManaGroup() { Philosophy = 1 };
                        if (((card.CardType == CardType.Misfortune) || (card.CardType == CardType.Status)) && card.IsForbidden)
                        {
                            yield return new DiscardAction(card);
                        }
                        else if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.Nobody) && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                        }
                        else if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.SingleEnemy) && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                        {
                            EnemyUnit enemy = base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                            UnitSelector unitSelector = new UnitSelector(enemy);
                            yield return new UseCardAction(card, unitSelector, this.Mana);
                        }
                        else if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.AllEnemies) && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, this.Mana);
                        }
                        else if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.RandomEnemy) && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                        else if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.Self) && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, this.Mana);
                        }
                        else if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.All) && ((card.CardType != CardType.Friend) || (card.CardType == CardType.Friend && !card.Summoned) || (card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost)) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, this.Mana);
                        }
                    }
                    count += 1;
                }
                base.GameRun.SynergyAdditionalCount -= 1;
                yield break;
            }
        }
    }
}