using System;
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

        // Ship object
        private Ship m_Ship;

        // Score
        private GameScore m_Score;

        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            m_Enemies = new Enemies();
            m_Ship = new Ship();
            m_Score = new GameScore();
            m_MotherShip = new MotherShip();

            m_CountEnemyKills = 0;

            m_Graphics.PreferredBackBufferWidth = 1024;
            m_Graphics.PreferredBackBufferHeight = 600;
            m_Graphics.ApplyChanges();

            base.Initialize();
        }

        private void initPositions()
        {
            m_PositionBackground = Vector2.Zero;

            m_Ship.InitPosition(GraphicsDevice);
            m_MotherShip.initPositions();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            m_TextureBackground = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_Enemies.InitAndLoad(Content, GraphicsDevice);
            m_Ship.LoadContent(Content);
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
            base.Update(gameTime);
        }

        private void updateEnemies(GameTime gameTime)
        {
            m_Enemies.Update(gameTime, GraphicsDevice);
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
            shipGotHitFromBullet();

            // ship bullet hit enemy
            Enemy tempEnemy = m_Enemies.EnemyGotHitFromBullet(m_Ship);
            if (tempEnemy != null)
            {
                m_Score.AddSCore(tempEnemy.Model);

                // for speed increse
                m_CountEnemyKills++;
                if(m_CountEnemyKills % 5 == 0)
                {
                    m_Enemies.IncreseSpeedForAllEnemis(k_IncreseSpeedAfterFiveKills);
                }    
            }

            // Enemy and ship intersect
            if (m_Enemies.ShipIntersection(m_Ship))
            {
                printScore();
            }

            // MotherSHip Intersect with bullets
            if (m_MotherShip.IntersectionWithShipBullets(m_Ship))
            {
                m_Score.AddSCore(m_MotherShip.Model);
            }
        }

        private bool timeForMotherShip()
        {
            bool answer = false;

            Random rnd = new Random();
            if(rnd.Next(0, 300) == 0)
            {
                answer = true;
            }

            return answer;
        }

        private void shipStillAlive()
        {
            if (m_Ship.Lifes == 0)
            {
                printScore();
            }
        }

        private void shipMoveByKB(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                System.Windows.Forms.MessageBox.Show(m_Score.Score.ToString(), "Game Over");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                m_Ship.MoveRight(gameTime, GraphicsDevice);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                m_Ship.MoveLeft(gameTime, GraphicsDevice);
            }
        }

        private bool shipMoveByMouse(GraphicsDevice i_graphicDevice)
        {
            bool isMouseMove = false;

            float mouseDeltaX = getMousePositionDelta().X;
            var newXposition = Math.Clamp(m_Ship.Position.X + mouseDeltaX, 0, (float)i_graphicDevice.Viewport.Width - m_Ship.Texture.Width);
            Vector2 mousePosition = new Vector2(newXposition, m_Ship.Position.Y);
            m_Ship.Position = mousePosition;

            if (mouseDeltaX != 0)
            {
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

            m_PrevMouseState = currState;

            return retVal;
        }

        private void newShot()
        {
            bool keyBoardClick = Keyboard.GetState().IsKeyDown(Keys.Space) && !m_PrevKbState.IsKeyDown(Keys.Space);
            bool mouseClick = Mouse.GetState().LeftButton == ButtonState.Pressed && m_PrevMouseState.LeftButton == ButtonState.Released;

            if (keyBoardClick || mouseClick)
            {
                m_Ship.Shot();
            }
        }

        private void shipGotHitFromBullet()
        {
            for (int i = 0; i < m_Enemies.Table.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.Table.GetLength(1); j++)
                {
                    if (m_Enemies.GetEnemy(i, j).Bullet.IsActive)
                    {
                        if (m_Ship.BulletIntersectsShip(m_Enemies.GetEnemy(i, j).Bullet))
                        {
                            m_Score.AddSCore(k_LifeLost);
                            m_Enemies.ActiveBullets--;
                        }
                    }
                }
            }
        }

        private void updateBulletsForShip(GameTime gameTime)
        {
            m_Ship.Bullet1.UpdateForShip(gameTime);
            m_Ship.Bullet2.UpdateForShip(gameTime);
        }

        private void printScore()
        {
            string message = $"Your score is: {m_Score.Score}";
            System.Windows.Forms.MessageBox.Show(message, "Game Over");
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            m_SpriteBatch.Begin();

            m_SpriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel
            m_Enemies.Draw(m_SpriteBatch);
            m_Ship.Draw(m_SpriteBatch);
            m_MotherShip.Draw(m_SpriteBatch);

            m_SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
