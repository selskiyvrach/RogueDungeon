using System.Collections.Generic;

namespace RogueDungeon.Stats
{
    public static class Constants
    {
        public const string FLAT = "Flat";
        public const string PERCENT = "Percent";
        public const string HP = "Hp";
        public const string BONUS = "Bonus";
        
        public const string RESIST = "Resist";
        
        // DAMAGE
        public const string UNARMED_DAMAGE = "UnarmedDamage";
        public const string TOTAL_DAMAGE = "TotalDamage";
        // DAMAGE TYPES
        public const string PHYSICAL_DAMAGE = "PhysicalDamage";
        public const string PIERCE_DAMAGE = "PierceDamage";
        public const string SLASH_DAMAGE = "SlashDamage";
        public const string BLUNT_DAMAGE = "BluntDamage";
        public static readonly Dictionary<string, string> DAMAGE_TYPE_PARENTS =  new (){
            {TOTAL_DAMAGE, null},
            {PHYSICAL_DAMAGE, TOTAL_DAMAGE},
            {PIERCE_DAMAGE, PHYSICAL_DAMAGE},
            {SLASH_DAMAGE, PHYSICAL_DAMAGE},
            {BLUNT_DAMAGE, PHYSICAL_DAMAGE},
        };
    }
}