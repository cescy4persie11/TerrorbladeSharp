using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrorbladeSharp.Abilities;

namespace TerrorbladeSharp.Features
{
    public class AutoArmlet
    {
        private Hero me
        {
            get
            {
                return Variables.Hero;
            }
        }

        private bool hasArmlet()
        {
            return me.Inventory.Items.Any(x => x.ClassID.Equals(ClassID.CDOTA_Item_Armlet));
        }

        private ArmletToggler armletToggler;       

        private int thresholdHP
        {
            get
            {
                return Variables.ArmletThreshold;
            }
        }

        public AutoArmlet()
        {
        }

        public void Update()
        {
            if (!hasArmlet()) return;
            this.armletToggler = new ArmletToggler(me.FindItem("item_armlet"));
        }

        public void Execute()
        {
            if (!hasArmlet()) return;
            this.armletToggler = new ArmletToggler(me.FindItem("item_armlet"));
            if (me.IsAttacking() && Heroes.GetByTeam(Variables.EnemyTeam)
                                .Any(x => x.IsValid && x.IsAlive && x.IsVisible
                                 && x.Distance2D(Variables.Hero) < x.GetAttackRange() + 200))
            {
                if (Utils.SleepCheck("TurnOn"))
                {
                    armletToggler.TurnOn();
                    Utils.Sleep(1000, "TurnOn");
                }
            }
            if (me.Health > thresholdHP) return;
            if (armletToggler.CanToggle)
            {
                if (Utils.SleepCheck("toggle"))
                {
                    armletToggler.Toggle();
                    Utils.Sleep(1000, "toggle");
                }
            }
        }

        public void PlayerExecution_Armlet(ExecuteOrderEventArgs args)
        {
            var spell = args.Ability;
            var conjure = me.Spellbook.Spell2;
            if (args.Order == Order.Ability)
            {
                if (args.Ability == conjure)
                {
                    if (!hasArmlet()) return;
                    Update();
                    if (!conjure.CanBeCasted()) return;
                    if (Variables.Hero.HasModifier("modifier_item_armlet_unholy_strength")) return;
                    args.Process = false;
                    if (armletToggler.CanToggle)
                    {
                        if (Utils.SleepCheck("TurnOn"))
                        {
                            armletToggler.TurnOn();
                            spell.UseAbility();
                            Utils.Sleep(100, "TurnOn");
                        }
                        DelayAction.Add(500, () =>
                        {
                            if (!me.IsAttacking() &&
                            !Heroes.GetByTeam(Variables.EnemyTeam)
                            .Any(x => x.IsValid && x.IsAlive && x.IsVisible
                             && x.Distance2D(Variables.Hero) < x.GetAttackRange() + 200))
                            {
                                if (Utils.SleepCheck("TurnOff"))
                                {
                                    armletToggler.TurnOff();
                                    Utils.Sleep(100, "TurnOff");
                                }
                            }
                        });
                    }

                }
            }

        }
    }
}
