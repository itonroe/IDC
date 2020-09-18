using C20_Ex01_Roe_313510489_Omer_206126138;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class Barrier
    {
        private Texture2D m_Texture;
        private Vector2 m_Position;
        private const string k_TexturePath = @"Sprites\Barrier_44x32";

        public Texture2D Texture
        {
            get
            {
                return m_Texture;
            }
        }

        public void Initialize(Vector2 i_Poition)
        {
            m_Position = i_Poition;
        }


        public void LoadContent(ContentManager i_ContentManager)
        {
            m_Texture = i_ContentManager.Load<Texture2D>(k_TexturePath);
        }

        public void Draw(SpriteBatch i_SpriteBatch)
        {
            i_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
        }

        public bool BulletIntersectionRectangle(Bullet i_bullet)
        {
            bool hit = false;

            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle barriesRectangle = new Rectangle((int)this.m_Position.X, (int)this.m_Position.Y, this.Texture.Width, this.Texture.Height);

            if (bulletRectangle.Intersects(barriesRectangle))
            {
                hit = true;
                i_bullet.IsActive = false;
                CloseRectangles(i_bullet);
            }

            return hit;
        }

        public void CloseRectangles(Bullet i_bullet)
        {
            Color[] barrierPixels = new Color[this.Texture.Width * this.Texture.Height];
            this.Texture.GetData<Color>(barrierPixels);

            Color[] bulletPixels = new Color[i_bullet.Texture.Width * i_bullet.Texture.Height];
            i_bullet.Texture.GetData<Color>(bulletPixels);

            
        }
    }
}
