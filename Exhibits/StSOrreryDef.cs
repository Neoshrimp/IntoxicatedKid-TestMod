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
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Cirno.Friend;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.MultiColor;
using LBoL.Presentation.UI.Panels;
using UnityEngine.InputSystem.Controls;
using JetBrains.Annotations;
using LBoL.Core.Stations;
using LBoL.Presentation.UI;
using static System.Collections.Specialized.BitVector32;
using static test.BlossomingYinYangOrbDef;
using LBoL.EntityLib.Exhibits.Shining;

namespace test
{
    public sealed class StSOrreryDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(StSOrrery);
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
                Value1: 5,
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
        [EntityLogic(typeof(StSOrreryDef))]
        [UsedImplicitly]
        [ExhibitInfo(WeighterType = typeof(StSOrrery.StSOrreryWeighter))]
        public sealed class StSOrrery : Exhibit
        {
            public class OrreryRewardInteraction : Interaction
            {
                public IReadOnlyList<StationReward> PendingRewards { get; }
                public OrreryRewardInteraction(IEnumerable<StationReward> rewards)
                {
                    this.PendingRewards = new List<StationReward>(rewards).AsReadOnly();
                }
            }
            protected override IEnumerator SpecialGain(PlayerUnit player)
            {
                base.OnGain(player);
                List<StationReward> rewards = new List<StationReward>();
                for (int i = 0; i < base.Value1; i++)
                {
                    rewards.Add(GameRun.CurrentStage.GetDrinkTeaCardReward());
                }
                OrreryRewardInteraction rewardInteraction = new OrreryRewardInteraction(rewards)
                {
                    CanCancel = false,
                    Source = this
                };
                yield return base.GameRun.InteractionViewer.View(rewardInteraction);
                yield break;
            }
            private StationReward GetOrreryReward()
            {
                return StationReward.CreateCards(base.GameRun.GetRewardCards(GameRun.CurrentStage.EnemyCardOnlyPlayerWeight, GameRun.CurrentStage.EnemyCardWithFriendWeight, GameRun.CurrentStage.EnemyCardNeutralWeight, GameRun.CurrentStage.DrinkTeaAdditionalCardWeight, base.GameRun.RewardCardCount, false));
            }
            private class StSOrreryWeighter : IExhibitWeighter
            {
                public float WeightFor(Type type, GameRunController gameRun)
                {
                    return (float)(99);
                }
            }
        }
    }
}