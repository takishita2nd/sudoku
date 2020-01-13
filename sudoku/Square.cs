using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Square
    {
        class Candidate
        {
            public bool value1;
            public bool value2;
            public bool value3;
            public bool value4;
            public bool value5;
            public bool value6;
            public bool value7;
            public bool value8;
            public bool value9;

            public Candidate()
            {
                this.value1 = false;
                this.value2 = false;
                this.value3 = false;
                this.value4 = false;
                this.value5 = false;
                this.value6 = false;
                this.value7 = false;
                this.value8 = false;
                this.value9 = false;
            }
        }

        // 確定した数字
        private int _value;
        // 確定したかどうか
        private bool _confirmed;
        // 候補の数字
        private Candidate _candidate;

        public Square()
        {
            this._value = 0;
            this._confirmed = false;
            this._candidate = new Candidate();
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
            this._candidate = new Candidate();
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
    }
}
