using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffComp_LasPlagasTransform_Superior : HediffComp
    {


        public HediffCompProperties_LasPlagasTransform_Superior Props
        {
            get { return (HediffCompProperties_LasPlagasTransform_Superior)props; }
        }
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (Pawn != null && parent.Severity >= 1f)
            {
                HediffDef newHediff = HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier");

                Messages.Message(
                    Pawn.LabelShort + " has been fully consumed by Las Plagas!",
                    Pawn, MessageTypeDefOf.NeutralEvent
                );
                Pawn.health.AddHediff(newHediff);
                Pawn.health.RemoveHediff(parent);

            }
        }
    }
}
