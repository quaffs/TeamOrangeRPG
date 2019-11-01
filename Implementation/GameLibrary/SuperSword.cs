using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
  public class SuperSword : Weapon
  {
    private Modifier defenseMod;

    public SuperSword()
    {
      Name = "Super Sword";
      DamageModifier = 5;
      Sprite = "supersword";

      defenseMod = new Modifier(1.5f);
    }

    public override void OnEquipped(Mortal equipper)
    {
      equipper.Stats["Def"].PercentModifiers.Add(defenseMod);
    }

    public override void OnUnequipped(Mortal unequipper)
    {
      unequipper.Stats["Def"].PercentModifiers.Remove(defenseMod);
    }
  }
}
