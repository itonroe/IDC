using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class Bullet
    {
        private const int r_BulletSpeed = 140; // pxl pre sec

        private readonly string r_TexturePath = @"Sprites\Bullet";

        private Texture2D m_BulletTexture;
        private Vector2 m_BulletPosition;
        private Color m_BulletColor;

        private bool m_Active;

        public Bullet(Color i_color)
        {
            m_Active = false;
            m_BulletColor = i_color;
        }

        public Vector2 Position 
        {
            get 
            { 
                return m_BulletPosition; 
            } 

            set 
            {
                m_BulletPosition = value; 
            } 
        }

        public Texture2D Texture 
        { 
            get 
            { 
                return m_BulletTexture; 
            } 

            set 
            { 
                m_BulletTexture = value; 
            } 
        }

        public bool IsActive 
        { 
            get 
            { 
                return m_Active; 
            } 

            set 
            { 
                m_Active = value; 
            } 
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_BulletTexture = i_contentManager.Load<Texture2D>(r_TexturePath);
        }

        private void moveUp(GameTime gameTime)
        {
            m_BulletPosition.Y -= r_BulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void moveDown(GameTime gameTime)
        {
            m_BulletPosition.Y += r_BulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void UpdateForShip(GameTime gameTime)
        {
            if(m_Active)
            {
                this.moveUp(gameTime);
                if (m_BulletPosition.Y <= 0) 
                {
                    m_Active = false;
                }
            }
        }

        public bool UpdateForEnemy(GraphicsDevice i_graphicDevice, GameTime gameTime)
        {
            bool bulletOutOfScreen = false;

            if (m_Active)
            {
                this.moveDown(gameTime);
                if (m_BulletPosition.Y >= i_graphicDevice.Viewport.Height)
                {
                    m_Active = false;
                    bulletOutOfScreen = true;
                }
            }

            return bulletOutOfScreen;
        }

        public void ChangedToActive(Vector2 i_shipPosition)
        {
            m_Active = true;
            Position = i_shipPosition;
        }

        public void ChangeToNotActive()
        {
            m_Active = false;
        }

        public void Draw(SpriteBatch i_spriteBatch)
        {
            i_spriteBatch.Draw(m_BulletTexture, m_BulletPosition, m_BulletColor);
        }
    }
}
