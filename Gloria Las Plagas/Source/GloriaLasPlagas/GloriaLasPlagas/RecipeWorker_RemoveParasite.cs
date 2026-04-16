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
        List<HediffDef> xenotypeHediffs = new List<HediffDef>();
        List<HediffDef> limbMutationHediffs = new List<HediffDef>();

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


            foreach(HediffDef hediff in xenotypeHediffs)
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
                xenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier" + i));
            }
            xenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier"));
            xenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Dominant_Tier"));
            xenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Superior"));
            xenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Subordinate"));
            xenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Dominant"));

            limbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_GuadanaHead"));
            limbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_MandibulaHead"));
            limbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_ClawArm"));
            limbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_BladeArm"));

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
            foreach (HediffDef hediff in xenotypeHediffs)
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
                        case "CCDevelopment_LasPlagas_Infection_Dominant":
                            positiveRemoval(pawn);
                            break;
                        case "CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier0":
                            if (decisionValue <= 0.3)
                            {
                                shatterSpine(pawn);
                            }
                            positiveRemoval(pawn);
                            addFactionDisassociation(pawn);
                            break;
                        case "CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier":
                            removeMutatedLimbs(pawn);
                            shatterSpine(pawn);
                            positiveRemoval(pawn);
                            addFactionDisassociation(pawn);
                            break;
                        case "CCDevelopment_LasPlagas_LasPlagasParasite_Dominant_Tier":
                            removeMutatedLimbs(pawn);
                            shatterSpine(pawn);
                            positiveRemoval(pawn);
                            addFactionDisassociation(pawn);
                            break;
                        default:
                            removeMutatedLimbs(pawn);
                            addFactionDisassociation(pawn);
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
        private void addFactionDisassociation(Pawn pawn)
        {
            pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDef.Named("CCDevelopment_LasPlagas_FactionDisassociation"),pawn));
        }
        private void shatterSpine(Pawn pawn)
        {
            BodyPartDef spineDef = DefDatabase<BodyPartDef>.GetNamedSilentFail("Spine");
            BodyPartRecord spine = pawn.RaceProps.body.GetPartsWithDef(spineDef).FirstOrDefault();
            Hediff shatteredSpine = HediffMaker.MakeHediff(HediffDefOf.Bite, pawn, spine);
            shatteredSpine.Severity = 9999f;
            pawn.health.AddHediff(shatteredSpine, spine);
        }
        private void removeMutatedLimbs(Pawn pawn)
        {
            if (pawn.health.hediffSet.HasHediff(limbMutationHediffs[0]) || pawn.health.hediffSet.HasHediff(limbMutationHediffs[1]))
            {
                BodyPartRecord head = pawn.RaceProps.body.GetPartsWithDef(BodyPartDefOf.Head).FirstOrDefault();
                Hediff missingHead = HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, head);
                pawn.health.AddHediff(missingHead, head);
            }
            if (pawn.health.hediffSet.HasHediff(limbMutationHediffs[2]))
            {
                Hediff arm1 = pawn.health.hediffSet.GetFirstHediffOfDef(limbMutationHediffs[2]);
                if (arm1 != null)
                {
                    pawn.health.RemoveHediff(arm1);
                }
                Hediff arm2 = pawn.health.hediffSet.GetFirstHediffOfDef(limbMutationHediffs[2]);
                if (arm2 != null)
                {
                    pawn.health.RemoveHediff(arm2);
                }
            }
            if (pawn.health.hediffSet.HasHediff(limbMutationHediffs[3]))
            {
                Hediff arm1 = pawn.health.hediffSet.GetFirstHediffOfDef(limbMutationHediffs[3]);
                if (arm1 != null)
                {
                    pawn.health.RemoveHediff(arm1);
                }
                Hediff arm2 = pawn.health.hediffSet.GetFirstHediffOfDef(limbMutationHediffs[3]);
                if (arm2 != null)
                {
                    pawn.health.RemoveHediff(arm2);
                }
            }
        }
        private void negativeRemoval(Pawn pawn)
        {
            Messages.Message(
                pawn.NameShortColored + "'s parasite has been removed... together with their head.",
                pawn,
                MessageTypeDefOf.NegativeEvent);
        }
        private void positiveRemoval(Pawn pawn)
        {
            Messages.Message(
                                pawn.NameShortColored + " has recovered from the plaga parasite.",
                                pawn,
                                MessageTypeDefOf.PositiveEvent);
        }
    }
}
