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
using test;
using LBoL.EntityLib.PlayerUnits;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.StatusEffects.Reimu;
using LBoL.Presentation.UI;
using LBoL.Presentation.UI.Panels;
using LBoLEntitySideloader.ExtraFunc;

namespace test
{
    public sealed class StSDoubleTapDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSDoubleTap);
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
                Type: CardType.Skill,
                TargetType: TargetType.Self,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 1 },
                UpgradedCost: new ManaGroup() { Red = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 1,
                UpgradedValue1: 2,
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
        [EntityLogic(typeof(StSDoubleTapDef))]
        public sealed class StSDoubleTap : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return base.BuffAction<StSDoubleTapSeDef.StSDoubleTapSe>(base.Value1, 0, 0, 0, 0.2f);
                yield break;
            }
        }

    }
    public sealed class StSDoubleTapSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSDoubleTapSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.StSDoubleTapSe.png", embeddedSource);
        }
        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                Id: "",
                Order: 20,
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
        [EntityLogic(typeof(StSDoubleTapSeDef))]
        public sealed class StSDoubleTapSe : StatusEffect
        {
            private bool Again = false;
            private Card Card = null;
            private UnitSelector unitSelector = null;
            ManaGroup manaConsumed = new ManaGroup();



            protected override void OnAdded(Unit unit)
            {
                base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsing, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsing));
                base.ReactOwnerEvent<CardMovingEventArgs>(base.Battle.CardMoving, new EventSequencedReactor<CardMovingEventArgs>(this.OnCardMoving));
                base.ReactOwnerEvent<CardEventArgs>(base.Battle.CardExiling, new EventSequencedReactor<CardEventArgs>(this.OnCardExiling));
                base.ReactOwnerEvent<CardEventArgs>(base.Battle.CardRemoving, new EventSequencedReactor<CardEventArgs>(this.OnCardRemoving));
            }
            private IEnumerable<BattleAction> OnCardUsing(CardUsingEventArgs args)
            {
                if (args.Card.CardType == CardType.Attack && args.Card != Card)
                {
                    this.Again = true;
                    Card = args.Card;
                    unitSelector = args.Selector;
                    manaConsumed = args.ConsumingMana;
                }
                yield break;
            }

            private IEnumerable<BattleAction> DoublePlay(Card card, GameEventArgs args)
            {
                this.Again = false;
                if (base.Battle.HandZone.Count >= base.Battle.MaxHand)
                {
                    this.Card = null;
                    unitSelector = null;
                    yield break;
                }
                base.NotifyActivating();
                args.CancelBy(this);
                yield return new MoveCardAction(card, CardZone.Hand);
                if (card.Zone == CardZone.Hand)
                {
                    yield return CardHelper.AutoCastAction(card, unitSelector, manaConsumed);
                    this.Card = null;
                    unitSelector = null;
                }
                int num = base.Level - 1;
                base.Level = num;
                if (base.Level <= 0)
                {
                    yield return new RemoveStatusEffectAction(this, true);
                }
            }

            private IEnumerable<BattleAction> OnCardMoving(CardMovingEventArgs args)
            {
                if (!base.Battle.BattleShouldEnd && this.Again && args.Card == Card && !(args.SourceZone == CardZone.PlayArea && args.DestinationZone == CardZone.Hand))
                {
                    foreach (var a in DoublePlay(args.Card, args))
                        yield return a;
                }
            }
            private IEnumerable<BattleAction> OnCardExiling(CardEventArgs args)
            {
                if (!base.Battle.BattleShouldEnd && this.Again && args.Card == Card)
                {
                    foreach (var a in DoublePlay(args.Card, args))
                        yield return a;
                }
            }
            private IEnumerable<BattleAction> OnCardRemoving(CardEventArgs args)
            {
                if (!base.Battle.BattleShouldEnd && this.Again && args.Card == Card)
                {
                    foreach (var a in DoublePlay(args.Card, args))
                        yield return a;
                }
            }
        }
    }
}