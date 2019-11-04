using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Weapons
{
    public class BasicStaff : Weapon
    {
        public BasicStaff()
        {
            Type = WeaponType.Magical;
            Name = "Basic Staff";
            DamageModifier = 5;
            Sprite = "basicstaff";
        }

        public override void OnEquipped(Mortal equipper)
        {
            // nothing special
        }

        public override void OnUnequipped(Mortal unequipper)
        {
            // nothing special
        }
    }
}
