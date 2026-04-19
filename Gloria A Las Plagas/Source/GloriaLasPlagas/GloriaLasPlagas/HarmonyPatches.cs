using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using HarmonyLib;

namespace CCDevelopment.LasPlagas
{
    [StaticConstructorOnStartup]
    public class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("net.glorialasplagas.rimworld.ccdevelopment");
            harmony.PatchAll();
        }

    }
}
