using GameLibrary;
using GameLibrary.Weapons;
using GenericRPG.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenericRPG {
  public partial class FrmMap : Form {
    private Character character;
    private Map map;
    private Game game;
    //private string level;   // selects which level .txt file to load from
        private string titleScreen; //titleScreen.txt file to load
    public FrmMap(string titleScreen) {
      this.titleScreen = titleScreen;
      InitializeComponent();
    }

    private void FrmMap_Load(object sender, EventArgs e) {
      game = Game.GetGame();

      map = new Map();
      character = map.LoadMap(titleScreen, grpMap, //CHANGE
        str => Resources.ResourceManager.GetObject(str) as Bitmap
      );
      Width = grpMap.Width + 25;
      Height = grpMap.Height + 50;
      game.SetCharacter(character);
      character.EquipWeapon(new BasicSword());
    }

    private void FrmMap_KeyDown(object sender, KeyEventArgs e) {
      // don't allow movement if the player is in a fight
      if (game.State == GameState.FIGHTING) return;

      MoveDir dir = MoveDir.NO_MOVE;
      switch (e.KeyCode) {
        case Keys.Left:
          dir = MoveDir.LEFT;
          break;
        case Keys.Right:
          dir = MoveDir.RIGHT;
          break;
        case Keys.Up:
          dir = MoveDir.UP;
          break;
        case Keys.Down:
          dir = MoveDir.DOWN;
          break;
      }
      if (dir != MoveDir.NO_MOVE) {
        character.Move(dir);
        if (game.State == GameState.CHANGE_LEVEL1)//TLF 
        {
                    this.Hide();      // hides/close old window
                    Game.GetGame().ChangeState(GameState.ON_MAP);
                    FrmMap frmMap = new FrmMap("Resources/level.txt");
                    frmMap.Show();    // shows LEVEL 1 window
                }
        if (game.State == GameState.CHANGE_LEVEL)
        {
          this.Hide();      // hides/close old window
          Game.GetGame().ChangeState(GameState.ON_MAP);
          FrmMap frmMap = new FrmMap("Resources/level2.txt");
          frmMap.Show();    // shows level 2 window
        }
        if (game.State == GameState.FIGHTING) {
          FrmArena frmArena = new FrmArena();
          frmArena.Show();
        }
        if(game.State == GameState.QUIT)//TLF
                {
                    // Changed this.Hide() to Close() because
                    // this.Hide() keeps the program running
                    // while Close() quits it altogether
                    //Application.Exit() would also work TLF
                    Close();
                }
            }
        }
  }
}
