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
        private const int daysUntilNextTier = 7;
        private const int ticksPerDay = 60000;
        private int nextTierTick;


        public HediffCompProperties_PlagasParasite_Subordinate Props
        {
            get { return (HediffCompProperties_PlagasParasite_Subordinate)props; }
        }
        public override void CompPostMake()
        {
            if (Pawn.genes.Xenotype.defName != Props.currentStagePlagaXenotype.defName)
            {
                Pawn.genes?.SetXenotype(Props.currentStagePlagaXenotype);
            }
            nextTierTick = Find.TickManager.TicksGame + (daysUntilNextTier * ticksPerDay);
            //only drop Headwear if Guadana or Mandibula
            if (Props.currentPlagaParasiteStage.defName == "CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier0") return;
            var apparelList = Pawn.apparel.WornApparel.Where(apperal => apperal.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.UpperHead) || apperal.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead)).ToList();
            foreach (var apparel in apparelList)
            {
                Pawn.apparel.Remove(apparel);
                if (Pawn.Map == null) continue;
                GenPlace.TryPlaceThing(apparel, Pawn.Position, Pawn.Map, ThingPlaceMode.Near);
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if(Props.currentPlagaParasiteStage.defName != "CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier2")
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
                    Hediff guadanaHead = Pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("CCDevelopment_LasPlagas_GuadanaHead"));
                    if (guadanaHead != null)
                    {
                        Pawn.health.hediffSet.hediffs.Remove(guadanaHead);
                    }
                    newHediffName = plagaParasiteSubBaseName + "2";
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
