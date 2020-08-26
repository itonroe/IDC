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
        private int m_numOfBullets;
        private const int m_MaxNumOfBullets = 5;
        private const int m_BulltDifficullty = 300; // 300 is easy - 1 is hard ( every frame)

        public Enemies()
        {
            m_LeftToRight = true;
            m_numOfBullets = 0;
        }

        public Enemy[,] Table { get { return m_Enemies; } set { m_Enemies = value; } }

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

        public void Update(GameTime gameTime, GraphicsDevice i_GraphicDevice, Ship i_ship)
        {
            EnemiesMovement(gameTime, i_GraphicDevice);
            TimeForShot();
            EnemyIsDown(i_ship);
        }
        
        public bool IsEndOfGame(GraphicsDevice i_GraphicDevice)
        {
            return EnemyReachedBottom(i_GraphicDevice); // means enemy reached button so we return false for no more updates
        }

        public void EnemiesMovement(GameTime gameTime, GraphicsDevice i_GraphicDevice)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].IsAlive && m_Enemies[i, j].Update(gameTime, m_LeftToRight, (float)i_GraphicDevice.Viewport.Width, i_GraphicDevice))
                    {
                        ChanegeDirection();
                    }
                }
            }
        }

        private void TimeForShot()
        {
            bool answer = false;

            Random rnd = new Random();
            if (rnd.Next(0, m_BulltDifficullty) == 0)
                answer = true;

            if (answer && (m_numOfBullets < m_MaxNumOfBullets))
                RandomEnemyShot();

        }

        private void RandomEnemyShot()
        {
            Random rndI = new Random();
            Random rndJ = new Random();

            int i = rndI.Next(0, m_Enemies.GetLength(0));
            int j = rndJ.Next(0, m_Enemies.GetLength(1));

            m_Enemies[i, j].Shot();

            m_numOfBullets++;
        }

        public void ChanegeDirection()
        {
            m_LeftToRight = !m_LeftToRight;
            MoveDown(); //
        }

        private void MoveDown()
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].IsAlive)
                    {
                        m_Enemies[i, j].MoveDown();
                        m_Enemies[i, j].IncreseSpeed();
                    }
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
                        BulletIntersectsEnemy(m_Enemies[i, j], i_ship.Bullet1);
                    }

                    if (i_ship.Bullet2.IsActive && m_Enemies[i, j].IsAlive)
                    {
                        BulletIntersectsEnemy(m_Enemies[i, j], i_ship.Bullet2);
                    }
                }
            }
        }

        public void BulletIntersectsEnemy(Enemy i_enemy, Bullet i_bullet)
        {
            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle enemyRectangle = new Rectangle((int)i_enemy.Position.X, (int)i_enemy.Position.Y, i_enemy.Texture.Width, i_enemy.Texture.Height);

            if(bulletRectangle.Intersects(enemyRectangle))
            {
                i_enemy.IsAlive = false;
                i_bullet.IsActive = false;
            }
        }

        private bool EnemyReachedBottom(GraphicsDevice i_GraphicDevice)
        {
            bool answer = false;

            for (int i = 0; i<m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j<m_Enemies.GetLength(1); j++)
                {
                    Enemy enemy = m_Enemies[i, j];
                    if (enemy.IsAlive && (enemy.Position.Y + enemy.Texture.Height >= i_GraphicDevice.Viewport.Height))
                        answer = true;
                }
            }

            return answer;
        }

        public void Draw(GameTime gameTime, SpriteBatch i_SpriteBatch)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].IsAlive)
                        m_Enemies[i, j].Draw(gameTime, i_SpriteBatch);
                }
            }
        }
    }
}
