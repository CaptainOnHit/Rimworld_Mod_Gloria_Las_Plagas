using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffComp_LasPlagasTransform : HediffComp
    {
        public HediffCompProperties_LasPlagasTransform Props
        {
            get { return (HediffCompProperties_LasPlagasTransform)props; }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (Pawn != null && parent.Severity >= 1f)
            {
                Random rand = new Random();
                float decisionValue = (float)rand.NextDouble();
                bool decided = false;
                int decidedPlagaStage = 0;
                float currentValue = 0f;
                int i = 0;
                foreach (float probability in Props.probabilities)
                {
                    currentValue += probability;
                    if (currentValue >= decisionValue && !decided)
                    {
                        decided = true;
                        decidedPlagaStage = i;
                    }
                    i++;
                }
                Messages.Message(
                    Pawn.LabelShort + " has been fully consumed by Las Plagas!",
                    Pawn, MessageTypeDefOf.NeutralEvent
                );
                HediffDef newHediff = Props.plagaStages[decidedPlagaStage];
                Pawn.health.AddHediff(newHediff);
                Pawn.health.RemoveHediff(parent);
            }
        }
    }
}
