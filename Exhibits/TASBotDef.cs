﻿using LBoL.ConfigData;
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
using HarmonyLib;
using LBoLEntitySideloader.ReflectionHelpers;
using System.Reflection.Emit;
using System.Reflection;

namespace test.Exhibits
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
            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite(folder + GetId() + s + ".png", embeddedSource);
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
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStarted));
            }
            private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
            {
                NotifyActivating();
                //Exhibit[] array = base.GameRun.Player.Exhibits.Where((Exhibit e) => e.Config.Rarity == Rarity.Shining).ToArray<Exhibit>();
                //int exhibitbonus = (int)(base.Value1 + decimal.Round(array.Length, MidpointRounding.ToEven));
                yield return new ApplyStatusEffectAction<TASBotSeDef.TASBotSe>(Owner, null, null, null, Value1, 0f, true);
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
        /*[HarmonyPatch]
        [HarmonyDebug]
        class funny_Patch
        {
            static IEnumerable<MethodBase> TargetMethods()
            {
                yield return ExtraAccess.InnerMoveNext(typeof(SelectCardPanel), nameof(SelectCardPanel.ViewMiniSelect));
            }
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                int i = 0;
                var ciList = instructions.ToList();
                var c = ciList.Count();
                CodeInstruction prevCi = null;
                foreach (var ci in instructions)
                {
                    if (ci.Is(OpCodes.Ldc_R4, 0.2f) && prevCi.Is(OpCodes.Ldc_R4, 0f))
                    {
                        yield return new CodeInstruction(OpCodes.Ldc_R4, 0f);
                    }
                    else if (ci.opcode == OpCodes.Leave)
                    {
                        yield return ci;
                        yield return new CodeInstruction(OpCodes.Nop);
                    }
                    else
                    {
                        yield return ci;
                    }
                    prevCi = ci;
                    i++;
                }
            }
        }*/
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
                LastSpell = false;
                foreach (Card card in Battle.HandZone.Where((card) => card.CardType == CardType.Status))
                {
                    React(new ExileCardAction(card));
                }
                HandleOwnerEvent(Battle.Player.TurnStarting, delegate (UnitEventArgs _)
                {
                    Count = Limit;
                });
                HandleOwnerEvent(Battle.Predraw, new GameEventHandler<CardEventArgs>(OnPredraw));
                HandleOwnerEvent(Owner.Dying, new GameEventHandler<DieEventArgs>(OnDying));
                HandleOwnerEvent(Battle.Player.DamageReceived, new GameEventHandler<DamageEventArgs>(OnPlayerDamageReceived));
                HandleOwnerEvent(Owner.StatusEffectAdding, new GameEventHandler<StatusEffectApplyEventArgs>(OnStatusEffectAdding));
                ReactOwnerEvent(Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnPlayerTurnStarted));
                ReactOwnerEvent(Battle.CardDrawn, new EventSequencedReactor<CardEventArgs>(OnCardDrawn));
                ReactOwnerEvent(Battle.CardMoved, new EventSequencedReactor<CardMovingEventArgs>(OnCardMoved));
                ReactOwnerEvent(Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(OnCardsAddedToHand));
            }
            private void OnPredraw(CardEventArgs args)
            {
                if (Count > 0)
                {
                    int num = Count - 1;
                    Count = num;
                    return;
                }
                NotifyActivating();
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
                if (GameRun.Battle != null && Battle.Player.Power >= Battle.Player.Us.PowerCost)
                {
                    NotifyActivating();
                    int num = (args.Unit.MaxHp * 20 / 100.0).RoundToInt();
                    GameRun.SetHpAndMaxHp(num, args.Unit.MaxHp, false);
                    args.CancelBy(this);
                    LastSpell = true;
                }
            }
            private void OnPlayerDamageReceived(DamageEventArgs args)
            {
                if (GameRun.Battle != null && LastSpell)
                {
                    LastSpell = false;
                    NotifyActivating();
                    React(new ApplyStatusEffectAction<Invincible>(Owner, null, 1, null, null, 0f, true));
                    EnemyUnit attacker = args.Source is EnemyUnit unit ? unit : Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                    UnitSelector unitSelector;
                    switch (Battle.Player.Us.TargetType)
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
                    React(new UseUsAction(Battle.Player.Us, unitSelector2, Battle.Player.Us.PowerCost).SetCause(ActionCause.Player));
                    NotifyActivating();
                    NotifyChanged();
                }
            }
            private void OnStatusEffectAdding(StatusEffectApplyEventArgs args)
            {
                if (args.Effect is FoxCharm)
                {
                    args.CancelBy(this);
                    NotifyActivating();
                }
            }
            private IEnumerable<BattleAction> OnPlayerTurnStarted(GameEventArgs args)
            {
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                NotifyActivating();
                GameRun.SynergyAdditionalCount += 1;
                List<Card> list = Battle.HandZone.ToList();
                foreach (Card card in list)
                {
                    if (Battle.BattleShouldEnd)
                    {
                        GameRun.SynergyAdditionalCount -= 1;
                        yield break;
                    }
                    else if (card.Config.TargetType == TargetType.Nobody && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.SingleEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.AllEnemies && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.RandomEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.Self && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.All && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Mana);
                        }
                    }
                }
                GameRun.SynergyAdditionalCount -= 1;
                yield return new RequestEndPlayerTurnAction();
                yield break;
            }
            private IEnumerable<BattleAction> OnCardDrawn(CardEventArgs args)
            {
                Card card = args.Card;
                if (args.Cause != ActionCause.TurnStart && args.Card.Zone == CardZone.Hand)
                {
                    if (Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    else if (card.Config.TargetType == TargetType.Nobody && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.SingleEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.AllEnemies && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.RandomEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.Self && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.All && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Mana);
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
                    if (Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    else if (card.Config.TargetType == TargetType.Nobody && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.SingleEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.AllEnemies && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.RandomEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.Self && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.All && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Mana);
                        }
                    }
                }
                yield break;
            }
            private IEnumerable<BattleAction> OnCardsAddedToHand(CardsEventArgs args)
            {
                List<Card> list = args.Cards.Where((card) => card.Zone == CardZone.Hand && !card.Summoned).ToList();
                foreach (Card card in list)
                {
                    if (Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    else if (card is Bribery || card is Payment)
                    {
                        yield return new ExileCardAction(card);
                    }
                    else if (card.Config.TargetType == TargetType.Nobody && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.SingleEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        EnemyUnit enemy = Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                        UnitSelector unitSelector = new UnitSelector(enemy);
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, unitSelector, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, unitSelector, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.AllEnemies && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.RandomEnemy && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.Self && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, Mana);
                        }
                    }
                    else if (card.Config.TargetType == TargetType.All && card.Zone == CardZone.Hand && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                    {
                        if (card.IsXCost)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Battle.BattleMana);
                        }
                        else
                        {
                            yield return new UseCardAction(card, UnitSelector.All, Mana);
                        }
                    }
                }
                yield break;
            }
        }
    }
}