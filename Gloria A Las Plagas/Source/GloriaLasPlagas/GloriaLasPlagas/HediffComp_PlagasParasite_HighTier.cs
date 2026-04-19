using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class HediffComp_PlagasParasite_HighTier : HediffComp
    {
        public HediffCompProperties_PlagasParasite_HighTier Props
        {
            get { return (HediffCompProperties_PlagasParasite_HighTier)props; }
        }
        public override void CompPostMake()
        {
            if (Pawn.genes.Xenotype.defName != Props.currentStagePlagaXenotype.defName)
            {
                Pawn.genes?.SetXenotype(Props.currentStagePlagaXenotype);
            }
        }
    }
}
