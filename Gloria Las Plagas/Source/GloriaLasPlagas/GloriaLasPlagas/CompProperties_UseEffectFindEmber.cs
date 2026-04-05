using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace CCDevelopment.LasPlagas
{
    public class CompProperties_UseEffectFindAmber : CompProperties_UseEffect 
    {
        public CompProperties_UseEffectFindAmber()
        {
            compClass = typeof(Comp_UseEffect_FindAmber);
        }
    }
}
