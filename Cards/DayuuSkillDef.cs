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
using LBoL.EntityLib.Exhibits.Common;
using System.Linq;
using LBoL.Presentation.UI.Panels;

namespace test
{
    public sealed class DayuuSkillDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DayuuSkill);
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
               Index: 12003,
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
               Type: CardType.Skill,
               TargetType: TargetType.Self,
               Colors: new List<ManaColor>() { ManaColor.Red },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 2, Red = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: null,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 3,
               UpgradedValue1: 5,
               Value2: 1,
               UpgradedValue2: 2,
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

               Keywords: Keyword.None,
               UpgradedKeywords: Keyword.None,
               EmptyDescription: false,
               RelativeKeyword: Keyword.Misfortune | Keyword.Ethereal,
               UpgradedRelativeKeyword: Keyword.Misfortune | Keyword.Ethereal,

               RelativeEffects: new List<string>() { "TempFirepower" },
               UpgradedRelativeEffects: new List<string>() { "TempFirepower" },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: null,
               Unfinished: false,
               Illustrator: "Moja4192",
               SubIllustrator: new List<string>() { "MIO" }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(DayuuSkillDef))]
    public sealed class DayuuSkill : Card
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.BuffAction<TempFirepower>(Value1, 0, 0, 0, 0.2f);
            foreach (EnemyUnit enemyUnit in base.Battle.AllAliveEnemies)
            {
                if (this.IsUpgraded)
                {
                    yield return base.BuffAction<TempFirepower>(2, 0, 0, 0, 0.2f);
                }
                else
                {
                    yield return base.BuffAction<TempFirepower>(3, 0, 0, 0, 0.2f);
                }
            }
            List<Card> playable = null;
            DrawManyCardAction drawAction = new DrawManyCardAction(base.Value2);
            yield return drawAction;
            IReadOnlyList<Card> drawnCards = drawAction.DrawnCards;
            if (drawnCards != null && drawnCards.Count > 0)
            {
                List<Card> negative = drawnCards.Where((Card card) => (card.CardType == CardType.Status) || (card.CardType == CardType.Misfortune)).ToList<Card>();
                if (negative.Count > 0)
                {
                    yield return new ExileManyCardAction(negative);
                }
                playable = drawnCards.Where((Card card) => !card.IsForbidden).ToList<Card>();
                foreach (Card card in playable)
                {
                    //if (card.TargetType == TargetType.SingleEnemy)
                    //{
                    //    yield return new UseCardAction(card, TargetType.RandomEnemy, consumingMana);
                    //}
                    //if (card.TargetType == TargetType.RandomEnemy)
                    //{
                    //    yield return new UseCardAction(card, TargetType.RandomEnemy, consumingMana);
                    //}
                    //if (card.TargetType == TargetType.AllEnemies)
                    //{
                    //    yield return new UseCardAction(card, TargetType.AllEnemies, consumingMana);
                    //}
                    //if (card.TargetType == TargetType.Nobody)
                    //{
                    //    yield return new UseCardAction(card, TargetType.Nobody, consumingMana);
                    //}
                    yield return new UseCardAction(card, selector, consumingMana);
                }
            }
            yield break;
        }
    }
}
