using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invaders.Classes
{
    class Ship
    {
        private GraphicsDeviceManager m_graphicsManager;
        private SpriteBatch m_spriteBatch;
        private GraphicsDevice m_graphicDevice;
        private Microsoft.Xna.Framework.Content.ContentManager m_contentManager;

        private readonly string m_texturePath = "Ship01_32x32";

        Texture2D m_shipTexture;
        Vector2 m_shipPosition;

        Bullet m_bullet1;
        Bullet m_bullet2;

        public Ship(Vector2 i_shipPosition, GraphicsDeviceManager i_graphicsManager, SpriteBatch i_spriteBatch, GraphicsDevice i_graphicsDevice, Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_graphicsManager = i_graphicsManager;
            m_spriteBatch = i_spriteBatch;
            m_shipPosition = i_shipPosition;
            m_graphicDevice = i_graphicsDevice;
            m_contentManager = i_contentManager;
        }

        protected void LoadContent()
        {
            m_shipTexture = m_contentManager.Load<Texture2D>(@"sprites\" + m_texturePath);
            m_bullet1 = new Bullet(m_shipPosition, m_graphicsManager, m_spriteBatch, m_graphicDevice);
            m_bullet2 = new Bullet(m_shipPosition, m_graphicsManager, m_spriteBatch, m_graphicDevice);
        }

        private void InitPosition()
        {
            float x = (float)m_graphicDevice.Viewport.Width / 2;
            float y = (float)m_graphicDevice.Viewport.Height;
        }

        public void MoveRight(int i_distance)
        {
            if(m_shipPosition.X < (float)m_graphicDevice.Viewport.Width)
                m_shipPosition.X += i_distance;
        }

        public void MoveLeft(int i_distance)
        {
            if(m_shipPosition.X > 0)
                m_shipPosition.X -= i_distance;
        }
    }

}
