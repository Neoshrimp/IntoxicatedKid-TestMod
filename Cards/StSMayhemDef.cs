﻿using LBoL.ConfigData;
using LBoL.Core.Cards;
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
using HarmonyLib;
using LBoL.Core.StatusEffects;
using UnityEngine.Rendering;
using LBoL.Core.Units;
using LBoL.EntityLib.Exhibits.Shining;
using Mono.Cecil;
using JetBrains.Annotations;
using System.Linq;
using LBoL.EntityLib.StatusEffects.Neutral.Black;
using LBoL.EntityLib.Cards.Character.Marisa;
using LBoL.EntityLib.Cards.Neutral.Black;
using LBoL.EntityLib.Cards.Other.Enemy;

namespace test.Cards
{
    public sealed class StSMayhemDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSMayhem);
        }
        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, ".png", relativePath: "Resources.");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.CardsEn.yaml");
            return locFiles;
        }
        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: sequenceTable.Next(typeof(CardConfig)),
                Id: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "Simple1",
                GunNameBurst: "Simple1",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Ability,
                TargetType: TargetType.Self,
                Colors: new List<ManaColor>() { ManaColor.Colorless },
                IsXCost: false,
                Cost: new ManaGroup() { Any = 3, Colorless = 1 },
                UpgradedCost: new ManaGroup() { Any = 1, Colorless = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 1,
                UpgradedValue1: null,
                Value2: null,
                UpgradedValue2: null,
                Mana: null,
                UpgradedMana: null,
                Scry: null,
                UpgradedScry: null,
                ToolPlayableTimes: null,

                Loyalty: null,
                UpgradedLoyalty: null,
                PassiveCost: null,
                UpgradedPassiveCost: null,
                ActiveCost: null,
                UpgradedActiveCost: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: null,
                Unfinished: false,
                Illustrator: "Mega Crit",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }
        [EntityLogic(typeof(StSMayhemDef))]
        public sealed class StSMayhem : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return BuffAction<StSMayhemSeDef.StSMayhemSe>(Value1, 0, 0, 0, 0.2f);
                yield break;
            }
        }

    }
    public sealed class StSMayhemSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSMayhemSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.StSMayhemSe.png", embeddedSource);
        }
        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                Id: "",
                Order: 10,
                Type: StatusEffectType.Positive,
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
        [EntityLogic(typeof(StSMayhemSeDef))]
        public sealed class StSMayhemSe : StatusEffect
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
                ReactOwnerEvent(Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnPlayerTurnStarted));
            }
            private IEnumerable<BattleAction> OnPlayerTurnStarted(GameEventArgs args)
            {
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                NotifyActivating();
                GameRun.SynergyAdditionalCount += 1;
                int mayhemcount = 1;
                while (mayhemcount <= Level)
                {
                    if (Battle.BattleShouldEnd)
                    {
                        GameRun.SynergyAdditionalCount -= 1;
                        yield break;
                    }
                    List<Card> list = Battle.DrawZone.ToList();
                    List<Card> list2 = Battle.DiscardZone.ToList();
                    if (list.Count <= 0 && list2.Count > 0)
                    {
                        yield return new ReshuffleAction();
                        mayhemcount -= 1;
                    }
                    else if (list.Count > 0)
                    {
                        Card card = list.First();
                        Battle.MaxHand += 1;
                        yield return new MoveCardAction(card, CardZone.Hand);
                        Battle.MaxHand -= 1;
                        if (card.Zone == CardZone.Hand && (card is Bribery || card is Payment))
                        {
                            yield return new ExileCardAction(card);
                        }
                        else if (card.Zone == CardZone.Hand && card.IsForbidden)
                        {
                            yield return new DiscardAction(card);
                        }
                        else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.Nobody && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                        {
                            Helpers.FakeQueueConsumingMana(Mana);
                            yield return new UseCardAction(card, UnitSelector.Nobody, Mana);
                        }
                        else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.SingleEnemy && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                        {
                            EnemyUnit enemy = Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                            UnitSelector unitSelector = new UnitSelector(enemy);
                            Helpers.FakeQueueConsumingMana(Mana);
                            yield return new UseCardAction(card, unitSelector, Mana);
                        }
                        else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.AllEnemies && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                        {
                            Helpers.FakeQueueConsumingMana(Mana);
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, Mana);
                        }
                        else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.RandomEnemy && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                        {
                            Helpers.FakeQueueConsumingMana(Mana);
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, Mana);
                        }
                        else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.Self && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                        {
                            Helpers.FakeQueueConsumingMana(Mana);
                            yield return new UseCardAction(card, UnitSelector.Self, Mana);
                        }
                        else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.All && (card.CardType != CardType.Friend || card.CardType == CardType.Friend && !card.Summoned || card.CardType == CardType.Friend && card.Summoned && card.Loyalty >= -card.MinFriendCost) && !card.IsForbidden)
                        {
                            Helpers.FakeQueueConsumingMana(Mana);
                            yield return new UseCardAction(card, UnitSelector.All, Mana);
                        }
                    }
                    mayhemcount += 1;
                }
                GameRun.SynergyAdditionalCount -= 1;
            }
        }
    }
}