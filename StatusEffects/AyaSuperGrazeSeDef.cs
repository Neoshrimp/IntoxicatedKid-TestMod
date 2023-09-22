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
using test;
using LBoL.EntityLib.PlayerUnits;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Other.Enemy;
using LBoL.EntityLib.StatusEffects.Cirno;
using System.Reflection;

namespace test
{
    public sealed class AyaSuperGrazeDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaSuperGraze);
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
                Cost: new ManaGroup() { Red = 3 },
                UpgradedCost: null,
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

                RelativeEffects: new List<string>() { "AyaSuperGrazeSe" },
                UpgradedRelativeEffects: new List<string>() { "AyaSuperGrazeSe" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: null,
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }
        [EntityLogic(typeof(AyaSuperGrazeDef))]
        public sealed class AyaSuperGraze : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return base.BuffAction<AyaSuperGrazeSeDef.AyaSuperGrazeSe>(base.Value1, 0, 0, 0, 0.2f);
                yield break;
            }
        }

    }
    public sealed class AyaSuperGrazeSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaSuperGrazeSe);
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
                Order: 1,
                Type: StatusEffectType.Positive,
                IsVerbose: true,
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
                VFX: "Graze",
                VFXloop: "Default",
                SFX: "Graze"
            );
            return statusEffectConfig;
        }
        [EntityLogic(typeof(AyaSuperGrazeSeDef))]
        public sealed class AyaSuperGrazeSe : StatusEffect
        {
            [HarmonyPatch(typeof(Unit), nameof(Unit.MeasureDamage))]
            class Unit_MeasureDamage_Patch
            {
                static bool Prefix(Unit __instance, ref DamageInfo info, ref DamageInfo __result)
                {
                    if (__instance.HasStatusEffect<AyaSuperGrazeSeDef.AyaSuperGrazeSe>())
                    {
                        Debug.LogError(info.DamageType);
                        if (info.Damage < 0f)
                        {
                            info.Damage = 0f;
                        }
                        else
                        {
                            info.Damage = info.Damage.Round(MidpointRounding.AwayFromZero);
                        }
                        if (info.DamageType == DamageType.Attack && info.Damage > 0f && __instance.HasStatusEffect<AyaSuperGrazeSeDef.AyaSuperGrazeSe>() && !info.IsAccuracy)
                        {
                            info = new DamageInfo(0f, info.DamageType, true, false).BlockBy(__instance.Block).ShieldBy(__instance.Shield);
                            __result = info;
                            return false;
                        }
                        else if (info.DamageType == DamageType.Attack && info.Damage > 0f && __instance.HasStatusEffect<AyaSuperGrazeSeDef.AyaSuperGrazeSe>() && info.IsAccuracy)
                        {
                            info = new DamageInfo(0f, info.DamageType, true, true).BlockBy(__instance.Block).ShieldBy(__instance.Shield);
                            __result = info;
                            return false;
                        }
                        if (info.DamageType != DamageType.HpLose)
                        {
                            info = info.BlockBy(__instance.Block).ShieldBy(__instance.Shield);
                            __result = info;
                            return false;
                        }
                        __result = info;
                        return false;
                    }
                    return true;
                }
            }
            [HarmonyPatch(typeof(Unit), nameof(Unit.TakeDamage))]
            class Unit_TakeDamage_Patch
            {
                static void Postfix(Unit __instance, ref DamageInfo __result)
                {
                    if (__result.DamageType == DamageType.Attack && __instance.HasStatusEffect<AyaSuperGrazeSe>())
                    {
                        if (__result.IsGrazed)
                        {
                            __instance.GetStatusEffect<AyaSuperGrazeSe>().Activate();
                        }
                    }
                }
            }
            protected override void OnAdded(Unit unit)
            {
                base.HandleOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new GameEventHandler<UnitEventArgs>(this.OnOwnerTurnStarted));
            }
            private void OnOwnerTurnStarted(UnitEventArgs args)
            {
                if (base.Owner.HasStatusEffect<WindGirl>())
                {
                    return;
                }
                if (base.IsAutoDecreasing)
                {
                    int num = base.Level - 1;
                    base.Level = num;
                    if (base.Level == 0)
                    {
                        this.React(new RemoveStatusEffectAction(this, true));
                        return;
                    }
                }
                else
                {
                    base.IsAutoDecreasing = true;
                }
            }
            public void Activate()
            {
                int num = base.Level - 1;
                base.Level = num;
                if (base.Level > 0)
                {
                    base.NotifyActivating();
                    return;
                }
                this.React(new RemoveStatusEffectAction(this, true));
            }
            public override string UnitEffectName
            {
                get
                {
                    return "GrazeLoop";
                }
            }
        }
    }
}