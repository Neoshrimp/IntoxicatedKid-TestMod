using System;
using System.Collections.Generic;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Other.Adventure;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.EntityLib.StatusEffects.Enemy;

namespace test.AllyUnits
{
    // Token: 0x020001C2 RID: 450
    public abstract class DroneAlly : AllyUnit
    {
        // Token: 0x060006B1 RID: 1713 RVA: 0x0000F744 File Offset: 0x0000D944
        protected override void OnEnterBattle(BattleController battle)
        {
            EnterBattle();
            ReactBattleEvent(Battle.BattleStarted, new Func<GameEventArgs, IEnumerable<BattleAction>>(OnBattleStarted));
            ReactBattleEvent(DamageReceived, new Func<DamageEventArgs, IEnumerable<BattleAction>>(OnDamageReceived));
        }

        // Token: 0x060006B2 RID: 1714 RVA: 0x0000F781 File Offset: 0x0000D981
        protected virtual void EnterBattle()
        {
        }

        // Token: 0x060006B3 RID: 1715 RVA: 0x0000F783 File Offset: 0x0000D983
        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs arg)
        {
            yield return new ApplyStatusEffectAction<Amulet>(this, new int?(1), null, null, null, 0f, true);
            yield break;
        }

        // Token: 0x060006B4 RID: 1716 RVA: 0x0000F794 File Offset: 0x0000D994
        public override void OnSpawn(PlayerUnit spawner)
        {
            React(new ApplyStatusEffectAction<Amulet>(this, new int?(1), null, null, null, 0f, true));
        }

        // Token: 0x060006B5 RID: 1717 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
        private IEnumerable<BattleAction> OnDamageReceived(DamageEventArgs arg)
        {
            if (arg.Cause == ActionCause.Card && arg.ActionSource is EmpCard)
            {
                int? num = new int?(2);
                yield return new ApplyStatusEffectAction<Emi>(this, null, num, null, null, 0f, true);
                Stun();
            }
            yield break;
        }

        // Token: 0x060006B6 RID: 1718 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
        protected virtual void Stun()
        {
        }
    }
}
