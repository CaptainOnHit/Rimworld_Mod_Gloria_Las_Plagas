using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class Comp_RetractTransformedArm : CompAbilityEffect
    {
        private List<BodyPartRecord> mutatedArms;

        public CompProperties_RetractTransformedArm Props
        {
            get { return (CompProperties_RetractTransformedArm)props; }
        }
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn pawn = parent.pawn;
            HediffDef newArmDef = DefDatabase<HediffDef>.GetNamed("CCDevelopment_LasPlagas_CrippledArm");

            //remove all hediffs on the part that is getting mutated
            foreach (BodyPartRecord mutatedArm in mutatedArms)
            {
                var existingHediffs = pawn.health.hediffSet.hediffs.Where(hediff => hediff.Part == mutatedArm).ToList(); //l kaputt
                foreach (var hediff in existingHediffs)
                {
                    pawn.health.RemoveHediff(hediff);
                }
                pawn.health.AddHediff(newArmDef, mutatedArm);
            }
            
        }
        public override bool GizmoDisabled(out string reason)
        {
            Pawn pawn = parent.pawn;
            if ((base.GizmoDisabled(out reason))) return true;

            if (!(HasMutatedArm(pawn)))
            {
                reason = "Pawn has no mutated arms.";
                return true;
            }

            return false;
        }
        private IEnumerable<BodyPartRecord> getArmParts(Pawn pawn)
        {
            return pawn.RaceProps.body.AllParts.Where(part => part.def.defName == Props.targetBodyPart);
        }
        private List<BodyPartRecord> GetMutatedArmParts(Pawn pawn)
        {
            IEnumerable<BodyPartRecord> armParts = getArmParts(pawn);
            List<BodyPartRecord> mutatedArmParts = new List<BodyPartRecord>();

            foreach (BodyPartRecord armPart in armParts)
            {
                bool alreadyReplaced = pawn.health.hediffSet.hediffs.Any(hediff => hediff.Part == armPart && (hediff.def.defName == Props.newArmHediffBladeArm || hediff.def.defName == Props.newArmHediffClawArm));
                if (alreadyReplaced) mutatedArmParts.Add(armPart);
            }
            return mutatedArmParts;
        }
        private bool HasMutatedArm(Pawn pawn)
        {
            mutatedArms = GetMutatedArmParts(pawn);
            if (mutatedArms == null) return false;
            return true;
        }
    }
}
