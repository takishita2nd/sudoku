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
            if (args.Length < 1)
            {
                Console.WriteLine("usage : sudoku.exe [input file] [output file]");
                return;
            }

            // ファイルの存在を確認
            string filePath = Environment.CurrentDirectory + "\\" + args[0];
            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("File not found.");
                return;
            }

            // 出力ファイル名
            string outfile = null;
            if (args.Length == 2)
            {
                outfile = Environment.CurrentDirectory + "\\" + args[1];
            }
            else
            {
                outfile = filePath + ".output";
            }

            var sq = FileRead.OpenFile(filePath);
            if(sq == null)
            {
                return;
            }

            Sudoku sudoku = new Sudoku(sq, outfile);
            sudoku.run();
        }
    }
}
