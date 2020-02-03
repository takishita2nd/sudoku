using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Square
    {
        // 確定した数字
        private int _value;
        // 確定したかどうか
        private bool _confirmed;

        public Square()
        {
            this._value = 0;
            this._confirmed = false;
        }

        public Square(int val)
        {
            this._value = val;
            if(val == 0)
            {
                this._confirmed = false;
            }
            else
            {
                this._confirmed = true;
            }
        }

        public int GetValue()
        {
            return this._value;
        }

        public void SetValue(int val)
        {
            this._value = val;
            this._confirmed = true;
        }

        public bool isConfirmed()
        {
            return this._confirmed;
        }

        public void checkCandidate(Candidate candidate)
        {
            if(candidate.Count() == 8)
            {
                for(int i = 0; i < 9; i++)
                {
                    if(candidate.value[i] == false)
                    {
                        SetValue(i + 1);
                        break;
                    }
                }
            }
        }

        public Square Clone()
        {
            return new Square(_value);
        }
    }
}
