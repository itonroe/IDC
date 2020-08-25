using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Enemy[,] m_Enemies;
        private bool m_LeftToRight;

        //Background Properties
        Texture2D m_TextureBackground;
        Vector2 m_PositionBackground;
        Color m_TintBackground = Color.White;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            if (m_Enemies == null)
            {
                initEnemies();
            }

            base.Initialize();
        }

        private void initEnemies()
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

                    m_Enemies[i, j].Initialize(this.Content, this.GraphicsDevice, model, j, i);
                }
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            m_TextureBackground = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");

            InitPositions();
        }

        private void InitPositions()
        {
            m_PositionBackground = Vector2.Zero;

            //create an alpah channel for background:
            Vector4 bgTint = Vector4.One;
            bgTint.W = 0f; // set the alpha component to 0.2
            m_TintBackground = new Color(bgTint);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            updateEnemies(gameTime);

            base.Update(gameTime);
        }

        private void updateEnemies(GameTime gameTime)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    if (m_Enemies[i, j].Update(gameTime, m_LeftToRight, (float)GraphicsDevice.Viewport.Width))
                    {
                        m_LeftToRight = !m_LeftToRight;
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel

            drawEnemies(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void drawEnemies(GameTime gameTime)
        {
            for (int i = 0; i < m_Enemies.GetLength(0); i++)
            {
                for (int j = 0; j < m_Enemies.GetLength(1); j++)
                {
                    m_Enemies[i, j].Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
