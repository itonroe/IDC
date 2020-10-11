﻿using System;
using System.Collections;
using System.Collections.Generic;
using C20_Ex03_Roe_313510489_Omer_206126138.Classes;
using C20_Ex03_Roe_313510489_Omer_206126138.Screens;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace C20_Ex03_Roe_313510489_Omer_206126138
{
    public class PlayScreen : GameScreen
    {
        private Game m_Game;

        private const double k_IncreseSpeedAfterFiveKills = 1.03;
        private const int k_LifeLost = -600;
        private const int k_RadnomPopDifficulty = 500;
        private const int k_NumOfBarriers = 4;

        private int m_GameLevel;

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

        // Barriers
        private Barriers m_Barriers;

        public PlayScreen(Game i_Game, int i_GameLevel) : base (i_Game)
        {
            m_Game = i_Game;
            m_GameLevel = i_GameLevel;
            Initialize();
        }

        public override void Initialize()
        {
            m_Enemies = new Enemies(this, CalculateNumOfColumnsForEnemiesMatrix(m_GameLevel));
            m_Barriers = new Barriers(this, k_NumOfBarriers);

            try
            {
                m_Player1 = new Player(1, this);
                m_Player1.Initialize();
                m_Player2 = new Player(2, this);
                m_Player2.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            m_MotherShip = new MotherShip(this);

            m_CountEnemyKills = 0;

            base.Initialize();
        }

        private void initPositions()
        {
            m_PositionBackground = Vector2.Zero;

            m_Player1.InitPosition();
            m_Player2.InitPosition();

            m_Barriers.InitPositions(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            m_TextureBackground = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_Enemies.InitAndLoad();
            m_MotherShip.LoadContent("Red");

            initPositions();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(new PauseScreen(this.Game));
            }

            if (InputManager.KeyPressed(Keys.M))
            {
                (Game as GameWithScreens).ToogleMuteAllSounds();
            }

            updateShip(gameTime);
            updateEnemies(gameTime);
            updateMotherShip(gameTime);
            updateIntersections(GraphicsDevice);
            m_Barriers.UpdateBarriers(gameTime);

            m_PrevKbState = Keyboard.GetState();
            m_PrevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        private void updateEnemies(GameTime gameTime)
        {
            m_Enemies.Update(gameTime);
            if (m_Enemies.AllEnemiesAreDead() && !m_MotherShip.IsAlive)
            {
                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.LevelWin].Play();
                (base.m_ScreensManager as ScreensMananger).Push(new PlayScreen(this.Game, m_GameLevel + 1));
                base.m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(this.Game, m_GameLevel + 1));
                ExitScreen();
            }
        }

        private void updateShip(GameTime gameTime)
        {
            GameIsOn();

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
                m_MotherShip.MoveRight(gameTime);
            }
        }

        private void updateIntersections(GraphicsDevice i_GraphicDevice)
        {
            // enemy reach bottom
            if (m_Enemies.IsEndOfGame())
            {
                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.BarriersHit].Play();
                printScore();
            }

            // enemy bullet hit ship
            shipGotHitFromBullet(m_Player1, i_GraphicDevice);
            shipGotHitFromBullet(m_Player2, i_GraphicDevice);

            // ship bullet hit enemy
            ShipHitEnemy(m_Player1);
            ShipHitEnemy(m_Player2);

            // Enemy and ship intersect
            if (m_Enemies.ShipIntersection(m_Player1) || m_Enemies.ShipIntersection(m_Player2))
            {
                printScore();
            }

            // Barriers and bullets
            m_Barriers.BulletIntersection(AllActiveBullets());

            // Barriers and enemies
            m_Barriers.EnemyIntersection(m_Enemies.Table);

            // Bullets and bullets
            BulletsIntersection();
        }

        public void BulletsIntersection()
        {
            List<Bullet> enemyBullets = new List<Bullet>();
            List<Bullet> playersBullets = new List<Bullet>();

            // Players bullets
            if (m_Player1.Bullet1.IsActive)
            {
                playersBullets.Add(m_Player1.Bullet1);
            }

            if (m_Player1.Bullet2.IsActive)
            {
                playersBullets.Add(m_Player1.Bullet2);
            }

            if (m_Player2.Bullet1.IsActive)
            {
                playersBullets.Add(m_Player2.Bullet1);
            }

            if (m_Player2.Bullet2.IsActive)
            {
                playersBullets.Add(m_Player2.Bullet2);
            }

            // Enemies bullets
            foreach (Enemy enemy in m_Enemies.Table)
            {
                if (enemy.Bullet.IsActive)
                {
                    enemyBullets.Add(enemy.Bullet);
                }
            }

            foreach(Bullet eBullet in enemyBullets)
            {
                foreach(Bullet pBullet in playersBullets)
                {
                    eBullet.BulletIntersectionBullet(pBullet);
                }
            }
        }

        public void EnemyBulletDisabled()
        {
            this.m_Enemies.ActiveBullets--;
        }

        public List<Bullet> AllActiveBullets()
        {
            List<Bullet> bullets = new List<Bullet>();

            // Enemies bullets
            foreach(Enemy enemy in m_Enemies.Table)
            {
                if (enemy.Bullet.IsActive)
                {
                    bullets.Add(enemy.Bullet);
                }
            }

            // players bullets
            if(m_Player1.Bullet1.IsActive)
            {
                bullets.Add(m_Player1.Bullet1);
            }

            if (m_Player1.Bullet2.IsActive)
            {
                bullets.Add(m_Player1.Bullet2);
            }

            if (m_Player2.Bullet1.IsActive)
            {
                bullets.Add(m_Player2.Bullet1);
            }

            if (m_Player2.Bullet2.IsActive)
            {
                bullets.Add(m_Player2.Bullet2);
            }

            return bullets;
        }

        public void ShipHitEnemy(Player i_Player)
        {
            Enemy tempEnemy = m_Enemies.EnemyGotHitFromBullet(i_Player);

            if (tempEnemy != null)
            {
                i_Player.Score.AddScore(tempEnemy.Model);
                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.EnemyKill].Play();
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

        private void GameIsOn()
        {
            if ((m_Player1.Lifes <= 0) && (m_Player2.Lifes <= 0))
            {
                printScore();
            }
        }

        public float GetBarriersPostionY()
        {
            return this.m_Player1.Position.Y - (2 * this.m_Player1.Height);
        }

        private void shipMoveByKB(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Keys.Escape))
            {
                System.Windows.Forms.MessageBox.Show(m_Player1.Score.Score.ToString() + "\n" + m_Player2.Score.Score.ToString(), "Game Over");
            }

            if (InputManager.KeyHeld(Keys.P))
            {
                m_Player1.MoveRight(gameTime);
            }

            if (InputManager.KeyHeld(Keys.I))
            {
                m_Player1.MoveLeft(gameTime);
            }

            if (InputManager.KeyHeld(Keys.R))
            {
                m_Player2.MoveRight(gameTime);
            }

            if (InputManager.KeyHeld(Keys.W))
            {
                m_Player2.MoveLeft(gameTime);
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

            //// m_PrevMouseState = currState

            return retVal;
        }

        private void newShot()
        {
            bool keyBoardClickPlayer1 = InputManager.KeyPressed(Keys.D9);
            bool keyBoardClickPlayer2 = InputManager.KeyPressed(Keys.D3);
            bool mouseClick = InputManager.ButtonPressed(eInputButtons.Left);

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
                        if (i_Player.BulletIntersectsShip(m_Enemies.GetEnemy(i, j).Bullet))
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

            string winner;

            if (m_Player1.Score.Score > m_Player2.Score.Score)
            {
                winner = "Player 1";
            }
            else if (m_Player1.Score.Score < m_Player2.Score.Score)
            {
                winner = "Player 2";
            }
            else
            {
                winner = "Tie";
            }

            message += $"\n\n";
            message += winner.Equals("Tie") ? "It's a Tie" : $"{winner} is the winner";

            System.Windows.Forms.MessageBox.Show(message, "Game Over");
            this.Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            m_SpriteBatch.Begin();

            m_SpriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel
            m_Player1.Draw(m_SpriteBatch);
            m_Player2.Draw(m_SpriteBatch);
            m_MotherShip.Draw(m_SpriteBatch);
            m_Enemies.Draw(m_SpriteBatch);
            m_Barriers.Draw(m_SpriteBatch);

            m_SpriteBatch.End();
            base.Draw(gameTime);
        }

        private int CalculateNumOfColumnsForEnemiesMatrix(int i_GameLevel)
        {
            int ans = 9;

            switch ((i_GameLevel - 1) % 4)
            {
                case 0:
                    ans = 1;
                    break;
                case 1:
                    ans = 10;
                    break;
                case 2:
                    ans = 11;
                    break;
                default:
                    ans = 9;
                    break;
            }

            return ans;
        }
    }
}
