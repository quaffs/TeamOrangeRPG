﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public enum GameState
    {
        LOADING,
        TITLE_SCREEN,
        ON_MAP,
        FIGHTING,
        DEAD,
        QUIT,
        CHANGE_LEVEL1,
        CHANGE_LEVEL,
        RESPAWN,
        OPTIONS
    }

    public class Game
    {
        private static Game game;

        public Character Character { get; private set; }
        public GameState State { get; private set; }
        public bool SoundOn { get; set; }
        public bool Fullscreen { get; set; }

        private Game()
        {
            State = GameState.LOADING;
            SoundOn = true;
        }

        public static Game GetGame()
        {
            if (game == null)
                game = new Game();
            return game;
        }

        public void ChangeState(GameState newState)
        {
            State = newState;
        }

        public void SetCharacter(Character character)
        {
            Character = character;
        }
    }
}
