using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    [HarmonyPatch(typeof(KidnappedPawnsTracker), "Kidnap")]
    public class HarmonyApplyPlagaHediffPatch
    {
        static List<HediffDef> allPlagaHediffs;
        static void Postfix(Pawn pawn, Pawn kidnapper)
        {

            if(kidnapper == null) return;
            if (pawn == null) return;
            if (kidnapper.Faction?.def?.defName !="CCDevelopment_LasPlagas_LosIluminados") return;
           
            fillPlagaHediffList();
            HediffDef hediffDef = HediffDef.Named("CCDevelopment_LasPlagas_Infection_Subordinate");

            //we dont want to infect someone a second time
            foreach(HediffDef plagaHediffDef in allPlagaHediffs)
            {
                if (pawn.health.hediffSet.HasHediff(plagaHediffDef)) return;
            }

            pawn.health.AddHediff(hediffDef);
        }

        static void fillPlagaHediffList()
        {
            allPlagaHediffs = new List<HediffDef>();
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Subordinate"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Superior"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Dominant"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier0"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier1"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier2"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier"));
            allPlagaHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Dominant_Tier"));
        }
    }
}
