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
using LBoL.EntityLib.Cards.Character.Cirno.Friend;
using UnityEngine;
using LBoL.Presentation;
using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI;
using System.Collections;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoL.EntityLib.StatusEffects.Basic;

namespace test.Cards
{
    public sealed class AyaFastestGunDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AyaFastestGun);
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
               GunName: "追查真凶0",
               GunNameBurst: "追查真凶0",
               DebugLevel: 0,
               Revealable: false,
               IsPooled: true,
               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Uncommon,
               Type: CardType.Attack,
               TargetType: TargetType.SingleEnemy,
               Colors: new List<ManaColor>() { ManaColor.Red },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 1, Red = 1 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: 12,
               UpgradedDamage: 18,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 3,
               UpgradedValue1: null,
               Value2: 2,
               UpgradedValue2: 3,
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

               Keywords: Keyword.Accuracy,
               UpgradedKeywords: Keyword.Accuracy,
               EmptyDescription: false,
               RelativeKeyword: Keyword.None,
               UpgradedRelativeKeyword: Keyword.None,

               RelativeEffects: new List<string>() { "LockedOn", "FastAttack" },
               UpgradedRelativeEffects: new List<string>() { "LockedOn", "FastAttack" },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: "AyaPlayerUnit",
               Unfinished: false,
               Illustrator: "たいちょん",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(AyaFastestGunDef))]
    public sealed class AyaFastestGun : Card
    {
        public override IEnumerable<BattleAction> OnDraw()
        {
            GameMaster.Instance.StartCoroutine(Trigger());
            return null;
        }
        private IEnumerator Trigger()
        {
            PlayInTriggered = true;
            yield return new WaitForSecondsRealtime(Value1);
            PlayInTriggered = false;
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return AttackAction(selector);
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            if (selectedEnemy.IsAlive && PlayInTriggered)
            {
                yield return DebuffAction<LockedOn>(selectedEnemy, Value2, 0, 0, 0, true, 0.2f);
                if (selectedEnemy.IsAlive && selectedEnemy.HasStatusEffect<FastAttack>())
                {
                    yield return new RemoveStatusEffectAction(selectedEnemy.GetStatusEffect<FastAttack>(), true);
                }
            }
            yield break;
        }
    }
}
