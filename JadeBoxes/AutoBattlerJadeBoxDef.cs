using HarmonyLib;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Stations;
using LBoL.Core.Units;
using LBoL.EntityLib.Adventures;
using LBoL.Presentation;
using LBoL.Presentation.Effect;
using LBoL.Presentation.UI.Widgets;
using LBoL.Presentation.Units;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace test.JadeBoxes
{
    public sealed class AutoBattlerJadeBoxDef : JadeBoxTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AutoBattlerJadeBox);
        }

        public override LocalizationOption LoadLocalization()
        {
            return new DirectLocalization(new Dictionary<string, object>() {
                { "Name", "Auto Battler" },
                { "Description", "Combats become (semi)automatic."}
            });
        }
        public override JadeBoxConfig MakeConfig()
        {
            var config = DefaultConfig();
            return config;
        }
    }
    [EntityLogic(typeof(AutoBattlerJadeBoxDef))]
    public sealed class AutoBattlerJadeBox : JadeBox
    {
        protected override void OnGain(GameRunController gameRun)
        {
            GameMaster.Instance.StartCoroutine(GainExhibits(gameRun));
        }
        private IEnumerator GainExhibits(GameRunController gameRun)
        {
            var ABexhibit = new HashSet<Type> { typeof(TASBotDef.TASBot) };
            foreach (var exhibit in ABexhibit)
            {
                yield return gameRun.GainExhibitRunner(Library.CreateExhibit(exhibit));
            }
            gameRun.ExhibitPool.RemoveAll(e => ABexhibit.Contains(e));
        }
    }
}
