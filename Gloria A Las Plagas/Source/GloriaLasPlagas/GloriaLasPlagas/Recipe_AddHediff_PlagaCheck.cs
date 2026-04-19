using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CCDevelopment.LasPlagas
{
    public class Recipe_AddHediff_PlagaCheck : Recipe_AddHediff
    {
        protected List<String> filteredXenotypes = new List<String>();
        
        public Recipe_AddHediff_PlagaCheck() 
        { 
            fillFilteredXenotypes();
        }
        
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!base.AvailableOnNow(thing, part))return false;
            Pawn pawn = thing as Pawn;
            if (pawn == null) return false;
            String xenotypeName = pawn.genes?.Xenotype?.defName;
            foreach(String xenotype in filteredXenotypes)
            {
                if(xenotype == xenotypeName) return false;
            }
            return true;
        }

        public void fillFilteredXenotypes()
        {
            filteredXenotypes.Add("CCDevelopment_LasPlagas_Ganado");
            filteredXenotypes.Add("CCDevelopment_LasPlagas_Guadana");
            filteredXenotypes.Add("CCDevelopment_LasPlagas_Mandibula");
            filteredXenotypes.Add("CCDevelopment_LasPlagas_Superior");
        }
    }
}
