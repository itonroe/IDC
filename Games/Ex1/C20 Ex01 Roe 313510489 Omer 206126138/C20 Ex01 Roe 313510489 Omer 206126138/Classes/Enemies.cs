using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class Enemies
    {
        private Enemy[,] m_Enemies;
        private bool m_LeftToRight;

        public void InitAndLoad(ContentManager i_ContentManager, GraphicsDevice i_GraphicDevice)
        {
            m_Enemies = new Enemy[5, 9];
            m_LeftToRight = true;

            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    m_Enemies[i, j] = new Enemy();

                    string model = "Pink";

                    if (i == 1 || i == 2)
                    {
                        model = "Blue";
                    }
                    else if (i != 0)
                    {
                        model = "White";
                    }

                    m_Enemies[i, j].Initialize(i_ContentManager, i_GraphicDevice, model, j, i);
                }
            }
        }

        public Enemy GetEnemy(int x, int y)
        {
            return m_Enemies[x, y];
        }

        public void UpdateEnemies(GameTime gameTime, GraphicsDevice i_GraphicDevice)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].Update(gameTime, m_LeftToRight, (float)i_GraphicDevice.Viewport.Width))
                    {
                        m_LeftToRight = !m_LeftToRight;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch i_SpriteBatch)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if(m_Enemies[i, j].IsAlive)
                        m_Enemies[i, j].Draw(gameTime, i_SpriteBatch);
                }
            }
        }

        public void EnemyIsDown(Ship i_ship)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if(i_ship.Bullet1.IsActive && m_Enemies[i, j].IsAlive)
                    {
                        BulletCrossEnemy(m_Enemies[i, j], i_ship.Bullet1);
                    }

                    if (i_ship.Bullet2.IsActive && m_Enemies[i, j].IsAlive)
                    {
                        BulletCrossEnemy(m_Enemies[i, j], i_ship.Bullet2);
                    }
                }
            }
        }

        public void BulletCrossEnemy(Enemy i_enemy, Bullet i_bullet)
        {
            if (BulletCrossEnemyX(i_enemy, i_bullet) && BulletCrossEnemyY(i_enemy, i_bullet))
            {
                i_enemy.IsAlive = false;
                i_bullet.IsActive = false;
            }
        }

        public bool BulletCrossEnemyX(Enemy i_enemy, Bullet i_bullet)
        {
            bool croosX = false;

            if ((i_bullet.Position.X >= i_enemy.Position.X) && (i_bullet.Position.X <= i_enemy.Position.X + i_enemy.Texture.Width))
            {
                croosX = true;
            }

            return croosX;
        }

        public bool BulletCrossEnemyY(Enemy i_enemy, Bullet i_bullet)
        {
            bool croosY = false;

            if ((i_bullet.Position.Y >= i_enemy.Position.Y) && (i_bullet.Position.Y <= i_enemy.Position.Y + i_enemy.Texture.Height))
            {
                croosY = true;
            }

            return croosY;
        }
    }
}
