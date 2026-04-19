using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffCompProperties_GiveHediffsInRangePlagaOnly : HediffCompProperties_GiveHediffsInRange
    {
        public HediffCompProperties_GiveHediffsInRangePlagaOnly()
        {
            compClass = typeof(HediffComp_GiveHediffsInRangePlagaOnly);
        }
    }
}
