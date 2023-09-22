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
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Cirno.Friend;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.MultiColor;
using LBoL.Presentation.UI.Panels;
using UnityEngine.InputSystem.Controls;
using JetBrains.Annotations;
using LBoL.EntityLib.Cards.Character.Marisa;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.EntityLib.Cards.Character.Cirno;

namespace test
{
    public sealed class StSOrangePelletsDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSOrangePellets);
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
                Order: 10,
                IsDebug: false,
                IsPooled: true,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.ShopOnly,
                Owner: "",
                LosableType: ExhibitLosableType.Losable,
                Rarity: Rarity.Common,
                Value1: 4,
                Value2: null,
                Value3: null,
                Mana: null,
                BaseManaRequirement: null,
                BaseManaColor: null,
                BaseManaAmount: 0,
                HasCounter: true,
                InitialCounter: 0,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(StSOrangePelletsDef))]
        [UsedImplicitly]
        public sealed class StSOrangePellets : Exhibit
        {
            private bool unknown = false;
            private bool attack = false;
            private bool defense = false;
            private bool skill = false;
            private bool ability = false;
            private bool friend = false;
            private bool tool = false;
            private bool status = false;
            private bool misfortune = false;
            protected override void OnEnterBattle()
            {
                base.ReactBattleEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
                base.HandleBattleEvent<UnitEventArgs>(base.Battle.Player.TurnEnding, delegate (UnitEventArgs _)
                {
                    base.Counter = 0;
                    this.unknown = false;
                    this.attack = false;
                    this.defense = false;
                    this.skill = false;
                    this.ability = false;
                    this.friend = false;
                    this.tool = false;
                    this.status = false;
                    this.misfortune = false;
                });
            }
            private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
            {
                if (base.Owner.IsInTurn)
                {
                    if (args.Card.CardType == CardType.Unknown && !this.unknown)
                    {
                        this.unknown = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Attack && !this.attack)
                    {
                        this.attack = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Defense && !this.defense)
                    {
                        this.defense = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Skill && !this.skill)
                    {
                        this.skill = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Ability && !this.ability)
                    {
                        this.ability = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Friend && !this.friend)
                    {
                        this.friend = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Tool && !this.tool)
                    {
                        this.tool = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Status && !this.status)
                    {
                        this.status = true;
                        base.Counter += 1;
                    }
                    else if (args.Card.CardType == CardType.Misfortune && !this.misfortune)
                    {
                        this.misfortune = true;
                        base.Counter += 1;
                    }
                    if (this.Counter >= base.Value1)
                    {
                        base.NotifyActivating();
                        base.Counter = 0;
                        this.unknown = false;
                        this.attack = false;
                        this.defense = false;
                        this.skill = false;
                        this.ability = false;
                        this.friend = false;
                        this.tool = false;
                        this.status = false;
                        this.misfortune = false;
                        yield return new RemoveAllNegativeStatusEffectAction(base.Battle.Player);
                    }
                }
                yield break;
            }
            protected override void OnLeaveBattle()
            {
                base.Counter = 0;
                this.unknown = false;
                this.attack = false;
                this.defense = false;
                this.skill = false;
                this.ability = false;
                this.friend = false;
                this.tool = false;
                this.status = false;
                this.misfortune = false;
            }
        }
    }
}