using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    static class FileAccess
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
                    string lineText = stream.ReadLine();
                    var val = lineText.Split(',');
                    int col = 0;
                    foreach (var v in val)
                    {
                        int i;
                        if (int.TryParse(v, out i))
                        {
                            matrix[row, col] = i;
                        }
                        else
                        {
                            error = true;
                        }
                        col++;
                    }
                    row++;
                    if (row > 9)
                    {
                        error = true;
                    }
                }
            }
            if (error)
            {
                Console.WriteLine("Illegal format.");
                return null;
            }

            Square[,] ret = new Square[9, 9]; 
            for (int row = 0; row < 9; row++ )
            {
                for(int col = 0; col < 9; col++ )
                {
                    Square sq = new Square(matrix[row, col]);
                    ret[row, col] = sq;
                }
            }

            return ret;
        }

        // debug
        public static void Output(Square[,] sq)
        {
            using (var stream = new StreamWriter(System.Environment.CurrentDirectory + "\\output", true))
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        stream.Write(sq[row, col].GetValue());
                    }
                    stream.Write("\r\n");
                }
                stream.Write("\r\n");
            }
        }
    }
}
