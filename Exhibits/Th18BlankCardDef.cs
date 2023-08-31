using LBoL.ConfigData;
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
using JetBrains.Annotations;

namespace test
{
    public sealed class Th18BlankCardDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Th18BlankCard);
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
            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite((folder + GetId() + s + ".png"), embeddedSource);
            exhibitSprites.main = wrap("");
            return exhibitSprites;
        }
        public override ExhibitConfig MakeConfig()
        {
            var exhibitConfig = new ExhibitConfig(
                Index: sequenceTable.Next(typeof(ExhibitConfig)),
                Id: "",
                Order: 9,
                IsDebug: false,
                IsPooled: true,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.ShopOnly,
                Owner: "",
                LosableType: ExhibitLosableType.Losable,
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
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                // example of referring to UniqueId of an entity without calling MakeConfig
                RelativeCards: new List<string>() { }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(Th18BlankCardDef))]
        [UsedImplicitly]
        public sealed class Th18BlankCard : Exhibit
        {
            protected override void OnEnterBattle()
            {
                base.ReactBattleEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarted));
            }
            private IEnumerable<BattleAction> OnPlayerTurnStarted(GameEventArgs args)
            {
                if (base.Battle.Player.TurnCounter == 1)
                {
                    base.NotifyActivating();
                    List<Card> attackc = null;
                    List<Card> attacku = null;
                    List<Card> attackr = null;
                    List<Card> defensec = null;
                    List<Card> defenseu = null;
                    List<Card> defenser = null;
                    List<Card> skillc = null;
                    List<Card> skillu = null;
                    List<Card> skillr = null;
                    List<Card> abilityc = null;
                    List<Card> abilityu = null;
                    List<Card> abilityr = null;
                    List<Card> friendc = null;
                    List<Card> friendu = null;
                    List<Card> friendr = null;
                    //List<Card> statusc = null;
                    //List<Card> statusu = null;
                    //List<Card> statusr = null;
                    //List<Card> misfortunec = null;
                    //List<Card> misfortuneu = null;
                    //List<Card> misfortuner = null;
                    List<Card> hand = base.Battle.HandZone.ToList<Card>();
                    if (hand.Count > 0)
                    {
                        attackc = hand.Where((Card card) => (card.CardType == CardType.Attack) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        attacku = hand.Where((Card card) => (card.CardType == CardType.Attack) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        attackr = hand.Where((Card card) => (card.CardType == CardType.Attack) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        defensec = hand.Where((Card card) => (card.CardType == CardType.Defense) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        defenseu = hand.Where((Card card) => (card.CardType == CardType.Defense) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        defenser = hand.Where((Card card) => (card.CardType == CardType.Defense) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        skillc = hand.Where((Card card) => (card.CardType == CardType.Skill) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        skillu = hand.Where((Card card) => (card.CardType == CardType.Skill) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        skillr = hand.Where((Card card) => (card.CardType == CardType.Skill) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        abilityc = hand.Where((Card card) => (card.CardType == CardType.Ability) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        abilityu = hand.Where((Card card) => (card.CardType == CardType.Ability) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        abilityr = hand.Where((Card card) => (card.CardType == CardType.Ability) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        friendc = hand.Where((Card card) => (card.CardType == CardType.Friend) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        friendu = hand.Where((Card card) => (card.CardType == CardType.Friend) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        friendr = hand.Where((Card card) => (card.CardType == CardType.Friend) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        //statusc = hand.Where((Card card) => (card.CardType == CardType.Status) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        //statusu = hand.Where((Card card) => (card.CardType == CardType.Status) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        //statusr = hand.Where((Card card) => (card.CardType == CardType.Status) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        //misfortunec = hand.Where((Card card) => (card.CardType == CardType.Misfortune) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        //misfortuneu = hand.Where((Card card) => (card.CardType == CardType.Misfortune) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        //misfortuner = hand.Where((Card card) => (card.CardType == CardType.Misfortune) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        List<Card> list = new List<Card>();
                        yield return new ExileManyCardAction(hand);
                        foreach (Card card in attackc)
                        {
                            Card[] attackC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Attack);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(attackC);
                            }
                            list.AddRange(attackC);
                        }
                        foreach (Card card in attacku)
                        {
                            Card[] attackU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Attack);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(attackU);
                            }
                            list.AddRange(attackU);
                        }
                        foreach (Card card in attackr)
                        {
                            Card[] attackR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Attack);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(attackR);
                            }
                            list.AddRange(attackR);
                        }
                        foreach (Card card in defensec)
                        {
                            Card[] defenseC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Defense);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(defenseC);
                            }
                            list.AddRange(defenseC);
                        }
                        foreach (Card card in defenseu)
                        {
                            Card[] defenseU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Defense);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(defenseU);
                            }
                            list.AddRange(defenseU);
                        }
                        foreach (Card card in defenser)
                        {
                            Card[] defenseR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Defense);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(defenseR);
                            }
                            list.AddRange(defenseR);
                        }
                        foreach (Card card in skillc)
                        {
                            Card[] skillC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Skill);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(skillC);
                            }
                            list.AddRange(skillC);
                        }
                        foreach (Card card in skillu)
                        {
                            Card[] skillU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Skill);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(skillU);
                            }
                            list.AddRange(skillU);
                        }
                        foreach (Card card in skillr)
                        {
                            Card[] skillR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Skill);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(skillR);
                            }
                            list.AddRange(skillR);
                        }
                        foreach (Card card in abilityc)
                        {
                            Card[] abilityC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Ability);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(abilityC);
                            }
                            list.AddRange(abilityC);
                        }
                        foreach (Card card in abilityu)
                        {
                            Card[] abilityU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Ability);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(abilityU);
                            }
                            list.AddRange(abilityU);
                        }
                        foreach (Card card in abilityr)
                        {
                            Card[] abilityR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Ability);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(abilityR);
                            }
                            list.AddRange(abilityR);
                        }
                        foreach (Card card in friendc)
                        {
                            Card[] friendC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Friend);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(friendC);
                            }
                            list.AddRange(friendC);
                        }
                        foreach (Card card in friendu)
                        {
                            Card[] friendU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Friend);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(friendU);
                            }
                            list.AddRange(friendU);
                        }
                        foreach (Card card in friendr)
                        {
                            Card[] friendR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Friend);
                            if (card.IsUpgraded)
                            {
                                yield return new UpgradeCardsAction(friendR);
                            }
                            list.AddRange(friendR);
                        }
                        //foreach (Card card in statusc)
                        //{
                        //    Card[] statusC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.AllOnes), 1, (CardConfig config) => config.Type == CardType.Status);
                        //    list.AddRange(statusC);
                        //}
                        //foreach (Card card in statusu)
                        //{
                        //    Card[] statusU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.AllOnes), 1, (CardConfig config) => config.Type == CardType.Status);
                        //    list.AddRange(statusU);
                        //}
                        //foreach (Card card in statusr)
                        //{
                        //    Card[] statusR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.AllOnes), 1, (CardConfig config) => config.Type == CardType.Status);
                        //    list.AddRange(statusR);
                        //}
                        //foreach (Card card in misfortunec)
                        //{
                        //    Card[] misfortuneC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.AllOnes), 1, (CardConfig config) => config.Type == CardType.Misfortune);
                        //    list.AddRange(misfortuneC);
                        //}
                        //foreach (Card card in misfortuneu)
                        //{
                        //    Card[] misfortuneU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.AllOnes), 1, (CardConfig config) => config.Type == CardType.Misfortune);
                        //    list.AddRange(misfortuneU);
                        //}
                        //foreach (Card card in misfortuner)
                        //{
                        //    Card[] misfortuneR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.AllOnes), 1, (CardConfig config) => config.Type == CardType.Misfortune);
                        //    list.AddRange(misfortuneR);
                        //}
                        yield return new AddCardsToHandAction(list);
                    }
                }
                yield break;
            }
        }
    }
}