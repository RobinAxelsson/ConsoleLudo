﻿using System;
using System.Collections.Generic;
using System.Linq;
using LudoEngine.BoardUnits.Interfaces;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoConsole.Mapping
{
    internal static class ConsoleDtoMapping
    {
        public static IEnumerable<ConsoleGameSquare> Map(IEnumerable<IGameSquare> gameSquares)
        {
            return gameSquares.Select(x => MapSingle(x));
        }

        private static ConsoleGameSquare MapSingle(IGameSquare square)
        {
            return new ConsoleGameSquare
            {
                IsBase = square.GetType().Name == "SquareTeamBase",
                BoardX = square.BoardX,
                BoardY = square.BoardY,
                Color = MapColor(square.Color),
                Pawns = square.Pawns.Select(MapPawn).ToList()
            };
        }

        private static ConsolePawnDto MapPawn(Pawn pawn)
        {
            return new ConsolePawnDto
            {
                Id = pawn.Id,
                IsSelected = true,
                Color = MapColor(pawn.Color)
            };
        }

        private static ConsoleTeamColor MapColor(TeamColorCore? color)
        {
            return color switch
            {
                null => ConsoleTeamColor.Default,
                TeamColorCore.Blue => ConsoleTeamColor.Blue,
                TeamColorCore.Red => ConsoleTeamColor.Red,
                TeamColorCore.Yellow => ConsoleTeamColor.Yellow,
                TeamColorCore.Green => ConsoleTeamColor.Green,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }
    }
}