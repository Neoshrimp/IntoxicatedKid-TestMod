﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Adventures;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Units;
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
using LBoL.EntityLib.StatusEffects.Reimu;
using LBoL.Base.Extensions;
using System.Linq;
using UnityEngine;

namespace test.Cards
{
    public sealed class AyaAttackGDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaAttackG);
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
               GunName: "飞刀弹射",
               GunNameBurst: "飞刀弹射B",
               DebugLevel: 0,
               Revealable: false,
               IsPooled: true,
               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Common,
               Type: CardType.Attack,
               TargetType: TargetType.SingleEnemy,
               Colors: new List<ManaColor>() { ManaColor.Green },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 1, Green = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: 10,
               UpgradedDamage: 14,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: null,
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

               Keywords: Keyword.Basic,
               UpgradedKeywords: Keyword.Basic,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: "AyaPlayerUnit",
               Unfinished: false,
               Illustrator: "",
               SubIllustrator: new List<string>() { "My_Computer" }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(AyaAttackGDef))]
    public sealed class AyaAttackG : Card
    {
    }
}
