using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Candidate
    {
        public bool[] value;

        public Candidate()
        {
            this.value = new bool[9] { false, false, false, false, false, false, false, false, false };
        }

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
