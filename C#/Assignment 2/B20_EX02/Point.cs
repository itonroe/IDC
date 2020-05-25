using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public struct Point
    {
        private int m_X;
        public int X
        {
            get
            {
                return m_X;
            }
            set
            {
                m_X = value;
            }
        }
        private int m_Y;
        public int Y
        {
            get
            {
                return m_Y;
            }
            set
            {
                m_Y = value;
            }
        }

        public Point(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }
    }
}
