﻿using System;
using System.Collections.Generic;
using LudoEngine.Enum;
using LudoEngine.GameLogic.Interfaces;

namespace LudoConsole.Main
{
    public class Dice
    {
        private int Highest { get; set; }
        private int Lowest { get; set; }
        private Random random { get; set; }
        public Dice(int lowest, int highest)
        {
            Highest = highest;
            Lowest = lowest;
            random = new Random();
        }
        public int Roll() => random.Next(Lowest, Highest);
    }
}