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
using LBoL.EntityLib.Exhibits;
using JetBrains.Annotations;
using LBoL.Core.Stations;
using LBoL.EntityLib.EnemyUnits.Character;
using HarmonyLib;
using LBoL.EntityLib.EnemyUnits.Normal;
using System.Runtime.CompilerServices;
using LBoL.Core.GapOptions;
using System.Reflection.Emit;

namespace test.Exhibits
{
    public sealed class StSEctoplasmDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSEctoplasm);
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
                Appearance: AppearanceType.Anywhere,
                Owner: "",
                LosableType: ExhibitLosableType.CantLose,
                Rarity: Rarity.Shining,
                Value1: 0,
                Value2: null,
                Value3: null,
                Mana: null,
                BaseManaRequirement: null,
                BaseManaColor: ManaColor.Green,
                BaseManaAmount: 2,
                HasCounter: false,
                InitialCounter: null,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },

                RelativeCards: new List<string>() { }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(StSEctoplasmDef))]
        [UsedImplicitly]
        [ExhibitInfo(ExpireStageLevel = 3, ExpireStationLevel = 0)]
        public sealed class StSEctoplasm : ShiningExhibit
        {
            [HarmonyPatch(typeof(GameRunController), nameof(GameRunController.InternalGainMoney))]
            class GameRunController_InternalGainMoney_Patch
            {
                static bool Prefix()
                {
                    if (GameMaster.Instance.CurrentGameRun.Player.HasExhibit<StSEctoplasm>())
                    {
                        return false;
                    }
                    return true;
                }
            }
            [HarmonyPatch(typeof(GapStation), nameof(GapStation.OnEnter))]
            class GapStation_OnEnter_Patch
            {
                static void Postfix(GapStation __instance)
                {
                    if (GameMaster.Instance.CurrentGameRun.Player.HasExhibit<StSEctoplasm>())
                    {
                        if (__instance.GapOptions.FirstOrDefault((gapOption) => gapOption.Type == GapOptionType.UpgradeCard) is UpgradeCard upgradeOption)
                        {
                            upgradeOption.Price = 0;
                        }
                    }
                }
            }
            protected override void OnAdded(PlayerUnit player)
            {
                HandleGameRunEvent(GameRun.MoneyGained, delegate (GameEventArgs _)
                {
                    NotifyActivating();
                });
            }
        }
    }
}