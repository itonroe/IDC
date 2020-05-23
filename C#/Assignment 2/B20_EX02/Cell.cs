using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class Cell
    {
        private char m_Letter;
        private bool m_Visisble;

        public Cell()
        {
            m_Visisble = false;
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
