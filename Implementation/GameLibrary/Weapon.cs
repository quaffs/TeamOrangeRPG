﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
  public abstract class Weapon
  {
    public string Name { get; protected set; }
    public string Sprite { get; protected set; }
    public int DamageModifier { get; protected set; }

    public abstract void OnEquipped(Mortal equipper);
    public abstract void OnUnequipped(Mortal unequipper);
  }
}
