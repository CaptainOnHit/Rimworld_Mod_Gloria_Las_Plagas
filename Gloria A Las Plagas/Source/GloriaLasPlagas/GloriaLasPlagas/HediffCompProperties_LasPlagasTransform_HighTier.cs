using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffCompProperties_LasPlagasTransform_HighTier : HediffCompProperties
    {
        public string xenotypeHediffName;
        public HediffCompProperties_LasPlagasTransform_HighTier()
        {
            compClass = typeof(HediffComp_LasPlagasTransform_HighTier);
        }

    }
}
