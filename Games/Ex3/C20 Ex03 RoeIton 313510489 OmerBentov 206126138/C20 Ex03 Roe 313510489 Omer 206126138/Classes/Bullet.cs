using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Classes
{
    public class Bullet : Sprite
    {
        private const int r_BulletSpeed = 140; // pxl pre sec

        private const string k_AssetName = @"Sprites\Bullet";

        private GameScreen m_Parent;

        public Bullet(Color i_Color, GameScreen i_GameScreen) : base(k_AssetName, i_GameScreen.Game)
        {
            m_Parent = i_GameScreen;
            Visible = false;

            TintColor = i_Color;

            Initialize();
        }

        public bool IsActive 
        { 
            get 
            { 
                return Visible; 
            } 

            set 
            { 
                Visible = value; 
            } 
        }

        private void moveUp(GameTime gameTime)
        {
            m_Position.Y -= r_BulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void moveDown(GameTime gameTime)
        {
            m_Position.Y += r_BulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void UpdateForShip(GameTime gameTime)
        {
            if(IsActive)
            {
                this.moveUp(gameTime);
                if (m_Position.Y <= 0) 
                {
                    IsActive = false;
                }
            }
        }

        public bool UpdateForEnemy(GraphicsDevice i_graphicDevice, GameTime gameTime)
        {
            bool bulletOutOfScreen = false;

            if (IsActive)
            {
                this.moveDown(gameTime);
                if (m_Position.Y >= i_graphicDevice.Viewport.Height)
                {
                    IsActive = false;
                    bulletOutOfScreen = true;
                }
            }

            return bulletOutOfScreen;
        }

        public void ChangedToActive(Vector2 i_shipPosition)
        {
            IsActive = true;
            Position = i_shipPosition;
        }

        public void ChangeToNotActive()
        {
            IsActive = false;
        }

        public bool BulletIntersectionBullet(Bullet i_Bullet)
        {
            bool hit = false;
            Random rnd = new Random();

            if(this.Bounds.Intersects(i_Bullet.Bounds))
            {
                hit = true;
                i_Bullet.ChangeToNotActive();
                if(rnd.Next(0, 4) == 0)
                {
                    this.ChangeToNotActive();
                    (m_Parent as PlayScreen).EnemyBulletDisabled();
                }
            }

            return hit;
        }

        public void Draw(SpriteBatch i_spriteBatch)
        {
            i_spriteBatch.Draw(Texture, m_Position, TintColor);
        }
    }
}
