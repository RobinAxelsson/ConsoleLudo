﻿using LudoConsole.Main;
using LudoEngine.Enum;
using LudoEngine.Models;
using System;
using System.Collections.Generic;

namespace LudoEngine.GameLogic.Interfaces
{ 
    public interface IGamePlayer
    {
        public TeamColor Color { get; set; }
        public List<Pawn> Pawns { get; set; }
        public void Play(Dice dice);
    }
}