using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            Thread.Sleep(250);
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
                DiagonalChecker(mark, rowData, bestMove, "col", 1);
                DiagonalChecker(mark, rowData, bestMove, "col", -1);
                DiagonalChecker(mark, rowData, bestMove, "row");

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
            Console.WriteLine($"freePlaces: {freePlaces.Count}");
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

                    lenToReact = (_board.RowLength - _board.ItemsNumberToWin) >= 1 ? 3 : 1;

                    if (rowData.Coords.Count >= (_board.ItemsNumberToWin - lenToReact) &&
                        rowData.FreeCoords.Count >= (_board.ItemsNumberToWin - rowData.Coords.Count))
                    {
                        /*
                        Point location = rowData.FreeCoords[0];

                        // If the number of positions of the same mark that are next to each other or are separated by at most one space
                        // is larger than length of the best move, then the bestMove object is updated
                        if (rowData.Coords.Count > bestMove.Length)
                        {
                            bestMove.BestMove = location;
                            bestMove.Length = rowData.Coords.Count;
                        }
                        */
                        
                        Point location;
                        if (rowData.FreeCoords.Count == 3)
                        {
                            foreach (Point l in rowData.FreeCoords)
                            {
                                Console.WriteLine($"{l.Row}, {l.Col}");
                            }
                            location = rowData.FreeCoords[1];
                        }
                        else
                            location = rowData.FreeCoords[^1];
                        
                        if (rowData.Coords.Count > bestMove.Length ||
                            (rowData.Coords.Count == 3 && rowData.FreeCoords.Count == 2))
                        {
                            bestMove.BestMove = location;
                            bestMove.Length = rowData.Coords.Count;
                        }
                    }
                }

                rowData.ClearCoords();
                rowData.ClearFreeCoords();
                //rowData.Num = 0;
            }
        }
        
        private void DiagonalChecker(int mark, LocationContainer rowData, BestMoveContainer bestMove, string type, int start=0)
        {
            Dictionary<int, List<int>> direct = new Dictionary<int, List<int>>();
            for (int i = 0; i < _board.RowLength; i++)
            {
                if ((_board.RowLength - i) > (_board.ItemsNumberToWin - 1))
                    if (direct.ContainsKey(i))
                        direct[i].Add(1);
                    else
                    {
                        direct[i] = new List<int>();
                        direct[i].Add(1);
                    }
                
                if ((i + 1) >= _board.ItemsNumberToWin)
                    if (direct.ContainsKey(i))
                        direct[i].Add(-1);
                    else
                    {
                        direct[i] = new List<int>();
                        direct[i].Add(-1);
                    }
            }
            
            int firstLoopMax = type == "row" ? _board.RowLength : _board.ColLength;
            int secondLoopMax = type == "row" ? _board.ColLength : _board.RowLength;
            
            for (int i = 0; i < firstLoopMax; i++)
            {
                if (!direct.ContainsKey(i))
                    continue;
                
                for (int col_d = 0; col_d < direct[i].Count; col_d++)
                {
                    int[] d = new int[2];
                    if (type == "col")
                    {
                        d[0] = start;
                        d[1] = direct[i][col_d];
                    }
                    else
                    {
                        d[0] = direct[i][col_d];
                        d[1] = 1;
                    }

                    int[] coord = new int[2];
                    if (type == "col" && start == 1)
                    {
                        coord[0] = 0;
                        coord[1] = i;
                    }
                    else if (type == "col" && start == -1)
                    {
                        coord[0] = _board.RowLength - 1;
                        coord[1] = i;
                    }
                    else
                    {
                        coord[0] = i;
                        coord[1] = 0;
                    }

                    bool loop = true;
                    
                    while (loop)
                    {
                        int lenToReact;
                        if (_board.UpperBound(0) < coord[0] || _board.UpperBound(1) < coord[1])
                            break;
                        
                        int boardValue = _board.GetValue(coord[0], coord[1]);
                        if (boardValue == mark)
                        {
                            if (rowData.CoordsContains(coord[0] - d[0], coord[1] - d[1]) ||
                                rowData.FreeCoordsContains(coord[0] - d[0], coord[1] - d[1]))
                            {
                                //rowData.AddCoordsAtFirstPos(coord[0], coord[1]);
                                
                                
                                // Check if there is a space between marks of the same type
                                if (rowData.CoordsContains(coord[0] - 2, coord[1] - 2))
                                    // if YES, then insert the position to the beginning of the list from which the best move are picked
                                    rowData.AddCoordsAtFirstPos(coord[0], coord[1]);
                                else
                                    // if NOT, then append the position to the end of the list
                                    rowData.AddCoords(coord[0], coord[1]);
                                
                            }
                            else
                            {
                                rowData.ClearCoords();
                                rowData.ClearFreeCoords();
                                rowData.AddCoords(coord[0], coord[1]);
                            }
                        }
                        else if (boardValue == 0 && !rowData.FreeCoordsContains(coord[0], coord[1]))
                        {
                            rowData.AddFreeCoords(coord[0], coord[1]);
                            //rowData.Num++;
                        }
                        else if (boardValue == 0)
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                            rowData.AddFreeCoords(coord[0], coord[1]);
                            //rowData.Num++;
                        }
                        else
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                        }

                        // lenToReact - the number of marks that need to be next to each other to trigger AI's attention 
                        lenToReact = (_board.RowLength - _board.ItemsNumberToWin) >= 1 ? 3 : 1;

                        if (rowData.Coords.Count >= (_board.ItemsNumberToWin - lenToReact) &&
                            rowData.FreeCoords.Count >= (_board.ItemsNumberToWin - rowData.Coords.Count))
                        {
                            //if (rowData.FreeCoords.Count > 0)
                            //{
                            Point location;
                            if (rowData.FreeCoords.Count == 3)
                            {
                                foreach (Point l in rowData.FreeCoords)
                                {
                                    Console.WriteLine($"{l.Row}, {l.Col}");
                                }
                                location = rowData.FreeCoords[1];
                            }
                            else
                                location = rowData.FreeCoords[^1];

                            // If the number of positions of the same mark that are next to each other or are separated by at most one space
                            // is larger than length of the best move, then the bestMove object is updated
                            Console.WriteLine($"free: {rowData.FreeCoords.Count}");
                            if (rowData.Coords.Count > bestMove.Length ||
                                (rowData.Coords.Count == 3 && rowData.FreeCoords.Count == 2))
                            {
                                bestMove.BestMove = location;
                                bestMove.Length = rowData.Coords.Count;
                            }

                            //}
                        }

                        coord[0] += d[0];
                        coord[1] += d[1];

                        if (coord[0] < 0 || coord[1] < 0)
                            loop = false;
                    }
                    
                    rowData.ClearCoords();
                    rowData.ClearFreeCoords();
                    //rowData.Num = 0;
                }
            }
        }
        
        private void _DiagonalChecker(int mark, LocationContainer rowData, BestMoveContainer bestMove)
        {
            for (int rowIndex = 0; rowIndex < _board.RowLength; rowIndex++)
            {
                for (int colIndex = 0; colIndex < _board.ColLength; colIndex++)
                {
                    int[] boardDimensions = new int[] {_board.RowLength, _board.ColLength};
                    int indexMin = boardDimensions.Min();
                    int row = rowIndex;
                    int col = colIndex;

                    Point loc = new Point(rowIndex, colIndex);
                    if (_board.RightPossible(loc) && _board.DownPossible(loc))
                    {
                        for (int i = 0; i < indexMin; i++)
                        {
                            // Check if the calculated indexes are valid for the board array
                            if (_board.UpperBound(0) >= row && _board.UpperBound(1) >= col)
                            {
                                int lenToReact;
                                int boardValue = _board.GetValue(row, col);
                                if (_board.marks[boardValue] == mark)
                                {
                                    if (rowData.CoordsContains(row - 1, col - 1) ||
                                        rowData.FreeCoordsContains(row - 1, col - 1))
                                    {
                                        // Check if there is a space between marks of the same type
                                        if (rowData.CoordsContains(row - 2, col - 2))
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
                                else if (boardValue == 0 && !rowData.FreeCoordsContains(row - 1, col - 1))
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

                                    if (rowData.Coords.Count > bestMove.Length)
                                    {
                                        bestMove.BestMove = location;
                                        bestMove.Length = rowData.Coords.Count;
                                    }
                                    //}
                                }
                            }

                            row++;
                            col++;
                        }
                    }
                    rowData.ClearCoords();
                    rowData.ClearFreeCoords();
                }
                
                //rowData.ClearCoords();
                //rowData.ClearFreeCoords();
                //rowData.Num = 0;
            }
        }
    }
}