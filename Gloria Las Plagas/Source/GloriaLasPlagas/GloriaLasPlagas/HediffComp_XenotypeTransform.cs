using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffComp_XenotypeTransform : HediffComp
    {
        private bool transformed = false;

        public HediffCompProperties_XenotypeTransform Props
        {
            get { return (HediffCompProperties_XenotypeTransform)props; }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (!transformed && Pawn != null && parent.Severity >= 1f)
            {
                Random rand = new Random();
                float decisionValue = (float)rand.NextDouble();
                bool decided = false;
                int decidedXenotype = 0;
                float currentValue = 0f;
                int i = 0;
                foreach (float probability in Props.probabilities)
                {
                    currentValue += probability;
                    if (currentValue >= decisionValue && !decided)
                    {
                        decided = true;
                        decidedXenotype = i;
                    }
                    i++;
                }
                transformed = true;
                // Apply the xenotype's genes as xenogenes
                Pawn.genes?.SetXenotype(Props.xenotypes[decidedXenotype]);
                Messages.Message(
                    Pawn.LabelShort + " has been fully consumed by Las Plagas!",
                    Pawn, MessageTypeDefOf.NeutralEvent
                );
                HediffDef newHediff = HediffDef.Named("CCDevelopment_LasPlagas_LasPlagaParasite_Tier0");
                Pawn.health.AddHediff(newHediff);
                Pawn.health.RemoveHediff(parent);
            }
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref transformed, "transformed", false);
        }
    }
}
