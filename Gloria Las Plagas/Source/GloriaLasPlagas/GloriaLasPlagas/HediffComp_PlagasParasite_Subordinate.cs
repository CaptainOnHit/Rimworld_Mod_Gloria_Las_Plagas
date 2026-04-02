using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffComp_PlagasParasite_Subordinate : HediffComp
    {
        private const int daysUntilNextTier = 1;
        private const int ticksPerDay = 10000;
        private int nextTierTick;


        public HediffCompProperties_PlagasParasite_Subordinate Props
        {
            get { return (HediffCompProperties_PlagasParasite_Subordinate)props; }
        }
        public override void CompPostMake()
        {
            Pawn.genes?.SetXenotype(Props.currentStagePlagaXenotype);
            nextTierTick = Find.TickManager.TicksGame + (daysUntilNextTier * ticksPerDay);
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if(Props.currentPlagaParasiteStage.defName != "CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier3")
            {
                if (Find.TickManager.TicksGame > nextTierTick)
                {
                    AdvanceTier();
                }
            }
                
        }

        private void AdvanceTier()
        {            
            const String plagaParasiteSubBaseName = "CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier";
            String newHediffName = "";
            switch (Props.currentPlagaParasiteStage.defName)
            {
                case plagaParasiteSubBaseName + "0":
                    newHediffName = plagaParasiteSubBaseName + "1";
                    break;
                case plagaParasiteSubBaseName + "1":
                    newHediffName = plagaParasiteSubBaseName + "2";
                    break;
                case plagaParasiteSubBaseName + "2":
                    newHediffName = plagaParasiteSubBaseName + "3";
                    break;
            }
            HediffDef newHediff = HediffDef.Named(newHediffName);
            Messages.Message(
                Pawn.LabelShort + " has reached the next Plaga Parasite tier!",
                Pawn, MessageTypeDefOf.NeutralEvent
            );
            Pawn.health.AddHediff(newHediff);
            Pawn.health.RemoveHediff(parent);
        }
    }
   
}
