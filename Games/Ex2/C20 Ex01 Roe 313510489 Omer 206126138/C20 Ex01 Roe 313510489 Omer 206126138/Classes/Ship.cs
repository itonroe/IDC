using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invaders.Classes
{
    public class Ship
    {
        private const int r_ShipSpeed = 140;

        private readonly string r_TexturePath = @"Sprites\Ship01_32x32";

        private Texture2D m_ShipTexture;
        private Vector2 m_ShipPosition;

        private Bullet m_Bullet1;
        private Bullet m_Bullet2;

        private int m_Lifes = 3;

        public Ship()
        {
            m_ShipPosition = new Vector2(0, 0);
            m_Bullet1 = new Bullet(Color.Red);
            m_Bullet2 = new Bullet(Color.Red);
        }

        public Vector2 Position 
        { 
            get 
            { 
                return m_ShipPosition; 
            } 

            set 
            { 
                m_ShipPosition = value; 
            } 
        }

        public Texture2D Texture 
        { 
            get 
            { 
                return m_ShipTexture; 
            } 
            
            set 
            { 
                m_ShipTexture = value; 
            } 
        }

        public Bullet Bullet1 
        { 
            get 
            { 
                return m_Bullet1; 
            } 
            
            set 
            { 
                m_Bullet1 = value; 
            } 
        }

        public Bullet Bullet2 
        { 
            get 
            { 
                return m_Bullet2; 
            } 

            set 
            { 
                m_Bullet2 = value; 
            } 
        }

        public int Lifes 
        { 
            get 
            { 
                return m_Lifes; 
            } 
            
            set 
            { 
                m_Lifes = value; 
            } 
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_ShipTexture = i_contentManager.Load<Texture2D>(r_TexturePath);
            m_Bullet1.LoadContent(i_contentManager);
            m_Bullet2.LoadContent(i_contentManager);
        }

        public void InitPosition(GraphicsDevice i_graphicDevice)
        {
            // Get the bottom and center:
            float x = (float)i_graphicDevice.Viewport.Width - m_ShipTexture.Width;
            float y = (float)i_graphicDevice.Viewport.Height - 50;
            m_ShipPosition = new Vector2(x, y);
        }

        public void MoveRight(GameTime gameTime, GraphicsDevice i_graphicDevice)
        {
            m_ShipPosition.X += r_ShipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_ShipPosition.X = Math.Clamp(m_ShipPosition.X, 0, (float)i_graphicDevice.Viewport.Width - m_ShipTexture.Width);
        }

        public void MoveLeft(GameTime gameTime, GraphicsDevice i_graphicDevice)
        {
            m_ShipPosition.X -= r_ShipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_ShipPosition.X = Math.Clamp(m_ShipPosition.X, 0, (float)i_graphicDevice.Viewport.Width - m_ShipTexture.Width);
        }

        public void Shot()
        {
            if(!m_Bullet1.IsActive)
            {
                m_Bullet1.ChangedToActive(new Vector2(m_ShipPosition.X + (m_ShipTexture.Width / 2), m_ShipPosition.Y));

                return;
            }

            if (!m_Bullet2.IsActive)
            {
                m_Bullet2.ChangedToActive(new Vector2(m_ShipPosition.X + (m_ShipTexture.Width / 2), m_ShipPosition.Y));
            }
        }

        public bool BulletIntersectsShip(Bullet i_bullet, GraphicsDevice i_graphicDevice)
        {
            bool hit = false;

            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle shipRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            if (bulletRectangle.Intersects(shipRectangle))
            {
                hit = true;
                this.Hit(i_graphicDevice);
                i_bullet.IsActive = false;
            }

            return hit;
        }

        private void Hit(GraphicsDevice i_graphicDevice)
        {
            InitPosition(i_graphicDevice);
            m_Lifes--;
        }

        public void Draw(SpriteBatch i_spriteBatch)
        {
            // Ship draw
            i_spriteBatch.Draw(m_ShipTexture, m_ShipPosition, Color.White);

            // BulletDraw
            if(m_Bullet1.IsActive)
            {
                m_Bullet1.Draw(i_spriteBatch);
            }

            if (m_Bullet2.IsActive)
            {
                m_Bullet2.Draw(i_spriteBatch);
            }
        }
    }
}
