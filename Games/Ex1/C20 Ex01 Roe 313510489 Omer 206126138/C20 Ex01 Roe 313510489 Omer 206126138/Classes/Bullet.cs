using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class Bullet
    {
        private readonly string m_texturePath = @"Sprites\Bullet";

        private Texture2D m_bulletTexture;
        private Vector2 m_bulletPosition;
        private Color m_bulletColor;

        private bool m_Active;
        private const int m_BulletSpeed = 140; //pxl pre sec
        private double m_CountSec = 0;

        public Bullet(Color i_color)
        {
            m_Active = false;
            m_bulletColor = i_color;
        }

        public Vector2 Position { get { return m_bulletPosition; } set { m_bulletPosition = value; } }

        public Texture2D Texture { get { return m_bulletTexture; } set { m_bulletTexture = value; } }

        public bool IsActive { get { return m_Active; } set { m_Active = value; } }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_bulletTexture = i_contentManager.Load<Texture2D>(m_texturePath);
        }

        public void MoveUp(GameTime gameTime, GraphicsDevice i_GraphicsDevice)
        {
            m_bulletPosition.Y -= 140 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveDown(GameTime gameTime, GraphicsDevice i_GraphicsDevice)
        {
            m_bulletPosition.Y += 140 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void UpdateForShip(GameTime gameTime, GraphicsDevice i_GraphicsDevice)
        {
            if(m_Active)
            {
                this.MoveUp(gameTime, i_GraphicsDevice);
                if (m_bulletPosition.Y <= 0) 
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
                this.MoveDown(gameTime, i_graphicDevice);
                if (m_bulletPosition.Y >= i_graphicDevice.Viewport.Height)
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
            i_spriteBatch.Draw(m_bulletTexture, m_bulletPosition, m_bulletColor);
        }
    }
}
