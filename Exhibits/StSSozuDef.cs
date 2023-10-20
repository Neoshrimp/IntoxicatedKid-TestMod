﻿using LBoL.ConfigData;
using LBoL.Core.Cards;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using static test.BepinexPlugin;
using UnityEngine;
using LBoL.Core;
using LBoL.Base;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Base.Extensions;
using System.Collections;
using LBoL.Presentation;
using LBoL.EntityLib.Cards.Neutral.Blue;
using Mono.Cecil;
using LBoL.Core.StatusEffects;
using System.Linq;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Randoms;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Other.Misfortune;
using static UnityEngine.TouchScreenKeyboard;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Cirno.Friend;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.MultiColor;
using LBoL.Presentation.UI.Panels;
using UnityEngine.InputSystem.Controls;
using JetBrains.Annotations;
using LBoL.EntityLib.Cards.Character.Marisa;

namespace test.Exhibits
{
    public sealed class StSSozuDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSSozu);
        }
        public override LocalizationOption LoadLocalization()
        {
            var locFiles = new LocalizationFiles(embeddedSource);
            locFiles.AddLocaleFile(Locale.En, "Resources.ExhibitsEn.yaml");
            return locFiles;
        }
        public override ExhibitSprites LoadSprite()
        {
            // embedded resource folders are separated by a dot
            var folder = "";
            var exhibitSprites = new ExhibitSprites();
            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite(folder + GetId() + s + ".png", embeddedSource);
            exhibitSprites.main = wrap("");
            return exhibitSprites;
        }
        public override ExhibitConfig MakeConfig()
        {
            var exhibitConfig = new ExhibitConfig(
                Index: sequenceTable.Next(typeof(ExhibitConfig)),
                Id: "",
                Order: 10,
                IsDebug: false,
                IsPooled: true,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.ShopOnly,
                Owner: "",
                LosableType: ExhibitLosableType.CantLose,
                Rarity: Rarity.Common,
                Value1: null,
                Value2: null,
                Value3: null,
                Mana: null,
                BaseManaRequirement: null,
                BaseManaColor: null,
                BaseManaAmount: 0,
                HasCounter: false,
                InitialCounter: 0,
                Keywords: Keyword.Forbidden,
                RelativeEffects: new List<string>() { },

                RelativeCards: new List<string>() { }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(StSSozuDef))]
        [UsedImplicitly]
        public sealed class StSSozu : Exhibit
        {
            protected override void OnEnterBattle()
            {
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStarted));
                ReactBattleEvent(Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarted));
                ReactBattleEvent(Battle.CardsAddedToDrawZone, new EventSequencedReactor<CardsAddingToDrawZoneEventArgs>(OnCardsAddedToDrawZone));
                ReactBattleEvent(Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(OnAddCard));
                ReactBattleEvent(Battle.CardsAddedToDiscard, new EventSequencedReactor<CardsEventArgs>(OnAddCard));
                ReactBattleEvent(Battle.CardsAddedToExile, new EventSequencedReactor<CardsEventArgs>(OnAddCard));
            }
            private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
            {
                foreach (Card card in Battle.EnumerateAllCards())
                {
                    if (card.CardType == CardType.Tool)
                    {
                        card.NotifyChanged();
                        card.IsForbidden = true;
                    }
                }
                yield break;
            }
            private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
            {
                NotifyActivating();
                ManaGroup manaGroup = ManaGroup.Single(ManaColors.Colors.Sample(GameRun.BattleRng));
                yield return new GainManaAction(manaGroup);
                yield break;
            }
            private IEnumerable<BattleAction> OnCardsAddedToDrawZone(CardsAddingToDrawZoneEventArgs args)
            {
                yield return ToolCardModify(args.Cards);
                yield break;
            }
            private IEnumerable<BattleAction> OnAddCard(CardsEventArgs args)
            {
                yield return ToolCardModify(args.Cards);
                yield break;
            }
            private BattleAction ToolCardModify(IEnumerable<Card> cards)
            {
                foreach (Card card in cards)
                {
                    if (card.CardType == CardType.Tool)
                    {
                        card.NotifyChanged();
                        card.IsForbidden = true;
                    }
                }
                return null;
            }
        }
    }
}