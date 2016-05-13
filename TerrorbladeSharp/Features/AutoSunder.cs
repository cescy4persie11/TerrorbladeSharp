using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrorbladeSharp.Abilities;

namespace TerrorbladeSharp.Features
{
    public class AutoSunder
    {
        private Hero me
        {
            get
            {
                return Variables.Hero;
            }
        }

        private Sunder sunder
        {
            get
            {
                return Variables.Sunder;
            }
        }

        private Hero sunderTarget;


        public AutoSunder()
        {
            
        }

        public void Execute()
        {
            if ((100.0 * me.Health/me.MaximumHealth) > Variables.SunderThreshold) return;
            if (!sunder.CanbeCast()) return; 
            if (me.CanCast())
            {
                FindSunderTarget();
                if (Utils.SleepCheck("sunder"))
                {
                    sunder.UseOn(this.sunderTarget);
                    Utils.Sleep(300, "sunder");
                }
            }
        }

        private void FindSunderTarget()
        {
            var Target = ObjectManager.GetEntities<Hero>().Where(x =>
                                       x.Team != Variables.Hero.Team && x.IsAlive
                                        && x.Distance2D(me) <= 550 + 100)
                                       .OrderByDescending(x => x.Health).FirstOrDefault();
            if (Target == null) return;
            this.sunderTarget = Target;            
        }


    }
}
