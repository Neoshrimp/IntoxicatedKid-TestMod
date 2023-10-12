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
using static test.BepinexPlugin;
using System.Linq;
using LBoL.Presentation;
using System.Collections;
using UnityEngine;
using System.Reflection;
using TMPro;
using DG.Tweening;
using LBoL.Presentation.UI;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LBoL.Presentation.UI.Dialogs;
using LBoL.Presentation.UI.Panels;
using UnityEngine.UI;

namespace test.Cards
{
    public sealed class CirnoSpellingTestDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(CirnoSpellingTest);
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
               HideMesuem: false,
               IsUpgradable: true,
               Rarity: Rarity.Uncommon,
               Type: CardType.Skill,
               TargetType: TargetType.Nobody,
               Colors: new List<ManaColor>() { ManaColor.Green },
               IsXCost: false,
               Cost: new ManaGroup() { Any = 1, Green = 1 },
               UpgradedCost: new ManaGroup() { Any = 1, Green = 1 },
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
               Mana: new ManaGroup() { Any = 1 },
               UpgradedMana: new ManaGroup() { Any = 0 },
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
               RelativeKeyword: Keyword.Morph,
               UpgradedRelativeKeyword: Keyword.Morph,

               RelativeEffects: new List<string>() { },
               UpgradedRelativeEffects: new List<string>() { },
               RelativeCards: new List<string>() { },
               UpgradedRelativeCards: new List<string>() { },
               Owner: "Cirno",
               Unfinished: false,
               Illustrator: "__fds",
               SubIllustrator: new List<string>() { }
            );

            return cardConfig;
        }
    }
    [EntityLogic(typeof(CirnoSpellingTestDef))]
    public sealed class CirnoSpellingTest : Card, IInputActionHandler
    {
        Card card = null;
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            inputField.text = "";
            List<Card> list = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Id != base.Id).ToList<Card>();
            if (list.Count > 0)
            {
                foreach (Card card1 in list)
                {
                    card1.IsExile = true;
                    card1.IsEthereal = true;
                    card = card1;
                    GameMaster.Instance.StartCoroutine(InputCoroutine());
                }
                yield return new AddCardsToHandAction(card);
            }
            yield break;
        }
        private IEnumerator InputCoroutine()
        {
            this.inputField.text = card.Name;
            this.nameInputRoot.gameObject.SetActive(true);
            this.nameInputRoot.GetComponent<CanvasGroup>().interactable = true;
            this.nameInputRoot.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).From(0f, true, false);
            return null;
        }
        void IInputActionHandler.OnConfirm()
        {
            if (this.nameInputRoot.activeSelf)
            {
                if (card.Config.Illustrator.Contains(this.inputField.text) || card.Config.SubIllustrator.Contains(this.inputField.text))
                {
                    card.BaseCost = this.Mana;
                }
                this.nameInputRoot.GetComponent<CanvasGroup>().interactable = false;
                TweenerCore<float, float, FloatOptions> tweenerCore = this.nameInputRoot.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).From(1f, true, false);
                tweenerCore.onComplete = (TweenCallback)Delegate.Combine(tweenerCore.onComplete, new TweenCallback(delegate
                {
                    this.nameInputRoot.gameObject.SetActive(false);
                }));
            }
        }
        void IInputActionHandler.OnCancel()
        {
            if (this.nameInputRoot.activeSelf)
            {
                this.nameInputRoot.GetComponent<CanvasGroup>().interactable = false;
                TweenerCore<float, float, FloatOptions> tweenerCore = this.nameInputRoot.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).From(1f, true, false);
                tweenerCore.onComplete = (TweenCallback)Delegate.Combine(tweenerCore.onComplete, new TweenCallback(delegate
                {
                    this.nameInputRoot.gameObject.SetActive(false);
                }));
            }
        }
        [SerializeField]
        private GameObject nameInputRoot = new GameObject();
        [SerializeField]
        private TMP_InputField inputField = null;
    }
}
