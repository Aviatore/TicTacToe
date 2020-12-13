using System;
using System.Collections.Generic;

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
            LocationContainer bestMove = new LocationContainer();
            
            int[] marksToCheck = new int[2];
            marksToCheck[0] = Mark;
            marksToCheck[1] = Mark == 1 ? 2 : 1;

            foreach (int mark in marksToCheck)
            {
                LocationContainer rowData = new LocationContainer();
                
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

                if (bestMove.FreeCoords.Count == _board.ItemsNumberToWin - 1)
                    return bestMove.FreeCoords[0];
            }

            if (bestMove.FreeCoords.Count > 0)
                return bestMove.FreeCoords[0];
            
            List<Point> freePlaces = _board.GetFreePlaces();
            Point randomLocation = freePlaces[_random.Next(freePlaces.Count)];
            return randomLocation;
            
        }
    }
}