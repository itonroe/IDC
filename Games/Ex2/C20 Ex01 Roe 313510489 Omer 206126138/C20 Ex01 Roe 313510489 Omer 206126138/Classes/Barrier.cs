using C20_Ex01_Roe_313510489_Omer_206126138;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class Barrier : Sprite
    {
        private const string k_TexturePath = @"Sprites\Barrier_44x32";

        public Barrier(Game i_Game) : base(k_TexturePath, i_Game)
        {
        }

        public void Initialize(Vector2 i_Poition)
        {
            Position = i_Poition;
            Visible = true;
        }

        public void Draw(SpriteBatch i_SpriteBatch)
        {
            if (Visible)
            {
                i_SpriteBatch.Draw(Texture, Position, Color.White);
            }
        }

        public bool BulletIntersectionRectangle(Bullet i_bullet)
        {
            bool hit = false;

            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle barriesRectangle = new Rectangle((int)this.m_Position.X, (int)this.m_Position.Y, this.Texture.Width, this.Texture.Height);

            if (bulletRectangle.Intersects(barriesRectangle))
            {
                int relativeX = Math.Clamp((int)(i_bullet.Position.X - this.m_Position.X), 0, this.Texture.Width);
                bool fromBottom = (i_bullet.Position.Y - this.m_Position.Y) >= 0;

                if (isBarrierGotHitFromBullet(fromBottom, (int)relativeX, i_bullet.Texture.Width, i_bullet.Texture.Height))
                {
                    hit = true;
                    i_bullet.IsActive = false;
                }
            }

            return hit;
        }

        private bool isBarrierGotHitFromBullet(bool i_bottom, int x, int i_bulletWidth , int i_bulletHeight)
        {
            bool hit = false;

            Color[] barrierPixels = new Color[this.Texture.Width * this.Texture.Height];
            this.Texture.GetData<Color>(barrierPixels);

            // max demage is 0.35 of bullet size
            int startCounter = (int)(i_bulletHeight * i_bulletWidth * 0.35);
            int demageCounter = startCounter;

            int yIteration = 0;

            // ship bullet
            if (i_bottom)
            {
                yIteration = this.Texture.Height - 1;

                for (int i = yIteration; i >= 0 && demageCounter > 0; i--)  // i=y
                {
                    for (int j = x; j < Math.Clamp(x + i_bulletWidth, 0, this.Texture.Width) && demageCounter > 0; j++) // j=x
                    {
                        if (barrierPixels[i * this.Texture.Width + j].A != 0)
                        {
                            barrierPixels[i * this.Texture.Width + j] = Color.Transparent;
                            demageCounter--;
                        }
                    }
                }
            }
            //enemy bullet
            else
            {
                for (int i = 0; i < this.Texture.Height && demageCounter > 0; i++)  // i=y
                {
                    for (int j = x; j < Math.Clamp(x + i_bulletWidth, 0, this.Texture.Width) && demageCounter > 0; j++) // j=x
                    {
                        if (barrierPixels[i * this.Texture.Width + j].A != 0)
                        {
                            barrierPixels[i * this.Texture.Width + j] = Color.Transparent;
                            demageCounter--;
                        }
                    }
                }
            }

            this.Texture = new Texture2D(GraphicsDevice, this.Texture.Width, this.Texture.Height);
            this.Texture.SetData<Color>(barrierPixels);

            if (demageCounter < startCounter)
                hit = true;

            return hit;
        }
    }
}
