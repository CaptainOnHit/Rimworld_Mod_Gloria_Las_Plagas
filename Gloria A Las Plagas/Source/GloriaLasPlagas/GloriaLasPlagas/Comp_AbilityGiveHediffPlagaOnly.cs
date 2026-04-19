using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static UnityEngine.GraphicsBuffer;

namespace CCDevelopment.LasPlagas
{
    public class Comp_AbilityGiveHediffPlagaOnly : CompAbilityEffect_GiveHediff
    {
        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (!DefNameHelper.ValidatePlagaXenotype(target.Pawn, true)) return false;
            return base.CanApplyOn(target, dest);
        }
    }
}
