using GameLibrary;
using GenericRPG.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenericRPG {
  public partial class FrmMap : Form {
    private Character character;
    private Map map;
    private Game game;
    private string level;   // selects which level .txt file to load from

    public FrmMap(string level) {
      this.level = level;
      InitializeComponent();
    }

    private void FrmMap_Load(object sender, EventArgs e) {
      game = Game.GetGame();

      map = new Map();
      character = map.LoadMap(level, grpMap, 
        str => Resources.ResourceManager.GetObject(str) as Bitmap
      );
      Width = grpMap.Width + 25;
      Height = grpMap.Height + 50;
      game.SetCharacter(character);
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
        if (game.State == GameState.CHANGE_LEVEL)
        {
          //////////////////////////////////////////////////////////////
          /// NEED TO FIGURE OUT HOW TO CLOSE OLD LEVEL WINDOW
          /// ///////////////////////////////////////////////////////
          Game.GetGame().ChangeState(GameState.ON_MAP);
          FrmMap frmMap = new FrmMap("Resources/level2.txt");
          frmMap.Show();
        }
        if (game.State == GameState.FIGHTING) {
          FrmArena frmArena = new FrmArena();
          frmArena.Show();
        }
      }
    }
  }
}
