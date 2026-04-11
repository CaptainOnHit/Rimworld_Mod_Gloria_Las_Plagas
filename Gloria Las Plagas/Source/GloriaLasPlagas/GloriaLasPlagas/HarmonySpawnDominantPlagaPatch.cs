using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    [HarmonyPatch]
    public class HarmonySpawnDominantPlagaPatch
    {
        static int stepsTaken = 0;
        static int stepsPerMap = 10;
        static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(GenStep_Settlement),
                nameof(GenStep_Settlement.Generate),
                new[] { typeof(Map), typeof(GenStepParams) }
            );
        }
        public static void Postfix(GenStep_Scatterer __instance, Map map, GenStepParams parms)
        {
            if (map.ParentFaction?.def?.defName != "CCDevelopment_LasPlagas_LosIluminados") return;
            if(stepsTaken++ %  stepsPerMap != 0) return;

            IntVec3 spawnPos = map.Center;
            Thing item = ThingMaker.MakeThing(ThingDef.Named("CCDevelopment_LasPlagas_Embryo_Dominant"));
            item.stackCount = 2;

            GenPlace.TryPlaceThing(item, spawnPos, map, ThingPlaceMode.Near);    
        }
    }
}
