using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
  public class StatAttribute
  {
    public List<Modifier> FlatModifiers { get; set; }
    public List<Modifier> PercentModifiers { get; set; }

    public float BaseValue { get; set; }
    public float CalcValue
    {
      get
      {
        float newValue = BaseValue;
        float flatMod = 0;
        float percMod = 0;

        foreach (var mod in FlatModifiers)
        {
          flatMod += mod.ModValue;
        }

        foreach (var mod in PercentModifiers)
        {
          percMod += mod.ModValue;
        }

        newValue *= percMod + 1; // apply total percentage modifier to base value
        newValue += flatMod; // add flat modifier on top of that

        return newValue;
      }
    }

    public StatAttribute(float baseValue)
    {
      FlatModifiers = new List<Modifier>();
      PercentModifiers = new List<Modifier>();

      BaseValue = baseValue;
    }
  }

  public class Modifier
  {
    public float ModValue { get; set; }

    public Modifier(float modValue)
    {
      ModValue = modValue;
    }
  }
}
