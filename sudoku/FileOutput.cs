using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class FileOutput
    {
        private string _filename;
        public FileOutput(string filename)
        {
            _filename = filename;
            if (File.Exists(_filename) == true)
            {
                File.Delete(_filename);
            }
        }

        public void Output(Square[,] sq)
        {
            using (var stream = new StreamWriter(_filename, true))
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

        public void Output(bool[,] sq)
        {
            using (var stream = new StreamWriter(_filename, true))
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (sq[row, col] == true)
                        {
                            stream.Write(1);
                        }
                        else
                        {
                            stream.Write(0);
                        }
                    }
                    stream.Write("\r\n");
                }
                stream.Write("\r\n");
            }
        }

        public void Output(int row, int col, int value)
        {
            using (var stream = new StreamWriter(_filename, true))
            {
                stream.Write("[{0},{1}] => {2}\r\n", row, col, value);
            }
        }

        public void Output(string text)
        {
            using (var stream = new StreamWriter(_filename, true))
            {
                stream.Write("{0}\r\n", text);
            }
        }

        public void Output(List<Square> list)
        {
            using (var stream = new StreamWriter(_filename, true))
            {
                stream.Write("候補 ");
                foreach (Square s in list)
                {
                    stream.Write("[{0},{1}]", s.Row, s.Col);
                }
                stream.Write("\r\n");
            }
        }
    }
}
