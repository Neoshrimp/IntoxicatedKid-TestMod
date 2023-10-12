using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using HarmonyLib;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.EntityLib.Exhibits.Common;
using LBoL.EntityLib.Exhibits.Shining;
using LBoL.EntityLib.PlayerUnits;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoL.Presentation;
using LBoL.Presentation.UI;
using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI.Widgets;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader.UIhelpers;
using LBoLEntitySideloader.Utils;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using test.StatusEffects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Windows;
using static test.BepinexPlugin;


namespace test.PlayerUnits
{
    public sealed class AyaPlayerUnitDef : PlayerUnitTemplate
    {
        public static DirectorySource dir = new DirectorySource(PluginInfo.GUID, "AyaPlayerUnit");
        public static string name = nameof(AyaPlayerUnit);
        public override IdContainer GetId() => nameof(AyaPlayerUnit);
        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(embeddedSource);
            gl.LocalizationFiles.AddLocaleFile(Locale.En, "PlayerUnitsEn");
            return gl;
        }
        public override PlayerImages LoadPlayerImages()
        {
            var sprites = new PlayerImages();
            sprites.AutoLoad("", (s) => ResourceLoader.LoadSprite(s, dir), (s) => ResourceLoader.LoadSpriteAsync(s, dir));
            return sprites;
        }
        public override PlayerUnitConfig MakeConfig()
        {
            var reimuConfig = PlayerUnitConfig.FromId("Reimu").Copy();

            var config = new PlayerUnitConfig(
            Id: "",
            ShowOrder: 6,
            Order: 0,
            UnlockLevel: 0,
            ModleName: "",
            NarrativeColor: "#e58c27",
            IsSelectable: true,
            MaxHp: 65,
            InitialMana: new ManaGroup() { Red = 2, Green = 2 },
            InitialMoney: 65,
            InitialPower: 0,
            //temp
            UltimateSkillA: reimuConfig.UltimateSkillA,
            UltimateSkillB: reimuConfig.UltimateSkillB,
            ExhibitA: reimuConfig.ExhibitA,
            ExhibitB: reimuConfig.ExhibitB,
            DeckA: reimuConfig.DeckA,
            DeckB: reimuConfig.DeckB,
            DifficultyA: 2,
            DifficultyB: 3
            );
            return config;
        }


        [EntityLogic(typeof(AyaPlayerUnitDef))]
        public sealed class AyaPlayerUnit : PlayerUnit
        {
            protected override void OnEnterBattle(BattleController battle)
            {
                base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new Func<GameEventArgs, IEnumerable<BattleAction>>(this.OnBattleStarted));
            }
            private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs arg)
            {
                yield return new ApplyStatusEffectAction(typeof(AyaPassiveSeDef.AyaPassiveSe), this, null, null, null, null, 0f, true);
                yield break;
            }
        }

    }
    public sealed class AyaUnitModelDef : UnitModelTemplate
    {
        public override IdContainer GetId() => new AyaPlayerUnitDef().UniqueId;
        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(embeddedSource);
            gl.LocalizationFiles.mergeTerms = true;
            gl.LocalizationFiles.AddLocaleFile(Locale.En, "UnitModelsEn");
            return gl;
        }

        public override ModelOption LoadModelOptions()
        {
            return new ModelOption(ResourcesHelper.LoadSpineUnitAsync("Aya"));
        }
        public override UniTask<Sprite> LoadSpellSprite() => ResourceLoader.LoadSpriteAsync("Aya.png", AyaPlayerUnitDef.dir, ppu: 1200);
        public override UnitModelConfig MakeConfig()
        {
            var config = UnitModelConfig.FromName("Aya").Copy();
            config.Flip = false;
            return config;
        }
    }
}