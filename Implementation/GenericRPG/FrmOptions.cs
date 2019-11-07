using GameLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericRPG
{
    public partial class FrmOptions : Form
    {
        private Rectangle origBounds;
        private FormBorderStyle origBorders;
        private GameState origState;

        public FrmOptions()
        {
            InitializeComponent();
            origBounds = Bounds;
            origBorders = FormBorderStyle;
            origState = Game.GetGame().State;

            FullscreenHandler.Register(this);

            rdoFullscreenOn.Checked = Game.GetGame().Fullscreen;
            rdoSoundOn.Checked = Game.GetGame().SoundOn;
        }

        private void rdoSoundOn_Click(object sender, EventArgs e)
        {
            Game.GetGame().SoundOn = true;
        }

        private void rdoSoundOff_Click(object sender, EventArgs e)
        {
            Game.GetGame().SoundOn = false;
        }

        private void rdoFullscreenOn_Click(object sender, EventArgs e)
        {
            Game.GetGame().Fullscreen = true;

            FullscreenHandler.GoFullscreen();
        }

        private void rdoFullscreenOff_Click(object sender, EventArgs e)
        {
            Game.GetGame().Fullscreen = false;

            FullscreenHandler.LeaveFullscreen();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Game.GetGame().ChangeState(origState);
            Close();
        }

        private void FrmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.GetGame().ChangeState(origState);
        }
    }

    public class FullscreenHandler
    {
        private static List<SizeInfo> registered = new List<SizeInfo>();

        public static void Register(Form f)
        {
            registered.Add(new SizeInfo { Bounds = f.Bounds, FormBorderStyle = f.FormBorderStyle, Form = f });
        }

        public static void GoFullscreen()
        {
            foreach (var sz in registered)
            {
                sz.GoFullscreen();
            }
        }

        public static void LeaveFullscreen()
        {
            foreach (var sz in registered)
            {
                sz.LeaveFullscreen();
            }
        }
    }

    public class SizeInfo
    {
        public Rectangle Bounds;
        public FormBorderStyle FormBorderStyle;
        public Form Form;

        public void GoFullscreen()
        {
            Form.Bounds = Screen.PrimaryScreen.Bounds;
            Form.FormBorderStyle = FormBorderStyle.None;
        }

        public void LeaveFullscreen()
        {
            Form.Bounds = Bounds;
            Form.FormBorderStyle = FormBorderStyle;
        }
    }
}
