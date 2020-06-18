using System;
using System.Collections.Generic;
using System.Text;

namespace Ex4
{

    public class MenuItem
    {
        private string m_Text;
        private List<MenuItem> m_Parent = null;
        private List<MenuItem> m_SubManu = new List<MenuItem>();

        public event Action<MenuItem> Click;

        private void OnClicked()
        {
            if (Click != null)
                Click.Invoke(this);
        }
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public List<MenuItem> Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        public override string ToString()
        {
            return "[{" + m_Text + "}]";
        }

        public void AddSubManuItem(MenuItem i_ManuItemToAdd)
        {
            m_SubManu.Add(i_ManuItemToAdd);
        }

    }
}
