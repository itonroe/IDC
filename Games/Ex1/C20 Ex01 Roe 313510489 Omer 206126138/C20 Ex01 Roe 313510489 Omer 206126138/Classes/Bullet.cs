using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invaders.Classes
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

        // 1000 refers to 1000 milisecond in a second
        public void MoveUp(GameTime gameTime)
        {
            m_CountSec += gameTime.ElapsedGameTime.TotalMilliseconds;

            if(m_CountSec >= 1000/m_BulletSpeed)
            {
                m_bulletPosition.Y -= 1;
                m_CountSec -= 1000 / m_BulletSpeed;
            }
        }

        public void MoveDown(GameTime gameTime)
        {
            m_CountSec += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (m_CountSec >= 1000 / m_BulletSpeed)
            {
                m_bulletPosition.Y += 1;
                m_CountSec -= 1000 / m_BulletSpeed;
            }
        }

        public void UpdateForShip(GameTime gameTime)
        {
            if(m_Active)
            {
                this.MoveUp(gameTime);
                if (m_bulletPosition.Y <= 0) 
                {
                    m_Active = false;
                }
            }
        }

        public void UpdateForEnemy(GraphicsDevice i_graphicDevice, GameTime gameTime)
        {
            if (m_Active)
            {
                this.MoveDown(gameTime);
                if (m_bulletPosition.Y >= i_graphicDevice.Viewport.Height)
                {
                    m_Active = false;
                }
            }
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
