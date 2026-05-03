using RimWorld;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDevelopment.LasPlagas
{
    public class Comp_AbilityTransformArm : CompAbilityEffect
    {
        private BodyPartRecord mutatableArm;
        public CompProperties_AbilityTransformArm Props
        {
            get { return (CompProperties_AbilityTransformArm)props; }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn pawn = parent.pawn;
            HediffDef newArmDef;
            if(Props.whichNewArm == "Blade") newArmDef = DefDatabase<HediffDef>.GetNamed(Props.newArmHediffBladeArm);
            else newArmDef = DefDatabase<HediffDef>.GetNamed(Props.newArmHediffClawArm);

            //remove all hediffs on the part that is getting mutated
            var existingHediffs = pawn.health.hediffSet.hediffs.Where(hediff => hediff.Part == mutatableArm).ToList(); //l kaputt
            foreach(var hediff in existingHediffs)
            {
                pawn.health.RemoveHediff(hediff);
            }


            pawn.health.AddHediff(newArmDef,mutatableArm);
        }

        public override bool GizmoDisabled(out string reason)
        {
            Pawn pawn = parent.pawn;
            if ((base.GizmoDisabled(out reason))) return true;
            
            if(!(HasNatrualArm(pawn))) return true;

            return false;
        }

        private IEnumerable<BodyPartRecord> getArmParts(Pawn pawn)
        {
            return pawn.RaceProps.body.AllParts.Where(part => part.def.defName == Props.targetBodyPart);
        }

        private BodyPartRecord GetNaturalArmPart(Pawn pawn)
        {
            IEnumerable<BodyPartRecord> armParts = getArmParts(pawn);

            foreach(BodyPartRecord armPart in armParts)
            {
                bool alreadyReplaced = pawn.health.hediffSet.hediffs.Any(hediff => hediff.Part == armPart && (hediff.def.defName == Props.newArmHediffBladeArm || hediff.def.defName == Props.newArmHediffClawArm));
                if (alreadyReplaced) continue;

                bool hasArtificialArm = pawn.health.hediffSet.hediffs.Any(hediff => hediff.Part == armPart && hediff.def.addedPartProps != null && hediff.def.defName != "CCDevelopment_LasPlagas_CrippledArm");
                if (hasArtificialArm) continue;
                return armPart; //returns missing arm
            }
            return null; //if it end here arm is null ==> no natural arm
        }




        private bool HasNatrualArm(Pawn pawn)
        {
            mutatableArm = GetNaturalArmPart(pawn);
            if (mutatableArm == null) return false;
            return true;
        }
    }
}
