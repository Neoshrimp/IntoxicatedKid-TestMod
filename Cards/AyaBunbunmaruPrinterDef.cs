using LBoL.ConfigData;
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
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Other.Enemy;

namespace test.Cards
{
    public sealed class AyaBunbunmaruPrinterDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaBunbunmaruPrinter);
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
               UpgradedCost: new ManaGroup() { Green = 1 },
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 1,
               UpgradedValue1: null,
               Value2: 0,
               UpgradedValue2: 1,
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
               RelativeCards: new List<string>() { "AyaNews", "HatateNews" },
               UpgradedRelativeCards: new List<string>() { "AyaNews", "HatateNews" },
               Owner: "AyaPlayerUnit",
               Unfinished: false,
               Illustrator: "torque",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(AyaBunbunmaruPrinterDef))]
    public sealed class AyaBunbunmaruPrinter : Card
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<AyaBunbunmaruPrinterSeDef.AyaBunbunmaruPrinterSe>(Value1, 0, 0, 0, 0.2f);
            if (Value2 > 0)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<Knife>(Value2, false));
            }
            yield break;
        }
    }
    public sealed class AyaBunbunmaruPrinterSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaBunbunmaruPrinterSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.DayuuAbilitySe.png", embeddedSource);
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
                VFX: "BuffGreen",
                VFXloop: "Default",
                SFX: "Default"
            );
            return statusEffectConfig;
        }
        [EntityLogic(typeof(AyaBunbunmaruPrinterSeDef))]
        public sealed class AyaBunbunmaruPrinterSe : StatusEffect
        {
            protected override void OnAdded(Unit unit)
            {
                ReactOwnerEvent(Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarted));
            }
            private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
            {
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                NotifyActivating();
                yield return new AddCardsToHandAction(Library.CreateCards<AyaNews>(Level, false));
                yield break;
            }
        }
    }
}