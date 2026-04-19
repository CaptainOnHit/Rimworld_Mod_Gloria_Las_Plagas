using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace CCDevelopment.LasPlagas
{
    public static class DefNameHelper
    {
        static public List<HediffDef> InfectionHediffs { get; } = new List<HediffDef>();
        static public List<HediffDef> XenotypeHediffs { get; } = new List<HediffDef>();
        static public List<HediffDef> LimbMutationHediffs { get; } = new List<HediffDef>();
        static public List<XenotypeDef> XenotypeDefs { get; } = new List<XenotypeDef>();
        static DefNameHelper()
        {
            fillHediffLists();
        }

        static private void fillHediffLists()
        {
            for (int i = 0; i < 3; i++)
            {
                XenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Subordinate_Tier" + i));
            }
            XenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Superior_Tier"));
            XenotypeHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_LasPlagasParasite_Dominant_Tier"));

            InfectionHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Superior"));
            InfectionHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Subordinate"));
            InfectionHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_Infection_Dominant"));

            LimbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_GuadanaHead"));
            LimbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_MandibulaHead"));
            LimbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_ClawArm"));
            LimbMutationHediffs.Add(HediffDef.Named("CCDevelopment_LasPlagas_BladeArm"));

            XenotypeDefs.Add(DefDatabase<XenotypeDef>.GetNamed("CCDevelopment_LasPlagas_Ganado"));
            XenotypeDefs.Add(DefDatabase<XenotypeDef>.GetNamed("CCDevelopment_LasPlagas_Guadana"));
            XenotypeDefs.Add(DefDatabase<XenotypeDef>.GetNamed("CCDevelopment_LasPlagas_Mandibula"));
            XenotypeDefs.Add(DefDatabase<XenotypeDef>.GetNamed("CCDevelopment_LasPlagas_Superior"));
            XenotypeDefs.Add(DefDatabase<XenotypeDef>.GetNamed("CCDevelopment_LasPlagas_Dominant"));
        }
        static public bool ValidatePlagaXenotype(Pawn targetPawn, bool showMessages = true)
        {
            string xenotypDef = targetPawn.genes?.Xenotype.defName;

            foreach (var item in XenotypeDefs)
            {
                if (item.defName == xenotypDef) return true;
            }
            if (showMessages)
            {
                Messages.Message("Target does not host a Plaga Parasite!", MessageTypeDefOf.RejectInput);
            }
            return false;
        }
    }
}
