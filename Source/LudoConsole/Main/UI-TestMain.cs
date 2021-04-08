﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LudoConsole.UI;
using LudoConsole.UI.Controls;
using LudoConsole.UI.Models;
using LudoConsole.UI.Screens;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.GameLogic;
using LudoEngine.Models;

namespace LudoConsole.Main
{
    internal static partial class UITestMain
    {
        public static List<ISquareDrawable> DrawSquares;
        static UITestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Board.Init();
            DrawSquares = UiControl.ConvertAllSquares(Board.BoardSquares);
            GameSetup.NewGame(Board.BoardSquares, players: 4);
            UiControl.SetDefault();
        }
        private static void Main(string[] args)
        {
            
            List<TeamColor> AIColors = new List<TeamColor>();
            AIColors.Add(TeamColor.Red);
            AIColors.Add(TeamColor.Yellow);
            AIColors.Add(TeamColor.Blue);
            AIColors.Add(TeamColor.Green);
            List<Stephan> stephans = new List<Stephan>();
            for(var i = 0; i <= AIColors.Count - 1; i++)
            {
                var stephan = new Stephan(AIColors[i]);
                stephan.TakeOutSquare = Board.StartSquare(AIColors[i]);
                stephans.Add(stephan);
            }

            var writerThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    ConsoleWriter.UpdateBoard(DrawSquares);
                    Thread.Sleep(300);
                }
            }));

            writerThread.Start();
            var diceLine = new LineData(0, 9);
            

            while (true)
            {
                diceLine.Update($"{ActivePlayer.CurrentTeam()} rolling dice...");
                Thread.Sleep(500);
                int dieRoll = ActivePlayer.RollDice();
                diceLine.Update($"{ActivePlayer.CurrentTeam()} got {dieRoll}");
                Console.ReadKey(true);
                var pawnsToMove = ActivePlayer.SelectablePawns(dieRoll);
                bool movePawn = false;
                int selection = 0;
                var AIPlayer = AIColors.Contains(ActivePlayer.CurrentTeam());
                var key = new ConsoleKeyInfo().Key;
                
                if (!AIPlayer)
                {
                    while (true)
                    {
                        if(pawnsToMove.Count == 0)
                        {
                            ActivePlayer.NextTeam();
                            break;
                        }
                        if (dieRoll == 6 && Board.PawnsInBase(ActivePlayer.CurrentTeam()).Count > 1)
                        {
                            diceLine.Update("'x' for two");
                            if (key == ConsoleKey.X)
                            {
                                ActivePlayer.TakeOutTwo();
                                ActivePlayer.NextTeam();
                                break;
                            }
                        }
                        ActivePlayer.SelectPawn(pawnsToMove[selection]);
                        key = Console.ReadKey(true).Key;

                        if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow)
                            selection++;

                        if (key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow)
                            selection--;

                        selection =
                            selection > pawnsToMove.Count - 1 ? 0 :
                            selection < 0 ? pawnsToMove.Count - 1 : selection;

                        if (key == ConsoleKey.Enter)
                        {
                            ActivePlayer.MoveSelectedPawn(dieRoll);
                            if (dieRoll != 6) ActivePlayer.NextTeam();
                            break;
                        }
                    }
                }
                else
                {
                    
                    while (true)
                    {
                        var CurrentStephan = stephans.Where(s => s.StephanColor == ActivePlayer.CurrentTeam()).Single();
                        var StephanResult = CurrentStephan.Play(dieRoll);
                        ActivePlayer.SelectedPawn = StephanResult.PlayPawn;
                        if (dieRoll == 6)
                        {
                            if (StephanResult.TakeOutTwo)
                            {
                                var basePawns = Board.BaseSquare(ActivePlayer.CurrentTeam()).Pawns;
                                for (int i = 0; i < 2; i++)
                                {
                                    CurrentStephan.StephanPawns.Add(basePawns[0]);
                                    basePawns[0].Move(1);
                                }
                                ActivePlayer.NextTeam();
                                break;
                            }
                            else
                            {
                                if (ActivePlayer.SelectedPawn != null)
                                {
                                    ActivePlayer.MoveSelectedPawn(dieRoll);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (ActivePlayer.SelectedPawn != null)
                            {
                                ActivePlayer.MoveSelectedPawn(dieRoll);
                            }
                            ActivePlayer.NextTeam();
                            break;
                        }
                    }
                 
                }
            }
        }

    }
}