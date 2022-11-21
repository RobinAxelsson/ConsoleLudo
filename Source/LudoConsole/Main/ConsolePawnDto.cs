﻿using LudoConsole.UI.Models;
using LudoEngine.Enum;

namespace LudoConsole.Main
{
    public class ConsolePawnDto
    {
        public int Id { get; init; }
        public bool IsSelected { get; init; }
        public ConsoleTeamColor Color { get; init; }
    }
}