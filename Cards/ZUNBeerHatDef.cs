using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Adventures;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Sakuya;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using LBoL.Core.StatusEffects;
using LBoL.Core.Randoms;
using static test.BepinexPlugin;
using LBoL.EntityLib.StatusEffects.Basic;
using JetBrains.Annotations;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoL.EntityLib.Cards.Other.Misfortune;
using LBoL.EntityLib.Cards.Character.Marisa;
using LBoL.Core.Units;
using UnityEngine;
using LBoL.EntityLib.StatusEffects.Cirno;

namespace test
{
    public sealed class ZUNBeerHatDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ZUNBeerHat);
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
               Colors: new List<ManaColor>() { ManaColor.White, ManaColor.Blue, ManaColor.Black, ManaColor.Red, ManaColor.Green },
               IsXCost: false,
               Cost: new ManaGroup() { White = 1, Blue = 1, Black = 1, Red = 1, Green = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 1,
               UpgradedValue1: null,
               Value2: 3,
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

               Keywords: Keyword.Ethereal,
               UpgradedKeywords: Keyword.Ethereal,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { "Drunk" },
               UpgradedRelativeCards: new List<string>() { },
               Owner: null,
               Unfinished: false,
               Illustrator: "ZUN",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(ZUNBeerHatDef))]
    public sealed class ZUNBeerHat : Card
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (!this.IsUpgraded)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<Drunk>(base.Value1, false));
            }
            yield return base.BuffAction<ZUNBeerHatSeDef.ZUNBeerHatSe>(0, base.Value2, 0, 0, 0.2f);
            yield break;
        }
    }
    public sealed class ZUNBeerHatSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ZUNBeerHatSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.ZUNBeerHatSe.png", embeddedSource);
        }
        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                Id: "",
                Order: 10,
                Type: StatusEffectType.Special,
                IsVerbose: false,
                IsStackable: true,
                StackActionTriggerLevel: null,
                HasLevel: false,
                LevelStackType: StackType.Add,
                HasDuration: true,
                DurationStackType: StackType.Add,
                DurationDecreaseTiming: DurationDecreaseTiming.RoundEnd,
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
        [EntityLogic(typeof(ZUNBeerHatSeDef))]
        public sealed class ZUNBeerHatSe : StatusEffect
        {
            protected override void OnAdded(Unit unit)
            {
                base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnEnded));
                foreach (var enemy in base.Battle.AllAliveEnemies)
                {
                    enemy._turnMoves.Clear();
                    enemy.ClearIntentions();
                    var stun = Intention.Stun();
                    stun.Source = enemy;
                    enemy._turnMoves.Add(new SimpleEnemyMove(stun, new EnemyMoveAction[] { new EnemyMoveAction(enemy, "Wasted", true) }));
                    enemy.Intentions.Add(stun);
                    enemy.NotifyIntentionsChanged();
                }
            }
            private IEnumerable<BattleAction> OnPlayerTurnEnded(UnitEventArgs args)
            {
                foreach (var enemy in base.Battle.AllAliveEnemies)
                {
                    enemy._turnMoves.Clear();
                    enemy.ClearIntentions();
                    var stun = Intention.Stun();
                    stun.Source = enemy;
                    enemy._turnMoves.Add(new SimpleEnemyMove(stun, new EnemyMoveAction[] { new EnemyMoveAction(enemy, "Wasted", true) }));
                    enemy.Intentions.Add(stun);
                    enemy.NotifyIntentionsChanged();
                }
                yield break;
            }
        }
    }
}
