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
using LBoL.EntityLib.PlayerUnits;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Other.Enemy;
using LBoL.EntityLib.StatusEffects.Cirno;

namespace test.Cards
{
    public sealed class AyaBreakingNewsDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaBreakingNews);
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
                Rarity: Rarity.Uncommon,
                Type: CardType.Ability,
                TargetType: TargetType.Self,
                Colors: new List<ManaColor>() { ManaColor.Green },
                IsXCost: false,
                Cost: new ManaGroup() { Green = 2 },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 9,
                UpgradedValue1: 12,
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
                RelativeKeyword: Keyword.Block | Keyword.Shield,
                UpgradedRelativeKeyword: Keyword.Block | Keyword.Shield,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { "AyaNews" },
                UpgradedRelativeCards: new List<string>() { "AyaNews" },
                Owner: "AyaPlayerUnit",
                Unfinished: false,
                Illustrator: "Masakichi",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }
        [EntityLogic(typeof(AyaBreakingNewsDef))]
        public sealed class AyaBreakingNews : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return BuffAction<AyaBreakingNewsSeDef.AyaBreakingNewsSe>(Value1, 0, 0, 0, 0.2f);
                yield break;
            }
        }

    }
    public sealed class AyaBreakingNewsSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaBreakingNewsSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.StSAccuracySe.png", embeddedSource);
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
        [EntityLogic(typeof(AyaBreakingNewsSeDef))]
        public sealed class AyaBreakingNewsSe : StatusEffect
        {
            protected override void OnAdded(Unit unit)
            {
                foreach (Card card in Battle.EnumerateAllCards())
                {
                    if (card is AyaNews)
                    {
                        card.DeltaDamage = Level;
                    }
                }
                HandleOwnerEvent(Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(OnAddCard));
                HandleOwnerEvent(Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(OnAddCard));
                HandleOwnerEvent(Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(OnAddCard));
                HandleOwnerEvent(Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(OnAddCardToDraw));
                HandleOwnerEvent(Owner.DamageDealing, new GameEventHandler<DamageDealingEventArgs>(OnDamageDealing));
            }
            private void OnAddCard(CardsEventArgs args)
            {
                foreach (Card card in args.Cards)
                {
                    if (card is AyaNews)
                    {
                        card.DeltaDamage = Level;
                    }
                }
            }
            private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
            {
                foreach (Card card in args.Cards)
                {
                    if (card is AyaNews)
                    {
                        card.DeltaDamage = Level;
                    }
                }
            }
            private void OnDamageDealing(DamageDealingEventArgs args)
            {
                Card card = args.ActionSource as Card;
                if (args.ActionSource == card && card is AyaNews && args.Targets.Any((target) => target.Block > 0 || target.Shield > 0))
                {
                    args.DamageInfo = args.DamageInfo.IncreaseBy((int)args.DamageInfo.Amount);
                    args.AddModifier(this);
                    if (args.Cause != ActionCause.OnlyCalculate)
                    {
                        NotifyActivating();
                    }
                }
            }
        }
    }
}