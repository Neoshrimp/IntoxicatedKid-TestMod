using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.Core;
using LBoL.Core.Attributes;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Presentation;
using UnityEngine;

namespace test.AllyUnits
{
    [Localizable]
    public abstract class AllyUnit : Unit
    {
        public AllyUnitConfig Config { get; set; }
        protected override string LocalizeProperty(string key, bool decorated = false, bool required = true)
        {
            return TypeFactory<AllyUnit>.LocalizeProperty(Id, key, decorated, required);
        }
        protected virtual IReadOnlyList<string> LocalizeListProperty(string key, bool required = true)
        {
            return TypeFactory<AllyUnit>.LocalizeListProperty(Id, key, required);
        }
        public string ModelName
        {
            get
            {
                if (!Config.ModleName.IsNullOrEmpty())
                {
                    return Config.ModleName;
                }
                return Id;
            }
        }
        public override UnitName GetName()
        {
            return UnitNameTable.GetName(Id, Config.NarrativeColor);
        }

        // Token: 0x170001A9 RID: 425
        // (get) Token: 0x06000570 RID: 1392 RVA: 0x000112A6 File Offset: 0x0000F4A6
        public string Title
        {
            get
            {
                return LocalizeProperty("Title", false, true);
            }
        }

        // Token: 0x170001AA RID: 426
        // (get) Token: 0x06000571 RID: 1393 RVA: 0x000112B5 File Offset: 0x0000F4B5
        private IReadOnlyList<string> Moves
        {
            get
            {
                return LocalizeListProperty("Move", true);
            }
        }

        // Token: 0x170001AB RID: 427
        // (get) Token: 0x06000572 RID: 1394 RVA: 0x000112C3 File Offset: 0x0000F4C3
        public override GameEventPriority DefaultEventPriority
        {
            get
            {
                return (GameEventPriority)Config.Order;
            }
        }

        // Token: 0x170001AC RID: 428
        // (get) Token: 0x06000573 RID: 1395 RVA: 0x000112D0 File Offset: 0x0000F4D0
        // (set) Token: 0x06000574 RID: 1396 RVA: 0x000112D8 File Offset: 0x0000F4D8
        internal int Index { get; set; }

        // Token: 0x170001AD RID: 429
        // (get) Token: 0x06000575 RID: 1397 RVA: 0x000112E1 File Offset: 0x0000F4E1
        // (set) Token: 0x06000576 RID: 1398 RVA: 0x000112E9 File Offset: 0x0000F4E9
        public int RootIndex { get; internal set; }

        // Token: 0x170001AE RID: 430
        // (get) Token: 0x06000577 RID: 1399 RVA: 0x000112F2 File Offset: 0x0000F4F2
        public bool IsServant
        {
            get
            {
                return HasStatusEffect<Servant>();
            }
        }

        // Token: 0x170001AF RID: 431
        // (get) Token: 0x06000578 RID: 1400 RVA: 0x000112FA File Offset: 0x0000F4FA
        protected string Gun1
        {
            get
            {
                return GetRandomGun(Config.Gun1);
            }
        }

        // Token: 0x170001B0 RID: 432
        // (get) Token: 0x06000579 RID: 1401 RVA: 0x0001130C File Offset: 0x0000F50C
        protected string Gun2
        {
            get
            {
                return GetRandomGun(Config.Gun2);
            }
        }

        // Token: 0x170001B1 RID: 433
        // (get) Token: 0x0600057A RID: 1402 RVA: 0x0001131E File Offset: 0x0000F51E
        protected string Gun3
        {
            get
            {
                return GetRandomGun(Config.Gun3);
            }
        }

        // Token: 0x170001B2 RID: 434
        // (get) Token: 0x0600057B RID: 1403 RVA: 0x00011330 File Offset: 0x0000F530
        protected string Gun4
        {
            get
            {
                return GetRandomGun(Config.Gun4);
            }
        }

        // Token: 0x0600057C RID: 1404 RVA: 0x00011344 File Offset: 0x0000F544
        private static string GetRandomGun(IReadOnlyList<string> guns)
        {
            int count = guns.Count;
            if (count > 1)
            {
                return guns[UnityEngine.Random.Range(0, count)];
            }
            return guns.FirstOrDefault();
        }

        // Token: 0x170001B3 RID: 435
        // (get) Token: 0x0600057D RID: 1405 RVA: 0x00011370 File Offset: 0x0000F570
        protected int Damage1
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Damage1, Config.Damage1Hard, Config.Damage1Lunatic, "Damage1");
            }
        }

        // Token: 0x170001B4 RID: 436
        // (get) Token: 0x0600057E RID: 1406 RVA: 0x0001139E File Offset: 0x0000F59E
        protected int Damage2
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Damage2, Config.Damage2Hard, Config.Damage2Lunatic, "Damage2");
            }
        }

        // Token: 0x170001B5 RID: 437
        // (get) Token: 0x0600057F RID: 1407 RVA: 0x000113CC File Offset: 0x0000F5CC
        protected int Damage3
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Damage3, Config.Damage3Hard, Config.Damage3Lunatic, "Damage3");
            }
        }

        // Token: 0x170001B6 RID: 438
        // (get) Token: 0x06000580 RID: 1408 RVA: 0x000113FA File Offset: 0x0000F5FA
        protected int Damage4
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Damage4, Config.Damage4Hard, Config.Damage4Lunatic, "Damage4");
            }
        }

        // Token: 0x170001B7 RID: 439
        // (get) Token: 0x06000581 RID: 1409 RVA: 0x00011428 File Offset: 0x0000F628
        protected int Power
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Power, Config.PowerHard, Config.PowerLunatic, "Power");
            }
        }

        // Token: 0x170001B8 RID: 440
        // (get) Token: 0x06000582 RID: 1410 RVA: 0x00011456 File Offset: 0x0000F656
        protected int Defend
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Defend, Config.DefendHard, Config.DefendLunatic, "Defend");
            }
        }

        // Token: 0x170001B9 RID: 441
        // (get) Token: 0x06000583 RID: 1411 RVA: 0x00011484 File Offset: 0x0000F684
        protected int Count1
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Count1, Config.Count1Hard, Config.Count1Lunatic, "Count1");
            }
        }

        // Token: 0x170001BA RID: 442
        // (get) Token: 0x06000584 RID: 1412 RVA: 0x000114B2 File Offset: 0x0000F6B2
        protected int Count2
        {
            get
            {
                return GuardedGetAdjustedConfigValue(Config.Count2, Config.Count2Hard, Config.Count2Lunatic, "Count2");
            }
        }

        // Token: 0x170001BB RID: 443
        // (get) Token: 0x06000585 RID: 1413 RVA: 0x000114E0 File Offset: 0x0000F6E0
        // (set) Token: 0x06000586 RID: 1414 RVA: 0x000114E8 File Offset: 0x0000F6E8
        protected int CountDown { get; set; }

        // Token: 0x170001BC RID: 444
        // (get) Token: 0x06000587 RID: 1415 RVA: 0x000114F1 File Offset: 0x0000F6F1
        protected IEnumerable<AllyUnit> AllAliveEnemies
        {
            get
            {
                return (IEnumerable<AllyUnit>)Battle.AllAliveEnemies;
            }
        }

        // Token: 0x170001BD RID: 445
        // (get) Token: 0x06000588 RID: 1416 RVA: 0x000114FE File Offset: 0x0000F6FE
        protected RandomGen EnemyMoveRng
        {
            get
            {
                return Battle.GameRun.EnemyMoveRng;
            }
        }

        // Token: 0x170001BE RID: 446
        // (get) Token: 0x06000589 RID: 1417 RVA: 0x00011510 File Offset: 0x0000F710
        protected RandomGen EnemyBattleRng
        {
            get
            {
                return Battle.GameRun.EnemyBattleRng;
            }
        }

        // Token: 0x170001BF RID: 447
        // (get) Token: 0x0600058A RID: 1418 RVA: 0x00011522 File Offset: 0x0000F722
        // (set) Token: 0x0600058B RID: 1419 RVA: 0x0001152A File Offset: 0x0000F72A
        private protected GameDifficulty Difficulty { get; private set; }

        // Token: 0x170001C0 RID: 448
        // (get) Token: 0x0600058C RID: 1420 RVA: 0x00011533 File Offset: 0x0000F733
        public virtual int MovePriority
        {
            get
            {
                return 10;
            }
        }

        // Token: 0x170001C1 RID: 449
        // (get) Token: 0x0600058D RID: 1421 RVA: 0x00011537 File Offset: 0x0000F737
        // (set) Token: 0x0600058E RID: 1422 RVA: 0x0001153F File Offset: 0x0000F73F
        public List<Intention> Intentions { get; protected set; } = new List<Intention>();

        // Token: 0x0600058F RID: 1423 RVA: 0x00011548 File Offset: 0x0000F748
        private int? GetAdjustedConfigValue(int? easyAndNormal, int? hard, int? lunatic)
        {
            GameDifficulty difficulty = Difficulty;
            int? num;
            if (difficulty != GameDifficulty.Hard)
            {
                if (difficulty != GameDifficulty.Lunatic)
                {
                    num = easyAndNormal;
                }
                else
                {
                    int? num2 = lunatic;
                    int? num4;
                    if (num2 == null)
                    {
                        int? num3 = hard;
                        num4 = num3 != null ? num3 : easyAndNormal;
                    }
                    else
                    {
                        num4 = num2;
                    }
                    num = num4;
                }
            }
            else
            {
                int? num2 = hard;
                num = num2 != null ? num2 : easyAndNormal;
            }
            return num;
        }

        // Token: 0x06000590 RID: 1424 RVA: 0x0001159C File Offset: 0x0000F79C
        private int GuardedGetAdjustedConfigValue(int? easyAndNormal, int? hard, int? lunatic, [CallerMemberName] string memberName = "")
        {
            int? adjustedConfigValue = GetAdjustedConfigValue(easyAndNormal, hard, lunatic);
            if (adjustedConfigValue == null)
            {
                throw new InvalidDataException(DebugName + " has no '" + memberName + "' in config");
            }
            return adjustedConfigValue.GetValueOrDefault();
        }

        // Token: 0x06000591 RID: 1425 RVA: 0x000115E0 File Offset: 0x0000F7E0
        public string GetMove(int index)
        {
            IReadOnlyList<string> moves = Moves;
            string text = moves != null ? moves.TryGetValue(index) : null;
            if (text != null)
            {
                return text;
            }
            Debug.LogError(string.Format("Move {0} of {1} not found", index, DebugName));
            return string.Format("<Move {0}>", index);
        }

        // Token: 0x06000592 RID: 1426 RVA: 0x00011634 File Offset: 0x0000F834
        protected string GetSpellCardName(int? title, int name)
        {
            if (Moves == null)
            {
                return null;
            }
            if (title != null)
            {
                return Moves.TryGetValue(title.Value) + "「" + Moves.TryGetValue(name) + "」";
            }
            return "「" + Moves.TryGetValue(name) + "」";
        }

        // Token: 0x06000593 RID: 1427 RVA: 0x000116A0 File Offset: 0x0000F8A0
        public override void Initialize()
        {
            base.Initialize();
            Config = AllyUnitConfig.FromId(Id);
            if (Config == null)
            {
                throw new InvalidDataException("Cannot find enemy-unit config for " + Id);
            }
            SetMaxHp(Config.MaxHp, Config.MaxHp);
        }

        // Token: 0x06000594 RID: 1428 RVA: 0x000116FE File Offset: 0x0000F8FE
        protected void SetMaxHpInBattle(int hp, int maxHp)
        {
            SetMaxHp(hp, maxHp);
        }

        // Token: 0x06000595 RID: 1429 RVA: 0x00011708 File Offset: 0x0000F908
        internal void EnterGameRun(GameRunController gameRun)
        {
            Difficulty = gameRun.Difficulty;
            int num = GuardedGetAdjustedConfigValue(new int?(Config.MaxHp), Config.MaxHpHard, Config.MaxHpLunatic, "MaxHp");
            int? maxHpAdd = Config.MaxHpAdd;
            int num2;
            if (maxHpAdd != null)
            {
                int valueOrDefault = maxHpAdd.GetValueOrDefault();
                num2 = num + gameRun.EnemyBattleRng.NextInt(0, valueOrDefault);
            }
            else
            {
                num2 = num;
            }
            int num3 = num2;
            SetMaxHp(num3, num3);
            EnterGameRun2(gameRun);
        }
        internal virtual void EnterGameRun2(GameRunController gameRun)
        {
            if (GameRun != null)
            {
                throw new InvalidOperationException("Cannot enter game-run while already in game-run");
            }
            GameRun = gameRun;
            PlayerUnit playerUnit = GameMaster.Instance.CurrentGameRun.Player;
            if (playerUnit != null && gameRun.Puzzles.HasFlag(PuzzleFlag.LowPower))
            {
                playerUnit.Us.MaxPowerLevel = Math.Max(1, playerUnit.Us.MaxPowerLevel - 1);
            }
            OnEnterGameRun(gameRun);
        }

        // Token: 0x06000596 RID: 1430 RVA: 0x00011794 File Offset: 0x0000F994
        public virtual void OnSpawn(PlayerUnit spawner)
        {
        }

        // Token: 0x06000597 RID: 1431 RVA: 0x00011796 File Offset: 0x0000F996
        internal void Die()
        {
            Die2();
            Intentions.Clear();
            NotifyIntentionsChanged();
        }
        internal virtual void Die2()
        {
            OnDie();
            UnitStatus status = Status;
            if (status == UnitStatus.Alive || status == UnitStatus.Dead)
            {
                Debug.LogError(string.Format("Unit die when {0}", Status));
            }
            Status = UnitStatus.Dead;
        }

        // Token: 0x06000598 RID: 1432 RVA: 0x000117AF File Offset: 0x0000F9AF
        internal void Escape()
        {
            Intentions.Clear();
            NotifyIntentionsChanged();
        }

        // Token: 0x06000599 RID: 1433 RVA: 0x000117C2 File Offset: 0x0000F9C2
        internal IEnumerable<BattleAction> GetActions()
        {
            return Actions();
        }

        // Token: 0x0600059A RID: 1434 RVA: 0x000117CA File Offset: 0x0000F9CA
        protected virtual IEnumerable<BattleAction> Actions()
        {
            foreach (IEnemyMove enemyMove in _turnMoves.ToList())
            {
                if (IsAlive && enemyMove.Actions != null)
                {
                    Intention intention = enemyMove.Intention;
                    if (intention != null)
                    {
                        intention.NotifyActivating();
                    }
                    foreach (BattleAction battleAction in enemyMove.Actions)
                    {
                        yield return battleAction;
                    }
                }
            }
            UpdateMoveCounters();
            yield break;
        }

        // Token: 0x0600059B RID: 1435 RVA: 0x000117DA File Offset: 0x0000F9DA
        protected virtual IEnumerable<IEnemyMove> GetTurnMoves()
        {
            yield break;
        }

        // Token: 0x0600059C RID: 1436 RVA: 0x000117E3 File Offset: 0x0000F9E3
        protected virtual void UpdateMoveCounters()
        {
        }

        // Token: 0x0600059D RID: 1437 RVA: 0x000117E5 File Offset: 0x0000F9E5
        protected virtual IEnumerable<BattleAction> AttackActions(string move, string gunName, int damage, int times = 1, bool isAccuracy = false, bool multiGun = false)
        {
            List<DamageAction> damageActions = new List<DamageAction>();
            if (move != null)
            {
                yield return new AllyMoveAction(this, move, true);
            }
            if (times < 2)
            {
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                DamageAction damageAction = new DamageAction(this, Battle.AllAliveEnemies.Sample(GameRun.BattleRng), DamageInfo.Attack(damage, isAccuracy), gunName, GunType.Single);
                damageActions.Add(damageAction);
                yield return damageAction;
            }
            else
            {
                DamageAction damageAction2 = new DamageAction(this, Battle.AllAliveEnemies.Sample(GameRun.BattleRng), DamageInfo.Attack(damage, isAccuracy), gunName, GunType.First);
                damageActions.Add(damageAction2);
                yield return damageAction2;
                if (times > 2)
                {
                    int num;
                    for (int i = 0; i < times - 2; i = num + 1)
                    {
                        if (Battle.BattleShouldEnd)
                        {
                            yield break;
                        }
                        DamageAction damageAction3 = new DamageAction(this, Battle.AllAliveEnemies.Sample(GameRun.BattleRng), DamageInfo.Attack(damage, isAccuracy), multiGun ? gunName : "Instant", GunType.Middle);
                        damageActions.Add(damageAction3);
                        yield return damageAction3;
                        num = i;
                    }
                }
                if (Battle.BattleShouldEnd)
                {
                    yield break;
                }
                DamageAction damageAction4 = new DamageAction(this, Battle.AllAliveEnemies.Sample(GameRun.BattleRng), DamageInfo.Attack(damage, isAccuracy), multiGun ? gunName : "Instant", GunType.Last);
                damageActions.Add(damageAction4);
                yield return damageAction4;
            }
            if (!damageActions.Empty())
            {
                yield return new StatisticalTotalDamageAction(damageActions);
            }
            yield break;
        }
        public class AllyMoveAction : SimpleAction
        {
            // Token: 0x06000D0A RID: 3338 RVA: 0x000230C9 File Offset: 0x000212C9
            public AllyMoveAction(AllyUnit ally, string moveName, bool closeMoveName = true)
            {
                Ally = ally;
                MoveName = moveName;
                CloseMoveName = closeMoveName;
            }

            // Token: 0x1700045B RID: 1115
            // (get) Token: 0x06000D0B RID: 3339 RVA: 0x000230E6 File Offset: 0x000212E6
            public AllyUnit Ally { get; }

            // Token: 0x1700045C RID: 1116
            // (get) Token: 0x06000D0C RID: 3340 RVA: 0x000230EE File Offset: 0x000212EE
            public string MoveName { get; }

            // Token: 0x1700045D RID: 1117
            // (get) Token: 0x06000D0D RID: 3341 RVA: 0x000230F6 File Offset: 0x000212F6
            public bool CloseMoveName { get; }
        }

        // Token: 0x0600059E RID: 1438 RVA: 0x00011824 File Offset: 0x0000FA24
        protected IEnemyMove AttackMove(string move, [CanBeNull] string gunName, int damage, int times = 1, bool isAccuracy = false, bool multiGun = false, bool withSpell = false)
        {
            return new SimpleEnemyMove(withSpell ? times > 1 ? Intention.Attack(damage, times, isAccuracy).WithMoveName(move) : Intention.Attack(damage, isAccuracy).WithMoveName(move) : times > 1 ? Intention.Attack(damage, times, isAccuracy) : Intention.Attack(damage, isAccuracy), AttackActions(move, gunName, damage, times, isAccuracy, multiGun));
        }

        // Token: 0x0600059F RID: 1439 RVA: 0x0001188C File Offset: 0x0000FA8C
        protected IEnumerable<BattleAction> DefendActions(Unit target, [CanBeNull] string move, int block = 0, int shield = 0, int graze = 0, bool cast = true, PerformAction performAction = null)
        {
            if (move != null)
            {
                yield return new AllyMoveAction(this, move, true);
            }
            if (block > 0 || shield > 0)
            {
                yield return new CastBlockShieldAction(this, target ?? this, block, shield, BlockShieldType.Normal, cast);
            }
            if (performAction != null)
            {
                yield return performAction;
            }
            if (graze > 0)
            {
                yield return new ApplyStatusEffectAction<Graze>(target ?? this, new int?(graze), null, null, null, 0f, true);
            }
            yield break;
        }

        // Token: 0x060005A0 RID: 1440 RVA: 0x000118DC File Offset: 0x0000FADC
        protected IEnemyMove DefendMove(Unit target, [CanBeNull] string move, int block = 0, int shield = 0, int graze = 0, bool cast = true, PerformAction performAction = null)
        {
            if (block == 0 && shield == 0 && graze == 0)
            {
                throw new ArgumentException("Cannot create defend move with block = 0 AND shield = 0 AND graze = 0");
            }
            return new SimpleEnemyMove(block > 0 || shield > 0 ? Intention.Defend() : Intention.Graze(), DefendActions(target, move, block, shield, graze, cast, performAction));
        }

        // Token: 0x060005A1 RID: 1441 RVA: 0x0001192A File Offset: 0x0000FB2A
        protected IEnumerable<BattleAction> AddCardActions([CanBeNull] string move, IEnumerable<Card> cards, AddCardZone zone = AddCardZone.Discard, PerformAction perform = null)
        {
            if (move != null)
            {
                yield return new AllyMoveAction(this, move, true);
            }
            if (perform != null)
            {
                yield return perform;
            }
            BattleAction battleAction;
            switch (zone)
            {
                case AddCardZone.Discard:
                    battleAction = new AddCardsToDiscardAction(cards);
                    break;
                case AddCardZone.Draw:
                    battleAction = new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);
                    break;
                case AddCardZone.Hand:
                    battleAction = new AddCardsToHandAction(cards);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("zone", zone, null);
            }
            yield return battleAction;
            yield break;
        }

        // Token: 0x060005A2 RID: 1442 RVA: 0x00011957 File Offset: 0x0000FB57
        protected IEnemyMove AddCardMove([CanBeNull] string move, IEnumerable<Card> cards, AddCardZone zone = AddCardZone.Discard, PerformAction perform = null, bool withSpell = false)
        {
            if (!withSpell)
            {
                return new SimpleEnemyMove(Intention.AddCard(), AddCardActions(move, cards, zone, perform));
            }
            return new SimpleEnemyMove(Intention.AddCard().WithMoveName(move), AddCardActions(move, cards, zone, perform));
        }

        // Token: 0x060005A3 RID: 1443 RVA: 0x00011990 File Offset: 0x0000FB90
        protected IEnemyMove AddCardMove([CanBeNull] string move, Type cardType, int amount = 1, AddCardZone zone = AddCardZone.Discard, PerformAction perform = null, bool withSpell = false)
        {
            List<Card> list = new List<Card>();
            if (amount > 1)
            {
                for (int i = 0; i < amount; i++)
                {
                    list.Add(Library.CreateCard(cardType));
                }
            }
            else
            {
                list.Add(Library.CreateCard(cardType));
            }
            if (!withSpell)
            {
                return new SimpleEnemyMove(Intention.AddCard(), AddCardActions(move, list, zone, perform));
            }
            return new SimpleEnemyMove(Intention.AddCard().WithMoveName(move), AddCardActions(move, list, zone, perform));
        }

        // Token: 0x060005A4 RID: 1444 RVA: 0x00011A04 File Offset: 0x0000FC04
        protected IEnumerable<BattleAction> PositiveActions([CanBeNull] string move, Type type, int? level = null, int? duration = null, float occupationTime = 0f, PerformAction performAction = null)
        {
            if (move != null)
            {
                yield return new AllyMoveAction(this, move, true);
            }
            if (performAction != null)
            {
                yield return performAction;
            }
            yield return new ApplyStatusEffectAction(type, this, level, duration, null, null, occupationTime, true);
            yield break;
        }

        // Token: 0x060005A5 RID: 1445 RVA: 0x00011A41 File Offset: 0x0000FC41
        protected IEnemyMove PositiveMove([CanBeNull] string move, Type type, int? level = null, int? duration = null, bool withSpell = false, PerformAction performAction = null)
        {
            return new SimpleEnemyMove(withSpell ? Intention.PositiveEffect().WithMoveName(move) : Intention.PositiveEffect(), PositiveActions(move, type, level, duration, 0f, performAction));
        }

        // Token: 0x060005A6 RID: 1446 RVA: 0x00011A70 File Offset: 0x0000FC70
        protected IEnumerable<BattleAction> NegativeActions([CanBeNull] string move, Type type, int? level = null, int? duration = null, bool startAutoDecreasing = true, PerformAction performAction = null)
        {
            if (move != null)
            {
                yield return new AllyMoveAction(this, move, true);
            }
            if (performAction != null)
            {
                yield return performAction;
            }
            yield return new ApplyStatusEffectAction(type, Battle.AllAliveEnemies.Sample(GameRun.BattleRng), level, duration, null, null, 0f, startAutoDecreasing);
            yield break;
        }

        // Token: 0x060005A7 RID: 1447 RVA: 0x00011AAD File Offset: 0x0000FCAD
        protected IEnemyMove NegativeMove([CanBeNull] string move, Type type, int? level = null, int? duration = null, bool startAutoDecreasing = true, bool withSpell = false, PerformAction performAction = null)
        {
            return new SimpleEnemyMove(withSpell ? Intention.NegativeEffect(null).WithMoveName(move) : Intention.NegativeEffect(null), NegativeActions(move, type, level, duration, startAutoDecreasing, performAction));
        }

        // Token: 0x060005A8 RID: 1448 RVA: 0x00011ADB File Offset: 0x0000FCDB
        protected IEnumerable<BattleAction> PerformActions([CanBeNull] string move, PerformAction performAction)
        {
            if (move != null)
            {
                yield return new AllyMoveAction(this, move, true);
            }
            yield return performAction;
            yield break;
        }

        // Token: 0x060005A9 RID: 1449 RVA: 0x00011AFC File Offset: 0x0000FCFC
        public void UpdateTurnMoves()
        {
            if (IsInTurn)
            {
                Debug.LogError("[AllyUnit: " + DebugName + "] UpdateTurnMoves() is invoked in turn, behaviour maybe undefined");
            }
            _turnMoves.Clear();
            foreach (IEnemyMove enemyMove in GetTurnMoves())
            {
                _turnMoves.Add(enemyMove);
            }
            //Intentions = (from i in GetIntentions()
            //              select i.SetSource(this)).ToList<Intention>();
            NotifyIntentionsChanged();
        }
        /*public AllyUnit Source { get; private set; }
        internal Intention SetSourceAlly(AllyUnit unit)
        {
            Source = unit;
            return this;
        }*/

        // Token: 0x060005AA RID: 1450 RVA: 0x00011BA4 File Offset: 0x0000FDA4
        public void ClearIntentions()
        {
            Intentions.Clear();
            NotifyIntentionsChanged();
        }

        // Token: 0x060005AB RID: 1451 RVA: 0x00011BB7 File Offset: 0x0000FDB7
        protected virtual IEnumerable<Intention> GetIntentions()
        {
            foreach (IEnemyMove enemyMove in _turnMoves)
            {
                yield return enemyMove.Intention;
            }
            yield break;
        }

        // Token: 0x1400000A RID: 10
        // (add) Token: 0x060005AC RID: 1452 RVA: 0x00011BC8 File Offset: 0x0000FDC8
        // (remove) Token: 0x060005AD RID: 1453 RVA: 0x00011C00 File Offset: 0x0000FE00
        public event Action<AllyUnit> IntentionsChanged;

        // Token: 0x060005AE RID: 1454 RVA: 0x00011C35 File Offset: 0x0000FE35
        protected void NotifyIntentionsChanged()
        {
            Action<AllyUnit> intentionsChanged = IntentionsChanged;
            if (intentionsChanged == null)
            {
                return;
            }
            intentionsChanged(this);
        }

        // Token: 0x060005AF RID: 1455 RVA: 0x00011C48 File Offset: 0x0000FE48
        public override void SetView(IUnitView view)
        {
            base.SetView(view);
            IAllyUnitView AllyUnitView = view as IAllyUnitView;
            if (AllyUnitView != null)
            {
                View = AllyUnitView;
            }
        }

        // Token: 0x170001C2 RID: 450
        // (get) Token: 0x060005B0 RID: 1456 RVA: 0x00011C6D File Offset: 0x0000FE6D
        // (set) Token: 0x060005B1 RID: 1457 RVA: 0x00011C75 File Offset: 0x0000FE75
        public new IAllyUnitView View { get; private set; }
        public interface IAllyUnitView : IUnitView
        {
        }

        // Token: 0x170001C3 RID: 451
        // (get) Token: 0x060005B2 RID: 1458 RVA: 0x00011C7E File Offset: 0x0000FE7E
        public GameEvent<DieEventArgs> EnemyPointGenerating { get; } = new GameEvent<DieEventArgs>();

        // Token: 0x060005B3 RID: 1459 RVA: 0x00011C86 File Offset: 0x0000FE86
        protected AllyUnit()
        {
        }

        // Token: 0x040002AA RID: 682
        private readonly List<IEnemyMove> _turnMoves = new List<IEnemyMove>();

        // Token: 0x020001FF RID: 511
        public enum AddCardZone
        {
            // Token: 0x040007B3 RID: 1971
            Discard,
            // Token: 0x040007B4 RID: 1972
            Draw,
            // Token: 0x040007B5 RID: 1973
            Hand
        }
    }
}
