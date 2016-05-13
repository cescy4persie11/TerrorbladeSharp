using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage;
using Ensage.Common;

namespace TerrorbladeSharp
{
    public class Bootstrap
    {
        private readonly TerrorbladeSharp terrorbladeSharp;

        public Bootstrap()
        {
            this.terrorbladeSharp = new TerrorbladeSharp();
        }

        public void SubscribeEvents()
        {
            Events.OnLoad += this.Events_Onload;
            Events.OnUpdate += this.Events_Update;
            Events.OnClose += this.Events_OnClose;
            Game.OnUpdate += this.Game_OnUpdate;
            //Game.OnWndProc += this.Game_OnWndProc;
            Drawing.OnDraw += this.Drawing_OnDraw;
            Player.OnExecuteOrder += this.Player_OnExecuteOrder;
        }

        private void Events_Update(EventArgs args)
        {
            this.terrorbladeSharp.Event_OnUpdate(args);
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            this.terrorbladeSharp.OnDraw();
        }

        private void Events_Onload(object sender, EventArgs e)
        {
            this.terrorbladeSharp.OnLoad();
        }

        private void Events_OnClose(object sender, EventArgs e)
        {
            this.terrorbladeSharp.OnClose();
        }

        private void Game_OnUpdate(EventArgs args)
        {
            this.terrorbladeSharp.OnUpdate_Combo();
            this.terrorbladeSharp.OnUpdate_AutoArmlet();
            this.terrorbladeSharp.OnUpdate_AutoSunder();
        }

        private void Game_OnWndProc(WndEventArgs args)
        {
            //this.visageSharp.OnWndProc(args);
        }

        private void Player_OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (sender.Equals(ObjectManager.LocalPlayer))
            {
                this.terrorbladeSharp.Player_OnExecuteOrder(sender, args);
            }
        }

    }
}
