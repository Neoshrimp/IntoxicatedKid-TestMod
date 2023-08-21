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
            // creates global localization for exhibits. Each entity type needs to have their own global localization
            var globalLoc = new GlobalLocalization(embeddedSource);
            globalLoc.LocalizationFiles.AddLocaleFile(Locale.En, "ExhibitsEn");

            return globalLoc;
        }
        
        public override ExhibitSprites LoadSprite()
        {
            // embedded resource folders are separated by a dot
            var folder = "";
            var exhibitSprites = new ExhibitSprites();

            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite((folder + GetId() + s + ".png"), embeddedSource);

            exhibitSprites.main = wrap("");

            // loads many custom sprites for futher use
            //exhibitSprites.customSprites.Add("none", wrap("_none"));
            //exhibitSprites.customSprites.Add("luna", wrap("_luna"));
            //exhibitSprites.customSprites.Add("-luna", wrap("_-luna"));
            //exhibitSprites.customSprites.Add("star", wrap("_star"));
            //exhibitSprites.customSprites.Add("-star", wrap("_-star"));
            //exhibitSprites.customSprites.Add("sunny", wrap("_sunny"));
            //exhibitSprites.customSprites.Add("-sunny", wrap("_-sunny"));


            return exhibitSprites;
        }



        public override ExhibitConfig MakeConfig()
        {
            var exhibitConfig = new ExhibitConfig(
                Index: 0,
                Id: "",
                Order: 10,
                IsDebug: false,
                IsPooled: true,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.Anywhere,
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
        public sealed class Th18BlankCard : Exhibit
        {
            // Changes the icon according to last three cards played 
            // Sunny = attack, Star = defense, Luna = skill
            // this is where the keys of custom sprites are used
            //public override string OverrideIconName
            //{
            //    get
            //    {
            //        if (Battle == null)
            //            return Id;
            //
            //        if (triggered)
            //            return Id;
            //
            //        if (cardTracker.Empty())
            //            return Id + "none";
            //        if (cardTracker.Count == 1)
            //        {
            //            if (cardTracker.Contains(CardType.Attack))
            //                return Id + "sunny";
            //            if (cardTracker.Contains(CardType.Defense))
            //                return Id + "star";
            //            if (cardTracker.Contains(CardType.Skill))
            //                return Id + "luna";
            //        }
            //        if (cardTracker.Count == 2)
            //        {
            //            if (!cardTracker.Contains(CardType.Attack))
            //                return Id + "-sunny";
            //            if (!cardTracker.Contains(CardType.Defense))
            //                return Id + "-star";
            //            if (!cardTracker.Contains(CardType.Skill))
            //                return Id + "-luna";
            //        }
            //
            //
            //        return Id;
            //    }
            //
            //}
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
                    List<Card> statusc = null;
                    List<Card> statusu = null;
                    List<Card> statusr = null;
                    List<Card> misfortunec = null;
                    List<Card> misfortuneu = null;
                    List<Card> misfortuner = null;
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
                        statusc = hand.Where((Card card) => (card.CardType == CardType.Status) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        statusu = hand.Where((Card card) => (card.CardType == CardType.Status) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        statusr = hand.Where((Card card) => (card.CardType == CardType.Status) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        misfortunec = hand.Where((Card card) => (card.CardType == CardType.Misfortune) && (card.Config.Rarity == Rarity.Common)).ToList<Card>();
                        misfortuneu = hand.Where((Card card) => (card.CardType == CardType.Misfortune) && (card.Config.Rarity == Rarity.Uncommon)).ToList<Card>();
                        misfortuner = hand.Where((Card card) => (card.CardType == CardType.Misfortune) && (card.Config.Rarity == Rarity.Rare)).ToList<Card>();
                        yield return new ExileManyCardAction(hand);


                        var newCards = new List<Card>();

                        

                        if (attackc.Count > 0)
                        {
                            foreach (Card card in attackc)
                            {
                                Card[] attackC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Attack);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(attackC);
                                }
                                newCards.AddRange(attackC);
                            }
                        }
                        if (attacku.Count > 0)
                        {
                            foreach (Card card in attacku)
                            {
                                Card[] attackU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Attack);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(attackU);
                                }
                                newCards.AddRange(attackU);
                            }
                        }
                        if (attackr.Count > 0)
                        {
                            foreach (Card card in attackr)
                            {
                                Card[] attackR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Attack);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(attackR);
                                }
                                newCards.AddRange(attackR);
                            }
                        }
                        if (defensec.Count > 0)
                        {
                            foreach (Card card in defensec)
                            {
                                Card[] defenseC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Defense);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(defenseC);
                                }
                                newCards.AddRange(defenseC);
                            }
                        }
                        if (defenseu.Count > 0)
                        {
                            foreach (Card card in defenseu)
                            {
                                Card[] defenseU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Defense);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(defenseU);
                                }
                                newCards.AddRange(defenseU);
                            }
                        }
                        if (defenser.Count > 0)
                        {
                            foreach (Card card in defenser)
                            {
                                Card[] defenseR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Defense);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(defenseR);
                                }
                                newCards.AddRange(defenseR);
                            }
                        }
                        if (skillc.Count > 0)
                        {
                            foreach (Card card in skillc)
                            {
                                Card[] skillC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Skill);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(skillC);
                                }
                                newCards.AddRange(skillC);
                            }
                        }
                        if (skillu.Count > 0)
                        {
                            foreach (Card card in skillu)
                            {
                                Card[] skillU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Skill);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(skillU);
                                }
                                newCards.AddRange(skillU);
                            }
                        }
                        if (skillr.Count > 0)
                        {
                            foreach (Card card in skillr)
                            {
                                Card[] skillR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Skill);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(skillR);
                                }
                                newCards.AddRange(skillR);
                            }
                        }
                        if (abilityc.Count > 0)
                        {
                            foreach (Card card in abilityc)
                            {
                                Card[] abilityC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Ability);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(abilityC);
                                }
                                newCards.AddRange(abilityC);
                            }
                        }
                        if (abilityu.Count > 0)
                        {
                            foreach (Card card in abilityu)
                            {
                                Card[] abilityU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Ability);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(abilityU);
                                }
                                newCards.AddRange(abilityU);
                            }
                        }
                        if (abilityr.Count > 0)
                        {
                            foreach (Card card in abilityr)
                            {
                                Card[] abilityR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Ability);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(abilityR);
                                }
                                newCards.AddRange(abilityR);
                            }
                        }
                        if (friendc.Count > 0)
                        {
                            foreach (Card card in friendc)
                            {
                                Card[] friendC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Friend);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(friendC);
                                }
                                newCards.AddRange(friendC);
                            }
                        }
                        if (friendu.Count > 0)
                        {
                            foreach (Card card in friendu)
                            {
                                Card[] friendU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Friend);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(friendU);
                                }
                                newCards.AddRange(friendU);
                            }
                        }
                        if (friendr.Count > 0)
                        {
                            foreach (Card card in friendr)
                            {
                                Card[] friendR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Friend);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(friendR);
                                }
                                newCards.AddRange(friendR);
                            }
                        }
                        if (statusc.Count > 0)
                        {
                            foreach (Card card in statusc)
                            {
                                Card[] statusC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Status);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(statusC);
                                }
                                newCards.AddRange(statusC);
                            }
                        }
                        if (statusu.Count > 0)
                        {
                            foreach (Card card in statusu)
                            {
                                Card[] statusU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Status);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(statusU);
                                }
                                newCards.AddRange(statusU);
                            }
                        }
                        if (statusr.Count > 0)
                        {
                            foreach (Card card in statusr)
                            {
                                Card[] statusR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Status);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(statusR);
                                }
                                newCards.AddRange(statusR);
                            }
                        }
                        if (misfortunec.Count > 0)
                        {
                            foreach (Card card in misfortunec)
                            {
                                Card[] misfortuneC = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Misfortune);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(misfortuneC);
                                }
                                newCards.AddRange(misfortuneC);
                            }
                        }
                        if (misfortuneu.Count > 0)
                        {
                            foreach (Card card in misfortuneu)
                            {
                                Card[] misfortuneU = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyUncommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Misfortune);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(misfortuneU);
                                }
                                newCards.AddRange(misfortuneU);
                            }
                        }
                        if (misfortuner.Count > 0)
                        {
                            foreach (Card card in misfortuner)
                            {
                                Card[] misfortuneR = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyRare, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, (CardConfig config) => config.Type == CardType.Misfortune);
                                if (card.IsUpgraded)
                                {
                                    yield return new UpgradeCardsAction(misfortuneR);
                                }
                                newCards.AddRange(misfortuneR);
                            }
                        }
                        yield return new AddCardsToHandAction(newCards);
                    }

                }


                yield break;
            }
        }
    }
}