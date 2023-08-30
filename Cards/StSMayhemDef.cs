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
using static test.StSPanacheDef;
using static test.StSMayhemDef;
using LBoL.EntityLib.Cards.Character.Marisa;
using LBoL.EntityLib.Cards.Neutral.Black;

namespace test
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
                Cost: new ManaGroup() { Any = 2, Colorless = 2 },
                UpgradedCost: new ManaGroup() { Any = 1, Colorless = 2 },
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
                yield return base.BuffAction<StSMayhemSeDef.StSMayhemSe>(base.Value1, 0, 0, 0, 0.2f);
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
                base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarting, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarting));
            }
            private IEnumerable<BattleAction> OnPlayerTurnStarting(GameEventArgs args)
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                base.NotifyActivating();
                int max = (this.Level);
                int mayhemcount = 1;
                while (mayhemcount <= max)
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    List<Card> list = base.Battle.DrawZone.ToList<Card>();
                    if (list.Count == 0)
                    {
                        yield return new ReshuffleAction();
                    }
                    if (list.Count > 0)
                    {
                        Card card = list.First();
                        yield return new MoveCardAction(card, CardZone.Hand);
                        if ((card.CardType == CardType.Misfortune) || (card.CardType == CardType.Status))
                        {
                            yield return new DiscardAction(card);
                        }
                        if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.Nobody) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                        }
                        if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.SingleEnemy) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                        if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.AllEnemies) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.AllEnemies, this.Mana);
                        }
                        if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.RandomEnemy) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.RandomEnemy, this.Mana);
                        }
                        if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.Self) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.Self, this.Mana);
                        }
                        if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.All) && !card.IsForbidden)
                        {
                            yield return new UseCardAction(card, UnitSelector.All, this.Mana);
                        }
                    }
                    mayhemcount += 1;
                }
                yield break;
            }
        }
    }
}