using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class Ai
    {
        public int Mark { get; set; }
        private Board _board;
        private Random _random = new Random();

        public Ai(int mark, Board board)
        {
            Mark = mark;
            _board = board;
        }

        public Point AiGetMove()
        {
            BestMoveContainer bestMove = new BestMoveContainer();
            
            int[] marksToCheck = new int[2];
            marksToCheck[0] = Mark;
            marksToCheck[1] = Mark == 1 ? 2 : 1;

            foreach (int mark in marksToCheck)
            {
                LocationContainer rowData = new LocationContainer();
                
                /*
                for (int rowIndex = 0; rowIndex < _board.RowLength; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < _board.ColLength; colIndex++)
                    {
                        int lenToReact;
                        int boardValue = _board.GetValue(rowIndex, colIndex);
                        if (boardValue == mark)
                        {
                            if (rowData.CoordsContains(rowIndex, colIndex - 1) ||
                                rowData.FreeCoordsContains(rowIndex, colIndex - 1))
                            {
                                rowData.AddCoords(rowIndex, colIndex);
                            }
                            else
                            {
                                rowData.ClearCoords();
                                rowData.ClearFreeCoords();
                                rowData.AddCoords(rowIndex, colIndex);
                            }
                        }
                        else if (boardValue == 0 && !rowData.FreeCoordsContains(rowIndex, colIndex))
                        {
                            rowData.AddFreeCoords(rowIndex, colIndex);
                            //rowData.Num++;
                        }
                        else if (boardValue == 0)
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                            rowData.AddFreeCoords(rowIndex, colIndex);
                            //rowData.Num++;
                        }
                        else
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                        }

                        lenToReact = (_board.RowLength - _board.ItemsNumberToWin) >= 1 ? 2 : 1;

                        if (rowData.Coords.Count >= (_board.ItemsNumberToWin - lenToReact) &&
                            rowData.FreeCoords.Count >= (_board.ItemsNumberToWin - rowData.Coords.Count))
                        {
                            //if (rowData.FreeCoords.Count > 0)
                            //{
                                Point location = rowData.FreeCoords[0];

                                if (rowData.Coords.Count > bestMove.FreeCoords.Count)
                                {
                                    bestMove.AddFreeCoords(location.Row, location.Col);
                                    //bestMove.Num = rowData.Coords.Count;
                                }
                            //}
                        }
                    }
                    
                    rowData.ClearCoords();
                    rowData.ClearFreeCoords();
                    //rowData.Num = 0;
                }
                */
                ColRowChecker(mark, rowData, bestMove, "row");
                ColRowChecker(mark, rowData, bestMove, "col");

                if (bestMove.Length == _board.ItemsNumberToWin - 1)
                {
                    Console.WriteLine($"1: {bestMove.BestMove.Row}, {bestMove.BestMove.Col}");

                    return bestMove.BestMove;
                }
            }

            Console.WriteLine($"2: {bestMove.BestMove.Row}, {bestMove.BestMove.Col}");
            
            if (bestMove.Length > 0)
                return bestMove.BestMove;
            else
            {
                Console.WriteLine("Random move");
            }
            
            List<Point> freePlaces = _board.GetFreePlaces();
            Point randomLocation = freePlaces[_random.Next(freePlaces.Count)];
            return randomLocation;
            
        }

        private void ColRowChecker(int mark, LocationContainer rowData, BestMoveContainer bestMove, string type)
        {
            int firstLoopMax = type == "row" ? _board.RowLength : _board.ColLength;
            int secondLoopMax = type == "row" ? _board.ColLength : _board.RowLength;
            
            for (int i = 0; i < firstLoopMax; i++)
            {
                for (int m = 0; m < secondLoopMax; m++)
                {
                    int rowMod = type == "row" ? i : m - 1;
                    int colMod = type == "row" ? m - 1 : i;
                    
                    int rowMod2 = type == "row" ? i : m - 2;
                    int colMod2 = type == "row" ? m - 2 : i;
                    
                    int row = type == "row" ? i : m;
                    int col = type == "row" ? m : i;
                    
                    int lenToReact;
                    int boardValue = _board.GetValue(row, col);
                    if (boardValue == mark)
                    {
                        if (rowData.CoordsContains(rowMod, colMod) ||
                            rowData.FreeCoordsContains(rowMod, colMod))
                        {
                            // Check if there is a space between marks of the same type
                            if (rowData.CoordsContains(rowMod2, colMod2))
                                // if YES, then insert the position to the beginning of the list from which the best move are picked
                                rowData.AddCoordsAtFirstPos(row, col);
                            else
                                // if NOT, then append the position to the end of the list
                                rowData.AddCoords(row, col);
                        }
                        else
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                            rowData.AddCoords(row, col);
                        }
                    }
                    else if (boardValue == 0 && !rowData.FreeCoordsContains(rowMod, colMod))
                    {
                        rowData.AddFreeCoords(row, col);
                        //rowData.Num++;
                    }
                    else if (boardValue == 0)
                    {
                        rowData.ClearCoords();
                        rowData.ClearFreeCoords();
                        rowData.AddFreeCoords(row, col);
                        //rowData.Num++;
                    }
                    else
                    {
                        rowData.ClearCoords();
                        rowData.ClearFreeCoords();
                    }

                    lenToReact = (_board.RowLength - _board.ItemsNumberToWin) >= 1 ? 2 : 1;

                    if (rowData.Coords.Count >= (_board.ItemsNumberToWin - lenToReact) &&
                        rowData.FreeCoords.Count >= (_board.ItemsNumberToWin - rowData.Coords.Count))
                    {
                        //if (rowData.FreeCoords.Count > 0)
                        //{
                        Point location = rowData.FreeCoords[0];

                        // If the number of positions of the same mark that are next to each other or are separated by at most one space
                        // is larger than length of the best move, then the bestMove object is updated
                        if (rowData.Coords.Count > bestMove.Length)
                        {
                            bestMove.BestMove = location;
                            bestMove.Length = rowData.Coords.Count;
                        }
                        //}
                    }
                }

                rowData.ClearCoords();
                rowData.ClearFreeCoords();
                //rowData.Num = 0;
            }
        }
    }
}