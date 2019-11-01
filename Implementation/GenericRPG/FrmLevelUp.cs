﻿using GameLibrary;
using System;
using System.Media;
using System.Windows.Forms;

namespace GenericRPG {
  public partial class FrmLevelUp : Form {
    public FrmLevelUp() {
      InitializeComponent();
    }

    private void FrmLevelUp_Load(object sender, EventArgs e) {
      SoundPlayer sp = new SoundPlayer(@"Resources\levelup.wav");
      sp.Play();

      Character character = Game.GetGame().Character;
      character.RefillHealthAndMana();

      lblOldLevel.Text  = character.Level.ToString();
      lblOldHealth.Text = ((float)Math.Round(character.Stats["MaxHealth"].BaseValue)).ToString();
      lblOldMana.Text   = ((float)Math.Round(character.Stats["MaxMana"].BaseValue)).ToString();
      lblOldStr.Text    = ((float)Math.Round(character.Stats["Str"].BaseValue)).ToString();
      lblOldDef.Text    = ((float)Math.Round(character.Stats["Def"].BaseValue)).ToString();

      character.LevelUp();
      lblNewLevel.Text  = character.Level.ToString();
      lblNewHealth.Text = ((float)Math.Round(character.Stats["MaxHealth"].BaseValue)).ToString();
      lblNewMana.Text = ((float)Math.Round(character.Stats["MaxMana"].BaseValue)).ToString();
      lblNewStr.Text = ((float)Math.Round(character.Stats["Str"].BaseValue)).ToString();
      lblNewDef.Text = ((float)Math.Round(character.Stats["Def"].BaseValue)).ToString();
    }

    private void btnClose_Click(object sender, EventArgs e) {
      Close();
    }
  }
}
