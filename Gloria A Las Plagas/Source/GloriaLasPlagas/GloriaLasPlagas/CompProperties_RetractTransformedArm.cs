using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDevelopment.LasPlagas
{
    public class CompProperties_RetractTransformedArm : CompProperties_AbilityEffect
    {
        public string targetBodyPart = "Shoulder";
        public string newArmHediffBladeArm = "CCDevelopment_LasPlagas_BladeArm";
        public string newArmHediffClawArm = "CCDevelopment_LasPlagas_ClawArm";
        public CompProperties_RetractTransformedArm()
        {
            compClass = typeof(Comp_RetractTransformedArm);
        }
    }
}
