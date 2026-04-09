using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using HarmonyLib;
using RimWorld;

namespace CCDevelopment.LasPlagas
{
    [HarmonyPatch(typeof(BodyPartDef), "GetMaxHealth")]
    public class HarmonyHPPatch
    {
        public static void Postfix(Pawn pawn, ref float __result, BodyPartDef __instance)
        {
            if (!(pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Guadana" || pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Mandibula" || pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Superior")) return;
            if (__instance == BodyPartDefOf.Head && pawn.genes?.Xenotype?.defName != "CCDevelopment_LasPlagas_Superior")
            {
                __result = __result * 3;
                return;
            }

            if (hasHediff(pawn, "CCDevelopment_LasPlagas_ClawArm") || hasHediff(pawn, "CCDevelopment_LasPlagas_BladeArm"))
            {
                if (__instance == BodyPartDefOf.Shoulder)
                {
                    __result = __result * 4;
                }
                return;
            }
            return;
        }

        private static bool hasHediff(Pawn pawn, string hediffName)
        {
            HediffDef hediffDef = HediffDef.Named(hediffName);
            bool hasHediff = pawn.health.hediffSet.HasHediff(hediffDef);
            return hasHediff;
        }
    }
}
