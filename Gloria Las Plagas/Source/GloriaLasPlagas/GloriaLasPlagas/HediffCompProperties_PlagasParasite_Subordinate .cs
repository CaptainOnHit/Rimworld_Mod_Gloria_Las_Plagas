using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffCompProperties_PlagasParasite_Subordinate : HediffCompProperties
    {
        public XenotypeDef currentStagePlagaXenotype;
        public HediffDef currentPlagaParasiteStage;
        

        public HediffCompProperties_PlagasParasite_Subordinate()
        {
            compClass = typeof(HediffComp_PlagasParasite_Subordinate);
        }
    }
}
