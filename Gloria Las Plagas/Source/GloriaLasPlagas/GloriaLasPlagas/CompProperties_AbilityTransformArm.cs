using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class CompProperties_AbilityTransformArm : CompProperties_AbilityEffect
    {
        public string newArmHediffBladeArm = "CCDevelopment_LasPlagas_BladeArm";
        public string newArmHediffClawArm = "CCDevelopment_LasPlagas_ClawArm";
        public string whichNewArm;
        public string targetBodyPart = "Shoulder";

        public CompProperties_AbilityTransformArm()
        {
            compClass = typeof(Comp_AbilityTransformArm);
        }
    }
}
