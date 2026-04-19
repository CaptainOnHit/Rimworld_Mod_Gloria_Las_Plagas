using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static UnityEngine.GraphicsBuffer;

namespace CCDevelopment.LasPlagas
{
    public class Comp_AbilityPlagaConvert : CompAbilityEffect_Convert
    {
        public CompProperties_AbilityPlagaConvert Props => (CompProperties_AbilityPlagaConvert)props;

        public override  bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (!base.Valid(target, throwMessages)) return false;

            return DefNameHelper.ValidatePlagaXenotype(target.Pawn, throwMessages);
        }
    }
}
