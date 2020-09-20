﻿using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;

namespace Invaders.Classes
{
    public class Ship : Sprite
    {
        private const int r_ShipSpeed = 140;

        protected string m_TexturePath;

        private Bullet m_Bullet1;
        private Bullet m_Bullet2;

        private int m_Lifes = 3;

        public Ship(string i_AssetName, Game i_Game) : base (i_AssetName, i_Game)
        {
            Position = new Vector2(0, 0);
            m_Bullet1 = new Bullet(Color.Red, i_Game);
            m_Bullet2 = new Bullet(Color.Red, i_Game);
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

        public void InitPosition()
        {
            // Get the bottom and center:
            float x = (float)GraphicsDevice.Viewport.Width - Texture.Width;
            float y = (float)GraphicsDevice.Viewport.Height - 50;
            Position = new Vector2(x, y);
        }

        public void MoveRight(GameTime gameTime)
        {
            m_Position.X += r_ShipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_Position.X = Math.Clamp(Position.X, 0, (float)GraphicsDevice.Viewport.Width - Texture.Width);
        }

        public void MoveLeft(GameTime gameTime)
        {
            m_Position.X -= r_ShipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_Position.X = Math.Clamp(Position.X, 0, (float)GraphicsDevice.Viewport.Width - Texture.Width);
        }

        public void Shot()
        {
            if(!m_Bullet1.IsActive)
            {
                m_Bullet1.ChangedToActive(new Vector2(Position.X + (Texture.Width / 2), Position.Y));

                return;
            }

            if (!m_Bullet2.IsActive)
            {
                m_Bullet2.ChangedToActive(new Vector2(Position.X + (Texture.Width / 2), Position.Y));
            }
        }

        public bool BulletIntersectsShip(Bullet i_bullet)
        {
            bool hit = false;

            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle shipRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            if (bulletRectangle.Intersects(shipRectangle))
            {
                hit = true;
                this.Hit();
                i_bullet.IsActive = false;
            }

            return hit;
        }

        private void Hit()
        {
            InitPosition();
            m_Lifes--;
        }

        public void Draw(SpriteBatch i_spriteBatch)
        {
            // Ship draw
            i_spriteBatch.Draw(Texture, Position, Color.White);

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
