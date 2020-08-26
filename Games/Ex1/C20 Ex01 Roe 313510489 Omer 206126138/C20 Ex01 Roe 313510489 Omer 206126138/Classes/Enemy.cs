using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    class Enemy
    {
        private float k_EnemyVelocityPerSecond = 80;
        private EnemyModel m_EnemyModel;
        private const double k_speedMultiplicationParam = 1.06;

        Texture2D m_Texutre;
        Vector2 m_Position;
        Bullet m_bullet;
        bool m_IsAlive;

        public Enemy()
        {
            m_Position = new Vector2();
            m_bullet = new Bullet(Color.Blue);
            m_IsAlive = true;
        }

        public Texture2D Texture { get { return m_Texutre; } set { m_Texutre = value; } }
        public Vector2 Position { get { return m_Position; } set { m_Position = value; } }
        public bool IsAlive { get { return m_IsAlive; } set { m_IsAlive = value; } }
        public Bullet Bullet { get { return m_bullet; }  set { m_bullet = value; } }

        enum EnemyModel
        {
            Pink,
            Blue,
            White
        }

        public void Initialize(ContentManager i_ContentManager, GraphicsDevice i_GraphicDevice, string i_Model, float i_DeltaX, float i_DeltaY)
        {
            LoadContent(i_ContentManager, i_Model);
            initPositions(i_GraphicDevice, i_DeltaX, i_DeltaY);
        }

        private void initPositions(GraphicsDevice i_GraphicDevice, float i_DeltaX, float i_DeltaY)
        {
            const int margin = 20;

            float x = i_DeltaX * (m_Texutre.Width + margin);
            float y = 3 * m_Texutre.Height + (i_DeltaY * (m_Texutre.Height + margin));

            m_Position = new Vector2(x, y);
        }

        private void LoadContent(ContentManager i_ContentManager, string i_Model)
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
                case EnemyModel.White:
                    assetName = @"Sprites\Enemy0301_32x32";
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
                    m_Position.X += k_EnemyVelocityPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                    m_Position.X -= k_EnemyVelocityPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    touchesTheBorder = true;
                }
            }

            if(m_bullet.IsActive)
            {
                m_bullet.UpdateForEnemy(i_GraphicDevice);
            }

            return touchesTheBorder;
        }

        public void MoveDown()
        {
            m_Position.Y += m_Texutre.Height / 2;
        }

        public void IncreseSpeed()
        {
            k_EnemyVelocityPerSecond = (float)(k_EnemyVelocityPerSecond * k_speedMultiplicationParam);
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
                case EnemyModel.White:
                    enemyColor = Color.White;
                    break;
            }

            i_SpriteBatch.Draw(m_Texutre, m_Position, enemyColor);

            // Bullet
            if(m_bullet.IsActive)
            {
                m_bullet.Draw(i_SpriteBatch);
            }
        }
    }
}
