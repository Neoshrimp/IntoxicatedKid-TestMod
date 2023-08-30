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

namespace test
{
    public sealed class UnitedWeStandDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(UnitedWeStand);
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
               IsPooled: true,
               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Rare,
               Type: CardType.Skill,
               TargetType: TargetType.SingleEnemy,
               Colors: new List<ManaColor>() { ManaColor.White, ManaColor.Blue, ManaColor.Black, ManaColor.Red, ManaColor.Green },
               IsXCost: false,
               Cost: new ManaGroup() { White = 1, Blue = 1, Black = 1, Red = 1, Green = 1},
               UpgradedCost: new ManaGroup() { White = 1, Blue = 1, Black = 1, Red = 1, Green = 1},
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: null,
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

               Keywords: Keyword.Exile,
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
               Illustrator: "Utarion",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(UnitedWeStandDef))]
    public sealed class UnitedWeStand : Card
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            _maxhand = base.Battle.MaxHand;
            base.Battle.MaxHand = 10;
            int num = base.Battle.MaxHand - base.Battle.HandZone.Count;
            if (num > 0)
            {
                yield return new DrawManyCardAction(num);
            }
            if (!base.Battle.Player.HasStatusEffect<UnitedWeStandSeDef.UnitedWeStandSe>())
            {
                yield return new ApplyStatusEffectAction<UnitedWeStandSeDef.UnitedWeStandSe>(base.Battle.Player, null, null, null, null, 0.0f, true);
            }
            base.GameRun.SynergyAdditionalCount += 1;
            EnemyUnit target = selector.SelectedEnemy;
            List<Card> list = base.Battle.HandZone.ToList<Card>();
            foreach (Card card in list)
            {
                if (!target.IsAlive)
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield return new EndShootAction(base.Battle.Player);
                        yield break;
                    }
                    target = base.Battle.AllAliveEnemies.Sample(base.GameRun.BattleRng);
                }
                if ((card.CardType == CardType.Misfortune) || (card.CardType == CardType.Status))
                {
                    yield return new ExileCardAction(card);
                }
                if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.Nobody) && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, UnitSelector.Nobody, this.Mana);
                }
                if ((card.Zone == CardZone.Hand) && (card.Config.TargetType == TargetType.SingleEnemy) && !card.IsForbidden)
                {
                    yield return new UseCardAction(card, selector, this.Mana);
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
                if ((card.CardType == CardType.Friend) && (card.Zone == CardZone.PlayArea) && (card.Loyalty <= 0))
                {
                    yield return new RemoveCardAction(card);
                }
                base.Battle.MaxHand = 10;
            }
            if (base.Battle.Player.HasStatusEffect<UnitedWeStandSeDef.UnitedWeStandSe>())
            {
                yield return new RemoveStatusEffectAction(base.Battle.Player.GetStatusEffect<UnitedWeStandSeDef.UnitedWeStandSe>(), true);
            }
            base.GameRun.SynergyAdditionalCount -= 1;
            base.Battle.MaxHand = _maxhand;
            yield break;
        }
        private int _maxhand;
    }
    public sealed class UnitedWeStandSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(UnitedWeStandSe);
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
        [EntityLogic(typeof(UnitedWeStandSeDef))]
        public sealed class UnitedWeStandSe : StatusEffect
        {
            protected override void OnAdded(Unit unit)
            {
                //base.HandleOwnerEvent<CardEventArgs>(base.Battle.Predraw, new GameEventHandler<CardEventArgs>(this.OnPredraw));
                //base.HandleOwnerEvent<CardMovingEventArgs>(base.Battle.CardMoving, new GameEventHandler<CardMovingEventArgs>(this.OnCardMoving));
                base.HandleOwnerEvent<DamageEventArgs>(unit.DamageTaking, new GameEventHandler<DamageEventArgs>(this.OnDamageTaking));
                base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnTurnEnded));
            }
            /*private void OnPredraw(CardEventArgs args)
            {
                base.NotifyActivating();
                args.CancelBy(this);
            }*/
            /*private void OnCardMoving(CardMovingEventArgs args)
            {
                if ((args.DestinationZone == CardZone.Hand) && (args.Card.CardType != CardType.Friend))
                {
                    base.NotifyActivating();
                    args.CancelBy(this);
                }
            }*/
            private void OnDamageTaking(DamageEventArgs args)
            {
                int num = args.DamageInfo.Damage.RoundToInt();
                if (num > 0)
                {
                    base.NotifyActivating();
                    args.DamageInfo = args.DamageInfo.ReduceActualDamageBy(num);
                    args.AddModifier(this);
                }
            }
            private IEnumerable<BattleAction> OnTurnEnded(UnitEventArgs args)
            {
                yield return new RemoveStatusEffectAction(this, true);
                yield break;
            }
        }
    }
}
