using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    class Enemy
    {
        Texture2D m_Texutre;
        Vector2 m_Position;
        bool m_IsAlive = true;

        //Vector2 m_PrevPosition;

        private const float k_EnemyVelocityPerSecond = 80;
        private EnemyModel m_EnemyModel;

        enum EnemyModel
        {
            Pink,
            Blue,
            White
        }

        public Texture2D Texture { get { return m_Texutre; } set { m_Texutre = value; } }
        public Vector2 Position { get { return m_Position; } set { m_Position = value; } }
        public bool IsAlive { get { return m_IsAlive; } set { m_IsAlive = value; } }

        public void Initialize(ContentManager i_ContentManager, GraphicsDevice i_GraphicDevice, string i_Model, float i_DeltaX, float i_DeltaY)
        {
            LoadContent(i_ContentManager, i_Model);
            initPositions(i_GraphicDevice, i_DeltaX, i_DeltaY);
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
        }

        private void initPositions(GraphicsDevice i_GraphicDevice, float i_DeltaX, float i_DeltaY)
        {
            const int margin = 20;

            float x = i_DeltaX * (m_Texutre.Width + margin);
            float y = 3 * m_Texutre.Height + (i_DeltaY * (m_Texutre.Height + margin));

            m_Position = new Vector2(x, y);
        }

        public bool Update(GameTime gameTime, bool i_LeftToRight, float i_MaxWidth)
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

            return touchesTheBorder;
        }

        public void Draw(GameTime i_GameTime, SpriteBatch i_SpriteBatch)
        {
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
        }
    }
}
