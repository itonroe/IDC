using System;
using C20_Ex01_Roe_313510489_Omer_206126138.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;

namespace C20_Ex01_Roe_313510489_Omer_206126138
{
    public class Enemy : Sprite
    {
        private int k_NumOfFrames = 5;

        private const double k_SpeedMultiplicationParam = 1.06;

        protected float k_EnemyVelocityPerSecond = 60;
        private eEnemyModels m_EnemyModel;

        private Bullet m_Bullet;

        private bool m_ImageFliped;

        private double m_GameTotalSeconds;
        private double m_TimeToJump;

        public Enemy(string i_Asset, Game i_Game) : base(i_Asset, i_Game)
        {
            m_Bullet = new Bullet(Color.Blue, i_Game);
            Visible = true;
            m_ImageFliped = false;
            m_GameTotalSeconds = 0;
            m_TimeToJump = 1;
        }

        public int Model 
        { 
            get 
            { 
                return (int)m_EnemyModel; 
            } 
        }

        public bool IsAlive 
        { 
            get 
            { 
                return Visible; 
            } 

            set 
            { 
                Visible = value; 
            } 
        }

        public Bullet Bullet 
        { 
            get 
            { 
                return m_Bullet; 
            } 

            set 
            { 
                m_Bullet = value; 
            } 
        }

        public enum eEnemyModels
        {
            Red = 600,
            Pink = 300,
            Blue = 200,
            Yellow = 70
        }

        public void Initialize(string i_Model, float i_DeltaX, float i_DeltaY)
        {
            base.Initialize();

            // Enemy
            Color enemyColor = Color.Black;

            switch (Enum.Parse(typeof(eEnemyModels), i_Model))
            {
                case eEnemyModels.Pink:
                    enemyColor = Color.LightPink;
                    break;
                case eEnemyModels.Blue:
                    enemyColor = Color.LightBlue;
                    break;
                case eEnemyModels.Yellow:
                    enemyColor = Color.LightYellow;
                    break;
                case eEnemyModels.Red:
                    enemyColor = Color.Red;
                    break;
            }

            TintColor = enemyColor;

            //initAnimations();

            LoadContent(i_Model);
            InitPositions(i_DeltaX, i_DeltaY);
        }

        private void initAnimations()
        {
            //const bool v_Loop = true;
            //const float k_TravelSpeed = 120;

            //CellAnimator celAnimation = new CellAnimator(TimeSpan.FromSeconds(0.3), k_NumOfFrames, TimeSpan.Zero);

            BlinkAnimator blinkAnimation = new BlinkAnimator("blink1", TimeSpan.FromSeconds(0.3), TimeSpan.FromSeconds(4.5));

            /*WaypointsAnymator waypointsAnimation1 =
                new WaypointsAnymator(
                "waypoints1",
                k_TravelSpeed,
                TimeSpan.FromSeconds(7),
                v_Loop,
                new Vector2(100),
                new Vector2(150, 100),
                new Vector2(130, 50));

            WaypointsAnymator waypointsAnimation2 =
                new WaypointsAnymator(
                    "waypoints2",
                    k_TravelSpeed / 2,
                    TimeSpan.Zero,
                    !v_Loop,
                    new Vector2(200));

            SequencialAnimator sequencialAnimations = new SequencialAnimator(
                "sequence1",
                TimeSpan.Zero, this,
                new BlinkAnimator("Blink", TimeSpan.FromSeconds(0.3), TimeSpan.FromSeconds(4.5)),
                waypointsAnimation1,
                waypointsAnimation2);*/

            //this.Animations.Add(celAnimation);
            this.Animations.Add(blinkAnimation);

            blinkAnimation.Finished += new EventHandler(sequencialAnimations_Finished);

            this.Animations.Enabled = true;
        }

        private void sequencialAnimations_Finished(object sender, EventArgs e)
        {
            //this.Animations["CelAnimation"].Pause();
        }

        public virtual void InitPositions(float i_DeltaX, float i_DeltaY)
        {
            const int margin = 20;

            float x = i_DeltaX * (Texture.Width + margin);
            float y = 3 * Texture.Height + (i_DeltaY * (Texture.Height + margin));

            m_Position = new Vector2(x, y);
        }

        public virtual void LoadContent(string i_Model)
        {
            m_EnemyModel = (eEnemyModels)Enum.Parse(typeof(eEnemyModels), i_Model);

            SwitchImage(ContentManager);
        }

        public bool Update(GameTime i_GameTime, bool i_LeftToRight, float i_MaxWidth)
        {
            return TimeToJump(i_GameTime) ? Jump(i_GameTime, i_LeftToRight, i_MaxWidth) : false;
        }

        public bool TimeToJump(GameTime I_GameTime)
        {
            bool jump = true;

            if (I_GameTime.TotalGameTime.TotalSeconds - m_GameTotalSeconds <= m_TimeToJump)
            {
                jump = false;
            }
            else
            {
                m_GameTotalSeconds = I_GameTime.TotalGameTime.TotalSeconds;
            }

            return jump;
        }

        public bool Jump(GameTime i_GameTime, bool i_LeftToRight, float i_MaxWidth)
        {
            bool touchesTheBorder = false;

            SwitchImage(ContentManager);

            if (i_LeftToRight)
            {
                if (m_Position.X + Texture.Width <= i_MaxWidth - Texture.Width)
                {
                    MoveRight(i_GameTime, Texture.Width);
                }
                else
                {
                    touchesTheBorder = true;
                }
            }
            else
            {
                if (m_Position.X - Texture.Width > 0)
                {
                    MoveLeft(i_GameTime, Texture.Width);
                }
                else
                {
                    touchesTheBorder = true;
                }
            }

            return touchesTheBorder;
        }

        public void SwitchImage(ContentManager i_ContentManager)
        {
            string assetName;

            switch (m_EnemyModel)
            {
                case eEnemyModels.Pink:
                    assetName = m_ImageFliped ? @"Sprites\Enemy0101_32x32" : @"Sprites\Enemy0102_32x32";
                    break;
                case eEnemyModels.Blue:
                    assetName = m_ImageFliped ? @"Sprites\Enemy0201_32x32" : @"Sprites\Enemy0202_32x32";
                    break;
                case eEnemyModels.Yellow:
                    assetName = m_ImageFliped ? @"Sprites\Enemy0301_32x32" : @"Sprites\Enemy0302_32x32";
                    break;
                case eEnemyModels.Red:
                    assetName = @"Sprites\MotherShip_32x120";
                    break;
                default:
                    throw new ArgumentException("Color Is Not Recognize, there is no such enemy");
            }

            m_ImageFliped = !m_ImageFliped;

            Texture = i_ContentManager.Load<Texture2D>(assetName);
        }

        public bool UpdateBullet(GameTime gameTime, GraphicsDevice i_GraphicDevice)
        {
            if (m_Bullet.IsActive)
            {
                 return m_Bullet.UpdateForEnemy(i_GraphicDevice, gameTime);
            }

            return false;
        }

        public void MoveRight(GameTime i_GameTime, int i_Distance)
        {
            m_Position.X += i_Distance;
        }

        public void MoveLeft(GameTime i_GameTime, int i_Distance)
        {
            m_Position.X -= i_Distance;
        }

        public void MoveDown()
        {
            m_Position.Y += Texture.Height / 2;
            m_TimeToJump -= ((0.05) * m_TimeToJump);
        }

        public void IncreseSpeed()
        {
            IncreseSpeed(k_SpeedMultiplicationParam);
        }

        public void IncreseSpeed(double i_Speed)
        {
            k_EnemyVelocityPerSecond = (float)(k_EnemyVelocityPerSecond * i_Speed);
        }

        public void Shot()
        {
            if (!m_Bullet.IsActive)
            {
                m_Bullet.ChangedToActive(new Vector2(m_Position.X + (Texture.Width / 2), m_Position.Y));
            }
        }

        public void Draw(SpriteBatch i_SpriteBatch)
        {
            // Bullet
            if(m_Bullet.IsActive)
            {
                m_Bullet.Draw(i_SpriteBatch);
            }
        }
    }
}
