﻿using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameLibrary
{
    public class Map
    {
        private int[,] layout;
        private const int TOP_PAD = 10;
        private const int BOUNDARY_PAD = 5;
        private const int BLOCK_SIZE = 50;
        public double encounterChance;
        private Random rand;

        public int CharacterStartRow { get; private set; }
        public int CharacterStartCol { get; private set; }
        private int NumRows { get { return layout.GetLength(0); } }
        private int NumCols { get { return layout.GetLength(1); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapFile"></param>
        /// <param name="grpMap"></param>
        /// <param name="LoadImg"></param>
        /// <returns></returns>
        public Character LoadMap(string mapFile, GroupBox grpMap, Func<string, Bitmap> LoadImg)
        {
            // declare and initialize locals
            int top = TOP_PAD;
            int left = BOUNDARY_PAD;
            Character character = null;
            List<string> mapLines = new List<string>();

            // read from map file
            using (FileStream fs = new FileStream(mapFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        mapLines.Add(line);
                        line = sr.ReadLine();
                    }
                }
            }
            // load map file into layout and create PictureBox objects
            layout = new int[mapLines.Count, mapLines[0].Length];
            int i = 0;
            foreach (string mapLine in mapLines)
            {
                int j = 0;
                foreach (char c in mapLine)
                {
                    int val = c - '0';
                    //layout[i, j] = (val == 1 ? 1 : 0);
                    //layout[i, j] = (val == 3 ? 3 : 0);
                    if (val == 1)         // wall
                    {
                        layout[i, j] = 1;
                    }
                    else if (val == 3)    // level 2
                    {
                        layout[i, j] = 3;
                    }
                    else if (val == 6)//Level 1 TLF
                    {
                        layout[i, j] = 6;
                    }
                    else if (val == 5)//quit game TLF
                    {
                        layout[i, j] = 5;
                    }
                    else if (val == 7)//save tile TLF
                    {
                        layout[i, j] = 7;
                    }
                    else if (val == 8)//load TLF
                    {
                        layout[i, j] = 8;
                    }
                    else if (val == 9) // options
                    {
                        layout[i, j] = 9;
                    }
                    else                  // walkable
                    {
                        layout[i, j] = 0;
                    }
                    PictureBox pb = CreateMapCell(val, LoadImg);
                    if (pb != null)
                    {
                        pb.Top = top;
                        pb.Left = left;
                        grpMap.Controls.Add(pb);
                    }
                    if (val == 2)
                    {
                        CharacterStartRow = i;
                        CharacterStartCol = j;
                        character = new Character(pb, new Position(i, j), this);
                    }
                    left += BLOCK_SIZE;
                    j++;
                }
                left = BOUNDARY_PAD;
                top += BLOCK_SIZE;
                i++;
            }

            // resize Group
            grpMap.Width = NumCols * BLOCK_SIZE + BOUNDARY_PAD * 2;
            grpMap.Height = NumRows * BLOCK_SIZE + TOP_PAD + BOUNDARY_PAD;
            grpMap.Top = 5;
            grpMap.Left = 5;

            // initialize for game
            encounterChance = 0.15;
            rand = new Random();
            Game.GetGame().ChangeState(GameState.ON_MAP);

            // return Character object from reading map
            return character;
        }

        private PictureBox CreateMapCell(int legendValue, Func<string, Bitmap> LoadImg)
        {
            PictureBox result = null;
            {
                switch (legendValue)
                {
                    // walkable
                    case 0:
                        break;

                    // wall
                    case 1:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("wall"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;

                    // character
                    case 2:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("character"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;

                    // next level
                    case 3:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("level2"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;

                    // boss
                    case 4:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("fightboss"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;

                    // quit
                    case 5:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("quitgame"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;
                    //level 1 
                    case 6:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("level1"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;
                    //Save game tile TLF
                    case 7:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("save"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;
                    //Load Tile TLF
                    case 8:
                        result = new PictureBox()
                        {
                            BackgroundImage = LoadImg("load"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Width = BLOCK_SIZE,
                            Height = BLOCK_SIZE
                        };
                        break;
                }
                return result;
            }
        }
        public bool IsValidPos(Position pos)
        {
            if (pos.row < 0 || pos.row >= NumRows ||
                pos.col < 0 || pos.col >= NumCols ||
                layout[pos.row, pos.col] == 1)
            {
                return false;
            }
            if (NumRows != 8) //This prevents enemies on TitleScreen TLF    
            {
                if (rand.NextDouble() < encounterChance)
                {
                    encounterChance = 0.15;
                    Game.GetGame().ChangeState(GameState.FIGHTING);
                }
                else
                {
                    encounterChance += 0.10;
                }
            }
            return true;
        }

        // this method checks if the player is
        // on the "level 2" square in the map:
        public int ChangeLevel(Position pos)
        {
            if (pos.row < 0 || pos.row >= NumRows ||
                pos.col < 0 || pos.col >= NumCols ||
                layout[pos.row, pos.col] == 1)
            {
                return 0;
            }
            if (layout[pos.row, pos.col] == 6)//Level 1 tile TLF
                return 1;
            if (layout[pos.row, pos.col] == 3)
            {
                return 2;
            }
            if (layout[pos.row, pos.col] == 5)//quit TLF
                return 3;
            else
            {
                return 0;
            }
        }
        public short Load_SaveGame(Position pos)
        {
            if (pos.row < 0 || pos.row >= NumRows ||
                pos.col < 0 || pos.col >= NumCols ||
                layout[pos.row, pos.col] == 1)
            {
                return 0;
            }
            if (layout[pos.row, pos.col] == 7)//save TLF
            {
                return 1;
            }
            if (layout[pos.row, pos.col] == 8)//Load TLF
            {
                return 2;
            }
            else
                return 0;
        }

        public Position RowColToTopLeft(Position p)
        {
            return new Position(p.row * BLOCK_SIZE + TOP_PAD, p.col * BLOCK_SIZE + BOUNDARY_PAD);
        }
    }
}
