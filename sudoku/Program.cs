using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // パラメータチェック
            if (args.Length != 1)
            {
                Console.WriteLine("usage : sudoku.exe [input file]");
                return;
            }

            // ファイルの存在を確認
            string filePath = Environment.CurrentDirectory + "\\" + args[0];
            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("File not found.");
                return;
            }

            var sq = FileAccess.OpenFile(filePath);
            if(sq == null)
            {
                return;
            }

            // debug
            FileAccess.Output(sq);
        }
    }
}
