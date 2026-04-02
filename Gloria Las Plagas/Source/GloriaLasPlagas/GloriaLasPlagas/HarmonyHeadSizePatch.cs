using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;



namespace CCDevelopment.LasPlagas
{
    [HarmonyPatch(typeof(PawnRenderTree), "TryGetMatrix")]
    public static class HeadSizePatch
    {
        public static void Postfix(PawnRenderNode node, ref Matrix4x4 matrix)
        {
            if (!(node is PawnRenderNode_Head))
                return;

            Pawn pawn = node.tree?.pawn;
            if (pawn == null) return;

            if (!(pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Guadana" || pawn.genes?.Xenotype?.defName == "CCDevelopment_LasPlagas_Mandibula")) return;
           
            float scale = 1.6f;
            Vector3 pos = matrix.GetColumn(3); //keep positions
            var rotation = matrix.rotation;
            var headOffset = rotation *  new Vector3(0, 0, 0.35f);
            pos += headOffset;
            matrix = Matrix4x4.TRS(
                pos,
                rotation,
                matrix.lossyScale * scale
                );
        }
    }
}
