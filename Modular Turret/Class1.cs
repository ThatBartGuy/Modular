using RimWorld;
using System.Linq;
using Verse;
using System;

namespace ModularTurret
{
    internal class ModularBullet : Bullet
    {
        float ExpSize = 0.0f;
        float ModularPenetration = 0.5f;
        int ModularDamage = 10;
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            if (Launcher != null)
            {
                ExpSize += (Launcher.PositionHeld.GetFirstThing(Launcher.Map, ThingDefOf.Turret_Moducannon).TryGetComp<CompAffectedByFacilities>().LinkedFacilitiesListForReading.Where(x => x.def == ThingDefOf.ToolCabinet).Count() * 200f);
                Log.Message(ExpSize.ToString());
                base.Impact(hitThing);
                GenExplosion.DoExplosion(Position, Launcher.Map, ExpSize, DamageDefOf.Bomb, Launcher, ModularDamage, ModularPenetration, null, null, null, null, null, 0f, 1, null, applyDamageToExplosionCellsNeighbors: false, null, 0f, 1, 1f, damageFalloff: true);
            }
        }

        public static class ThingDefOf
        {
            public static ThingDef ToolCabinet;
            public static ThingDef Turret_Moducannon;
        }
    }
}
