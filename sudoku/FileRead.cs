using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    static class FileRead
    {
        /**
         * ファイルからデータを取得する
         */
        public static Square[,] OpenFile(string filePath)
        {
            int[,] matrix = new int[9, 9];

            // ファイルを開く
            bool error = false;
            using (var stream = new StreamReader(filePath))
            {
                int row = 0;
                while (stream.EndOfStream == false)
                {
                    if (row >= 9)
                    {
                        error = true;
                        break;
                    }
                    string lineText = stream.ReadLine();
                    var val = lineText.Split(',');
                    if (val.Length > 9)
                    {
                        error = true;
                        break;
                    }
                    int col = 0;
                    foreach (var v in val)
                    {
                        int i;
                        if (int.TryParse(v, out i))
                        {
                            if(i >= 10)
                            {
                                error = true;
                                break;
                            }
                            matrix[row, col] = i;
                        }
                        else
                        {
                            error = true;
                            break;
                        }
                        col++;
                    }
                    if (error)
                    {
                        break;
                    }
                    row++;
                }
            }
            if (error)
            {
                Console.WriteLine("Illegal format.");
                return null;
            }

            Square[,] ret = new Square[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Square sq = new Square(matrix[row, col], row, col);
                    ret[row, col] = sq;
                }
            }

            return ret;
        }
    }
}
