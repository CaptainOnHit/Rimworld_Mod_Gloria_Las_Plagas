using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.Code;

namespace CCDevelopment.LasPlagas
{
    public class RecipeWorker_StopParasite : Recipe_Surgery
    {
        List<HediffDef> xenotypeHediffs = DefNameHelper.XenotypeHediffs.Take(2).ToList();


        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            //check whether the pawn has the parasite in any form
            if(!base.AvailableOnNow(thing, part)) return base.AvailableOnNow(thing, part);


            Pawn pawn = thing as Pawn;

            if (pawn == null) return false;

            if(pawn.health.hediffSet.HasHediff(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Progress_Stopped"))) return false;


            foreach(HediffDef hediff in xenotypeHediffs)
            {
                if (pawn.health.hediffSet.HasHediff(hediff, false))
                {
                    return true;
                }
            }
            return false;
        }   

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
            }

            pawn.health.AddHediff(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Progress_Stopped"));
        }
    }
}
