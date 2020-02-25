using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Sudoku
    {
        private Square[,] _square;
        private int now_count = 0;
        private int prev_coount = 0;

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

                prev_coount = now_count;
                now_count = countInputedNumber();

                if(prev_coount == now_count)
                {
                    Console.WriteLine("仮置きロジック");
                    doKarioki();
                    return;
                }

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

        private Square[,] makeClone(Square[,] _square)
        {
            Square[,] ret = new Square[9, 9];
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    ret[i, j] = _square[i, j].Clone();
                }
            }

            return ret;
        }

        private int countInputedNumber()
        {
            int ret = 0;
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(_square[i, j].isConfirmed())
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }

        private void doKarioki()
        {
            Square[,] copySquare = makeClone(_square);
            List<Square> kariokiList = searchKariokiSquare(copySquare);
            foreach(var s in kariokiList)
            {
                Console.WriteLine("[{0},{1}]", s.Row, s.Col);
            }
        }

        private List<Square> searchKariokiSquare(Square[,] squares)
        {
            List<Square> ret = null;
            for(int row = 0; row < 9; row += 3)
            {
                for(int col = 0; col < 9; col += 3)
                {
                    List<Square> temp = new List<Square>();
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            if(squares[row + i, col + j].isConfirmed() == false)
                            {
                                temp.Add(_square[row + i, col + j]);
                            }
                        }
                    }
                    if(ret != null)
                    {
                        if(ret.Count > temp.Count && temp.Count != 0)
                        {
                            ret = temp;
                        }
                    }
                    else
                    {
                        ret = temp;
                    }
                }
            }
            return ret;
        }
    }
}
