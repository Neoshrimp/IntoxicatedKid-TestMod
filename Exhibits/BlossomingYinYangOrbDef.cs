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
using LBoL.EntityLib.Exhibits;
using JetBrains.Annotations;
using LBoL.EntityLib.Exhibits.Shining;
using LBoL.EntityLib.StatusEffects.Neutral.TwoColor;
using LBoL.Core.Stations;
using LBoL.EntityLib.Exhibits.Common;

namespace test.Exhibits
{
    public sealed class BlossomingYinYangOrbDef : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BlossomingYinYangOrb);
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
                Owner: "Reimu",
                LosableType: ExhibitLosableType.CantLose,
                Rarity: Rarity.Shining,
                Value1: 1,
                Value2: 3,
                Value3: null,
                Mana: new ManaGroup() { Red = 2 },
                BaseManaRequirement: null,
                BaseManaColor: null,
                BaseManaAmount: 0,
                HasCounter: false,
                InitialCounter: null,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { "Firepower" },
                RelativeCards: new List<string>() { "YinyangCard" }
            );
            return exhibitConfig;
        }
        [EntityLogic(typeof(BlossomingYinYangOrbDef))]
        [UsedImplicitly]
        [ExhibitInfo(WeighterType = typeof(BlossomingYinYangOrbWeighter))]
        public sealed class BlossomingYinYangOrb : ShiningExhibit
        {
            protected override void OnAdded(PlayerUnit player)
            {
                if (GameRun.Player.HasExhibit<ReimuR>())
                {
                    GameRun.LoseExhibit(GameRun.Player.GetExhibit<ReimuR>(), false, true);
                }
                player.GameRun.GainBaseMana(Mana, false);
            }
            protected override void OnRemoved(PlayerUnit player)
            {
                if (!GameRun.Player.HasExhibit<ReimuR>())
                {
                    GameRun.GainExhibitRunner(GameRun.Player.GetExhibit<ReimuR>(), false, null);
                }
                player.GameRun.TryLoseBaseMana(Mana, false);
            }
            protected override void OnEnterBattle()
            {
                Active = true;
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStarted));
                ReactBattleEvent(Battle.EnemyDied, new EventSequencedReactor<DieEventArgs>(OnEnemyDied));
                //base.HandleBattleEvent<DamageEventArgs>(base.Battle.Player.DamageTaking, new GameEventHandler<DamageEventArgs>(this.OnPlayerDamageTaking));
                ReactBattleEvent(Battle.Player.DamageReceived, new EventSequencedReactor<DamageEventArgs>(OnPlayerDamageReceived));
            }
            private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
            {
                NotifyActivating();
                yield return new ApplyStatusEffectAction<Firepower>(Owner, new int?(Value1), null, null, null, 0.1f, true);
                //yield return new ApplyStatusEffectAction<Spirit>(base.Owner, new int?(base.Value1), null, null, null, 0.1f, true);
                yield return new HealAction(Owner, Owner, Value2, HealType.Base, 0.1f);
                yield break;
            }
            private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs arg)
            {
                NotifyActivating();
                if (!Battle.BattleShouldEnd)
                {
                    yield return new ApplyStatusEffectAction<Firepower>(Owner, new int?(Value1), null, null, null, 0.1f, true);
                    //yield return new ApplyStatusEffectAction<Spirit>(base.Owner, new int?(base.Value1), null, null, null, 0.1f, true);
                }
                yield return new HealAction(Owner, Owner, Value2, HealType.Base, 0.1f);
                yield break;
            }
            /*private void OnPlayerDamageTaking(DamageEventArgs args)
            {
                DamageInfo damageInfo = args.DamageInfo;
                int num = damageInfo.Damage.RoundToInt();
                if (num >= 1 && this.Active)
                {
                    base.NotifyActivating();
                    args.DamageInfo = damageInfo.ReduceActualDamageBy(num);
                    args.AddModifier(this);
                }
            }*/
            private IEnumerable<BattleAction> OnPlayerDamageReceived(DamageEventArgs args)
            {
                Unit source = args.Source;
                if (source is EnemyUnit && source.IsAlive && Active)
                {
                    NotifyActivating();
                    Active = false;
                    Card card = Library.CreateCard<YinyangCard>();
                    yield return new AddCardsToHandAction(card);
                    EnemyUnit attacker = (EnemyUnit)args.Source;
                    UnitSelector unitSelector = new UnitSelector(attacker);
                    yield return new UseCardAction(card, unitSelector, new ManaGroup() { Any = 0 });
                }
                yield break;
            }
            protected override void OnLeaveBattle()
            {
                Active = false;
            }
            private class BlossomingYinYangOrbWeighter : IExhibitWeighter
            {
                public float WeightFor(Type type, GameRunController gameRun)
                {
                    return gameRun.Player.HasExhibit<ReimuR>() ? 1 : 0;
                }
            }
        }
    }
}