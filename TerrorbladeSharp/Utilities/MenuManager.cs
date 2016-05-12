using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage.Common.Menu;
using SharpDX;

namespace TerrorbladeSharp.Utilities
{
    public class MenuManager
    {
        public Menu Menu { get; private set; }

        public readonly MenuItem ComboMenu;

        public MenuManager(string heroName)
        {
            this.Menu = new Menu("TerrorbladeSharp", "TerrorbladeSharp", true, heroName, true);
            this.ComboMenu = new MenuItem("ComboMenu", "ComboMenu").SetValue(new KeyBind('D', KeyBindType.Press));
            this.Menu.AddItem(this.ComboMenu);
        }

        public bool ComboOn
        {
            get
            {
                return this.ComboMenu.GetValue<KeyBind>().Active;
            }
        }
    }
}
