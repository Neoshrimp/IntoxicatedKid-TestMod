using LBoL.ConfigData;
using LBoL.Core.Cards;
using JetBrains.Annotations;
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

namespace test.Exhibits
{
    public sealed class TestExhibitDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(TestExhibit);
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
                IsPooled: false,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.Nowhere,
                Owner: "",
                LosableType: ExhibitLosableType.CantLose,
                Rarity: Rarity.Common,
                Value1: 999,
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
                RelativeCards: new List<string>() { }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(TestExhibitDef))]
        [UsedImplicitly]
        public sealed class TestExhibit : Exhibit, IMapModeOverrider
        {
            protected override void OnAdded(PlayerUnit player)
            {
                GameRun.AddMapModeOverrider(this);
            }
            protected override void OnRemoved(PlayerUnit player)
            {
                GameRun.RemoveMapModeOverrider(this);
            }
            public GameRunMapMode? MapMode
            {
                get
                {
                    return new GameRunMapMode?(GameRunMapMode.Free | GameRunMapMode.Crossing | GameRunMapMode.TeleportBoss);
                }
            }
            public void OnEnteredWithMode()
            {
                NotifyActivating();
            }
            protected override void OnEnterBattle()
            {
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStarted));
            }
            private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
            {
                NotifyActivating();
                yield return new ApplyStatusEffectAction<Firepower>(Owner, new int?(Value1), null, null, null, 0f, true);
                yield return new ApplyStatusEffectAction<Spirit>(Owner, new int?(Value1), null, null, null, 0f, true);
                yield break;
            }
        }
    }
}