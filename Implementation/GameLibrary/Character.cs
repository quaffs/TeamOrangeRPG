using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GameLibrary { 
public struct Position {
    public int row;
    public int col;

    /// <summary>
    /// Construct a new 2D position
    /// </summary>
    /// <param name="row">Given row or y value</param>
    /// <param name="col">Given col or x value</param>
    public Position(int row, int col) {
      this.row = row;
      this.col = col;
    }
  }
    /// <summary>
    /// This represents our player in our game
    /// </summary>

   
    public class Character : Mortal {
    public PictureBox Pic { get; private set; }
    private Position pos;
    private Map map;
    public float XP { get; private set; }
    public float SP { get; private set; }
    public bool ShouldLevelUp { get; private set; }
    public int hearts = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="pos"></param>
        /// <param name="map"></param>
        public Character(PictureBox pb, Position pos, Map map) : base("Player 1", 1) {
      Pic = pb;
      this.pos = pos;
      this.map = map;
      ShouldLevelUp = false;
    }

    public void GainXP(float amount) {
      XP += amount;

      // every 100 experience points you gain a level
      if ((int)XP / 100 >= Level) {
        ShouldLevelUp = true;
      }
    }

        public void UseSP() {
            SP -= 1;
        }

        public void GainSP(float amount)
        {
            SP += amount;
        }

        public override void LevelUp() {
            base.LevelUp();
            ShouldLevelUp = false;
            GainSP(2);

        }
    public bool ShouldRespawn() {
        hearts--;
            if (hearts == 0)
                return false;
            else
            return true;

    }

    public void BackToStart()
        {
            Game.GetGame().ChangeState(GameState.RESPAWN);
            return;
        }
  //  {
    //        pos.row = map.CharacterStartRow;
      //      pos.col = map.CharacterStartCol;
        //    Position topleft = map.RowColToTopLeft(pos);
          //  Pic.Left = topleft.col;
            //Pic.Top = topleft.row;
        //}
    
    public override void ResetStats() {
      base.ResetStats();
      XP = 0;
    }

    public void Move(MoveDir dir) {
      Position newPos = pos;
      switch (dir) {
        case MoveDir.UP:
          newPos.row--;
          break;
        case MoveDir.DOWN:
          newPos.row++;
          break;
        case MoveDir.LEFT:
          newPos.col--;
          break;
        case MoveDir.RIGHT:
          newPos.col++;
          break;
      }
       if(map.ChangeLevel(newPos)==1)//TLF
            {
                Game.GetGame().ChangeState(GameState.CHANGE_LEVEL1);
                return;
            }
      if (map.ChangeLevel(newPos)==2)  // checks if player stepped on level change tile
      {
        Game.GetGame().ChangeState(GameState.CHANGE_LEVEL);
        return;
      }
      if(map.ChangeLevel(newPos)==3)//quit TLF
      {
        Game.GetGame().ChangeState(GameState.QUIT);
      }
            //Checks to see if the game needs to be saved or loaded if so it does it. TLF
            if ( map.Load_SaveGame(newPos) != 0 || Game.GetGame().State == GameState.RESPAWN)
            {
                if (map.Load_SaveGame(newPos) == 1)//This is save
                {
                    var dictionary = new Dictionary<string, float>();
                    dictionary["health"] = Health;
                    dictionary["mana"] = Mana;
                    dictionary["def"] = Def;
                    dictionary["str"] = Str;
                    dictionary["hearts"] = hearts;
                    dictionary["lvl"] = Level;
                    Write(dictionary, "dictionary.bin");


                    // var dictionary = new Dictionary<string, float>();
 //                   Dict.dictionary["health"] = Health;
   //                 Dict.dictionary["mana"] = Mana;
     //                   Dict.dictionary["def"] = Def;
       //                 Dict.dictionary["str"] = Str;
         //               Dict.dictionary["hearts"] = hearts;
                    //GC.SuppressFinalize(Dict.dictionary);//limit Garbage Collection
           //         Write((Dictionary<string, float>)Dict.dictionary, "C:\\dictionary.bin");
             //       Dict.dictionary["lvl"] = 1;
                        //Dict.dictionary["lvl"] = this.Level;
                       

                }
                else if (map.Load_SaveGame(newPos) == 2)//This is load
                {
                    var dictionary = Read("dictionary.bin");
                    this.Health = dictionary["health"];
                    this.Mana = dictionary["mana"];
                    this.Def = dictionary["def"];
                    this.Str = dictionary["str"];
                    this.hearts = (int)dictionary["hearts"];
                    this.Level = (int)dictionary["lvl"];
               //     string[] allKeys = dictionary.AllKeys;
                 //   foreach (var Key in allKeys)
                   // {
                     //   string str = Convert.ToString(Key)
                       // this.str = dictionary[Key];
                   //     Console.WriteLine(Key);
                    //}
                   // if (Dict.dictionary != null)
                    //{
                      //   Dict.dictionary = Read("C:\\dictionary.bin");
                       // foreach (var pair in Dict.dictionary)
                        //{
                          //  Console.WriteLine(pair);
                        //}
                    }
                void Write(Dictionary<string, float> dictionary, string file)
                {
                    using (FileStream fs = File.OpenWrite(file))
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        // Put count.
                        writer.Write(dictionary.Count);
                        // Write pairs.
                        foreach (var pair in dictionary)
                        {
                            writer.Write(pair.Key);
                            writer.Write(pair.Value);
                        }
                    }
                }
                Dictionary<string, float> Read(string file)
                {
                    var result = new Dictionary<string, float>();
                    using (FileStream fs = File.OpenRead(file))
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        // Get count.
                        int count = reader.ReadInt32();
                        // Read in all pairs.
                        for (int i = 0; i < count; i++)
                        {
                            string key = reader.ReadString();
                            float value = (float)reader.ReadSingle();
                            result[key] = value;
                        }
                    }
                    return result;
                }
            }

            if (map.IsValidPos(newPos)) {
        pos = newPos;
        Position topleft = map.RowColToTopLeft(pos);
        Pic.Left = topleft.col;
        Pic.Top = topleft.row;
        return;
      }
           
        }
  }
}
