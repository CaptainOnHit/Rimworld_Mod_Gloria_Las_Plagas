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
    public class HarmonyHeadHPPatch
    {
        public static void Postfix(Pawn pawn, ref float __result, BodyPartDef __instance)
        {
            if (!(pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Guadana" || pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Mandibula")) return;
            if (!(__instance == BodyPartDefOf.Head)) return;
            __result = __result * 3;
            return;
        }
    }
}
