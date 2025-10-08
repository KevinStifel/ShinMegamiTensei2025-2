namespace Shin_Megami_Tensei
{
    public static class DamageCalculator
    {
        private static double CalculateBaseDamage(int statValue, int modifier)
        {
            double baseDmg = statValue * modifier * GameConstants.BaseDamageModifier;
            return baseDmg;
        }

        private static double CalculatePhysicalDamage(UnitBase attacker)
        {
            double dmg = CalculateBaseDamage(attacker.Stats.Str, GameConstants.PhysicalDamageModifier);
            Console.WriteLine($"[DEBUG] Physical Damage (STR:{attacker.Stats.Str}) = {dmg}");
            return dmg;
        }

        private static double CalculateGunDamage(UnitBase attacker)
        {
            double dmg = CalculateBaseDamage(attacker.Stats.Skl, GameConstants.GunDamageModifier);
            Console.WriteLine($"[DEBUG] Gun Damage (SKL:{attacker.Stats.Skl}) = {dmg}");
            return dmg;
        }

        private static int CalculateMagicDamage(UnitBase attacker, int skillPower)
        {
            int dmg = (int)Math.Sqrt(attacker.Stats.Mag * skillPower);
            Console.WriteLine($"[DEBUG] Magic Damage √({attacker.Stats.Mag} * {skillPower}) = {dmg}");
            return dmg;
        }
        
        private static int ApplyAffinityDamage(double baseDamage, AffinityBehavior affinityBehavior)
        {
            // Delega la modificación del daño a la clase de afinidad correspondiente
            double final = affinityBehavior.ModifyDamage(baseDamage);
            int finalDamage = (int)Math.Floor(final);

            Console.WriteLine($"[DEBUG] ApplyAffinityDamage: {baseDamage:F2} → {final}");
            return finalDamage;
        }

        public static int CalculateFinalDamage(UnitBase attacker, AffinityBehavior affinityBehavior, AffinityElement element)
        {
            double baseDamage = element switch
            {
                AffinityElement.Physical => CalculatePhysicalDamage(attacker),
                AffinityElement.Gun => CalculateGunDamage(attacker),
                _ => CalculatePhysicalDamage(attacker)
            };
            
            int finalDamage = ApplyAffinityDamage(baseDamage, affinityBehavior);

            return finalDamage;
        }

        public static int CalculateFinalDamageForSkill(UnitBase attacker, SkillData skillData, AffinityBehavior behavior)
        {
            var element = AffinityMapper.Parse(skillData.Type);
            Console.WriteLine($"[DEBUG] Element parsed: {element}");

            double baseDamage = element switch
            {
                AffinityElement.Physical => Math.Sqrt(attacker.Stats.Str * skillData.Power),
                AffinityElement.Gun => Math.Sqrt(attacker.Stats.Skl * skillData.Power),
                AffinityElement.Fire or
                AffinityElement.Ice or
                AffinityElement.Elec or
                AffinityElement.Force => Math.Sqrt(attacker.Stats.Mag * skillData.Power),
                _ => Math.Sqrt(attacker.Stats.Str * skillData.Power)
            };

            // Aplicar afinidad sin perder precisión
            int finalDamage = ApplyAffinityDamage(baseDamage, behavior);

            Console.WriteLine($"[DEBUG] BaseDamage: {baseDamage:F2} | FinalDamage: {finalDamage}");
            return finalDamage;
        }
    }
}
