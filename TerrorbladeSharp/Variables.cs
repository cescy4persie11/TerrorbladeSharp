using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage;

using global::TerrorbladeSharp.Abilities;
using global::TerrorbladeSharp.Utilities;
using TerrorbladeSharp.Features;

namespace TerrorbladeSharp
{
    public class Variables
    {


        public static bool ComboOn
        {
            get
            {
                return MenuManager.ComboOn;
            }
        }

        public static Hero Hero { get; set; }

        public static List<Unit> Illusions { get; set; }

        public static Reflection Reflection;

        public static ConjureImage ConjureImage;

        public static Metamorphosis Metamorphsis;

        public static Sunder Sunder;

        public static Team EnemyTeam { get; set; }

        public static MenuManager MenuManager { get; set; }
        
        public static PowerTreadsSwitcher PowerTreadsSwitcher { get; set; }

        public static float TickCount
        {
            get
            {
                return Environment.TickCount & int.MaxValue;
            }
        }
    }
}
