﻿using LBoL.Base;
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
using LBoL.Core.Units;
using LBoL.Base.Extensions;
using LBoL.EntityLib.StatusEffects.Cirno;
using LBoL.Core.Battle.Interactions;
using LBoL.EntityLib.StatusEffects.Others;
using System.Linq;
using UnityEngine;
using System.Security.Cryptography;
using LBoL.EntityLib.JadeBoxes;
using static test.BepinexPlugin;
using UnityEngine.Playables;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Cards.Character.Cirno.Friend;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Neutral.MultiColor;
using LBoL.Presentation.UI.Panels;
using LBoL.Core.GapOptions;
using Mono.Cecil;

namespace test.Cards
{
    public sealed class DayuuFriendDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DayuuFriend);
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
               Type: CardType.Friend,
               TargetType: TargetType.Nobody,
               Colors: new List<ManaColor>() { ManaColor.Black },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 2, Black = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 2,
               UpgradedValue1: null,
               Value2: 1,
               UpgradedValue2: null,
               Mana: null,
               UpgradedMana: null,
               Scry: null,
               UpgradedScry: null,
               ToolPlayableTimes: null,
               Loyalty: 8,
               UpgradedLoyalty: 9,
               PassiveCost: -1,
               UpgradedPassiveCost: 0,
               ActiveCost: -3,
               UpgradedActiveCost: -3,
               UltimateCost: -6,
               UpgradedUltimateCost: -6,

               Keywords: Keyword.None,
               UpgradedKeywords: Keyword.None,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { "TempFirepowerNegative", "FirepowerNegative", "Weak", "Vulnerable", "DayuuFriendSe" },
               UpgradedRelativeEffects: new List<string>() { "TempFirepowerNegative", "FirepowerNegative", "Weak", "Vulnerable", "DayuuFriendSe" },
               RelativeCards: new List<string>() { "DayuuExodia" },
               UpgradedRelativeCards: new List<string>() { "DayuuExodia" },
               Owner: null,
               Unfinished: false,
               Illustrator: "Liz Triangle",
               SubIllustrator: new List<string>() { "MIO" }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(DayuuFriendDef))]
    public sealed class DayuuFriend : Card
    {
        protected override void OnEnterBattle(BattleController battle)
        {
            ReactBattleEvent(Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(OnCardUsed));
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (!Battle.BattleShouldEnd && Battle.Player.IsInTurn && Zone == CardZone.Hand && Summoned)
            {
                List<Card> DayuuA = Battle.HandZone.Where((card) => card is DayuuAttack).ToList();
                List<Card> DayuuD = Battle.HandZone.Where((card) => card is DayuuDefense).ToList();
                List<Card> DayuuS = Battle.HandZone.Where((card) => card is DayuuSkill).ToList();
                List<Card> DayuuP = Battle.HandZone.Where((card) => card is DayuuAbility).ToList();
                if (DayuuA.Count > 0 && DayuuD.Count > 0 && DayuuS.Count > 0 && DayuuP.Count > 0)
                {
                    List<Card> Dayuu = Battle.HandZone.Where((card) => card is DayuuAttack || card is DayuuDefense || card is DayuuSkill || card is DayuuAbility || card is DayuuFriend || card is DayuuFriend2).ToList();
                    foreach (Card card in Dayuu)
                    {
                        yield return new RemoveCardAction(card);
                    }
                    Card Exodia = Library.CreateCard<DayuuExodia>();
                    Exodia.Summon();
                    yield return new AddCardsToHandAction(new Card[] { Exodia });
                }
            }
        }
        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            return GetPassiveActions();
        }
        public override IEnumerable<BattleAction> GetPassiveActions()
        {
            if (!Summoned || Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            Loyalty += PassiveCost;
            int num;
            for (int i = 0; i < Battle.FriendPassiveTimes; i = num + 1)
            {
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                foreach (BattleAction battleAction in DebuffAction<TempFirepowerNegative>(Battle.AllAliveEnemies, Value1, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                num = i;
            }
            if (Loyalty <= 0)
            {
                yield return new RemoveCardAction(this);
            }
            yield break;
        }
        public override IEnumerable<BattleAction> SummonActions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            IsEthereal = false;
            foreach (BattleAction battleAction in base.SummonActions(selector, consumingMana, precondition))
            {
                yield return battleAction;
            }
            yield break;
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition == null || ((MiniSelectCardInteraction)precondition).SelectedCard.FriendToken == FriendToken.Active)
            {
                Loyalty += ActiveCost;
                yield return PerformAction.Effect(Battle.Player, "Wave1s", 0f, "BirdSing", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                foreach (BattleAction battleAction in DebuffAction<FirepowerNegative>(Battle.AllAliveEnemies, Value2, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                foreach (BattleAction battleAction2 in DebuffAction<Weak>(Battle.AllAliveEnemies, 0, Value2, 0, 0, true, 0.2f))
                {
                    yield return battleAction2;
                }
            }
            else
            {
                Loyalty += UltimateCost;
                UltimateUsed = true;
                yield return PerformAction.Effect(Battle.Player, "Wave1s", 0f, "BirdSing", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                foreach (EnemyUnit enemyUnit in Battle.AllAliveEnemies)
                {
                    if (enemyUnit.Hp <= (enemyUnit.MaxHp + 1) / 4)
                    {
                        yield return new ForceKillAction(Battle.Player, enemyUnit);
                    }
                }
                foreach (BattleAction battleAction in DebuffAction<FirepowerNegative>(Battle.AllAliveEnemies, 3, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                foreach (BattleAction battleAction2 in DebuffAction<Weak>(Battle.AllAliveEnemies, 0, 3, 0, 0, true, 0.2f))
                {
                    yield return battleAction2;
                }
                foreach (BattleAction battleAction3 in DebuffAction<Vulnerable>(Battle.AllAliveEnemies, 0, 3, 0, 0, true, 0.2f))
                {
                    yield return battleAction3;
                }
            }
            yield break;
        }
        public override IEnumerable<BattleAction> AfterUseAction()
        {
            if (!Summoned || Battle.BattleShouldEnd)
            {
                yield break;
            }
            if (Loyalty <= 0 || UltimateUsed == true)
            {
                yield return new RemoveCardAction(this);
                yield break;
            }
            yield return new MoveCardAction(this, CardZone.Hand);
            yield break;
        }
    }
    public sealed class DayuuFriendSeDef : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DayuuFriendSe);
        }

        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.StatusEffectsEn.yaml");
            return locFiles;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Resources.DayuuExodiaSe.png", embeddedSource);
        }
        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                Id: "",
                Order: 10,
                Type: StatusEffectType.Positive,
                IsVerbose: false,
                IsStackable: false,
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



        [EntityLogic(typeof(DayuuFriendSeDef))]
        public sealed class DayuuFriendSe : StatusEffect
        {
        }
    }
    public sealed class DayuuFriend2Def : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DayuuFriend2);
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
               IsPooled: false,
               HideMesuem: true,
               IsUpgradable: true,
               Rarity: Rarity.Rare,
               Type: CardType.Friend,
               TargetType: TargetType.Nobody,
               Colors: new List<ManaColor>() { ManaColor.Black },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 2, Black = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 2,
               UpgradedValue1: null,
               Value2: 1,
               UpgradedValue2: null,
               Mana: null,
               UpgradedMana: null,
               Scry: null,
               UpgradedScry: null,
               ToolPlayableTimes: null,
               Loyalty: 9,
               UpgradedLoyalty: 9,
               PassiveCost: 0,
               UpgradedPassiveCost: 0,
               ActiveCost: -3,
               UpgradedActiveCost: -3,
               UltimateCost: -6,
               UpgradedUltimateCost: -6,

               Keywords: Keyword.None,
               UpgradedKeywords: Keyword.None,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { "TempFirepowerNegative", "FirepowerNegative", "Weak", "Vulnerable", "DayuuFriendSe" },
               UpgradedRelativeEffects: new List<string>() { "TempFirepowerNegative", "FirepowerNegative", "Weak", "Vulnerable", "DayuuFriendSe" },
               RelativeCards: new List<string>() { "DayuuExodia" },
               UpgradedRelativeCards: new List<string>() { "DayuuExodia" },
               Owner: null,
               Unfinished: false,
               Illustrator: "Liz Triangle",
               SubIllustrator: new List<string>() { "MIO" }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(DayuuFriend2Def))]
    public sealed class DayuuFriend2 : Card
    {
        protected override void OnEnterBattle(BattleController battle)
        {
            ReactBattleEvent(Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(OnCardUsed));
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (!Battle.BattleShouldEnd && Battle.Player.IsInTurn && Zone == CardZone.Hand && Summoned)
            {
                List<Card> DayuuA = Battle.HandZone.Where((card) => card is DayuuAttack).ToList();
                List<Card> DayuuD = Battle.HandZone.Where((card) => card is DayuuDefense).ToList();
                List<Card> DayuuS = Battle.HandZone.Where((card) => card is DayuuSkill).ToList();
                List<Card> DayuuP = Battle.HandZone.Where((card) => card is DayuuAbility).ToList();
                if (DayuuA.Count > 0 && DayuuD.Count > 0 && DayuuS.Count > 0 && DayuuP.Count > 0)
                {
                    List<Card> Dayuu = Battle.HandZone.Where((card) => card is DayuuAttack || card is DayuuDefense || card is DayuuSkill || card is DayuuAbility || card is DayuuFriend || card is DayuuFriend2).ToList();
                    foreach (Card card in Dayuu)
                    {
                        yield return new RemoveCardAction(card);
                    }
                    Card Exodia = Library.CreateCard<DayuuExodia>();
                    Exodia.Summon();
                    yield return new AddCardsToHandAction(new Card[] { Exodia });
                }
            }
        }
        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            return GetPassiveActions();
        }
        public override IEnumerable<BattleAction> GetPassiveActions()
        {
            if (!Summoned || Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            Loyalty += PassiveCost;
            int num;
            for (int i = 0; i < Battle.FriendPassiveTimes; i = num + 1)
            {
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                foreach (BattleAction battleAction in DebuffAction<TempFirepowerNegative>(Battle.AllAliveEnemies, Value1, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                if (IsUpgraded)
                {
                    foreach (BattleAction battleAction2 in DebuffAction<TempFirepowerNegative>(Battle.AllAliveEnemies, Value1, 0, 0, 0, true, 0.2f))
                    {
                        yield return battleAction2;
                    }
                }
                num = i;
            }
            if (Loyalty <= 0)
            {
                yield return new RemoveCardAction(this);
            }
            yield break;
        }
        public override IEnumerable<BattleAction> SummonActions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            IsEthereal = false;
            foreach (BattleAction battleAction in base.SummonActions(selector, consumingMana, precondition))
            {
                yield return battleAction;
            }
            yield break;
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition == null || ((MiniSelectCardInteraction)precondition).SelectedCard.FriendToken == FriendToken.Active)
            {
                Loyalty += ActiveCost;
                yield return PerformAction.Effect(Battle.Player, "Wave1s", 0f, "BirdSing", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                foreach (BattleAction battleAction in DebuffAction<FirepowerNegative>(Battle.AllAliveEnemies, IsUpgraded ? Value1 : Value2, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                foreach (BattleAction battleAction2 in DebuffAction<Weak>(Battle.AllAliveEnemies, 0, IsUpgraded ? Value1 : Value2, 0, 0, true, 0.2f))
                {
                    yield return battleAction2;
                }
            }
            else
            {
                Loyalty += UltimateCost;
                UltimateUsed = true;
                yield return PerformAction.Effect(Battle.Player, "Wave1s", 0f, "BirdSing", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                foreach (EnemyUnit enemyUnit in Battle.AllAliveEnemies)
                {
                    if (enemyUnit.Hp <= (enemyUnit.MaxHp + 1) / (IsUpgraded ? 3 : 4))
                    {
                        yield return new ForceKillAction(Battle.Player, enemyUnit);
                    }
                }
                foreach (BattleAction battleAction in DebuffAction<FirepowerNegative>(Battle.AllAliveEnemies, IsUpgraded ? 6 : 3, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                foreach (BattleAction battleAction2 in DebuffAction<Weak>(Battle.AllAliveEnemies, 0, IsUpgraded ? 6 : 3, 0, 0, true, 0.2f))
                {
                    yield return battleAction2;
                }
                foreach (BattleAction battleAction3 in DebuffAction<Vulnerable>(Battle.AllAliveEnemies, 0, IsUpgraded ? 6 : 3, 0, 0, true, 0.2f))
                {
                    yield return battleAction3;
                }
            }
            yield break;
        }
        public override IEnumerable<BattleAction> AfterUseAction()
        {
            if (!Summoned || Battle.BattleShouldEnd)
            {
                yield break;
            }
            if (Loyalty <= 0 || UltimateUsed == true)
            {
                yield return new RemoveCardAction(this);
                yield break;
            }
            yield return new MoveCardAction(this, CardZone.Hand);
            yield break;
        }
    }
}
