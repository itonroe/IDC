﻿using System;
using System.Collections.Generic;
using System.Text;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class Enemies
    {
        private const int m_MaxNumOfBullets = 15;
        private const int m_BulltDifficullty = 100; // 300 is easy - 1 is hard ( every frame)

        private Enemy[,] m_Enemies;
        private bool m_LeftToRight;
        private int m_NumOfBullets;

        public Enemies()
        {
            m_LeftToRight = true;
            m_NumOfBullets = 0;
        }

        public int ActiveBullets 
        { 
            get 
            { 
                return m_NumOfBullets; 
            } 

            set 
            {
                m_NumOfBullets = value; 
            } 
        }

        public Enemy[,] Table 
        { 
            get 
            { 
                return m_Enemies; 
            } 

            set 
            { 
                m_Enemies = value; 
            } 
        }

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
                        model = "Yellow";
                    }

                    m_Enemies[i, j].Initialize(i_ContentManager, i_GraphicDevice, model, j, i);
                }
            }
        }

        public Enemy GetEnemy(int x, int y)
        {
            return m_Enemies[x, y];
        }

        public void Update(GameTime gameTime, GraphicsDevice i_GraphicDevice)
        {
            EnemiesMovement(gameTime, i_GraphicDevice);
            TimeForShot();
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
                    if (m_Enemies[i, j].IsAlive && m_Enemies[i, j].Update(gameTime, m_LeftToRight, (float)i_GraphicDevice.Viewport.Width))
                    {
                        ChanegeDirection();
                    }

                    if (m_Enemies[i, j].UpdateBullet(gameTime, i_GraphicDevice))
                    {
                        m_NumOfBullets--;
                    }
                }
            }
        }

        private void TimeForShot()
        {
            bool answer = false;

            Random rnd = new Random();
            if (rnd.Next(0, m_BulltDifficullty) == 0)
            {
                answer = true;
            }

            if (answer && (m_NumOfBullets < m_MaxNumOfBullets))
            {
                RandomEnemyShot();
            }
        }

        private void RandomEnemyShot()
        {
            Random rndI = new Random();
            Random rndJ = new Random();

            int i = rndI.Next(0, m_Enemies.GetLength(0));
            int j = rndJ.Next(0, m_Enemies.GetLength(1));

            m_Enemies[i, j].Shot();

            m_NumOfBullets++;
        }

        public void ChanegeDirection()
        {
            m_LeftToRight = !m_LeftToRight;
            MoveDown();
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

        public Enemy EnemyGotHitFromBullet(Ship i_ship)
        {
            Enemy enemyGotHit = null;

            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if(i_ship.Bullet1.IsActive && m_Enemies[i, j].IsAlive && BulletIntersectsEnemy(m_Enemies[i, j], i_ship.Bullet1))
                    {
                        enemyGotHit = m_Enemies[i, j];
                    }

                    if (i_ship.Bullet2.IsActive && m_Enemies[i, j].IsAlive && BulletIntersectsEnemy(m_Enemies[i, j], i_ship.Bullet2))
                    {
                        enemyGotHit = m_Enemies[i, j];
                    }
                }
            }

            return enemyGotHit;
        }

        public bool ShipIntersection(Ship i_ship)
        {
            bool answer = false;

            Rectangle ShipRectangle = new Rectangle((int)i_ship.Position.X, (int)i_ship.Position.Y, i_ship.Texture.Width, i_ship.Texture.Height);
            Rectangle enemyRectangle;

            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    enemyRectangle = new Rectangle((int)m_Enemies[i, j].Position.X, (int)m_Enemies[i, j].Position.Y, m_Enemies[i, j].Texture.Width, m_Enemies[i, j].Texture.Height);
                    if (m_Enemies[i, j].IsAlive && enemyRectangle.Intersects(ShipRectangle))
                    {
                        answer = true;
                    }
                }
            }

            return answer;
        }

        public bool BulletIntersectsEnemy(Enemy i_enemy, Bullet i_bullet)
        {
            bool hit = false;
            Rectangle bulletRectangle = new Rectangle((int)i_bullet.Position.X, (int)i_bullet.Position.Y, i_bullet.Texture.Width, i_bullet.Texture.Height);
            Rectangle enemyRectangle = new Rectangle((int)i_enemy.Position.X, (int)i_enemy.Position.Y, i_enemy.Texture.Width, i_enemy.Texture.Height);

            if(bulletRectangle.Intersects(enemyRectangle))
            {
                hit = true;
                i_enemy.IsAlive = false;
                i_bullet.IsActive = false;
            }

            return hit;
        }

        private bool EnemyReachedBottom(GraphicsDevice i_GraphicDevice)
        {
            bool answer = false;

            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    Enemy enemy = m_Enemies[i, j];
                    if (enemy.IsAlive && (enemy.Position.Y + enemy.Texture.Height >= i_GraphicDevice.Viewport.Height))
                    {
                        answer = true;
                    }
                }
            }

            return answer;
        }

        public bool AllEnemiesAreDead()
        {
            bool allDead = true;

            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].IsAlive)
                    {
                        allDead = false;
                    }
                }
            }

            return allDead;
        }

        public void Draw(SpriteBatch i_SpriteBatch)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].IsAlive)
                    {
                        m_Enemies[i, j].Draw(i_SpriteBatch);
                    }
                }
            }
        }

        public void IncreseSpeedForAllEnemis(double i_Speed)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    m_Enemies[i, j].IncreseSpeed(i_Speed);
                }
            }
        }
    }
}
