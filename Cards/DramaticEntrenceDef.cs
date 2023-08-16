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
using System.Linq;
using LBoL.EntityLib.Cards.Character.Marisa;
using LBoL.EntityLib.StatusEffects.Cirno;
using LBoL.EntityLib.StatusEffects.Others;

namespace test
{
    public sealed class DramaticEntrenceDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DramaticEntrence);
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
               Index: 12012,
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
               Type: CardType.Attack,
               TargetType: TargetType.AllEnemies,
               Colors: new List<ManaColor>() { ManaColor.Colorless },
               IsXCost: false,
               Cost: new ManaGroup() { },
               UpgradedCost: new ManaGroup() { },
               MoneyCost: null,
               Damage: 0,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 12,
               UpgradedValue1: 18,
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

               Keywords: Keyword.Forbidden,
               UpgradedKeywords: Keyword.Forbidden,
               EmptyDescription: false,
               RelativeKeyword: Keyword.Exile,
               UpgradedRelativeKeyword: Keyword.Exile,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: null,
               Unfinished: false,
               Illustrator: null,
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(DramaticEntrenceDef))]
    public sealed class DramaticEntrence : Card
    {
        public override int AdditionalDamage
        {
            get
            {
                if (base.Battle != null)
                {
                    List<Card> list = base.GameRun.BaseDeck.Where((Card card) => card is DramaticEntrence).ToList<Card>();
                    return list.Sum((Card card) => card.Value1);
                }
                return 0;
            }
        }
        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
        }
        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
        {
            if (this == base.Battle.EnumerateAllCards().First((Card card) => card is DramaticEntrence))
            {
                List<Card> list = base.Battle.DrawZone.Where((Card card) => card is DramaticEntrence).ToList<Card>();
                yield return new ExileManyCardAction(list);
                yield return base.AttackAction(base.Battle.AllAliveEnemies, "StarPasNoAni");
            }
            yield break;
        }
    }
}
