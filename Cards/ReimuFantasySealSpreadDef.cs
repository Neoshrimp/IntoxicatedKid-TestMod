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
using static test.BepinexPlugin;
using UnityEngine;
using JetBrains.Annotations;
using LBoL.Core.Randoms;
using LBoL.Presentation;
using UnityEngine.UI;
using static LBoL.Core.CrossPlatformHelper;
using static UnityEngine.UI.GridLayoutGroup;
using LBoL.Base.Extensions;
using System.Collections;
using System.Reflection.Emit;

namespace test.Cards
{
    public sealed class ReimuFantasySealSpreadDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ReimuFantasySealSpread);
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
               GunName: "扩散结界",
               GunNameBurst: "扩散结界",
               DebugLevel: 0,
               Revealable: false,
               IsPooled: true,
               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Rare,
               Type: CardType.Attack,
               TargetType: TargetType.AllEnemies,
               Colors: new List<ManaColor>() { ManaColor.White, ManaColor.Red },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 2, White = 1, Red = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: 0,
               UpgradedDamage: null,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 9,
               UpgradedValue1: 12,
               Value2: 18,
               UpgradedValue2: 24,
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

               Keywords: Keyword.Accuracy | Keyword.Exile,
               UpgradedKeywords: Keyword.Accuracy | Keyword.Exile,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: null,
               Unfinished: false,
               Illustrator: "tu2ki",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(ReimuFantasySealSpreadDef))]
    public sealed class ReimuFantasySealSpread : Card
    {
        [UsedImplicitly]
        public int Value3
        {
            get
            {
                return 2;
            }
        }
        protected override void SetGuns()
        {
            base.CardGuns = new Guns(base.GunName, base.Value1, true);
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            for (int i = 0; i < Value3; i++)
            {
                yield return new DamageAction(base.Battle.Player, Battle.EnemyGroup.Alives, DamageInfo.Attack(random.Next(base.Value1, base.Value2 + 1)), "扩散结界", GunType.Single);
            }
        }
        private System.Random random = new System.Random();
    }
}
