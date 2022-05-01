using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SolStandardAPI.Utility;

namespace SolStandardAPI.Models
{
    public enum Stats
    {
        Hp,
        Attack,
        Armor,
        Movement,
        AtkRange,
        Luck,
        Retribution,
        Block,
        CommandPoints,
    }

    public class UnitStatistics
    {
        private static readonly Dictionary<Stats, string> Abbreviation = new()
        {
            { Stats.Hp, "HP" },
            { Stats.Attack, "ATK" },
            { Stats.Armor, "AMR" },
            { Stats.Movement, "MV" },
            { Stats.AtkRange, "RNG" },
            { Stats.Luck, "LCK" },
            { Stats.Retribution, "RET" },
            { Stats.Block, "BLK" },
            { Stats.CommandPoints, "CP" }
        };

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int HP { get; set; }
        public int Armor { get; set; }
        public int MaxCmd { get; set; }
        public int[] BaseAtkRange { get; set; }

        public int Atk { get; set; }
        public int Ret { get; set; }
        public int Luck { get; set; }
        public int Mv { get; set; }
        public int Blk { get; set; }

        public UnitStatistics() : this(0, 0, 0, 0, 0, 0, 0, Array.Empty<int>(), 0)
        {
        }

        public UnitStatistics(int hp, int armor, int atk, int ret, int blk, int luck, int mv, int[] atkRange,
            int maxCmd)
        {
            HP = hp;
            Armor = armor;
            MaxCmd = maxCmd;
            Atk = atk;
            Ret = ret;
            Blk = blk;
            Luck = luck;
            Mv = mv;
            BaseAtkRange = ArrayDeepCopier<int>.DeepCopyArray(atkRange);
        }

        public bool IsIdentity()
        {
            return Equals(new UnitStatistics());
        }

        public override string ToString()
        {
            return
                $"UNIT STATISTICS: (HP: {HP}, Armor: {Armor}, MaxCmd: {MaxCmd}, Atk: {Atk}, Ret: {Ret}, Blk: {Blk}, Luck: {Luck}, Mv: {Mv}, BaseAtkRange: {string.Join(",", BaseAtkRange)})";
        }

        public override bool Equals(object? obj)
        {
            return obj is UnitStatistics other && Equals(other);
        }

        private bool Equals(UnitStatistics other)
        {
            return HP == other.HP &&
                   Armor == other.Armor &&
                   BaseAtkRange.SequenceEqual(other.BaseAtkRange) &&
                   Atk == other.Atk &&
                   Ret == other.Ret &&
                   Blk == other.Blk &&
                   Luck == other.Luck &&
                   Mv == other.Mv &&
                   Blk == other.Blk &&
                   Mv == other.Mv &&
                   MaxCmd == other.MaxCmd;
        }

        // ReSharper disable NonReadonlyMemberInGetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = HP;
                hashCode += (hashCode * 397) ^ Armor;
                hashCode += (hashCode * 397) ^ (BaseAtkRange.GetHashCode());
                hashCode += (hashCode * 397) ^ Atk;
                hashCode += (hashCode * 397) ^ Ret;
                hashCode += (hashCode * 397) ^ Luck;
                hashCode += (hashCode * 397) ^ Blk;
                hashCode += (hashCode * 397) ^ Mv;
                hashCode += (hashCode * 397) ^ MaxCmd;
                return hashCode;
            }
        }

        public UnitStatistics CloneCurrentStats()
        {
            return new(
                hp: HP,
                armor: Armor,
                atk: Atk,
                ret: Ret,
                blk: Blk,
                luck: Luck,
                mv: Mv,
                atkRange: BaseAtkRange,
                maxCmd: MaxCmd
            );
        }
    }
}