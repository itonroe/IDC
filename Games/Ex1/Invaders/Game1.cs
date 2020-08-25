using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_graphicsManager;
        private SpriteBatch m_spriteBatch;

        private Texture2D m_TextureBackground;

        private Ship m_ship;


        public Game1()
        {
            m_graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            //init ship
            float x = (float)GraphicsDevice.Viewport.Width / 2;
            float y = (float)GraphicsDevice.Viewport.Height;
            m_ship = new Ship(new Vector2(x, y), m_graphicsManager, m_spriteBatch, GraphicsDevice, Content);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_TextureBackground = Content.Load<Texture2D>("BG_Space01_1024x768");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin();

            m_spriteBatch.Draw(m_TextureBackground, new Vector2(0,0), Color.White); 

            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
