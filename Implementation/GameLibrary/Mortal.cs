using System;
using System.Collections.Generic;

namespace GameLibrary {
  public class Mortal {
    #region Constants
    private const float INIT_HEALTH = 100;
    private const float INIT_STR = 10;
    private const float INIT_DEF = 5;
    private const float INIT_LUCK = 2;
    private const float INIT_SPEED = 2;
    private const float INIT_MANA = 40;

    private const float LVLINC_HEALTH = 20;
    private const float LVLINC_STR = 3;
    private const float LVLINC_DEF = 2;
    private const float LVLINC_LUCK = 1;
    private const float LVLINC_SPEED = 2;
    private const float LVLINC_MANA = 10;

    private const float SIMPLEATTACK_RANDOM_AMT = 0.25f;
    #endregion

    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public float Health { get; protected set; }
    public float Mana { get; protected set; }

    public Dictionary<string, StatAttribute> Stats { get; protected set; }

    public Weapon EquippedWeapon { get; set; }

    private Random rand;

    public Mortal(string name, int level) {
      Name = name;
      ResetStats();
      SetLevel(level);
      rand = new Random();
    }

    public virtual void ResetStats() {
      Level = 1;

      Stats = new Dictionary<string, StatAttribute>()
      {
        { "MaxHealth", new StatAttribute(INIT_HEALTH) },
        { "Health", new StatAttribute(INIT_HEALTH) },
        { "MaxMana", new StatAttribute(INIT_MANA) },
        { "Mana", new StatAttribute(INIT_MANA) },
        { "Str", new StatAttribute(INIT_STR) },
        { "Def", new StatAttribute(INIT_DEF) },
        { "Luck", new StatAttribute(INIT_LUCK) },
        { "Speed", new StatAttribute(INIT_SPEED) }
      };

      Health = INIT_HEALTH;
      Mana = INIT_MANA;
    }

    public void SetLevel(int level) {
      for (int i = 1; i < level; i++) {
        LevelUp();
      }
    }

    public virtual void LevelUp() {
      // level increases
      Level++;

      // health and mana
      Stats["MaxHealth"].BaseValue += LVLINC_HEALTH;
      Stats["MaxMana"].BaseValue += LVLINC_MANA;
      Stats["Health"].BaseValue = Stats["MaxHealth"].BaseValue;
      Stats["Mana"].BaseValue = Stats["MaxMana"].BaseValue;

      RefillHealthAndMana();

      // other stats
      Stats["Str"].BaseValue += LVLINC_STR;
      Stats["Def"].BaseValue += LVLINC_DEF;
      Stats["Luck"].BaseValue += LVLINC_LUCK;
      Stats["Speed"].BaseValue += LVLINC_SPEED;
    }

    public void RefillHealthAndMana() {
      Health = Stats["MaxHealth"].BaseValue;
      Mana = Stats["MaxMana"].BaseValue;
    }

    public void SimpleAttack(Mortal receiver) {
      float baseDamage = Math.Max(Stats["Str"].CalcValue * 1.2f - receiver.Stats["Def"].CalcValue, 0);

      // add weapon damage if applicable
      if (EquippedWeapon != null) baseDamage += EquippedWeapon.DamageModifier;

      float randMax = 1 + SIMPLEATTACK_RANDOM_AMT;
      float randMin = 1 - SIMPLEATTACK_RANDOM_AMT;
      float randMult = (float)(rand.NextDouble() * (randMax - randMin)) + randMin;

      receiver.Health -= (baseDamage * randMult);
    }

    public void EquipWeapon(Weapon weapon)
    {
      if (EquippedWeapon != null)
      {
        // unequip existing weapon
        EquippedWeapon.OnUnequipped(this);
      }

      EquippedWeapon = weapon;
      weapon.OnEquipped(this);
    }
  }
}
