﻿using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    class Enemy
    {
        protected float k_EnemyVelocityPerSecond = 60;
        private EnemyModel m_EnemyModel;
        private const double k_speedMultiplicationParam = 1.06;

        private Texture2D m_Texutre;
        private Vector2 m_Position;
        private Bullet m_bullet;
        private bool m_IsAlive;

        public Enemy()
        {
            m_Position = new Vector2();
            m_bullet = new Bullet(Color.Blue);
            m_IsAlive = true;
        }

        public int Model { get { return (int)m_EnemyModel; } }
        public Texture2D Texture { get { return m_Texutre; } set { m_Texutre = value; } }
        public Vector2 Position { get { return m_Position; } set { m_Position = value; } }
        public bool IsAlive { get { return m_IsAlive; } set { m_IsAlive = value; } }
        public Bullet Bullet { get { return m_bullet; }  set { m_bullet = value; } }

        public enum EnemyModel
        {
            Red = 600,
            Pink = 300,
            Blue = 200,
            Yellow = 70
        }

        public void Initialize(ContentManager i_ContentManager, GraphicsDevice i_GraphicDevice, string i_Model, float i_DeltaX, float i_DeltaY)
        {
            LoadContent(i_ContentManager, i_Model);
            initPositions(i_GraphicDevice, i_DeltaX, i_DeltaY);
        }

        public virtual void initPositions(GraphicsDevice i_GraphicDevice, float i_DeltaX, float i_DeltaY)
        {
            const int margin = 20;

            float x = i_DeltaX * (m_Texutre.Width + margin);
            float y = 3 * m_Texutre.Height + (i_DeltaY * (m_Texutre.Height + margin));

            m_Position = new Vector2(x, y);
        }

        public virtual void LoadContent(ContentManager i_ContentManager, string i_Model)
        {
            m_EnemyModel = (EnemyModel)Enum.Parse(typeof(EnemyModel), i_Model);

            string assetName = String.Empty;

            switch (m_EnemyModel)
            {
                case EnemyModel.Pink:
                    assetName = @"Sprites\Enemy0101_32x32";
                    break;
                case EnemyModel.Blue:
                    assetName = @"Sprites\Enemy0201_32x32";
                    break;
                case EnemyModel.Yellow:
                    assetName = @"Sprites\Enemy0301_32x32";
                    break;
                case EnemyModel.Red:
                    assetName = @"Sprites\MotherShip_32x120";
                    break;
                default:
                    throw new ArgumentException("Color Is Not Recognize, there is no such enemy");
            }

            m_Texutre = i_ContentManager.Load<Texture2D>(assetName);
            m_bullet.LoadContent(i_ContentManager);
        }

        public bool Update(GameTime gameTime, bool i_LeftToRight, float i_MaxWidth, GraphicsDevice i_GraphicDevice)
        {

            bool touchesTheBorder = false;

            if (i_LeftToRight)
            {
                if (m_Position.X + k_EnemyVelocityPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds < i_MaxWidth - m_Texutre.Width)
                {
                    MoveRight(gameTime);
                }
                else
                {
                    touchesTheBorder = true;
                }
            }
            else
            {
                if (m_Position.X - k_EnemyVelocityPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds > 0)
                {
                    MoveLeft(gameTime);
                }
                else
                {
                    touchesTheBorder = true;
                }
            }

            return touchesTheBorder;
        }

        public bool UpdateBullet(GameTime gameTime, GraphicsDevice i_GraphicDevice)
        {
            if (m_bullet.IsActive)
            {
                 return m_bullet.UpdateForEnemy(i_GraphicDevice, gameTime);
            }

            return false;
        }

        public void MoveRight(GameTime i_GameTime)
        {
            m_Position.X += k_EnemyVelocityPerSecond * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveLeft(GameTime i_GameTime)
        {
            m_Position.X -= k_EnemyVelocityPerSecond * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveDown()
        {
            m_Position.Y += m_Texutre.Height / 2;
        }

        public void IncreseSpeed()
        {
            k_EnemyVelocityPerSecond = (float)(k_EnemyVelocityPerSecond * k_speedMultiplicationParam);
        }

        public void IncreseSpeed(double i_speed)
        {
            k_EnemyVelocityPerSecond = (float)(k_EnemyVelocityPerSecond * i_speed);
        }

        public void Shot()
        {
            if (!m_bullet.IsActive)
            {
                m_bullet.ChangedToActive(new Vector2(m_Position.X + (m_Texutre.Width / 2), m_Position.Y));
            }
        }

        public void Draw(GameTime i_GameTime, SpriteBatch i_SpriteBatch)
        {
            //Enemy
            Color enemyColor = Color.Black;

            switch (m_EnemyModel)
            {
                case EnemyModel.Pink:
                    enemyColor = Color.LightPink;
                    break;
                case EnemyModel.Blue:
                    enemyColor = Color.LightBlue;
                    break;
                case EnemyModel.Yellow:
                    enemyColor = Color.LightYellow;
                    break;
                case EnemyModel.Red:
                    enemyColor = Color.Red;
                    break;
            }

            if(IsAlive)
                i_SpriteBatch.Draw(m_Texutre, m_Position, enemyColor);

            // Bullet
            if(m_bullet.IsActive)
            {
                m_bullet.Draw(i_SpriteBatch);
            }
        }
    }
}
