using System.Linq;
using RimWorld;
using Verse;

namespace ModularTurret
{
    public class ModularBullet : Bullet
    {
        float ModularExplosionFactor = 10f;
        float ModularPenetration = 0.5f;
        int ModularDamage = 10;
        float ExpSize = 1f;

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            if (Launcher == null) return;
            ExpSize += Launcher.TryGetComp<CompAffectedByFacilities>()
                           .LinkedFacilitiesListForReading.Count() * ModularExplosionFactor;
            base.Impact(hitThing, blockedByShield);
            GenExplosion.DoExplosion(Position, Launcher.Map, ExpSize, DamageDefOf.Bomb, Launcher, ModularDamage,
                ModularPenetration, null, null, null, null, null, 0f, 1, null,
                applyDamageToExplosionCellsNeighbors: false, null, 0f, 1, 1f, damageFalloff: true);
        }
    }
}
