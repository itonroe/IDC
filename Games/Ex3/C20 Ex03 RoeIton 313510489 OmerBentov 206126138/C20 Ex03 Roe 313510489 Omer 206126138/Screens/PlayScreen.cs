using System;
using System.Collections;
using System.Collections.Generic;
using C20_Ex03_Roe_313510489_Omer_206126138.Classes;
using C20_Ex03_Roe_313510489_Omer_206126138.Screens;
using GameScreens.Sprites;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138
{
    public class PlayScreen : GameScreen
    {
        private const double k_IncreseSpeedAfterFiveKills = 1.03;
        private const int k_LifeLost = -600;
        private const int k_RadnomPopDifficulty = 500;
        private const int k_NumOfBarriers = 4;

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

        //Background
        Background m_Background;

        private int m_GameLevel;


        public PlayScreen(Game i_Game, int i_GameLevel) : base(i_Game)
        {
            ConstructorMethod(i_Game, i_GameLevel);
        }

        public PlayScreen(Game i_Game, int i_GameLevel, Player i_Player1, Player i_Player2) : base(i_Game)
        {
            ConstructorMethod(i_Game, i_GameLevel);

            m_Player1 = i_Player1;

            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                m_Player2 = i_Player2;
            }
        }

        private void ConstructorMethod(Game i_Game, int i_GameLevel)
        {
            Game.Window.AllowUserResizing = false;
            m_GameLevel = i_GameLevel;
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);
        }

        public override void Initialize()
        {
            m_Enemies = new Enemies(this, CalculateNumOfColumnsForEnemiesMatrix());
            m_Barriers = new Barriers(this, k_NumOfBarriers, CalculateBarriersSpeed());


            if (m_GameLevel == 1)
            {
                try
                {
                    m_Player1 = new Player(1, this);
                    m_Player1.Initialize();

                    this.Add(m_Player1);
                    this.Add(m_Player1.Bullet1);
                    this.Add(m_Player1.Bullet2);

                    if ((Game as GameWithScreens).NumOfPlayers == 2)
                    {

                        m_Player2 = new Player(2, this);
                        m_Player2.Initialize();


                        this.Add(m_Player2);
                        this.Add(m_Player2.Bullet1);
                        this.Add(m_Player2.Bullet2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                this.Add(m_Player1);
                this.Add(m_Player1.Bullet1);
                this.Add(m_Player1.Bullet2);

                if ((Game as GameWithScreens).NumOfPlayers == 2)
                {
                    this.Add(m_Player2);
                    this.Add(m_Player2.Bullet1);
                    this.Add(m_Player2.Bullet2);
                }
            }

            m_MotherShip = new MotherShip(this);
            this.Add(m_MotherShip);

            m_CountEnemyKills = 0;
            m_Enemies.AddEnemies();

            base.Initialize();

            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }

        private void initPositions()
        {
            m_PositionBackground = Vector2.Zero;

            m_Player1.InitPosition();

            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                m_Player2.InitPosition();
            }

            m_Barriers.InitPositions(GraphicsDevice);
            m_Enemies.InitPositions();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            m_TextureBackground = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_MotherShip.LoadContent("Red");

            initPositions();
        }

        public void UpdateNumberOfPlayers()
        {
            if (m_Player2 != null && (Game as GameWithScreens).NumOfPlayers == 1)
            {
                this.Remove(m_Player2);
                this.Remove(m_Player2.Bullet1);
                this.Remove(m_Player2.Bullet2);
            }
            else if (m_Player2 == null && (Game as GameWithScreens).NumOfPlayers == 2)
            {
                m_Player2 = new Player(2, this);
                m_Player2.Initialize();
                this.Add(m_Player2);
                this.Add(m_Player2.Bullet1);
                this.Add(m_Player2.Bullet2);
                m_Player2.InitPosition();
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdateNumberOfPlayers();

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
            if (m_Enemies.AllEnemiesAreDead())
            {
                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.LevelWin].Play();

                (base.m_ScreensManager as ScreensMananger).Push(new PlayScreen(this.Game, m_GameLevel + 1, m_Player1, m_Player2));

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


            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
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
            }
            else
            {
                // enemy bullet hit ship
                shipGotHitFromBullet(m_Player1, i_GraphicDevice);

                // ship bullet hit enemy
                ShipHitEnemy(m_Player1);

                // Enemy and ship intersect
                if (m_Enemies.ShipIntersection(m_Player1))
                {
                    printScore();
                }
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

            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                if (m_Player2.Bullet1.IsActive)
                {
                    playersBullets.Add(m_Player2.Bullet1);
                }

                if (m_Player2.Bullet2.IsActive)
                {
                    playersBullets.Add(m_Player2.Bullet2);
                }
            }

            // Enemies bullets
            foreach (Enemy enemy in m_Enemies.Table)
            {
                if (enemy.Bullet.IsActive)
                {
                    enemyBullets.Add(enemy.Bullet);
                }
            }

            foreach (Bullet eBullet in enemyBullets)
            {
                foreach (Bullet pBullet in playersBullets)
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
            foreach (Enemy enemy in m_Enemies.Table)
            {
                if (enemy.Bullet.IsActive)
                {
                    bullets.Add(enemy.Bullet);
                }
            }

            // players bullets
            if (m_Player1.Bullet1.IsActive)
            {
                bullets.Add(m_Player1.Bullet1);
            }

            if (m_Player1.Bullet2.IsActive)
            {
                bullets.Add(m_Player1.Bullet2);
            }

            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                if (m_Player2.Bullet1.IsActive)
                {
                    bullets.Add(m_Player2.Bullet1);
                }

                if (m_Player2.Bullet2.IsActive)
                {
                    bullets.Add(m_Player2.Bullet2);
                }
            }

            return bullets;
        }

        public void ShipHitEnemy(Player i_Player)
        {
            Enemy tempEnemy = m_Enemies.EnemyGotHitFromBullet(i_Player);

            if (tempEnemy != null)
            {
                i_Player.Score.AddScore(tempEnemy.Model);
                if (m_GameLevel >= 2 && m_GameLevel <= 4)
                {
                    i_Player.Score.AddScore((m_GameLevel - 1) * 100);
                }

                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.EnemyKill].Play();
            }

            // MotherSHip Intersect with bullets
            if (m_MotherShip.IntersectionWithShipBullets(i_Player))
            {
                i_Player.Score.AddScore(m_MotherShip.Model);
                if (m_GameLevel >= 2 && m_GameLevel <= 4)
                {
                    i_Player.Score.AddScore((m_GameLevel - 1) * 100);
                }

                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.MotherShipKill].Play();
            }
        }

        private bool timeForMotherShip()
        {
            bool answer = false;

            Random rnd = new Random();
            if (rnd.Next(0, k_RadnomPopDifficulty) == 0)
            {
                answer = true;
            }

            return answer;
        }

        private void GameIsOn()
        {
            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                if ((m_Player1.Lifes <= 0) && (m_Player2.Lifes <= 0))
                {
                    printScore();
                }
            }
            else
            {
                if (m_Player1.Lifes <= 0)
                {
                    printScore();
                }
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
                printScore();
            }

            if (InputManager.KeyHeld(Keys.P))
            {
                m_Player1.MoveRight(gameTime);
            }

            if (InputManager.KeyHeld(Keys.I))
            {
                m_Player1.MoveLeft(gameTime);
            }

            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                if (InputManager.KeyHeld(Keys.R))
                {
                    m_Player2.MoveRight(gameTime);
                }

                if (InputManager.KeyHeld(Keys.W))
                {
                    m_Player2.MoveLeft(gameTime);
                }
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

            if (keyBoardClickPlayer2 && (Game as GameWithScreens).NumOfPlayers == 2)
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
            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                m_Player2.Bullet1.UpdateForShip(gameTime);
                m_Player2.Bullet2.UpdateForShip(gameTime);
            }
        }

        private void printScore()
        {
            string message;
            string winner;

            if ((Game as GameWithScreens).NumOfPlayers == 2)
            {
                message = $"Player 1 Score is: {m_Player1.Score.Score}\nPlayer 2 Score is: {m_Player2.Score.Score}";

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
            }
            else
            {
                message = $"Player Score is: {m_Player1.Score.Score}";
            }

            ScreensManager.SetCurrentScreen(new GameOverScreen(Game, message));
            ExitScreen();
        }

        private int CalculateNumOfColumnsForEnemiesMatrix()
        {
            int ans = 9;

            switch ((m_GameLevel - 1) % 4)
            {
                case 0:
                    ans = 9;
                    break;
                case 1:
                    ans = 10;
                    break;
                case 2:
                    ans = 11;
                    break;
                case 3:
                    ans = 12;
                    break;
                default:
                    ans = 9;
                    break;
            }

            return ans;
        }

        private float CalculateBarriersSpeed()
        {
            const int defaultVelocityPerSecond = 35;
            float ans = 0;

            switch ((m_GameLevel - 1) % 4)
            {
                case 0:
                    ans = 0;
                    break;
                case 1:
                    ans = defaultVelocityPerSecond;
                    break;
                case 2:
                    ans = (float)(defaultVelocityPerSecond * 0.6);
                    break;
                case 3:
                    ans = (float)(defaultVelocityPerSecond * 0.36);
                    break;
                default:
                    ans = defaultVelocityPerSecond;
                    break;
            }

            return ans;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
            initPositions();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            try
            {

                SpriteBatch.Begin();
                m_Player1.DrawLives(SpriteBatch);
                m_Player1.DrawScore(SpriteBatch);

                if ((Game as GameWithScreens).NumOfPlayers == 2)
                {
                    m_Player2.DrawLives(SpriteBatch);
                    m_Player2.DrawScore(SpriteBatch);
                }

                SpriteBatch.End();
            }
            catch
            {
                Console.WriteLine("Draw Player before screen");
            }
        }
    }
}
