using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;


namespace CCDevelopment.LasPlagas
{
    public class HediffComp_FactionDisassociation : HediffComp
    {
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Pawn != null && parent.Severity >= 1f)
            {
                changeFaction(Pawn);
                Pawn.health.RemoveHediff(parent);
            }
        }

        private void changeFaction(Pawn pawn)
        {
            Random rand = new Random();
            float decisionValue = (float)rand.NextDouble();
            if (decisionValue <= 0.5)
            {
                pawn.SetFaction(Find.FactionManager.RandomNonHostileFaction());
            }
            else
            {
                pawn.SetFaction(Find.FactionManager.RandomEnemyFaction());
            }
        }
    }
}
