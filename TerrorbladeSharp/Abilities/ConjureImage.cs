using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Objects;
using Ensage.Common.Objects.UtilityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrorbladeSharp.Features;

namespace TerrorbladeSharp.Abilities
{
    public class ConjureImage
    {
        private Hero me
        {
            get
            {
                return Variables.Hero;
            }
        }

        private Ability ability;       

        private ArmletToggler armletToggler;

        public ConjureImage(Ability ability)
        {
            this.ability = ability;
        }

        


    }
}
