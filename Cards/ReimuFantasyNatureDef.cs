using LBoL.Base;
using LBoL.ConfigData;
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
using static test.BepinexPlugin;
using System.Linq;
using LBoL.Core.Units;
using LBoL.EntityLib.EnemyUnits.Character;
using JetBrains.Annotations;
using LBoL.Base.Extensions;
using static UnityEngine.GraphicsBuffer;
using LBoL.Presentation.UI.Widgets;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.EntityLib.StatusEffects.Others;
using UnityEngine;

namespace test.Cards
{
    public sealed class ReimuFantasyNatureDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ReimuFantasyNature);
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
               GunName: "Instant",
               GunNameBurst: "Instant",
               DebugLevel: 0,
               Revealable: false,
               IsPooled: false,
               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Rare,
               Type: CardType.Attack,
               TargetType: TargetType.SingleEnemy,
               Colors: new List<ManaColor>() { ManaColor.White, ManaColor.Blue, ManaColor.Black, ManaColor.Red, ManaColor.Green },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 4, White = 1, Blue = 1, Black = 1, Red = 1, Green = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: 9,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 14,
               UpgradedValue1: null,
               Value2: null,
               UpgradedValue2: null,
               Mana: new ManaGroup() { Any = 0 },
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

               Keywords: Keyword.Accuracy | Keyword.Exile | Keyword.Ethereal,
               UpgradedKeywords: Keyword.Accuracy | Keyword.Exile | Keyword.Ethereal,
               EmptyDescription: false,
               RelativeKeyword: Keyword.Friend,
               UpgradedRelativeKeyword: Keyword.Friend,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: "Reimu",
               Unfinished: false,
               Illustrator: "",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(ReimuFantasyNatureDef))]
    public sealed class ReimuFantasyNature : Card
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            _maxhand = Battle.MaxHand;
            Battle.MaxHand = 14;
            int num = Battle.MaxHand - Battle.HandZone.Count;
            if (num > 0)
            {
                yield return new DrawManyCardAction(num);
            }
            if (!Battle.Player.HasStatusEffect<UnitedWeStandSeDef.UnitedWeStandSe>())
            {
                yield return new ApplyStatusEffectAction<UnitedWeStandSeDef.UnitedWeStandSe>(Battle.Player, null, null, null, null, 0.0f, true);
            }
            GameRun.SynergyAdditionalCount += 1;
            EnemyUnit target = selector.SelectedEnemy;
            List<Card> list = Battle.HandZone.ToList();
            foreach (Card card in list)
            {
                if (!target.IsAlive)
                {
                    if (Battle.BattleShouldEnd)
                    {
                        GameRun.SynergyAdditionalCount -= 1;
                        yield break;
                    }
                    target = Battle.AllAliveEnemies.Sample(GameRun.BattleRng);
                }
                if (card.CardType == CardType.Misfortune || card.CardType == CardType.Status)
                {
                    yield return new ExileCardAction(card);
                }
                else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.Nobody && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, UnitSelector.Nobody, Mana);
                }
                else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.SingleEnemy && !card.IsForbidden)
                {
                    UnitSelector unitSelector = new UnitSelector(target);
                    yield return new UseCardAction(card, unitSelector, Mana);
                }
                else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.AllEnemies && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, UnitSelector.AllEnemies, Mana);
                }
                else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.RandomEnemy && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, UnitSelector.RandomEnemy, Mana);
                }
                else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.Self && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, UnitSelector.Self, Mana);
                }
                else if (card.Zone == CardZone.Hand && card.Config.TargetType == TargetType.All && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, UnitSelector.All, Mana);
                }
                if (card.CardType == CardType.Friend && card.Zone == CardZone.PlayArea && card.Loyalty <= 0)
                {
                    yield return new RemoveCardAction(card);
                }
                Battle.MaxHand = 14;
            }
            if (Battle.Player.HasStatusEffect<UnitedWeStandSeDef.UnitedWeStandSe>())
            {
                yield return new RemoveStatusEffectAction(Battle.Player.GetStatusEffect<UnitedWeStandSeDef.UnitedWeStandSe>(), true);
            }
            Battle.MaxHand = _maxhand;
            GameRun.SynergyAdditionalCount -= 1;
            yield break;
        }
        private int _maxhand;
    }
    public sealed class ReimuFantasyNatureSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ReimuFantasyNatureSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.UnitedWeStandSe.png", embeddedSource);
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
        [EntityLogic(typeof(ReimuFantasyNatureSeDef))]
        public sealed class ReimuFantasyNatureSe : StatusEffect
        {
            protected override void OnAdded(Unit unit)
            {
                HandleOwnerEvent(unit.DamageTaking, new GameEventHandler<DamageEventArgs>(OnDamageTaking));
                ReactOwnerEvent(Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(OnTurnEnded));
            }
            private void OnDamageTaking(DamageEventArgs args)
            {
                int num = args.DamageInfo.Damage.RoundToInt();
                if (num > 0)
                {
                    NotifyActivating();
                    args.DamageInfo = args.DamageInfo.ReduceActualDamageBy(num);
                    args.AddModifier(this);
                }
            }
            private IEnumerable<BattleAction> OnTurnEnded(UnitEventArgs args)
            {
                yield return new RemoveStatusEffectAction(this, true);
                yield break;
            }
            public override string UnitEffectName
            {
                get
                {
                    return "InvincibleLoop";
                }
            }
        }
    }
}
