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

namespace test
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
               Rarity: Rarity.Common,
               Type: CardType.Attack,
               TargetType: TargetType.SingleEnemy,
               Colors: new List<ManaColor>() { ManaColor.Red },
               IsXCost: false,
               Cost: new ManaGroup() { Red = 2 },
               UpgradedCost: null,
               MoneyCost: null,
               Damage: 12,
               UpgradedDamage: 15,
               Block: null,
               UpgradedBlock: null,
               Shield: null,
               UpgradedShield: null,
               Value1: 2,
               UpgradedValue1: 3,
               Value2: 3,
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

               RelativeEffects: new List<string>() { "LockedOn" },
               UpgradedRelativeEffects: new List<string>() { "LockedOn" },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: null,
               Unfinished: false,
               Illustrator: "",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(AyaFastestGunDef))]
    public sealed class AyaFastestGun : Card
    {
        private bool trigger;
        public override bool Triggered
        {
            get
            {
                if (base.Battle != null)
                {
                    return this.trigger;
                }
                return false;
            }
        }
        public override IEnumerable<BattleAction> OnDraw()
        {
            GameMaster.Instance.StartCoroutine(Trigger());
            return null;
        }
        private IEnumerator Trigger()
        {
            this.trigger = true;
            yield return new WaitForSeconds(base.Value1);
            this.trigger = false;
            base.NotifyChanged();
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector);
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            if (selectedEnemy.IsAlive && base.PlayInTriggered)
            {
                if (selectedEnemy.HasStatusEffect<FastAttack>())
                {
                    yield return new RemoveStatusEffectAction(selectedEnemy.GetStatusEffect<FastAttack>(), true);
                }
                yield return base.DebuffAction<LockedOn>(selectedEnemy, base.Value2, 0, 0, 0, true, 0.2f);
            }
            yield break;
        }
    }
}
