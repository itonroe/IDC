﻿using System;
using System.Collections;
using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    public class Game1 : Game
    {
        private const double k_IncreseSpeedAfterFiveKills = 1.03;
        private const int k_LifeLost = -600;
        private const int k_RadnomPopDifficulty = 500;

        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;
        private KeyboardState m_PrevKbState;
        private MouseState m_PrevMouseState;

        // Enemies collection
        private Enemies m_Enemies;
        private int m_CountEnemyKills;

        // MotherShip object
        private MotherShip m_MotherShip;

        // Background Properties
        private Texture2D m_TextureBackground;
        private Vector2 m_PositionBackground;
        private Color m_TintBackground = Color.White;

        // Players object
        private Player m_Player1;
        private Player m_Player2;

        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            m_Enemies = new Enemies();
            m_Player1 = new Player();
            m_Player2 = new Player();
            m_MotherShip = new MotherShip();

            m_CountEnemyKills = 0;

            m_Graphics.PreferredBackBufferWidth = 750;
            m_Graphics.PreferredBackBufferHeight = 600;
            m_Graphics.ApplyChanges();

            base.Initialize();
        }

        private void initPositions()
        {
            m_PositionBackground = Vector2.Zero;

            m_Player1.InitPosition(GraphicsDevice);
            m_Player2.InitPosition(GraphicsDevice);

            m_MotherShip.initPositions();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            m_TextureBackground = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_Enemies.InitAndLoad(Content, GraphicsDevice);
            m_Player1.LoadContent(Content);
            m_Player2.LoadContent(Content);
            m_MotherShip.LoadContent(Content, "Red");

            initPositions();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            updateShip(gameTime);
            updateEnemies(gameTime);
            updateMotherShip(gameTime);
            updateIntersections(GraphicsDevice);

            m_PrevKbState = Keyboard.GetState();
            m_PrevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        private void updateEnemies(GameTime gameTime)
        {
            m_Enemies.Update(Content, gameTime, GraphicsDevice);
            if (m_Enemies.AllEnemiesAreDead() && !m_MotherShip.IsAlive)
            {
                printScore();
            }
        }

        private void updateShip(GameTime gameTime)
        {
            shipStillAlive();
            if (!shipMoveByMouse(GraphicsDevice))
            {
                shipMoveByKB(gameTime);
            }

            updateBulletsForShip(gameTime);
            newShot();
        }

        private void updateMotherShip(GameTime gameTime)
        {
            if (timeForMotherShip() && !m_MotherShip.IsAlive)
            {
                m_MotherShip.GetReadyToPop();
            }

            if (m_MotherShip.IsAlive)
            {
                m_MotherShip.MoveRight(gameTime, GraphicsDevice);
            }
        }

        private void updateIntersections(GraphicsDevice i_GraphicDevice)
        {
            // enemy reach bottom
            if (m_Enemies.IsEndOfGame(i_GraphicDevice))
            {
                printScore();
            }

            // enemy bullet hit ship
            shipGotHitFromBullet(m_Player1, i_GraphicDevice);
            shipGotHitFromBullet(m_Player2, i_GraphicDevice);

            // ship bullet hit enemy
            shipHitEnemy(m_Player1);
            shipHitEnemy(m_Player2);

            // Enemy and ship intersect
            if (m_Enemies.ShipIntersection(m_Player1) || m_Enemies.ShipIntersection(m_Player2))
            {
                printScore();
            }
        }

        public void shipHitEnemy(Player i_Player)
        {
            Enemy tempEnemy = m_Enemies.EnemyGotHitFromBullet(i_Player);

            if (tempEnemy != null)
            {
                i_Player.Score.AddScore(tempEnemy.Model);

                // for speed increse
                m_CountEnemyKills++;
                if (m_CountEnemyKills % 5 == 0)
                {
                    m_Enemies.IncreseSpeedForAllEnemis(k_IncreseSpeedAfterFiveKills);
                }
            }

            // MotherSHip Intersect with bullets
            if (m_MotherShip.IntersectionWithShipBullets(i_Player))
            {
                i_Player.Score.AddScore(m_MotherShip.Model);
            }
        }

        private bool timeForMotherShip()
        {
            bool answer = false;

            Random rnd = new Random();
            if(rnd.Next(0, k_RadnomPopDifficulty) == 0)
            {
                answer = true;
            }

            return answer;
        }

        private void shipStillAlive()
        {
            if (m_Player1.Lifes == 0 || m_Player2.Lifes == 0)
            {
                printScore();
            }
        }

        private void shipMoveByKB(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                System.Windows.Forms.MessageBox.Show(m_Player1.Score.Score.ToString() + "\n" + m_Player2.Score.Score.ToString(), "Game Over");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                m_Player1.MoveRight(gameTime, GraphicsDevice);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                m_Player1.MoveLeft(gameTime, GraphicsDevice);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                m_Player2.MoveRight(gameTime, GraphicsDevice);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                m_Player2.MoveLeft(gameTime, GraphicsDevice);
            }
        }

        private bool shipMoveByMouse(GraphicsDevice i_graphicDevice)
        {
            bool isMouseMove = false;

            float mouseDeltaX = getMousePositionDelta().X;
            var newXposition = Math.Clamp(/*m_Player1.Position.X + mouseDeltaX*/ Mouse.GetState().X, 0, (float)i_graphicDevice.Viewport.Width - m_Player1.Texture.Width);
            Vector2 mousePosition = new Vector2(newXposition, m_Player1.Position.Y);

            if (mouseDeltaX != 0)
            {
                m_Player1.Position = mousePosition;
                isMouseMove = true;
            }

            return isMouseMove;
        }

        private Vector2 getMousePositionDelta()
        {
            Vector2 retVal = Vector2.Zero;

            MouseState currState = Mouse.GetState();

            if (m_PrevMouseState != null)
            {
                retVal.X = currState.X - m_PrevMouseState.X;
                retVal.Y = currState.Y - m_PrevMouseState.Y;
            }

            //m_PrevMouseState = currState;

            return retVal;
        }

        private void newShot()
        {
            bool keyBoardClickPlayer1 = Keyboard.GetState().IsKeyDown(Keys.D9) && !m_PrevKbState.IsKeyDown(Keys.D9);
            bool keyBoardClickPlayer2 = Keyboard.GetState().IsKeyDown(Keys.D3) && !m_PrevKbState.IsKeyDown(Keys.D3);
            bool mouseClick = Mouse.GetState().LeftButton == ButtonState.Pressed && m_PrevMouseState.LeftButton == ButtonState.Released;

            if (keyBoardClickPlayer1 || mouseClick)
            {
                m_Player1.Shot();
            }
            
            if (keyBoardClickPlayer2)
            {
                m_Player2.Shot();
            }
        }

        private void shipGotHitFromBullet(Player i_Player, GraphicsDevice i_GraphicDevice)
        {
            for (int i = 0; i < m_Enemies.Table.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.Table.GetLength(1); j++)
                {
                    if (m_Enemies.GetEnemy(i, j).Bullet.IsActive)
                    {
                        if (i_Player.BulletIntersectsShip(m_Enemies.GetEnemy(i, j).Bullet, i_GraphicDevice))
                        {
                            i_Player.Score.AddScore(k_LifeLost);
                            m_Enemies.ActiveBullets--;
                        }
                    }
                }
            }
        }

        private void updateBulletsForShip(GameTime gameTime)
        {
            m_Player1.Bullet1.UpdateForShip(gameTime);
            m_Player1.Bullet2.UpdateForShip(gameTime);
            m_Player2.Bullet1.UpdateForShip(gameTime);
            m_Player2.Bullet2.UpdateForShip(gameTime);
        }

        private void printScore()
        {
            string message = $"Player 1 Score is: {m_Player1.Score.Score}\nPlayer 2 Score is: {m_Player2.Score.Score}";
            System.Windows.Forms.MessageBox.Show(message, "Game Over");
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            m_SpriteBatch.Begin();

            m_SpriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel
            m_Enemies.Draw(m_SpriteBatch);
            m_Player1.Draw(m_SpriteBatch);
            m_Player2.Draw(m_SpriteBatch);
            m_MotherShip.Draw(m_SpriteBatch);

            m_SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
