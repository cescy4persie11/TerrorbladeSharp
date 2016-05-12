using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;
using Ensage;
using TerrorbladeSharp.Features;
using TerrorbladeSharp.Utilities;

namespace TerrorbladeSharp
{
    public class TerrorbladeSharp
    {
        public TerrorbladeSharp()
        {
            this.combo = new Combo();
        }

        private static Hero Me
        {
            get
            {
                return Variables.Hero;
            }
        }

        private static List<Unit> Illusions
        {
            get
            {
                return Variables.Illusions;
            }
        }

        private bool pause;

        private Combo combo;

        private ArmletToggler armToggler;

        private TargetFind targetFind;

        private Hero Target
        {
            get
            {
                return this.targetFind.Target;
            }
        }

        public void Draw()
        {
            if (Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
        }

        public void OnLoad()
        {
            Variables.Hero = ObjectManager.LocalHero;
            this.pause = Variables.Hero.ClassID != ClassID.CDOTA_Unit_Hero_Terrorblade;
            if (this.pause) return;
            Variables.MenuManager = new MenuManager(Me.Name);
            Variables.MenuManager.Menu.AddToMainMenu();
            Variables.EnemyTeam = Me.GetEnemyTeam();
            Variables.Illusions = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassID.Equals(ClassID.CDOTA_Unit_Hero_Terrorblade)).ToList();
            //Variables.Reflection = 
            this.targetFind = new TargetFind();
            this.combo = new Combo();
            
            Game.PrintMessage(
                "TerrorbladeSharp" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " loaded",
                MessageType.LogMessage);
        }

        public void OnUpdate_Combo()
        {
            if (Variables.Hero == null || !Variables.Hero.IsValid || this.pause)
            {
                this.pause = Game.IsPaused;
                return;
            }
            if (!Variables.ComboOn) return;
            this.targetFind.Find();
            Variables.Illusions = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassID.Equals(ClassID.CDOTA_Unit_Hero_Terrorblade) && unit.IsIllusion).ToList();
            
            if (Target == null) return;
            if (Illusions == null) return;
            Orbwalking.Orbwalk(Target, 0, 0, false, true);

        }

        public void Player_OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            if (Target == null) return;
            Console.WriteLine("my Order is " + args.Order);
            if (args.Order == Order.AttackTarget || args.Order == Order.AttackLocation || !Target.IsAlive)
            {
                this.targetFind.UnlockTarget();
                this.targetFind.Find();
                this.targetFind.LockTarget();
            }
        }


        public void OnClose()
        {
            this.pause = true;
            if (Variables.MenuManager != null)
            {
                Variables.MenuManager.Menu.RemoveFromMainMenu();
            }
            Variables.PowerTreadsSwitcher = null;
            Variables.Illusions = null;
        }
    }
}
