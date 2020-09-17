﻿using System;
using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    public class Enemy
    {
        private const double k_SpeedMultiplicationParam = 1.06;

        protected float k_EnemyVelocityPerSecond = 60;
        private eEnemyModels m_EnemyModel;

        private Texture2D m_Texutre;
        private Vector2 m_Position;
        private Bullet m_Bullet;
        private bool m_IsAlive;

        private bool m_ImageFliped;

        private double m_GameTotalSeconds;
        private double m_TimeToJump;

        public Enemy()
        {
            m_Position = new Vector2();
            m_Bullet = new Bullet(Color.Blue);
            m_IsAlive = true;
            m_ImageFliped = false;
            m_GameTotalSeconds = 0;
            m_TimeToJump = 1;
        }

        public int Model 
        { 
            get 
            { 
                return (int)m_EnemyModel; 
            } 
        }

        public Texture2D Texture 
        { 
            get 
            { 
                return m_Texutre; 
            } 

            set 
            { 
                m_Texutre = value; 
            } 
        }

        public Vector2 Position 
        { 
            get 
            { 
                return m_Position; 
            } 

            set 
            { 
                m_Position = value; 
            } 
        }

        public bool IsAlive 
        { 
            get 
            { 
                return m_IsAlive; 
            } 

            set 
            { 
                m_IsAlive = value; 
            } 
        }

        public Bullet Bullet 
        { 
            get 
            { 
                return m_Bullet; 
            } 

            set 
            { 
                m_Bullet = value; 
            } 
        }

        public enum eEnemyModels
        {
            Red = 600,
            Pink = 300,
            Blue = 200,
            Yellow = 70
        }

        public void Initialize(ContentManager i_ContentManager, GraphicsDevice i_GraphicDevice, string i_Model, float i_DeltaX, float i_DeltaY)
        {
            LoadContent(i_ContentManager, i_Model);
            InitPositions(i_GraphicDevice, i_DeltaX, i_DeltaY);
        }

        public virtual void InitPositions(GraphicsDevice i_GraphicDevice, float i_DeltaX, float i_DeltaY)
        {
            const int margin = 20;

            float x = i_DeltaX * (m_Texutre.Width + margin);
            float y = 3 * m_Texutre.Height + (i_DeltaY * (m_Texutre.Height + margin));

            m_Position = new Vector2(x, y);
        }

        public virtual void LoadContent(ContentManager i_ContentManager, string i_Model)
        {
            m_EnemyModel = (eEnemyModels)Enum.Parse(typeof(eEnemyModels), i_Model);

            SwitchImage(i_ContentManager);

            m_Bullet.LoadContent(i_ContentManager);
        }

        public bool Update(ContentManager i_ContentManager, GameTime I_GameTime, bool i_LeftToRight, float i_MaxWidth)
        {
            return TimeToJump(I_GameTime) ? Jump(i_ContentManager, I_GameTime, i_LeftToRight, i_MaxWidth) : false;
        }

        public bool TimeToJump(GameTime I_GameTime)
        {
            bool jump = true;

            if (I_GameTime.TotalGameTime.TotalSeconds - m_GameTotalSeconds <= m_TimeToJump)
            {
                jump = false;
            }
            else
            {
                m_GameTotalSeconds = I_GameTime.TotalGameTime.TotalSeconds;
            }

            return jump;
        }

        public bool Jump(ContentManager i_ContentManager, GameTime i_GameTime, bool i_LeftToRight, float i_MaxWidth)
        {
            bool touchesTheBorder = false;

            SwitchImage(i_ContentManager);

            if (i_LeftToRight)
            {
                //1.a
                if (m_Position.X + m_Texutre.Width <= i_MaxWidth - m_Texutre.Width)
                {
                    MoveRight(i_GameTime, m_Texutre.Width);
                }
                else
                {
                    touchesTheBorder = true;
                }
            }
            else
            {
                //1.a
                if (m_Position.X - m_Texutre.Width > 0)
                {
                    MoveLeft(i_GameTime, m_Texutre.Width);
                }
                else
                {
                    touchesTheBorder = true;
                }
            }

            return touchesTheBorder;
        }

        public void SwitchImage(ContentManager i_ContentManager)
        {
            string assetName;

            switch (m_EnemyModel)
            {
                case eEnemyModels.Pink:
                    assetName = m_ImageFliped ? @"Sprites\Enemy0101_32x32" : @"Sprites\Enemy0102_32x32";
                    break;
                case eEnemyModels.Blue:
                    assetName = m_ImageFliped ? @"Sprites\Enemy0201_32x32" : @"Sprites\Enemy0202_32x32";
                    break;
                case eEnemyModels.Yellow:
                    assetName = m_ImageFliped ? @"Sprites\Enemy0301_32x32" : @"Sprites\Enemy0302_32x32";
                    break;
                case eEnemyModels.Red:
                    assetName = @"Sprites\MotherShip_32x120";
                    break;
                default:
                    throw new ArgumentException("Color Is Not Recognize, there is no such enemy");
            }

            m_ImageFliped = !m_ImageFliped;

            m_Texutre = i_ContentManager.Load<Texture2D>(assetName);
        }

        public bool UpdateBullet(GameTime gameTime, GraphicsDevice i_GraphicDevice)
        {
            if (m_Bullet.IsActive)
            {
                 return m_Bullet.UpdateForEnemy(i_GraphicDevice, gameTime);
            }

            return false;
        }

        public void MoveRight(GameTime i_GameTime, int i_Distance)
        {
            m_Position.X += i_Distance;
        }

        public void MoveLeft(GameTime i_GameTime, int i_Distance)
        {
            m_Position.X -= i_Distance;
        }

        public void MoveDown()
        {
            m_Position.Y += m_Texutre.Height / 2;
            m_TimeToJump -= ((0.05) * m_TimeToJump);
        }

        public void IncreseSpeed()
        {
            IncreseSpeed(k_SpeedMultiplicationParam);
        }

        public void IncreseSpeed(double i_Speed)
        {
            k_EnemyVelocityPerSecond = (float)(k_EnemyVelocityPerSecond * i_Speed);
        }

        public void Shot()
        {
            if (!m_Bullet.IsActive)
            {
                m_Bullet.ChangedToActive(new Vector2(m_Position.X + (m_Texutre.Width / 2), m_Position.Y));
            }
        }

        public void Draw(SpriteBatch i_SpriteBatch)
        {
            // Enemy
            Color enemyColor = Color.Black;

            switch (m_EnemyModel)
            {
                case eEnemyModels.Pink:
                    enemyColor = Color.LightPink;
                    break;
                case eEnemyModels.Blue:
                    enemyColor = Color.LightBlue;
                    break;
                case eEnemyModels.Yellow:
                    enemyColor = Color.LightYellow;
                    break;
                case eEnemyModels.Red:
                    enemyColor = Color.Red;
                    break;
            }

            if (IsAlive)
            {
                i_SpriteBatch.Draw(m_Texutre, m_Position, enemyColor);
            }

            // Bullet
            if(m_Bullet.IsActive)
            {
                m_Bullet.Draw(i_SpriteBatch);
            }
        }
    }
}
