using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffCompProperties_LasPlagasTransform : HediffCompProperties
    {
        public List<HediffDef> plagaStages;
        public List<float> probabilities;

        public HediffCompProperties_LasPlagasTransform()
        {
            compClass = typeof(HediffComp_LasPlagasTransform);
        }

    }
}
