using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class Cell
    {
        private char m_Letter;
        private bool m_Visisble;
        private Point m_Point;

        public Cell(int i_X, int i_Y)
        {
            m_Visisble = false;
            m_Point = new Point(i_X, i_Y);
        }

        public bool Visible
        {
            get
            {
                return m_Visisble;
            }
            set
            {
                m_Visisble = value;
            }
        }

        public char Letter
        {
            get
            {
                return m_Letter;
            }
            set
            {
                m_Letter = value;
            }
        }

        public Point Point
        {
            get
            {
                return m_Point;
            }
        }
    }
}
