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
using TerrorbladeSharp.Abilities;

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

        private AutoArmlet autoArmlet;

        private AutoSunder autoSunder;

        private TargetFind targetFind;

        private DrawText drawText;

        private Hero Target
        {
            get
            {
                return this.targetFind.Target;
            }
        }

        public void OnDraw()
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            drawText.DrawTextCombo(Variables.ComboOn);
            if (Variables.ComboOn)
            {
                this.targetFind.DrawTarget();
            }
            combo.DrawTarget(Target);
        }

        public void OnLoad()
        {
            Variables.Hero = ObjectManager.LocalHero;
            this.pause = Variables.Hero.ClassID != ClassID.CDOTA_Unit_Hero_Terrorblade;
            if (this.pause) return;
            Variables.Hero = ObjectManager.LocalHero;
            Variables.MenuManager = new MenuManager(Me.Name);
            Variables.MenuManager.Menu.AddToMainMenu();
            Variables.EnemyTeam = Me.GetEnemyTeam();
            Variables.Sunder = new Sunder(Me.Spellbook.Spell4);
            Variables.Illusions = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassID.Equals(ClassID.CDOTA_Unit_Hero_Terrorblade)).ToList();
            this.targetFind = new TargetFind();
            this.combo = new Combo();
            this.drawText = new DrawText();
            this.autoArmlet = new AutoArmlet();
            this.autoSunder = new AutoSunder();


            Game.PrintMessage(
                "TerrorbladeSharp" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " loaded",
                MessageType.LogMessage);
        }

        public void OnUpdate_Combo()
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            if (!Variables.ComboOn) return;
            //this.targetFind.Find();
            //Variables.Illusions = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassID.Equals(ClassID.CDOTA_Unit_Hero_Terrorblade) && unit.IsIllusion).ToList();
            
            //if (Target == null) return;
            //if (Illusions == null) return;
            //Orbwalking.Orbwalk(Target, 0, 0, false, true);
            //if (Utils.SleepCheck("attack"))
            //{
                //Me.Attack(Target);
                //Utils.Sleep(800, "attack");
            //}
        }

        public void OnUpdate_AutoArmlet()
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            autoArmlet.Execute();
        }

        public void OnUpdate_AutoSunder()
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            autoSunder.Execute();
        }

        public void Player_OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            autoArmlet.PlayerExecution_Armlet(args);
            if (Target == null) return;
            if (args.Order == Order.AttackTarget)
            {
                this.targetFind.UnlockTarget();
                this.targetFind.Find();
            }
            else
            {
                this.targetFind.UnlockTarget();
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
        }

        public void Event_OnUpdate(EventArgs e)
        {
            if (this.pause || Variables.Hero == null || !Variables.Hero.IsValid || !Variables.Hero.IsAlive)
            {
                return;
            }
            if (!Variables.ComboOn) return;
            Variables.Illusions = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassID.Equals(ClassID.CDOTA_Unit_Hero_Terrorblade) && unit.IsIllusion).ToList();

            if (Illusions == null) return;
            //if (!Variables.ComboOn) return;
            this.targetFind.Find();
            if (Target == null) return;
            this.targetFind.LockTarget();
            combo.SetTarget(Target);
            combo.Events_OnUpdate();
              

        }
    }
}
