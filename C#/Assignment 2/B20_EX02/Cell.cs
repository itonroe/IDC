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

        public Cell(Point i_Point, char i_Letter)
        {
            m_Visisble = false;
            m_Point = i_Point;
            m_Letter = i_Letter;
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
