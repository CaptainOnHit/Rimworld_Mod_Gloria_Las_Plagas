using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class CompAbilityEffect_EatHead : CompAbilityEffect
    {
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (target.Pawn == null) return;
            if (!(target.Pawn.health.hediffSet.HasHead))
            {
                Messages.Message("Target has no Head!",MessageTypeDefOf.RejectInput);
                return;
            }
            var head = target.Pawn.health.hediffSet.GetNotMissingParts().FirstOrDefault(p => p.def == BodyPartDefOf.Head);

            DamageInfo damageInfo = new DamageInfo(def : DamageDefOf.Bite, amount: 9999f, armorPenetration : 9999f, instigator: parent.pawn, hitPart : head);

            var logEntry = new BattleLogEntry_AbilityUsed(caster: parent.pawn, target: target.Pawn, ability: parent.def, eventDef: DefDatabase<RulePackDef>.GetNamed("CCDevelopment_LasPlagas_EatHeadEvent"));
            Find.BattleLog.Add(logEntry);

            DamageWorker.DamageResult result = target.Pawn.TakeDamage(damageInfo);
        }
    }
}
