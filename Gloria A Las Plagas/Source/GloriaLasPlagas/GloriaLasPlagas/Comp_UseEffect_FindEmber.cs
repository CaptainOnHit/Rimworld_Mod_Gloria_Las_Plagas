using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace CCDevelopment.LasPlagas
{
    public class Comp_UseEffect_FindAmber : CompUseEffect
    {
        const int resourceAmount = 10;

        public CompProperties_UseEffectFindAmber Props => (CompProperties_UseEffectFindAmber)props;
        public override void DoEffect(Pawn usedBy)
        {
            //adopted the logic of CompDeepScanner.DoFind() with some changes to match out needs
            base.DoEffect(usedBy);
            bool anyPlaced = false;

            Map currentMap = usedBy.Map;
            ThingDef deepAmberDef = DefDatabase<ThingDef>.GetNamed("CCDevelopment_LasPlagas_Amber");


            if (!CellFinderLoose.TryFindRandomNotEdgeCellWith(10, (IntVec3 x) => CanScatterAt(x, currentMap), currentMap, out var result))
            {
                Log.Error("Could not find a center cell for deep scanning lump generation!");
                return;
            }

            FieldInfo revealedField = typeof(DeepResourceGrid).GetField("revealed", BindingFlags.NonPublic | BindingFlags.Instance);
            bool[] revealed = revealedField != null 
                ? (bool[])revealedField.GetValue(currentMap.deepResourceGrid)
                : null;



            int numCells = Mathf.CeilToInt(deepAmberDef.deepLumpSizeRange.RandomInRange);
            foreach (IntVec3 item in GridShapeMaker.IrregularLump(result, currentMap, numCells))
            {
                if (CanScatterAt(item, currentMap) && !item.InNoBuildEdgeArea(currentMap))
                {
                    currentMap.deepResourceGrid.SetAt(item, deepAmberDef, resourceAmount);

                    if (revealed != null)
                    {
                        int index = CellIndicesUtility.CellToIndex(item, currentMap.Size.x);
                        revealed[index] = true;
                    }
                        
                    anyPlaced = true;


                }
            }

            currentMap.mapDrawer.MapMeshDirty(result, MapMeshFlagDefOf.Things);

            if (anyPlaced)
            {
                Messages.Message(
                     "EmberScanner_DepositFound",
                     new TargetInfo(result, currentMap),
                     MessageTypeDefOf.PositiveEvent
                   );
            }
            else
            {
                Messages.Message(
                    "EmberScanner_NoneFound".Translate(),
                    usedBy,
                    MessageTypeDefOf.NeutralEvent
                    );
            }
            
        }

        private bool CanScatterAt(IntVec3 pos, Map map)
        {
            //taken from the CompDeepScanner Rimworld class since it is the same logic
            int index = CellIndicesUtility.CellToIndex(pos, map.Size.x);
            TerrainDef terrainDef = map.terrainGrid.BaseTerrainAt(pos);
            if ((terrainDef != null && terrainDef.IsWater && terrainDef.passability == Traversability.Impassable) || !pos.GetAffordances(map).Contains(ThingDefOf.DeepDrill.terrainAffordanceNeeded))
            {
                return false;
            }
            return !map.deepResourceGrid.GetCellBool(index);
        }

    }
}
