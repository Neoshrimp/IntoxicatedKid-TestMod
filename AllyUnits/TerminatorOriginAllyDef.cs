using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.EntityLib.StatusEffects.Enemy;

namespace test.AllyUnits
{
    // Token: 0x020001CB RID: 459
    [UsedImplicitly]
    public abstract class TerminatorOriginAlly : DroneAlly
    {
        public override void Initialize()
        {
            base.Initialize();
            var Config = EnemyUnitConfig.FromId("TerminatorElite");
            if (Config == null)
            {
                throw new InvalidDataException("Cannot find enemy-unit config for " + Id);
            }
            SetMaxHp(Config.MaxHp, Config.MaxHp);
        }

        protected override void EnterBattle()
        {
            Next = MoveType.ShootAccuracy;
            CountDown = 4;
            ReactBattleEvent(Battle.BattleStarted, new Func<GameEventArgs, IEnumerable<BattleAction>>(OnBattleStarted));
        }

        // Token: 0x060006D3 RID: 1747 RVA: 0x0000FA70 File Offset: 0x0000DC70
        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs arg)
        {
            yield return new ApplyStatusEffectAction<DroneBlock>(this, new int?(Defend), null, null, null, 0f, true);
            yield break;
        }

        // Token: 0x060006D4 RID: 1748 RVA: 0x0000FA80 File Offset: 0x0000DC80
        public override void OnSpawn(PlayerUnit spawner)
        {
            React(new ApplyStatusEffectAction<Amulet>(this, new int?(1), null, null, null, 0f, true));
            React(new ApplyStatusEffectAction<DroneBlock>(this, new int?(Defend), null, null, null, 0f, true));
        }

        // Token: 0x060006D5 RID: 1749 RVA: 0x0000FB02 File Offset: 0x0000DD02
        protected override void Stun()
        {
            Next = MoveType.Stun;
            UpdateTurnMoves();
        }

        // Token: 0x060006D6 RID: 1750 RVA: 0x0000FB11 File Offset: 0x0000DD11
        private IEnumerable<BattleAction> RepairActions()
        {
            CountDown = 4;
            yield return PerformAction.Chat(this, "Chat.Terminator0".Localize(true), 1.3f, 0.2f, 0f, true);
            yield return PerformAction.Animation(this, "shoot3", 1.5f, null, 0f, -1);
            int num = MaxHp - Hp;
            if (num == 0)
            {
                yield return PerformAction.Chat(this, "Chat.Terminator1".Localize(true), 2.5f, 0.2f, 0f, true);
                yield return new ApplyStatusEffectAction<Firepower>(this, new int?(2), null, null, null, 1f, true);
            }
            else if (num < Count1 / 2)
            {
                yield return PerformAction.Chat(this, "Chat.Terminator2".Localize(true), 2.5f, 0.2f, 0f, true);
                yield return new HealAction(this, this, Count1 / 2, HealType.Base, 1f);
                yield return new ApplyStatusEffectAction<Firepower>(this, new int?(1), null, null, null, 0f, true);
            }
            else
            {
                yield return PerformAction.Chat(this, "Chat.Terminator3".Localize(true), 2.5f, 0.2f, 0f, true);
                yield return new HealAction(this, this, Count1, HealType.Base, 1f);
            }
            yield break;
        }

        // Token: 0x060006D7 RID: 1751 RVA: 0x0000FB21 File Offset: 0x0000DD21
        protected override IEnumerable<IEnemyMove> GetTurnMoves()
        {
            IEnemyMove enemyMove;
            switch (Next)
            {
                case MoveType.TripleShoot:
                    enemyMove = AttackMove(GetMove(1), Gun2, Damage1, 3, false, false, false);
                    break;
                case MoveType.ShootAccuracy:
                    enemyMove = AttackMove(GetMove(0), Gun1, Damage2, 1, true, false, false);
                    break;
                case MoveType.Repair:
                    enemyMove = new SimpleEnemyMove(Intention.Repair(), RepairActions());
                    break;
                case MoveType.Stun:
                    enemyMove = new SimpleEnemyMove(Intention.Stun(), PerformActions(GetMove(3), PerformAction.Animation(this, "defend", 0.5f, null, 0f, -1)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            yield return enemyMove;
            yield break;
        }

        // Token: 0x060006D8 RID: 1752 RVA: 0x0000FB34 File Offset: 0x0000DD34
        protected override void UpdateMoveCounters()
        {
            if (HasStatusEffect<Emi>())
            {
                Next = MoveType.Stun;
                return;
            }
            int num = CountDown - 1;
            CountDown = num;
            Last = Next;
            if (CountDown <= 0)
            {
                Next = MoveType.Repair;
                return;
            }
            Next = _pool.Without(Last).Sample(EnemyMoveRng);
        }

        // Token: 0x170000A0 RID: 160
        // (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
        // (set) Token: 0x060006DA RID: 1754 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
        private MoveType Last { get; set; }

        // Token: 0x170000A1 RID: 161
        // (get) Token: 0x060006DB RID: 1755 RVA: 0x0000FBB1 File Offset: 0x0000DDB1
        // (set) Token: 0x060006DC RID: 1756 RVA: 0x0000FBB9 File Offset: 0x0000DDB9
        private MoveType Next { get; set; }

        // Token: 0x0400007F RID: 127
        private const int RepairInterval = 4;

        // Token: 0x04000080 RID: 128
        private const float ResultChatTime = 2.5f;

        // Token: 0x04000083 RID: 131
        private readonly RepeatableRandomPool<MoveType> _pool = new RepeatableRandomPool<MoveType>
        {
            {
                MoveType.TripleShoot,
                1f
            },
            {
                MoveType.ShootAccuracy,
                1f
            }
        };

        // Token: 0x02000618 RID: 1560
        private enum MoveType
        {
            // Token: 0x0400088B RID: 2187
            TripleShoot,
            // Token: 0x0400088C RID: 2188
            ShootAccuracy,
            // Token: 0x0400088D RID: 2189
            Repair,
            // Token: 0x0400088E RID: 2190
            Stun
        }
    }
}
