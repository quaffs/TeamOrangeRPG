using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Weapons
{
    public class BasicSword : Weapon
    {
        public BasicSword()
        {
            Type = WeaponType.Physical;
            Name = "Sword";
            DamageModifier = 2;
            Sprite = "sword";
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
