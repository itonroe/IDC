using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;

        Enemies m_enemies;

        //Background Properties
        Texture2D m_TextureBackground;
        Vector2 m_PositionBackground;
        Color m_TintBackground = Color.White;

        Ship m_Ship;

        KeyboardState m_PrevKbState;

        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            m_enemies = new Enemies();
            m_Ship = new Ship();

            base.Initialize();
        }

        private void InitPositions()
        {
            m_PositionBackground = Vector2.Zero;

            m_Ship.InitPosition(GraphicsDevice);

            //create an alpah channel for background:
            Vector4 bgTint = Vector4.One;
            bgTint.W = 0f; // set the alpha component to 0.2
            m_TintBackground = new Color(bgTint);
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_TextureBackground = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_enemies.InitAndLoad(Content, GraphicsDevice);
            m_Ship.LoadContent(Content);

            InitPositions();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateShip();
            UpdateEnemies(gameTime, GraphicsDevice, m_Ship);

            m_PrevKbState = Keyboard.GetState();
            base.Update(gameTime);
        }

        private void UpdateEnemies(GameTime gameTime, GraphicsDevice i_GraphicDevice, Ship i_ship)
        {
            m_enemies.Update(gameTime, GraphicsDevice, m_Ship);
            //if (m_enemies.IsEndOfGame(i_GraphicDevice))
                //Exit();
        }

        private void UpdateShip()
        {
            ShipStillAlive();
            ShipMoveByKB();
            UpdateBulletsForShip();
            NewShot();
            UpdateShipLifes();
        }

        private void ShipStillAlive()
        {
            if (m_Ship.Lifes == 0)
                Exit();
        }

        private void ShipMoveByKB()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                m_Ship.MoveRight(5, GraphicsDevice);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                m_Ship.MoveLeft(5);
            }
        }

        private void UpdateShipLifes()
        {
            for (int i = 0; i< m_enemies.Table.GetLength(0); i++)
            {
                for (int j = 0; j< m_enemies.Table.GetLength(1); j++)
                {
                    if(m_enemies.GetEnemy(i,j).Bullet.IsActive)
                    {
                        m_Ship.BulletIntersectsShip(m_enemies.GetEnemy(i, j).Bullet);
                    }
                }
            }
        }

        private void NewShot()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !m_PrevKbState.IsKeyDown(Keys.Space))
            {
                m_Ship.Shot();
            }
        }

        private void UpdateBulletsForShip()
        {
            m_Ship.Bullet1.UpdateForShip();
            m_Ship.Bullet2.UpdateForShip();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            m_spriteBatch.Begin();

            m_spriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel
            m_enemies.Draw(gameTime, m_spriteBatch);
            m_Ship.Draw(m_spriteBatch);

            m_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
