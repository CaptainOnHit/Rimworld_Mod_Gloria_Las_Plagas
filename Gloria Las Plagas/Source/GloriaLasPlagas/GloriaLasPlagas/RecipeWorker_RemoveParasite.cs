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
    public class RecipeWorker_RemoveParasite : Recipe_Surgery
    {
        List<HediffDef> requiredHediffs = new List<HediffDef>();


        public RecipeWorker_RemoveParasite() 
        {
            fillRequiredHediffs();
        }

        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            //check whether the pawn has the parasite in any form
            if(!base.AvailableOnNow(thing, part)) return base.AvailableOnNow(thing, part);


            Pawn pawn = thing as Pawn;

            if (pawn == null) return false;


            foreach(HediffDef hediff in requiredHediffs)
            {
                if (pawn.health.hediffSet.HasHediff(hediff, false))
                {
                    return true;
                }
            }
            return false;
        }   
        private void fillRequiredHediffs()
        {
            for(int i = 0; i < 3; i++)
            {
                requiredHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier" + i));
            }
            requiredHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier"));
            requiredHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Superior"));
            requiredHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Subordinate"));

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
            foreach (HediffDef hediff in requiredHediffs)
            {
                if (pawn.health.hediffSet.HasHediff(hediff, false))
                {
                    Hediff removeHediff = pawn.health.hediffSet.GetFirstHediffOfDef(hediff);
                    pawn.health.RemoveHediff(removeHediff);
                    pawn.genes?.SetXenotype(XenotypeDefOf.Baseliner);
                    Random rand = new Random();
                    float decisionValue = (float)rand.NextDouble();
                    switch (hediff.defName)
                    {
                        case "CCDevelopment_LasPlagas_Infection_Superior":
                            positiveRemoval(pawn);
                            break;
                        case "CCDevelopment_LasPlagas_Infection_Subordinate":
                            positiveRemoval(pawn);
                            break;
                        case "CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier0":
                            if (decisionValue <= 0.3)
                            {
                                shatterSpine(pawn);
                            }
                            positiveRemoval(pawn);
                            setNewFaction(pawn);
                            break;
                        case "CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier":
                            shatterSpine(pawn);
                            positiveRemoval(pawn);
                            setNewFaction(pawn);
                            break;
                        default:
                            setNewFaction(pawn);
                            negativeRemoval(pawn);
                            break;
                    }
                }
            }


            if (IsViolationOnPawn(pawn, part, Faction.OfPlayerSilentFail))
            {
                ReportViolation(pawn, billDoer, pawn.HomeFaction, -10);
            }
        }
        public void setNewFaction(Pawn pawn)
        {
            Random rand = new Random();
            float decisionValue = (float)rand.NextDouble();
            if (decisionValue <= 0.5)
            {
                pawn.SetFaction(Find.FactionManager.RandomNonHostileFaction());
            }
            else {
                pawn.SetFaction(Find.FactionManager.RandomEnemyFaction());
            }

        }
        public void shatterSpine(Pawn pawn)
        {
            BodyPartDef spineDef = DefDatabase<BodyPartDef>.GetNamedSilentFail("Spine");
            BodyPartRecord spine = pawn.RaceProps.body.GetPartsWithDef(spineDef).FirstOrDefault();
            Hediff shatteredSpine = HediffMaker.MakeHediff(HediffDefOf.Bite, pawn, spine);
            shatteredSpine.Severity = 9999f;
            pawn.health.AddHediff(shatteredSpine, spine);
        }
        public void negativeRemoval(Pawn pawn)
        {
            BodyPartRecord head = pawn.RaceProps.body.GetPartsWithDef(BodyPartDefOf.Head).FirstOrDefault();
            Hediff missingHead = HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, head);
            pawn.health.AddHediff(missingHead, head);
            Messages.Message(
                pawn.NameShortColored + "'s parasite has been removed... together with their head.",
                pawn,
                MessageTypeDefOf.NegativeEvent);
        }
        public void positiveRemoval(Pawn pawn)
        {
            Messages.Message(
                                pawn.NameShortColored + " has recovered from the plaga parasite.",
                                pawn,
                                MessageTypeDefOf.PositiveEvent);
        }
    }
}
