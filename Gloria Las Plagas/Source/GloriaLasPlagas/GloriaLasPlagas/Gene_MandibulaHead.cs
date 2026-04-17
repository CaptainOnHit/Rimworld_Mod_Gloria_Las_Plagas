using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class Gene_MandibulaHead : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            HediffDef newHediff = HediffDef.Named("CCDevelopment_LasPlagas_MandibulaHead");
            var head = pawn.health.hediffSet.GetNotMissingParts().FirstOrDefault(part => part.def == BodyPartDefOf.Head);
            pawn.health.AddHediff(newHediff, head);

            HediffDef xenoTypeHediff = HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier2");
            if (pawn.health.hediffSet.HasHediff(xenoTypeHediff)) return;
            pawn.health.AddHediff(xenoTypeHediff);
        }
    }
}
