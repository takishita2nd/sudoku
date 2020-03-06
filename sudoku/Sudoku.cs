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
            int now_count = 0;
            int prev_coount = 0;
            while (roop)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (_square[row, col].isConfirmed() == false)
                        {
                            Candidate candidate = new Candidate();
                            searchRowLine(_square, row, candidate);
                            searchColLine(_square, col, candidate);
                            search9Area(_square, row, col, candidate);
                            _square[row, col].checkCandidate(candidate);
                        }
                    }
                }
                searchNumber(_square);

                prev_coount = now_count;
                now_count = countInputedNumber(_square);

                if (prev_coount == now_count)
                {
                    Console.WriteLine("仮置きロジック");
                    Square s = doKarioki(_square);
                    if(s == null)
                    {
                        Console.WriteLine("失敗しました");
                        return;
                    }
                    else
                    {
                        _square[s.Row, s.Col].SetValue(s.GetValue());
                    }
                }

                roop = !checkEnd(_square);
                FileAccess.Output(_square);
            }
        }

        private void searchRowLine(Square[,] squares, int row, Candidate candidate)
        {
            for (int i = 0; i < 9; i++)
            {
                int val = squares[row, i].GetValue();
                if (val != 0)
                {
                    candidate.value[val - 1] = true;
                }
            }
        }

        private void searchColLine(Square[,] squares, int col, Candidate candidate)
        {
            for (int i = 0; i < 9; i++)
            {
                int val = squares[i, col].GetValue();
                if (val != 0)
                {
                    candidate.value[val - 1] = true;
                }
            }
        }

        private void search9Area(Square[,] squares, int row, int col, Candidate candidate)
        {
            int rowStart;
            int colStart;
            getRowCol9Area(row, col, out rowStart, out colStart);

            for (int r = rowStart; r < rowStart + 3; r++)
            {
                for (int c = colStart; c < colStart + 3; c++)
                {
                    int val = squares[r, c].GetValue();
                    if (val != 0)
                    {
                        candidate.value[val - 1] = true;
                    }
                }
            }
        }

        private void searchNumber(Square[,] squares)
        {
            for (int number = 1; number <= 9; number++)
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
                    for (int j = 0; j < 9; j++)
                    {
                        if (tempTable[i, j] == false)
                        {
                            tempTable[i, j] = squares[i, j].isConfirmed();
                            if (squares[i, j].GetValue() == number)
                            {
                                for (int row = 0; row < 9; row++)
                                {
                                    tempTable[row, j] = true;
                                }
                                for (int col = 0; col < 9; col++)
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
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (tempTable[i, j] == false)
                        {
                            int rowStart;
                            int colStart;
                            getRowCol9Area(i, j, out rowStart, out colStart);

                            int count = 0;
                            for (int r = rowStart; r < rowStart + 3; r++)
                            {
                                for (int c = colStart; c < colStart + 3; c++)
                                {
                                    if (tempTable[r, c] == false)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (count == 1)
                            {
                                squares[i, j].SetValue(number);
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

        private bool checkEnd(Square[,] squares)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (squares[i, j].isConfirmed() == false)
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
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ret[i, j] = _square[i, j].Clone();
                }
            }

            return ret;
        }

        private int countInputedNumber(Square[,] _square)
        {
            int ret = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_square[i, j].isConfirmed())
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }

        private Square doKarioki(Square[,] squares)
        {
            Square ret = null;
            List<Square> kariokiList = searchKariokiSquare(squares);
            foreach (var s in kariokiList)
            {
                bool roop = true;
                int kariValue = GetUnconfirmedValue(s.GetCandidate());
                if (kariValue == 0)
                {
                    return null;
                }
                Square[,] copySquare = makeClone(squares);
                copySquare[s.Row, s.Col].SetValue(kariValue);
                int now_count = 0;
                int prev_coount = 0;
                while (roop)
                {
                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            if (copySquare[row, col].isConfirmed() == false)
                            {
                                Candidate candidate = new Candidate();
                                searchRowLine(copySquare, row, candidate);
                                searchColLine(copySquare, col, candidate);
                                search9Area(copySquare, row, col, candidate);
                                copySquare[row, col].checkCandidate(candidate);
                            }
                        }
                    }
                    searchNumber(copySquare);

                    if (checkContradict(copySquare))
                    {
                        break;
                    }

                    prev_coount = now_count;
                    now_count = countInputedNumber(copySquare);

                    if (prev_coount == now_count)
                    {
                        Console.WriteLine("仮置きロジック");
                        Square s2 = doKarioki(copySquare);
                        if (s2 == null)
                        {
                            Console.WriteLine("失敗しました");
                            return null;
                        }
                        else
                        {
                            copySquare[s2.Row, s2.Col].SetValue(s2.GetValue());
                        }
                    }

                    if (checkEnd(copySquare) == true)
                    {
                        roop = false;
                        s.SetValue(kariValue);
                        Console.WriteLine("[{0},{1}] = {2}", s.Row, s.Col, s.GetValue());
                        ret = s;
                    }
                    FileAccess.Output(copySquare);
                }
                if(ret != null)
                {
                    break;
                }
            }
            return ret;
        }

        private List<Square> searchKariokiSquare(Square[,] squares)
        {
            List<Square> ret = null;
            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    List<Square> temp = new List<Square>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (squares[row + i, col + j].isConfirmed() == false)
                            {
                                temp.Add(_square[row + i, col + j]);
                            }
                        }
                    }
                    if (ret != null)
                    {
                        if (ret.Count > temp.Count && temp.Count != 0)
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

        private int GetUnconfirmedValue(Candidate candidate)
        {
            for (int i = 0; i < candidate.value.Length; i++)
            {
                if (candidate.value[i] == false)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private bool checkContradict(Square[,] squares)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if(squares[row,col].isConfirmed() == false)
                    {
                        Candidate candidate = new Candidate();
                        searchRowLine(_square, row, candidate);
                        searchColLine(_square, col, candidate);
                        search9Area(_square, row, col, candidate);
                        if(candidate.Count() == 9)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
