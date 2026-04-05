using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    [HarmonyPatch]
    public static class ForbidHatPatch
    {



        static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(EquipmentUtility),
                nameof(EquipmentUtility.CanEquip),
                new[] { typeof(Thing), typeof(Pawn), typeof(string).MakeByRefType(), typeof(bool) }
            );
        }
        public static void Postfix(Thing thing, ref Pawn pawn, ref string cantReason, bool checkBonded, ref bool __result)
        {
   
            
            if (!(pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Guadana" || pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Mandibula")) return;
            if (!(thing is Apparel apparel)) return;
            if (!(apparel.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead) ||apparel.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.UpperHead))) return;



            cantReason = "This xenotype can't wear headwear!";
            __result = false;
            return;
        }
    }
}
