using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    /**
     * マスクラス
     */
    class Square
    {
        // 確定した数字
        private int _value;
        public int Row { get; }
        public int Col { get; }
        private Candidate _candidate = null;

        // 確定したかどうか
        private bool _confirmed;

        /**
         * コンストラクタ
         */
        public Square()
        {
            this._value = 0;
            this._confirmed = false;
        }

        /**
         * コンストラクタ
         * 
         * val:初期値
         * row:列
         * col:行
         */
        public Square(int val, int row, int col)
        {
            this.Row = row;
            this.Col = col;
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

        /**
         * 設定済み値を取得する
         * 
         * return:設定値
         */
        public int GetValue()
        {
            return this._value;
        }

        /**
         * 値を設定する
         * 
         * val:設定値
         */
        public void SetValue(int val)
        {
            this._value = val;
            this._confirmed = true;
        }

        /**
         * 値が確定しているかを返す
         * 
         * return true:確定済み/false:未確定
         */
        public bool isConfirmed()
        {
            return this._confirmed;
        }

        /**
         * マス値候補から値を設定する
         * 
         * candidate:マス値候補
         */
        public void checkCandidate(Candidate candidate)
        {
            _candidate = candidate;
            if (candidate.Count() == 8)
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

        /**
         * オブジェクトのクローン作成
         * 
         * return Square
         */
        public Square Clone()
        {
            return new Square(_value, Row, Col);
        }

        /**
         * マス値候補を取得する
         * 
         * return Candidate
         */
        public Candidate GetCandidate()
        {
            return _candidate;
        }
    }
}
