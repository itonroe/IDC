﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invaders.Classes
{
    class Ship
    {

        private readonly string m_texturePath = @"Sprites\Ship01_32x32";

        Texture2D m_shipTexture;
        Vector2 m_shipPosition;

        Bullet m_bullet1;
        Bullet m_bullet2;

        private int m_lifes;

        public Ship()
        {
            m_shipPosition = new Vector2(0, 0);
            m_bullet1 = new Bullet(Color.Red);
            m_bullet2 = new Bullet(Color.Red);
            m_lifes = 2;
        }

        public Vector2 Position { get { return m_shipPosition; } set { m_shipPosition = value; } }
        public Texture2D Texture { get { return m_shipTexture; } set { m_shipTexture = value; } }

        public Bullet Bullet1 { get { return m_bullet1; } set { m_bullet1 = value; } }

        public Bullet Bullet2 { get { return m_bullet2; } set { m_bullet2 = value; } }

        public int Lifes { get { return m_lifes; } set { m_lifes = value; } }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_shipTexture = i_contentManager.Load<Texture2D>(m_texturePath);
            m_bullet1.LoadContent(i_contentManager);
            m_bullet2.LoadContent(i_contentManager);

        }

        public void InitPosition(GraphicsDevice i_graphicDevice)
        {
            // Get the bottom and center:
            float x = (float)i_graphicDevice.Viewport.Width / 2;
            float y = (float)i_graphicDevice.Viewport.Height - 50 ;
            m_shipPosition = new Vector2(x, y);
        }

        public void MoveRight(int i_distance, GraphicsDevice i_graphicDevice)
        {
            m_shipPosition.X = Math.Clamp((m_shipPosition.X + i_distance), 0, (float)i_graphicDevice.Viewport.Width - m_shipTexture.Width);
        }

        public void MoveLeft(int i_distance)
        {
            m_shipPosition.X = Math.Clamp((m_shipPosition.X - i_distance), 0, m_shipPosition.X);
        }

        public void Shot()
        {
            if(!m_bullet1.IsActive)
            {
                m_bullet1.ChangedToActive(new Vector2(m_shipPosition.X + (m_shipTexture.Width/2) ,m_shipPosition.Y));

                return;
            }

            if (!m_bullet2.IsActive)
            {
                m_bullet2.ChangedToActive(new Vector2(m_shipPosition.X + (m_shipTexture.Width / 2), m_shipPosition.Y));
            }
        }

        public void BulletIntersectsShip(Bullet i_bullet)
        {
            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle shipRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            if (bulletRectangle.Intersects(shipRectangle))
            {
                this.Hit();
                i_bullet.IsActive = false;
            }
        }

        private void Hit()
        {
            m_lifes--;
        }

        public void Draw(SpriteBatch i_spriteBatch)
        {
            //Ship draw
            i_spriteBatch.Draw(m_shipTexture, m_shipPosition,Color.White);

            // BulletDraw
            if(m_bullet1.IsActive)
            {
                m_bullet1.Draw(i_spriteBatch);
            }

            if (m_bullet2.IsActive)
            {
                m_bullet2.Draw(i_spriteBatch);
            }

        }
    }

}
