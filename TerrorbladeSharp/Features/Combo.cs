using Ensage;
using Ensage.Common.Objects.UtilityObjects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrorbladeSharp.Features
{   
    public class Combo
    {
        private static Dictionary<float, Orbwalker> orbwalkerDictionary = new Dictionary<float, Orbwalker>();

        private Hero target;

        private List<Unit> Illusions
        {
            get
            {
                return Variables.Illusions;
            }
        }

        public Combo()
        {

        }

        public void SetTarget(Hero target)
        {
            this.target = target;
        }

        public void Events_OnUpdate()
        {
            if (ObjectManager.LocalHero == null) return;
            foreach(var i in Illusions)
            {
                Orbwalker orbwalker;
                if (!orbwalkerDictionary.TryGetValue(i.Handle, out orbwalker))
                {
                    orbwalker = new Orbwalker(i);
                    orbwalkerDictionary.Add(i.Handle, orbwalker);
                }
                orbwalker.OrbwalkOn(target, 0, 0, false, true);
            }
        }

        public void DrawTarget(Hero target)
        {
            var textPos = new Vector2(Convert.ToSingle(Drawing.Width) - 130, Convert.ToSingle(Drawing.Height * 0.67));
            var text = "Attack Target:";
            Drawing.DrawText(text, textPos, new Vector2(20), Color.Yellow, FontFlags.AntiAlias);
            if (target == null) return;
            var startPos = new Vector2(Convert.ToSingle(Drawing.Width) - 110, Convert.ToSingle(Drawing.Height * 0.7));
            var name = "materials/ensage_ui/heroes_horizontal/" + target.Name.Replace("npc_dota_hero_", "") + ".vmat";
            var size = new Vector2(50, 50);
            Drawing.DrawRect(startPos, size + new Vector2(13, -6),
                Drawing.GetTexture(name));
            Drawing.DrawRect(startPos, size + new Vector2(14, -5),
                                    new Color(0, 0, 0, 255), true);
        }
    }
}
