﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Sudoku
    {
        private Square[,] _square;

        /**
         * コンストラクタ
         */
        public Sudoku(Square[,] square)
        {
            _square = square;
        }

        /**
         * 実行
         */
        public void run()
        {
            bool roop = true;
            while (roop)
            {
                for(int row = 0; row < 9; row++)
                {
                    for(int col = 0; col < 9; col++)
                    {
                        if(_square[row,col].isConfirmed() == false)
                        {
                            Candidate candidate = new Candidate();
                            searchRowLine(row, candidate);
                            searchColLine(col, candidate);
                            search9Area(row, col, candidate);
                            _square[row, col].checkCandidate(candidate);
                        }
                    }
                }
                searchNumber();

                roop = !checkEnd();
                FileAccess.Output(_square);
            }
        }

        private void searchRowLine(int row, Candidate candidate)
        {
            for(int i = 0; i < 9; i++)
            {
                int val = _square[row, i].GetValue();
                if(val != 0)
                {
                    candidate.value[val - 1] = true;
                }
            }
        }

        private void searchColLine(int col, Candidate candidate)
        {
            for (int i = 0; i < 9; i++)
            {
                int val = _square[i, col].GetValue();
                if (val != 0)
                {
                    candidate.value[val - 1] = true;
                }
            }
        }

        private void search9Area(int row, int col, Candidate candidate)
        {
            int rowStart;
            int colStart;
            getRowCol9Area(row, col, out rowStart, out colStart);

            for (int r = rowStart; r < rowStart+3; r++)
            {
                for (int c = colStart; c < colStart + 3; c++)
                {
                    int val = _square[r, c].GetValue();
                    if (val != 0)
                    {
                        candidate.value[val - 1] = true;
                    }
                }
            }
        }

        private void searchNumber()
        {
            for(int number = 1; number <= 9; number++)
            {
                bool[,] tempTable = new bool[9, 9];
                // 初期化
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        tempTable[i, j] = false;
                    }
                }

                // 数字が入らないところをtrueにする
                for (int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        if(tempTable[i,j] == false)
                        {
                            tempTable[i, j] = _square[i, j].isConfirmed();
                            if(_square[i,j].GetValue() == number)
                            {
                                for(int row = 0; row < 9; row++)
                                {
                                    tempTable[row, j] = true;
                                }
                                for(int col = 0; col < 9; col++)
                                {
                                    tempTable[i, col] = true;
                                }

                                int rowStart;
                                int colStart;
                                getRowCol9Area(i, j, out rowStart, out colStart);
                                for (int r = rowStart; r < rowStart + 3; r++)
                                {
                                    for (int c = colStart; c < colStart + 3; c++)
                                    {
                                        tempTable[r, c] = true;
                                    }
                                }
                            }
                        }
                    }
                }

                // debug
                FileAccess.Output(tempTable);

                // 結果を確認する
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        if(tempTable[i,j] == false)
                        {
                            int rowStart;
                            int colStart;
                            getRowCol9Area(i, j, out rowStart, out colStart);

                            int count = 0;
                            for (int r = rowStart; r < rowStart + 3; r++)
                            {
                                for (int c = colStart; c < colStart + 3; c++)
                                {
                                    if(tempTable[r,c] == false)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if(count == 1)
                            {
                                _square[i, j].SetValue(number);
                            }
                        }
                    }
                }
            }
        }

        private void getRowCol9Area(int row, int col, out int rowStart, out int colStart)
        {
            if (row >= 0 && row <= 2)
            {
                rowStart = 0;
            }
            else if (row >= 3 && row <= 5)
            {
                rowStart = 3;
            }
            else
            {
                rowStart = 6;
            }

            if (col >= 0 && col <= 2)
            {
                colStart = 0;
            }
            else if (col >= 3 && col <= 5)
            {
                colStart = 3;
            }
            else
            {
                colStart = 6;
            }
        }

        private bool checkEnd()
        {
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(_square[i, j].isConfirmed() == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
