using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common.Objects;
using Ensage.Common.Objects.UtilityObjects;

namespace TerrorbladeSharp.Features
{
    public class ArmletToggler
    {
        public ArmletToggler(Item armlet)
        {
            this.Armlet = armlet;
            this.Sleeper = new Sleeper();
        }

        public Item Armlet { get; set; }

        public bool CanToggle
        {
            get
            {
                return this.Armlet.IsValid && !this.Sleeper.Sleeping;
                      // && Variables.Hero.Health <= Variables.MenuManager.ArmletHpTreshold;
            }
        }

        public Sleeper Sleeper { get; private set; }

        public void Toggle()
        {
            if (!Variables.Hero.CanUseItems())
            {
                return;
            }

            if (
                !Heroes.GetByTeam(Variables.EnemyTeam)
                     .Any(
                         x =>
                         x.IsValid && x.IsAlive && x.IsVisible
                         && x.Distance2D(Variables.Hero) < x.GetAttackRange() + 200)
                && !Variables.Hero.HasModifiers(
                    new[]
                        {
                            "modifier_axe_battle_hunger", "modifier_queenofpain_shadow_strike",
                            "modifier_phoenix_fire_spirit_burn", "modifier_venomancer_poison_nova",
                            "modifier_venomancer_venomous_gale", "modifier_huskar_burning_spear_debuff",
                            "modifier_item_urn_damage"
                        },
                    false))
            {
                return;
            }

            if (Variables.Hero.HasModifier("modifier_item_armlet_unholy_strength") || this.Armlet.IsToggled)
            {
                this.Armlet.ToggleAbility();
                this.Armlet.ToggleAbility();
            }
            else
            {
                this.Armlet.ToggleAbility();
            }

            this.Sleeper.Sleep(500);
        }
    }
}
