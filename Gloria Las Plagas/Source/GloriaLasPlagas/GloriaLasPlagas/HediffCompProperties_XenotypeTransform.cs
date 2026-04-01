using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffCompProperties_XenotypeTransform : HediffCompProperties
    {
        public List<XenotypeDef> xenotypes;
        public List<float> probabilities;

        public HediffCompProperties_XenotypeTransform()
        {
            compClass = typeof(HediffComp_XenotypeTransform);
        }
    }
}
