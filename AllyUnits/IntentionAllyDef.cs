/*using System;
using JetBrains.Annotations;
using LBoL.Base.Extensions;
using LBoL.Core;
using LBoL.Core.Attributes;
using LBoL.Core.Intentions;
using LBoL.Core.Units;

namespace test.AllyUnits
{
    // Token: 0x02000079 RID: 121
    [Localizable]
    public abstract class IntentionAlly : GameEntity
    {
        // Token: 0x170001C7 RID: 455
        // (get) Token: 0x060005BB RID: 1467
        public abstract IntentionType Type { get; }

        // Token: 0x060005BC RID: 1468 RVA: 0x00011CFB File Offset: 0x0000FEFB
        protected override string LocalizeProperty(string key, bool decorated = false, bool required = true)
        {
            return TypeFactory<IntentionAlly>.LocalizeProperty(GetType().Name, key, decorated, required);
        }

        // Token: 0x170001C8 RID: 456
        // (get) Token: 0x060005BD RID: 1469 RVA: 0x00011D10 File Offset: 0x0000FF10
        public override GameEventPriority DefaultEventPriority
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        // Token: 0x170001C9 RID: 457
        // (get) Token: 0x060005BE RID: 1470 RVA: 0x00011D17 File Offset: 0x0000FF17
        // (set) Token: 0x060005BF RID: 1471 RVA: 0x00011D1F File Offset: 0x0000FF1F
        public EnemyUnit Source { get; private set; }

        // Token: 0x060005C0 RID: 1472 RVA: 0x00011D28 File Offset: 0x0000FF28
        internal IntentionAlly SetSource(EnemyUnit unit)
        {
            Source = unit;
            return this;
        }

        // Token: 0x060005C1 RID: 1473 RVA: 0x00011D32 File Offset: 0x0000FF32
        internal GameEntityFormatWrapper CreateFormatWrapper()
        {
            return new IntentionAllyFormatWrapper(this);
        }

        // Token: 0x170001CA RID: 458
        // (get) Token: 0x060005C2 RID: 1474 RVA: 0x00011D3A File Offset: 0x0000FF3A
        [UsedImplicitly]
        public override UnitName PlayerName
        {
            get
            {
                return Source.GameRun.Player.GetName();
            }
        }

        // Token: 0x170001CB RID: 459
        // (get) Token: 0x060005C3 RID: 1475 RVA: 0x00011D51 File Offset: 0x0000FF51
        [UsedImplicitly]
        public UnitName OwnerName
        {
            get
            {
                return Source.GetName();
            }
        }

        // Token: 0x170001CC RID: 460
        // (get) Token: 0x060005C4 RID: 1476 RVA: 0x00011D5E File Offset: 0x0000FF5E
        // (set) Token: 0x060005C5 RID: 1477 RVA: 0x00011D66 File Offset: 0x0000FF66
        public string MoveName { get; private set; }

        // Token: 0x060005C6 RID: 1478 RVA: 0x00011D6F File Offset: 0x0000FF6F
        public IntentionAlly WithMoveName(string moveName)
        {
            MoveName = moveName;
            return this;
        }

        // Token: 0x060005C7 RID: 1479 RVA: 0x00011D7C File Offset: 0x0000FF7C
        protected int CalculateDamage(DamageInfo damage)
        {
            EnemyUnit source = Source;
            return source.Battle.CalculateDamage(source, source, source.Battle.Player, damage);
        }

        // Token: 0x1400000B RID: 11
        // (add) Token: 0x060005C8 RID: 1480 RVA: 0x00011DAC File Offset: 0x0000FFAC
        // (remove) Token: 0x060005C9 RID: 1481 RVA: 0x00011DE4 File Offset: 0x0000FFE4
        public event Action Activating;

        // Token: 0x060005CA RID: 1482 RVA: 0x00011E19 File Offset: 0x00010019
        public void NotifyActivating()
        {
            Action activating = Activating;
            if (activating == null)
            {
                return;
            }
            activating();
        }

        // Token: 0x060005CB RID: 1483 RVA: 0x00011E2B File Offset: 0x0001002B
        public static IntentionAlly Attack(int damage, int times, bool isAccuracy = false)
        {
            AttackIntentionAllyDef attackIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<AttackIntentionAllyDef>();
            attackIntentionAlly.Damage = DamageInfo.Attack(damage, isAccuracy);
            attackIntentionAlly.Times = new int?(times);
            attackIntentionAlly.IsAccuracy = isAccuracy;
            return attackIntentionAlly;
        }

        // Token: 0x060005CC RID: 1484 RVA: 0x00011E53 File Offset: 0x00010053
        public static IntentionAlly Attack(int damage, bool isAccuracy = false)
        {
            AttackIntentionAlly attackIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<AttackIntentionAlly>();
            attackIntentionAlly.Damage = DamageInfo.Attack(damage, isAccuracy);
            attackIntentionAlly.IsAccuracy = isAccuracy;
            return attackIntentionAlly;
        }

        // Token: 0x060005CD RID: 1485 RVA: 0x00011E6F File Offset: 0x0001006F
        public static IntentionAlly Defend()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<DefendIntentionAlly>();
        }

        // Token: 0x060005CE RID: 1486 RVA: 0x00011E76 File Offset: 0x00010076
        public static IntentionAlly Graze()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<GrazeIntentionAlly>();
        }

        // Token: 0x060005CF RID: 1487 RVA: 0x00011E7D File Offset: 0x0001007D
        public static IntentionAlly PositiveEffect()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<PositiveEffectIntentionAlly>();
        }

        // Token: 0x060005D0 RID: 1488 RVA: 0x00011E84 File Offset: 0x00010084
        public static IntentionAlly NegativeEffect(string specialIconName = null)
        {
            NegativeEffectIntentionAlly negativeEffectIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<NegativeEffectIntentionAlly>();
            if (!specialIconName.IsNullOrEmpty())
            {
                negativeEffectIntentionAlly.SpecialIconName = specialIconName;
            }
            return negativeEffectIntentionAlly;
        }

        // Token: 0x060005D1 RID: 1489 RVA: 0x00011EA7 File Offset: 0x000100A7
        public static IntentionAlly Spawn()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<SpawnIntentionAlly>();
        }

        // Token: 0x060005D2 RID: 1490 RVA: 0x00011EAE File Offset: 0x000100AE
        public static IntentionAlly Sleep()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<SleepIntentionAlly>();
        }

        // Token: 0x060005D3 RID: 1491 RVA: 0x00011EB5 File Offset: 0x000100B5
        public static IntentionAlly Stun()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<StunIntentionAlly>();
        }

        // Token: 0x060005D4 RID: 1492 RVA: 0x00011EBC File Offset: 0x000100BC
        public static IntentionAlly Escape()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<EscapeIntentionAlly>();
        }

        // Token: 0x060005D5 RID: 1493 RVA: 0x00011EC3 File Offset: 0x000100C3
        public static IntentionAlly Explode(int damage)
        {
            ExplodeIntentionAlly explodeIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<ExplodeIntentionAlly>();
            explodeIntentionAlly.Damage = DamageInfo.Attack(damage, false);
            return explodeIntentionAlly;
        }

        // Token: 0x060005D6 RID: 1494 RVA: 0x00011ED8 File Offset: 0x000100D8
        public static IntentionAlly ExplodeAlly()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<ExplodeAllyIntentionAlly>();
        }

        // Token: 0x060005D7 RID: 1495 RVA: 0x00011EDF File Offset: 0x000100DF
        public static IntentionAlly Charge()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<ChargeIntentionAlly>();
        }

        // Token: 0x060005D8 RID: 1496 RVA: 0x00011EE6 File Offset: 0x000100E6
        public static IntentionAlly AddCard()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<AddCardIntentionAlly>();
        }

        // Token: 0x060005D9 RID: 1497 RVA: 0x00011EED File Offset: 0x000100ED
        public static IntentionAlly Heal()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<HealIntentionAlly>();
        }

        // Token: 0x060005DA RID: 1498 RVA: 0x00011EF4 File Offset: 0x000100F4
        public static IntentionAlly Repair()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<RepairIntentionAlly>();
        }

        // Token: 0x060005DB RID: 1499 RVA: 0x00011EFB File Offset: 0x000100FB
        public static IntentionAlly CountDown(int counter)
        {
            CountDownIntentionAlly countDownIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<CountDownIntentionAlly>();
            countDownIntentionAlly.Counter = counter;
            return countDownIntentionAlly;
        }

        // Token: 0x060005DC RID: 1500 RVA: 0x00011F0C File Offset: 0x0001010C
        public static IntentionAlly SpellCard(string name, int? damage, bool isAccuracy)
        {
            SpellCardIntentionAlly spellCardIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<SpellCardIntentionAlly>();
            spellCardIntentionAlly.MoveName = name;
            DamageInfo? damageInfo;
            if (damage != null)
            {
                int valueOrDefault = damage.GetValueOrDefault();
                damageInfo = new DamageInfo?(DamageInfo.Attack(valueOrDefault, isAccuracy));
            }
            else
            {
                damageInfo = null;
            }
            spellCardIntentionAlly.Damage = damageInfo;
            spellCardIntentionAlly.IsAccuracy = isAccuracy;
            return spellCardIntentionAlly;
        }

        // Token: 0x060005DD RID: 1501 RVA: 0x00011F60 File Offset: 0x00010160
        public static IntentionAlly SpellCard(string name, int? damage = null, int? times = null, bool isAccuracy = false)
        {
            SpellCardIntentionAlly spellCardIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<SpellCardIntentionAlly>();
            spellCardIntentionAlly.MoveName = name;
            DamageInfo? damageInfo;
            if (damage != null)
            {
                int valueOrDefault = damage.GetValueOrDefault();
                damageInfo = new DamageInfo?(DamageInfo.Attack(valueOrDefault, isAccuracy));
            }
            else
            {
                damageInfo = null;
            }
            spellCardIntentionAlly.Damage = damageInfo;
            spellCardIntentionAlly.Times = times;
            spellCardIntentionAlly.IsAccuracy = isAccuracy;
            return spellCardIntentionAlly;
        }

        // Token: 0x060005DE RID: 1502 RVA: 0x00011FB8 File Offset: 0x000101B8
        public static IntentionAlly SpellCard(string name, string iconName, int? damage = null, int? times = null, bool isAccuracy = false)
        {
            SpellCardIntentionAlly spellCardIntentionAlly = TypeFactory<IntentionAlly>.CreateInstance<SpellCardIntentionAlly>();
            spellCardIntentionAlly.MoveName = name;
            DamageInfo? damageInfo;
            if (damage != null)
            {
                int valueOrDefault = damage.GetValueOrDefault();
                damageInfo = new DamageInfo?(DamageInfo.Attack(valueOrDefault, isAccuracy));
            }
            else
            {
                damageInfo = null;
            }
            spellCardIntentionAlly.Damage = damageInfo;
            spellCardIntentionAlly.Times = times;
            spellCardIntentionAlly.IconName = iconName;
            spellCardIntentionAlly.IsAccuracy = isAccuracy;
            return spellCardIntentionAlly;
        }

        // Token: 0x060005DF RID: 1503 RVA: 0x00012019 File Offset: 0x00010219
        public static IntentionAlly Unknown()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<UnknownIntentionAlly>();
        }

        // Token: 0x060005E0 RID: 1504 RVA: 0x00012020 File Offset: 0x00010220
        public static IntentionAlly DoNothing()
        {
            return TypeFactory<IntentionAlly>.CreateInstance<DoNothingIntentionAlly>();
        }

        // Token: 0x060005E1 RID: 1505 RVA: 0x00012027 File Offset: 0x00010227
        protected IntentionAlly()
        {
        }

        // Token: 0x040002B4 RID: 692
        public string SpecialIconName;

        // Token: 0x02000209 RID: 521
        internal sealed class IntentionAllyFormatWrapper : GameEntityFormatWrapper
        {
            // Token: 0x06001034 RID: 4148 RVA: 0x0002B817 File Offset: 0x00029A17
            public IntentionAllyFormatWrapper(IntentionAlly IntentionAlly)
                : base(IntentionAlly)
            {
                _IntentionAlly = IntentionAlly;
            }

            // Token: 0x06001035 RID: 4149 RVA: 0x0002B828 File Offset: 0x00029A28
            protected override string FormatArgument(object arg, string format)
            {
                if (arg is DamageInfo)
                {
                    DamageInfo damageInfo = (DamageInfo)arg;
                    return base.FormatArgument(_IntentionAlly.CalculateDamage(damageInfo), format);
                }
                return base.FormatArgument(arg, format);
            }

            // Token: 0x0400081C RID: 2076
            private readonly IntentionAlly _IntentionAlly;
        }
    }
}
*/