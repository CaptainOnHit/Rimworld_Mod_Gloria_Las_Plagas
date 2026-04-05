using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffCompProperties_PlagasParasite_Superior : HediffCompProperties
    {
        public XenotypeDef currentStagePlagaXenotype;
        public HediffCompProperties_PlagasParasite_Superior()
        {
            compClass = typeof(HediffComp_PlagasParasite_Superior);
        }
    }
}
