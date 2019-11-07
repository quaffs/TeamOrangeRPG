using GameLibrary;
using System;
using System.Media;
using System.Windows.Forms;

namespace GenericRPG
{
    public partial class FrmLevelUp : Form
    {
        private SoundPlayer sp;

        public FrmLevelUp()
        {
            InitializeComponent();
            sp = new SoundPlayer(@"Resources\levelup.wav");
        }

        private void FrmLevelUp_Load(object sender, EventArgs e)
        {
            if (Game.GetGame().SoundOn)
            {
                sp.Play();
            }           

            Character character = Game.GetGame().Character;
            character.RefillHealthAndMana();

            lblOldLevel.Text = character.Level.ToString();
            lblOldHealth.Text = ((float)Math.Round(character.GetBaseStat("MaxHealth"))).ToString();
            lblOldMana.Text = ((float)Math.Round(character.GetBaseStat("MaxMana"))).ToString();
            lblOldStr.Text = ((float)Math.Round(character.GetBaseStat("Str"))).ToString();
            lblOldDef.Text = ((float)Math.Round(character.GetBaseStat("Def"))).ToString();

            character.LevelUp();
            lblNewLevel.Text = character.Level.ToString();
            lblNewHealth.Text = ((float)Math.Round(character.GetBaseStat("MaxHealth"))).ToString();
            lblNewMana.Text = ((float)Math.Round(character.GetBaseStat("MaxMana"))).ToString();
            lblNewStr.Text = ((float)Math.Round(character.GetBaseStat("Str"))).ToString();
            lblNewDef.Text = ((float)Math.Round(character.GetBaseStat("Def"))).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            sp.Stop();
            Close();
        }

        private void FrmLevelUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            sp.Stop();
        }
    }
}
