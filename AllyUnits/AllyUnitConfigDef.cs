using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LBoL.Base;
using LBoL.ConfigData;
using UnityEngine;

namespace test.AllyUnits
{
    public sealed class AllyUnitConfig
    {
        private AllyUnitConfig(string Id, bool RealName, bool OnlyLore, IReadOnlyList<ManaColor> BaseManaColor, int Order, string ModleName, string NarrativeColor, EnemyType Type, bool IsPreludeOpponent, float? HpLength, int? MaxHpAdd, int MaxHp, int? Damage1, int? Damage2, int? Damage3, int? Damage4, int? Power, int? Defend, int? Count1, int? Count2, int? MaxHpHard, int? Damage1Hard, int? Damage2Hard, int? Damage3Hard, int? Damage4Hard, int? PowerHard, int? DefendHard, int? Count1Hard, int? Count2Hard, int? MaxHpLunatic, int? Damage1Lunatic, int? Damage2Lunatic, int? Damage3Lunatic, int? Damage4Lunatic, int? PowerLunatic, int? DefendLunatic, int? Count1Lunatic, int? Count2Lunatic, MinMax PowerLoot, MinMax BluePointLoot, IReadOnlyList<string> Gun1, IReadOnlyList<string> Gun2, IReadOnlyList<string> Gun3, IReadOnlyList<string> Gun4)
        {
            this.Id = Id;
            this.RealName = RealName;
            this.OnlyLore = OnlyLore;
            this.BaseManaColor = BaseManaColor;
            this.Order = Order;
            this.ModleName = ModleName;
            this.NarrativeColor = NarrativeColor;
            this.Type = Type;
            this.IsPreludeOpponent = IsPreludeOpponent;
            this.HpLength = HpLength;
            this.MaxHpAdd = MaxHpAdd;
            this.MaxHp = MaxHp;
            this.Damage1 = Damage1;
            this.Damage2 = Damage2;
            this.Damage3 = Damage3;
            this.Damage4 = Damage4;
            this.Power = Power;
            this.Defend = Defend;
            this.Count1 = Count1;
            this.Count2 = Count2;
            this.MaxHpHard = MaxHpHard;
            this.Damage1Hard = Damage1Hard;
            this.Damage2Hard = Damage2Hard;
            this.Damage3Hard = Damage3Hard;
            this.Damage4Hard = Damage4Hard;
            this.PowerHard = PowerHard;
            this.DefendHard = DefendHard;
            this.Count1Hard = Count1Hard;
            this.Count2Hard = Count2Hard;
            this.MaxHpLunatic = MaxHpLunatic;
            this.Damage1Lunatic = Damage1Lunatic;
            this.Damage2Lunatic = Damage2Lunatic;
            this.Damage3Lunatic = Damage3Lunatic;
            this.Damage4Lunatic = Damage4Lunatic;
            this.PowerLunatic = PowerLunatic;
            this.DefendLunatic = DefendLunatic;
            this.Count1Lunatic = Count1Lunatic;
            this.Count2Lunatic = Count2Lunatic;
            this.PowerLoot = PowerLoot;
            this.BluePointLoot = BluePointLoot;
            this.Gun1 = Gun1;
            this.Gun2 = Gun2;
            this.Gun3 = Gun3;
            this.Gun4 = Gun4;
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x06000065 RID: 101 RVA: 0x00003160 File Offset: 0x00001360
        // (set) Token: 0x06000066 RID: 102 RVA: 0x00003168 File Offset: 0x00001368
        public string Id { get; private set; }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000067 RID: 103 RVA: 0x00003171 File Offset: 0x00001371
        // (set) Token: 0x06000068 RID: 104 RVA: 0x00003179 File Offset: 0x00001379
        public bool RealName { get; private set; }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000069 RID: 105 RVA: 0x00003182 File Offset: 0x00001382
        // (set) Token: 0x0600006A RID: 106 RVA: 0x0000318A File Offset: 0x0000138A
        public bool OnlyLore { get; private set; }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x0600006B RID: 107 RVA: 0x00003193 File Offset: 0x00001393
        // (set) Token: 0x0600006C RID: 108 RVA: 0x0000319B File Offset: 0x0000139B
        public IReadOnlyList<ManaColor> BaseManaColor { get; private set; }

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x0600006D RID: 109 RVA: 0x000031A4 File Offset: 0x000013A4
        // (set) Token: 0x0600006E RID: 110 RVA: 0x000031AC File Offset: 0x000013AC
        public int Order { get; private set; }

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x0600006F RID: 111 RVA: 0x000031B5 File Offset: 0x000013B5
        // (set) Token: 0x06000070 RID: 112 RVA: 0x000031BD File Offset: 0x000013BD
        public string ModleName { get; private set; }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x06000071 RID: 113 RVA: 0x000031C6 File Offset: 0x000013C6
        // (set) Token: 0x06000072 RID: 114 RVA: 0x000031CE File Offset: 0x000013CE
        public string NarrativeColor { get; private set; }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x06000073 RID: 115 RVA: 0x000031D7 File Offset: 0x000013D7
        // (set) Token: 0x06000074 RID: 116 RVA: 0x000031DF File Offset: 0x000013DF
        public EnemyType Type { get; private set; }

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x06000075 RID: 117 RVA: 0x000031E8 File Offset: 0x000013E8
        // (set) Token: 0x06000076 RID: 118 RVA: 0x000031F0 File Offset: 0x000013F0
        public bool IsPreludeOpponent { get; private set; }

        // Token: 0x17000025 RID: 37
        // (get) Token: 0x06000077 RID: 119 RVA: 0x000031F9 File Offset: 0x000013F9
        // (set) Token: 0x06000078 RID: 120 RVA: 0x00003201 File Offset: 0x00001401
        public float? HpLength { get; private set; }

        // Token: 0x17000026 RID: 38
        // (get) Token: 0x06000079 RID: 121 RVA: 0x0000320A File Offset: 0x0000140A
        // (set) Token: 0x0600007A RID: 122 RVA: 0x00003212 File Offset: 0x00001412
        public int? MaxHpAdd { get; private set; }

        // Token: 0x17000027 RID: 39
        // (get) Token: 0x0600007B RID: 123 RVA: 0x0000321B File Offset: 0x0000141B
        // (set) Token: 0x0600007C RID: 124 RVA: 0x00003223 File Offset: 0x00001423
        public int MaxHp { get; private set; }

        // Token: 0x17000028 RID: 40
        // (get) Token: 0x0600007D RID: 125 RVA: 0x0000322C File Offset: 0x0000142C
        // (set) Token: 0x0600007E RID: 126 RVA: 0x00003234 File Offset: 0x00001434
        public int? Damage1 { get; private set; }

        // Token: 0x17000029 RID: 41
        // (get) Token: 0x0600007F RID: 127 RVA: 0x0000323D File Offset: 0x0000143D
        // (set) Token: 0x06000080 RID: 128 RVA: 0x00003245 File Offset: 0x00001445
        public int? Damage2 { get; private set; }

        // Token: 0x1700002A RID: 42
        // (get) Token: 0x06000081 RID: 129 RVA: 0x0000324E File Offset: 0x0000144E
        // (set) Token: 0x06000082 RID: 130 RVA: 0x00003256 File Offset: 0x00001456
        public int? Damage3 { get; private set; }

        // Token: 0x1700002B RID: 43
        // (get) Token: 0x06000083 RID: 131 RVA: 0x0000325F File Offset: 0x0000145F
        // (set) Token: 0x06000084 RID: 132 RVA: 0x00003267 File Offset: 0x00001467
        public int? Damage4 { get; private set; }

        // Token: 0x1700002C RID: 44
        // (get) Token: 0x06000085 RID: 133 RVA: 0x00003270 File Offset: 0x00001470
        // (set) Token: 0x06000086 RID: 134 RVA: 0x00003278 File Offset: 0x00001478
        public int? Power { get; private set; }

        // Token: 0x1700002D RID: 45
        // (get) Token: 0x06000087 RID: 135 RVA: 0x00003281 File Offset: 0x00001481
        // (set) Token: 0x06000088 RID: 136 RVA: 0x00003289 File Offset: 0x00001489
        public int? Defend { get; private set; }

        // Token: 0x1700002E RID: 46
        // (get) Token: 0x06000089 RID: 137 RVA: 0x00003292 File Offset: 0x00001492
        // (set) Token: 0x0600008A RID: 138 RVA: 0x0000329A File Offset: 0x0000149A
        public int? Count1 { get; private set; }

        // Token: 0x1700002F RID: 47
        // (get) Token: 0x0600008B RID: 139 RVA: 0x000032A3 File Offset: 0x000014A3
        // (set) Token: 0x0600008C RID: 140 RVA: 0x000032AB File Offset: 0x000014AB
        public int? Count2 { get; private set; }

        // Token: 0x17000030 RID: 48
        // (get) Token: 0x0600008D RID: 141 RVA: 0x000032B4 File Offset: 0x000014B4
        // (set) Token: 0x0600008E RID: 142 RVA: 0x000032BC File Offset: 0x000014BC
        public int? MaxHpHard { get; private set; }

        // Token: 0x17000031 RID: 49
        // (get) Token: 0x0600008F RID: 143 RVA: 0x000032C5 File Offset: 0x000014C5
        // (set) Token: 0x06000090 RID: 144 RVA: 0x000032CD File Offset: 0x000014CD
        public int? Damage1Hard { get; private set; }

        // Token: 0x17000032 RID: 50
        // (get) Token: 0x06000091 RID: 145 RVA: 0x000032D6 File Offset: 0x000014D6
        // (set) Token: 0x06000092 RID: 146 RVA: 0x000032DE File Offset: 0x000014DE
        public int? Damage2Hard { get; private set; }

        // Token: 0x17000033 RID: 51
        // (get) Token: 0x06000093 RID: 147 RVA: 0x000032E7 File Offset: 0x000014E7
        // (set) Token: 0x06000094 RID: 148 RVA: 0x000032EF File Offset: 0x000014EF
        public int? Damage3Hard { get; private set; }

        // Token: 0x17000034 RID: 52
        // (get) Token: 0x06000095 RID: 149 RVA: 0x000032F8 File Offset: 0x000014F8
        // (set) Token: 0x06000096 RID: 150 RVA: 0x00003300 File Offset: 0x00001500
        public int? Damage4Hard { get; private set; }

        // Token: 0x17000035 RID: 53
        // (get) Token: 0x06000097 RID: 151 RVA: 0x00003309 File Offset: 0x00001509
        // (set) Token: 0x06000098 RID: 152 RVA: 0x00003311 File Offset: 0x00001511
        public int? PowerHard { get; private set; }

        // Token: 0x17000036 RID: 54
        // (get) Token: 0x06000099 RID: 153 RVA: 0x0000331A File Offset: 0x0000151A
        // (set) Token: 0x0600009A RID: 154 RVA: 0x00003322 File Offset: 0x00001522
        public int? DefendHard { get; private set; }

        // Token: 0x17000037 RID: 55
        // (get) Token: 0x0600009B RID: 155 RVA: 0x0000332B File Offset: 0x0000152B
        // (set) Token: 0x0600009C RID: 156 RVA: 0x00003333 File Offset: 0x00001533
        public int? Count1Hard { get; private set; }

        // Token: 0x17000038 RID: 56
        // (get) Token: 0x0600009D RID: 157 RVA: 0x0000333C File Offset: 0x0000153C
        // (set) Token: 0x0600009E RID: 158 RVA: 0x00003344 File Offset: 0x00001544
        public int? Count2Hard { get; private set; }

        // Token: 0x17000039 RID: 57
        // (get) Token: 0x0600009F RID: 159 RVA: 0x0000334D File Offset: 0x0000154D
        // (set) Token: 0x060000A0 RID: 160 RVA: 0x00003355 File Offset: 0x00001555
        public int? MaxHpLunatic { get; private set; }

        // Token: 0x1700003A RID: 58
        // (get) Token: 0x060000A1 RID: 161 RVA: 0x0000335E File Offset: 0x0000155E
        // (set) Token: 0x060000A2 RID: 162 RVA: 0x00003366 File Offset: 0x00001566
        public int? Damage1Lunatic { get; private set; }

        // Token: 0x1700003B RID: 59
        // (get) Token: 0x060000A3 RID: 163 RVA: 0x0000336F File Offset: 0x0000156F
        // (set) Token: 0x060000A4 RID: 164 RVA: 0x00003377 File Offset: 0x00001577
        public int? Damage2Lunatic { get; private set; }

        // Token: 0x1700003C RID: 60
        // (get) Token: 0x060000A5 RID: 165 RVA: 0x00003380 File Offset: 0x00001580
        // (set) Token: 0x060000A6 RID: 166 RVA: 0x00003388 File Offset: 0x00001588
        public int? Damage3Lunatic { get; private set; }

        // Token: 0x1700003D RID: 61
        // (get) Token: 0x060000A7 RID: 167 RVA: 0x00003391 File Offset: 0x00001591
        // (set) Token: 0x060000A8 RID: 168 RVA: 0x00003399 File Offset: 0x00001599
        public int? Damage4Lunatic { get; private set; }

        // Token: 0x1700003E RID: 62
        // (get) Token: 0x060000A9 RID: 169 RVA: 0x000033A2 File Offset: 0x000015A2
        // (set) Token: 0x060000AA RID: 170 RVA: 0x000033AA File Offset: 0x000015AA
        public int? PowerLunatic { get; private set; }

        // Token: 0x1700003F RID: 63
        // (get) Token: 0x060000AB RID: 171 RVA: 0x000033B3 File Offset: 0x000015B3
        // (set) Token: 0x060000AC RID: 172 RVA: 0x000033BB File Offset: 0x000015BB
        public int? DefendLunatic { get; private set; }

        // Token: 0x17000040 RID: 64
        // (get) Token: 0x060000AD RID: 173 RVA: 0x000033C4 File Offset: 0x000015C4
        // (set) Token: 0x060000AE RID: 174 RVA: 0x000033CC File Offset: 0x000015CC
        public int? Count1Lunatic { get; private set; }

        // Token: 0x17000041 RID: 65
        // (get) Token: 0x060000AF RID: 175 RVA: 0x000033D5 File Offset: 0x000015D5
        // (set) Token: 0x060000B0 RID: 176 RVA: 0x000033DD File Offset: 0x000015DD
        public int? Count2Lunatic { get; private set; }

        // Token: 0x17000042 RID: 66
        // (get) Token: 0x060000B1 RID: 177 RVA: 0x000033E6 File Offset: 0x000015E6
        // (set) Token: 0x060000B2 RID: 178 RVA: 0x000033EE File Offset: 0x000015EE
        public MinMax PowerLoot { get; private set; }

        // Token: 0x17000043 RID: 67
        // (get) Token: 0x060000B3 RID: 179 RVA: 0x000033F7 File Offset: 0x000015F7
        // (set) Token: 0x060000B4 RID: 180 RVA: 0x000033FF File Offset: 0x000015FF
        public MinMax BluePointLoot { get; private set; }

        // Token: 0x17000044 RID: 68
        // (get) Token: 0x060000B5 RID: 181 RVA: 0x00003408 File Offset: 0x00001608
        // (set) Token: 0x060000B6 RID: 182 RVA: 0x00003410 File Offset: 0x00001610
        public IReadOnlyList<string> Gun1 { get; private set; }

        // Token: 0x17000045 RID: 69
        // (get) Token: 0x060000B7 RID: 183 RVA: 0x00003419 File Offset: 0x00001619
        // (set) Token: 0x060000B8 RID: 184 RVA: 0x00003421 File Offset: 0x00001621
        public IReadOnlyList<string> Gun2 { get; private set; }

        // Token: 0x17000046 RID: 70
        // (get) Token: 0x060000B9 RID: 185 RVA: 0x0000342A File Offset: 0x0000162A
        // (set) Token: 0x060000BA RID: 186 RVA: 0x00003432 File Offset: 0x00001632
        public IReadOnlyList<string> Gun3 { get; private set; }

        // Token: 0x17000047 RID: 71
        // (get) Token: 0x060000BB RID: 187 RVA: 0x0000343B File Offset: 0x0000163B
        // (set) Token: 0x060000BC RID: 188 RVA: 0x00003443 File Offset: 0x00001643
        public IReadOnlyList<string> Gun4 { get; private set; }

        // Token: 0x060000BD RID: 189 RVA: 0x0000344C File Offset: 0x0000164C
        public static IReadOnlyList<AllyUnitConfig> AllConfig()
        {
            ConfigDataManager.Initialize();
            return Array.AsReadOnly(_data);
        }

        // Token: 0x060000BE RID: 190 RVA: 0x00003460 File Offset: 0x00001660
        public static AllyUnitConfig FromId(string Id)
        {
            ConfigDataManager.Initialize();
            AllyUnitConfig AllyUnitConfig;
            return !_IdTable.TryGetValue(Id, out AllyUnitConfig) ? null : AllyUnitConfig;
        }

        // Token: 0x060000BF RID: 191 RVA: 0x0000348C File Offset: 0x0000168C
        public override string ToString()
        {
            string[] array = new string[89];
            array[0] = "{AllyUnitConfig Id=";
            array[1] = ConfigDataManager.System_String.ToString(Id);
            array[2] = ", RealName=";
            array[3] = ConfigDataManager.System_Boolean.ToString(RealName);
            array[4] = ", OnlyLore=";
            array[5] = ConfigDataManager.System_Boolean.ToString(OnlyLore);
            array[6] = ", BaseManaColor=[";
            array[7] = string.Join(", ", BaseManaColor.Select((v1) => ConfigDataManager.LBoL_Base_ManaColor.ToString(v1)));
            array[8] = "], Order=";
            array[9] = ConfigDataManager.System_Int32.ToString(Order);
            array[10] = ", ModleName=";
            array[11] = ConfigDataManager.System_String.ToString(ModleName);
            array[12] = ", NarrativeColor=";
            array[13] = ConfigDataManager.System_String.ToString(NarrativeColor);
            array[14] = ", Type=";
            array[15] = Type.ToString();
            array[16] = ", IsPreludeOpponent=";
            array[17] = ConfigDataManager.System_Boolean.ToString(IsPreludeOpponent);
            array[18] = ", HpLength=";
            array[19] = HpLength == null ? "null" : ConfigDataManager.System_Single.ToString(HpLength.Value);
            array[20] = ", MaxHpAdd=";
            array[21] = MaxHpAdd == null ? "null" : ConfigDataManager.System_Int32.ToString(MaxHpAdd.Value);
            array[22] = ", MaxHp=";
            array[23] = ConfigDataManager.System_Int32.ToString(MaxHp);
            array[24] = ", Damage1=";
            array[25] = Damage1 == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage1.Value);
            array[26] = ", Damage2=";
            array[27] = Damage2 == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage2.Value);
            array[28] = ", Damage3=";
            array[29] = Damage3 == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage3.Value);
            array[30] = ", Damage4=";
            array[31] = Damage4 == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage4.Value);
            array[32] = ", Power=";
            array[33] = Power == null ? "null" : ConfigDataManager.System_Int32.ToString(Power.Value);
            array[34] = ", Defend=";
            array[35] = Defend == null ? "null" : ConfigDataManager.System_Int32.ToString(Defend.Value);
            array[36] = ", Count1=";
            array[37] = Count1 == null ? "null" : ConfigDataManager.System_Int32.ToString(Count1.Value);
            array[38] = ", Count2=";
            array[39] = Count2 == null ? "null" : ConfigDataManager.System_Int32.ToString(Count2.Value);
            array[40] = ", MaxHpHard=";
            array[41] = MaxHpHard == null ? "null" : ConfigDataManager.System_Int32.ToString(MaxHpHard.Value);
            array[42] = ", Damage1Hard=";
            array[43] = Damage1Hard == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage1Hard.Value);
            array[44] = ", Damage2Hard=";
            array[45] = Damage2Hard == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage2Hard.Value);
            array[46] = ", Damage3Hard=";
            array[47] = Damage3Hard == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage3Hard.Value);
            array[48] = ", Damage4Hard=";
            array[49] = Damage4Hard == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage4Hard.Value);
            array[50] = ", PowerHard=";
            array[51] = PowerHard == null ? "null" : ConfigDataManager.System_Int32.ToString(PowerHard.Value);
            array[52] = ", DefendHard=";
            array[53] = DefendHard == null ? "null" : ConfigDataManager.System_Int32.ToString(DefendHard.Value);
            array[54] = ", Count1Hard=";
            array[55] = Count1Hard == null ? "null" : ConfigDataManager.System_Int32.ToString(Count1Hard.Value);
            array[56] = ", Count2Hard=";
            array[57] = Count2Hard == null ? "null" : ConfigDataManager.System_Int32.ToString(Count2Hard.Value);
            array[58] = ", MaxHpLunatic=";
            array[59] = MaxHpLunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(MaxHpLunatic.Value);
            array[60] = ", Damage1Lunatic=";
            array[61] = Damage1Lunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage1Lunatic.Value);
            array[62] = ", Damage2Lunatic=";
            array[63] = Damage2Lunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage2Lunatic.Value);
            array[64] = ", Damage3Lunatic=";
            array[65] = Damage3Lunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage3Lunatic.Value);
            array[66] = ", Damage4Lunatic=";
            array[67] = Damage4Lunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(Damage4Lunatic.Value);
            array[68] = ", PowerLunatic=";
            array[69] = PowerLunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(PowerLunatic.Value);
            array[70] = ", DefendLunatic=";
            array[71] = DefendLunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(DefendLunatic.Value);
            array[72] = ", Count1Lunatic=";
            array[73] = Count1Lunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(Count1Lunatic.Value);
            array[74] = ", Count2Lunatic=";
            array[75] = Count2Lunatic == null ? "null" : ConfigDataManager.System_Int32.ToString(Count2Lunatic.Value);
            array[76] = ", PowerLoot=";
            array[77] = ConfigDataManager.LBoL_Base_MinMax.ToString(PowerLoot);
            array[78] = ", BluePointLoot=";
            array[79] = ConfigDataManager.LBoL_Base_MinMax.ToString(BluePointLoot);
            array[80] = ", Gun1=[";
            array[81] = string.Join(", ", Gun1.Select((v1) => ConfigDataManager.System_String.ToString(v1)));
            array[82] = "], Gun2=[";
            array[83] = string.Join(", ", Gun2.Select((v1) => ConfigDataManager.System_String.ToString(v1)));
            array[84] = "], Gun3=[";
            array[85] = string.Join(", ", Gun3.Select((v1) => ConfigDataManager.System_String.ToString(v1)));
            array[86] = "], Gun4=[";
            array[87] = string.Join(", ", Gun4.Select((v1) => ConfigDataManager.System_String.ToString(v1)));
            array[88] = "]}";
            return string.Concat(array);
        }

        // Token: 0x060000C0 RID: 192 RVA: 0x00003E90 File Offset: 0x00002090
        private static void Load(byte[] bytes)
        {
            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes)))
            {
                AllyUnitConfig[] array = new AllyUnitConfig[binaryReader.ReadInt32()];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = new AllyUnitConfig(ConfigDataManager.System_String.ReadFrom(binaryReader), ConfigDataManager.System_Boolean.ReadFrom(binaryReader), ConfigDataManager.System_Boolean.ReadFrom(binaryReader), ConfigDataManager.ReadList(binaryReader, (r1) => ConfigDataManager.LBoL_Base_ManaColor.ReadFrom(r1)), ConfigDataManager.System_Int32.ReadFrom(binaryReader), ConfigDataManager.System_String.ReadFrom(binaryReader), ConfigDataManager.System_String.ReadFrom(binaryReader), (EnemyType)binaryReader.ReadInt32(), ConfigDataManager.System_Boolean.ReadFrom(binaryReader), !binaryReader.ReadBoolean() ? null : new float?(ConfigDataManager.System_Single.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), ConfigDataManager.System_Int32.ReadFrom(binaryReader), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), !binaryReader.ReadBoolean() ? null : new int?(ConfigDataManager.System_Int32.ReadFrom(binaryReader)), ConfigDataManager.LBoL_Base_MinMax.ReadFrom(binaryReader), ConfigDataManager.LBoL_Base_MinMax.ReadFrom(binaryReader), ConfigDataManager.ReadList(binaryReader, (r1) => ConfigDataManager.System_String.ReadFrom(r1)), ConfigDataManager.ReadList(binaryReader, (r1) => ConfigDataManager.System_String.ReadFrom(r1)), ConfigDataManager.ReadList(binaryReader, (r1) => ConfigDataManager.System_String.ReadFrom(r1)), ConfigDataManager.ReadList(binaryReader, (r1) => ConfigDataManager.System_String.ReadFrom(r1)));
                }
                _data = array;
                _IdTable = _data.ToDictionary((elem) => elem.Id);
            }
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x000044EC File Offset: 0x000026EC
        internal static void Reload()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("ConfigData/AllyUnitConfig");
            if (textAsset != null)
            {
                try
                {
                    Load(textAsset.bytes);
                }
                catch (Exception ex)
                {
                    Debug.LogError(string.Format("Failed to load AllyUnitConfig data: {0}, try reimport config data", ex.Message));
                }
                Resources.UnloadAsset(textAsset);
            }
            else
            {
                Debug.LogError("Cannot load config data of 'AllyUnitConfig', please reimport config data");
            }
        }

        // Token: 0x060000C2 RID: 194 RVA: 0x00004560 File Offset: 0x00002760
        // Note: this type is marked as 'beforefieldinit'.
        static AllyUnitConfig()
        {
        }

        // Token: 0x04000057 RID: 87
        private static AllyUnitConfig[] _data = Array.Empty<AllyUnitConfig>();

        // Token: 0x04000058 RID: 88
        private static Dictionary<string, AllyUnitConfig> _IdTable = _data.ToDictionary((elem) => elem.Id);
    }
}
