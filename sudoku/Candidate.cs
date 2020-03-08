using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    /**
     * マス値候補を調査するクラス
     */
    class Candidate
    {
        /* true:除外/false:候補 */
        public bool[] value;

        /**
         * コンストラクタ
         */
        public Candidate()
        {
            this.value = new bool[9] { false, false, false, false, false, false, false, false, false };
        }

        /**
         * 除外する値の数を数える
         */
        public int Count()
        {
            int ret = 0;
            foreach(var val in this.value)
            {
                if(val == true)
                {
                    ret++;
                }
            }

            return ret;
        }
    }
}
