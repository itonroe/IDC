using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class Barriers
    {
        Barrier[] m_Barriers;

        public Barriers(Game i_Game, int i_NumOfBarriers)
        {
            m_Barriers = new Barrier[i_NumOfBarriers];
            for(int i=0; i< i_NumOfBarriers; i++)
            {
                m_Barriers[i] = new Barrier(i_Game);
            }
        }

        public void InitPositions(GraphicsDevice i_graphicDevice)
        {
            int barriersWidth = m_Barriers[0].Texture.Width;

            int allMiddleX = (int) (4 * barriersWidth + 1.3 * 3 * barriersWidth);
            int firstX = (i_graphicDevice.Viewport.Width - allMiddleX) / 2;

            for(int i=0; i<m_Barriers.Length; i++)
            {
                m_Barriers[i].Initialize(new Vector2(firstX + ( i * (barriersWidth * (float)(1 + 1.3))), i_graphicDevice.Viewport.Height - 100));
            }
        }

        public void BulletIntersection(List<Bullet> i_Bullets)
        {
            foreach (Barrier barrier in m_Barriers)
            {
                foreach(Bullet bullet in i_Bullets)
                {
                    barrier.BulletIntersectionRectangle(bullet);
                }
       
            }
        }
    }
}
