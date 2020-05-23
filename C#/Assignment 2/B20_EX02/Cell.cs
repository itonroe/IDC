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
        public Point Point
        {
            get
            {
                return m_Point;
            }
        }

        public Cell(int x, int y)
        {
            m_Visisble = false;
            m_Point = new Point(x, y);
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
    }
}
