using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class Gene_GuanadaHead : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            HediffDef newHediff = HediffDef.Named("CCDevelopment_LasPlagas_GuadanaHead");
            var head = pawn.health.hediffSet.GetNotMissingParts().FirstOrDefault(part => part.def == BodyPartDefOf.Head);
            pawn.health.AddHediff(newHediff, head);
        }
    }
}
