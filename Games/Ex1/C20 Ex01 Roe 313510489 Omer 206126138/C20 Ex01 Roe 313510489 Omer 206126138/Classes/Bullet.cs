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
        private GraphicsDeviceManager m_graphicsManager;
        private SpriteBatch m_spriteBatch;
        private GraphicsDevice m_graphicDevice;

        Texture2D m_bulletTexture;
        Vector2 m_bulletPosition;

        public Bullet(Vector2 i_bulletPosition, GraphicsDeviceManager i_graphicsManager, SpriteBatch i_spriteBatch, GraphicsDevice i_graphicsDevice)
        {
            m_graphicsManager = i_graphicsManager;
            m_spriteBatch = i_spriteBatch;
            m_bulletPosition = i_bulletPosition;
            m_graphicDevice = i_graphicsDevice;
        }

        protected void LoadContent(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_bulletTexture = i_contentManager.Load<Texture2D>(@"sprites\Bullet");
        }

        public void MoveUp()
        {
            m_bulletPosition.Y -= 1;
        }
    }
}
