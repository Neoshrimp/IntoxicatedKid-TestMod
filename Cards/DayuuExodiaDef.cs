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
using LBoL.Core.Units;
using LBoL.Base.Extensions;
using LBoL.EntityLib.StatusEffects.Cirno;
using LBoL.Core.Battle.Interactions;
using LBoL.EntityLib.StatusEffects.Others;
using System.Linq;
using UnityEngine;
using System.Security.Cryptography;
using LBoL.EntityLib.JadeBoxes;
using static UnityEngine.GraphicsBuffer;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;
using LBoL.EntityLib.Cards.Character.Sakuya;

namespace test
{
    public sealed class DayuuExodiaDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DayuuExodia);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(BepinexPlugin.embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(LBoL.Core.Locale.En, "CardsEn.yaml");
            return loc;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
               Index: 12006,
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
               Colors: new List<ManaColor>() { ManaColor.Colorless },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 0 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: 0,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 9999,
               UpgradedValue1: null,
               Value2: null,
               UpgradedValue2: null,
               Mana: null,
               UpgradedMana: null,
               Scry: null,
               UpgradedScry: null,
               ToolPlayableTimes: null,
               Loyalty: 9,
               UpgradedLoyalty: null,
               PassiveCost: 0,
               UpgradedPassiveCost: null,
               ActiveCost: 0,
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
               Illustrator: "Yoshioka Yoshiko",
               SubIllustrator: new List<string>() { "MIO" }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(DayuuExodiaDef))]
    public sealed class DayuuExodia : Card
    {
        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            return this.GetPassiveActions();
        }
        public override IEnumerable<BattleAction> GetPassiveActions()
        {
            if (!base.Summoned || base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            base.NotifyActivating();
            base.Loyalty += base.PassiveCost;
            int num;
            for (int i = 0; i < base.Battle.FriendPassiveTimes; i = num + 1)
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 2.75f);
                foreach (EnemyUnit enemyUnit in base.Battle.AllAliveEnemies)
                {
                    enemyUnit.ClearBlockShield();
                    enemyUnit.ClearStatusEffects();
                }
                yield return new DamageAction(base.Battle.Player, base.Battle.AllAliveEnemies, DamageInfo.Attack(9999f), "Instant", GunType.Single);
                num = i;
            }
            yield break;
        }
        //        public override IEnumerable<BattleAction> SummonActions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        //        {
        //            this.IsEthereal = false;
        //            foreach (BattleAction battleAction in base.SummonActions(selector, consumingMana, precondition))
        //            {
        //                yield return battleAction;
        //            }
        //            yield break;
        //        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition == null || ((MiniSelectCardInteraction)precondition).SelectedCard.FriendToken == FriendToken.Active)
            {
                base.Loyalty += base.ActiveCost;
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 0f);
                yield return PerformAction.Gun(base.Battle.Player, base.Battle.AllAliveEnemies.FirstOrDefault<EnemyUnit>(), "究极火花B", 2.75f);
                foreach (EnemyUnit enemyUnit in base.Battle.AllAliveEnemies)
                {
                    enemyUnit.ClearBlockShield();
                    enemyUnit.ClearStatusEffects();
                }
                yield return new DamageAction(base.Battle.Player, base.Battle.AllAliveEnemies, this.Damage, "Instant", GunType.Single);
            }
            yield break;
        }
        //        }
        //        public override IEnumerable<BattleAction> AfterUseAction()
        //        {
        //            if (!base.Summoned || base.Battle.BattleShouldEnd)
        //            {
        //                yield break;
        //            }
        //            if (base.Loyalty <= 0 || base.UltimateUsed == true)
        //            {
        //                yield return new RemoveCardAction(this);
        //                yield break;
        //            }
        //            yield return new MoveCardAction(this, CardZone.Hand);
        //            yield break;
        //        }
    }
}
