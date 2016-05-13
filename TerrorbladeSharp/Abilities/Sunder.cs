using Ensage;
using Ensage.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrorbladeSharp.Abilities
{
    public class Sunder
    {
        private Ability ability;

        public Sunder(Ability ability)
        {
            this.ability = ability;
        }

        public bool CanbeCast()
        {
            return this.ability.CanBeCasted();
        }

        public void UseOn(Hero target)
        {
            if (target == null) return;
            this.ability.UseAbility(target);
        }
    }
}
