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
            int roop = 0;
            while (true)
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

                //debug
                roop++;
                FileAccess.Output(_square);
                if(roop == 10)
                {
                    break;
                }
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
            if (row >= 0 && row <= 2)
            {
                rowStart = 0;
            }
            else if(row >= 3 && row <= 5)
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

            for(int r = rowStart; r < rowStart+3; r++)
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
    }
}
