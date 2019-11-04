using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Weapons
{
    public class SuperSword : Weapon
    {
        private Modifier defenseMod;

        public SuperSword()
        {
            Type = WeaponType.Physical;
            Name = "Super Sword";
            DamageModifier = 5;
            Sprite = "supersword";

            defenseMod = new Modifier(1.5f);
        }

        public override void OnEquipped(Mortal equipper)
        {
            equipper.GetStat("Def").PercentModifiers.Add(defenseMod);
        }

        public override void OnUnequipped(Mortal unequipper)
        {
            unequipper.GetStat("Def").PercentModifiers.Remove(defenseMod);
        }
    }
}
