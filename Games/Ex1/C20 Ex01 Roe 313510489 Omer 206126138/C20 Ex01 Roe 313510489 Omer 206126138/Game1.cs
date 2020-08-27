using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private KeyboardState m_PrevKbState;
        private MouseState m_PrevMouseState;

        //Enemies collection
        private Enemies m_enemies;
        private int m_CountEnemyKills;
        private const double k_IncreseSpeedAfterFiveKills = 1.03;

        //MotherShip object
        private MotherShip m_MotherShip;

        //Background Properties
        private Texture2D m_TextureBackground;
        private Vector2 m_PositionBackground;
        private Color m_TintBackground = Color.White;

        // Ship object
        private Ship m_Ship;

        // Score
        private GameScore m_Score;
        private const int lifeLost = -600;

        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            m_enemies = new Enemies();
            m_Ship = new Ship();
            m_Score = new GameScore();
            m_MotherShip = new MotherShip();

            m_CountEnemyKills = 0;

            m_graphics.PreferredBackBufferWidth = 1024;
            m_graphics.PreferredBackBufferHeight = 600  ;
            m_graphics.ApplyChanges();

            base.Initialize();
        }

        private void InitPositions()
        {
            m_PositionBackground = Vector2.Zero;

            m_Ship.InitPosition(GraphicsDevice);
            m_MotherShip.initPositions();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_TextureBackground = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_enemies.InitAndLoad(Content, GraphicsDevice);
            m_Ship.LoadContent(Content);
            m_MotherShip.LoadContent(Content, "Red");

            InitPositions();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateShip(gameTime);
            UpdateEnemies(gameTime, GraphicsDevice, m_Ship);
            UpdateMotherShip(gameTime);
            UpdateIntersections(GraphicsDevice);

            m_PrevKbState = Keyboard.GetState();
            base.Update(gameTime);
        }

        private void UpdateEnemies(GameTime gameTime, GraphicsDevice i_GraphicDevice, Ship i_ship)
        {
            m_enemies.Update(gameTime, GraphicsDevice, m_Ship);
            if (m_enemies.AllEnemiesAreDead() && !m_MotherShip.IsAlive)
                PrintScore();
        }

        private void UpdateShip(GameTime gameTime)
        {
            ShipStillAlive();
            if(!ShipMoveByMouse(GraphicsDevice))
                ShipMoveByKB(gameTime);
            UpdateBulletsForShip(gameTime);
            NewShot();
        }

        private void UpdateMotherShip(GameTime gameTime)
        {
            if (TimeForMotherShip() && !m_MotherShip.IsAlive)
            {
                m_MotherShip.GetReadyToPop();
            }

            if (m_MotherShip.IsAlive)
                m_MotherShip.MoveRight(gameTime, GraphicsDevice);
        }

        private void UpdateIntersections(GraphicsDevice i_GraphicDevice)
        {
            //enemy reach bottom
            if (m_enemies.IsEndOfGame(i_GraphicDevice))
                PrintScore();

            //enemy bullet hit ship
            ShipGotHitFromBullet();

            //ship bullet hit enemy
            Enemy tempEnemy = m_enemies.EnemyGotHitFromBullet(m_Ship);
            if (tempEnemy != null)
            {
                m_Score.AddSCore(tempEnemy.Model);

                // for speed increse
                m_CountEnemyKills++;
                if(m_CountEnemyKills % 5 == 0)
                {
                    m_enemies.IncreseSpeedForAllEnemis(k_IncreseSpeedAfterFiveKills);
                }
                
            }

            // Enemy and ship intersect
            if (m_enemies.ShipIntersection(m_Ship) )
                PrintScore();

            //MotherSHip Intersect with bullets
            if (m_MotherShip.IntersectionWithShipBullets(m_Ship))
                m_Score.AddSCore(m_MotherShip.Model);
        }

        private bool TimeForMotherShip()
        {
            bool answer = false;

            Random rnd = new Random();
            if(rnd.Next(0,300) == 0)
            {
                answer = true;
            }

            return answer;
        }

        private void ShipStillAlive()
        {
            if (m_Ship.Lifes == 0)
                PrintScore();
        }

        private void ShipMoveByKB(GameTime gameTime)
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

        private bool ShipMoveByMouse(GraphicsDevice i_graphicDevice)
        {
            bool isMouseMove = false;

            float mouseDeltaX = GetMousePositionDelta().X;
            var newXposition = Math.Clamp(m_Ship.Position.X + mouseDeltaX , 0, (float)i_graphicDevice.Viewport.Width - m_Ship.Texture.Width);
            Vector2 mousePosition = new Vector2(newXposition,m_Ship.Position.Y);
            m_Ship.Position = mousePosition;

            if (mouseDeltaX != 0)
                isMouseMove = true;

            return isMouseMove;
        }

        private Vector2 GetMousePositionDelta()
        {
            Vector2 retVal = Vector2.Zero;

            MouseState currState = Mouse.GetState();

            if (m_PrevMouseState != null)
            {
                retVal.X = (currState.X - m_PrevMouseState.X);
                retVal.Y = (currState.Y - m_PrevMouseState.Y);
            }

            m_PrevMouseState = currState;

            return retVal;
        }

        private void NewShot()
        {
            bool keyBoardClick = Keyboard.GetState().IsKeyDown(Keys.Space) && !m_PrevKbState.IsKeyDown(Keys.Space);
            bool mouseClick = Mouse.GetState().LeftButton == ButtonState.Pressed && m_PrevMouseState.LeftButton == ButtonState.Released;

            if (keyBoardClick || mouseClick)
            {
                m_Ship.Shot();
            }
        }

        private void ShipGotHitFromBullet()
        {
            for (int i = 0; i< m_enemies.Table.GetLength(0); i++)
            {
                for (int j = 0; j< m_enemies.Table.GetLength(1); j++)
                {
                    if (m_enemies.GetEnemy(i, j).Bullet.IsActive)
                    {
                        if (m_Ship.BulletIntersectsShip(m_enemies.GetEnemy(i, j).Bullet))
                        {
                            m_Score.AddSCore(lifeLost);
                            m_enemies.ActiveBullets--;
                        }
                    }
                }
            }
        }

        private void UpdateBulletsForShip(GameTime gameTime)
        {
            m_Ship.Bullet1.UpdateForShip(gameTime, GraphicsDevice);
            m_Ship.Bullet2.UpdateForShip(gameTime, GraphicsDevice);
        }

        private void PrintScore()
        {
            string message = $"Your score is: {m_Score.Score}";
            System.Windows.Forms.MessageBox.Show(message, "Game Over");
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            m_spriteBatch.Begin();

            m_spriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel
            m_enemies.Draw(gameTime, m_spriteBatch);
            m_Ship.Draw(m_spriteBatch);
            m_MotherShip.Draw(gameTime, m_spriteBatch);

            m_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
