using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class Gene_Ganado : Gene
    {
        public override void PostAdd()
        {
            HediffDef xenoTypeHediff = HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier0");
            if (pawn.health.hediffSet.HasHediff(xenoTypeHediff)) return;
            pawn.health.AddHediff(xenoTypeHediff);
        }
    }
}
