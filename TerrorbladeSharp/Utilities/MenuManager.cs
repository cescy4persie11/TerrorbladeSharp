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

        public readonly MenuItem ArmletThresholdMenu;

        public readonly MenuItem SunderThresholdMenu;

        public MenuManager(string heroName)
        {
            this.Menu = new Menu("TerrorbladeSharp", "TerrorbladeSharp", true, heroName, true);
            this.ComboMenu = new MenuItem("ComboMenu", "ComboMenu").SetValue(new KeyBind('D', KeyBindType.Press));
            //this.ArmletMenu = new Menu("ArmletMenu", "Armlet Menu settings");
            this.ArmletThresholdMenu = new MenuItem("ArmletThreshold", "Armlet Threshold").SetValue(new Slider(150, 0, 500)).SetTooltip("toggle when HP is belw");
            this.SunderThresholdMenu = new MenuItem("SunderThresholdMenu", "Sunder Threshold").SetValue(new Slider(25, 0, 100)).SetTooltip("Auto Sunder when HP is below %");
            this.Menu.AddItem(SunderThresholdMenu);
            this.Menu.AddItem(ArmletThresholdMenu);
            this.Menu.AddItem(this.ComboMenu);

        }

        public bool ComboOn
        {
            get
            {
                return this.ComboMenu.GetValue<KeyBind>().Active;
            }
        }

        public int HP
        {
            get
            {
                return this.ArmletThresholdMenu.GetValue<Slider>().Value;
            }
        }

        public int HealthPerc
        {
            get
            {
                return this.SunderThresholdMenu.GetValue<Slider>().Value;
            }
        }
    }
}
