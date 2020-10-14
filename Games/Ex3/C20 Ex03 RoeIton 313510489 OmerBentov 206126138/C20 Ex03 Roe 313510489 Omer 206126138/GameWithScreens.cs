using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Roe_313510489_Omer_206126138.Screens;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace C20_Ex03_Roe_313510489_Omer_206126138
{
    public class GameWithScreens : Game
    {
        private GraphicsDeviceManager m_GraphicManager;
        private SoundEffectInstance m_BgSound;
        private SoundEffectInstance m_MenuMove;
        private List<SoundEffectInstance> m_EffectsSounds;

        private bool m_OnMute;
        private float m_PrevBGSoundVolume;
        private float m_PrevEfeectsSoundsVolume;
        private int m_NumOfPlayers;

        public float PrevBGSoundVolume
        {
            get
            {
                return m_PrevBGSoundVolume;
            }

            set
            {
                m_PrevBGSoundVolume = value;
            }
        }

        public float PrevEfeectsSoundsVolume
        {
            get
            {
                return m_PrevEfeectsSoundsVolume;
            }

            set
            {
                m_PrevEfeectsSoundsVolume = value;
            }
        }

        public bool SoundsOn 
        { 
            get
            {
                return !m_OnMute;
            }
        }

        public int NumOfPlayers 
        { 
            get
            {
                return m_NumOfPlayers;
            }

            set
            {
                m_NumOfPlayers = value;
            }
        }

        public List<SoundEffectInstance> EffectsSounds
        {
            get
            {
                return m_EffectsSounds;
            }
        }

        public SoundEffectInstance BackgroundSound
        {
            get
            {
                return m_BgSound;
            }

            set
            {
                m_BgSound = value;
            }
        }

        public GraphicsDeviceManager GraphicManager
        {
            get
            {
                return m_GraphicManager;
            }

            set
            {
                m_GraphicManager = value;
            }
        }

        public GameWithScreens()
        {
            m_GraphicManager = new GraphicsDeviceManager(this);

            m_GraphicManager.PreferredBackBufferWidth = 750;
            m_GraphicManager.PreferredBackBufferHeight = 600;
            m_GraphicManager.ApplyChanges();

            this.Content.RootDirectory = "Content";

            InputManager inputManager = new InputManager(this);
            m_EffectsSounds = new List<SoundEffectInstance>();
            ScreensMananger screensManager = new ScreensMananger(this);
            setScreens(screensManager);

            m_OnMute = false;
            m_PrevBGSoundVolume = 100;
            m_PrevEfeectsSoundsVolume = 100;
            m_NumOfPlayers = 2;
        }

        private void setScreens(ScreensMananger i_ScreenManager)
        {
            i_ScreenManager.Push(new PlayScreen(this, 1));
            i_ScreenManager.Push(new LevelTransitionScreen(this, 1));
            i_ScreenManager.SetCurrentScreen(new WelcomeScreen(this));
        }

        public enum eBGSounds
        {
            Main = 0,
        }

        public enum eEffectsSounds
        {
            SSGunShot = 0,
            LifeDie = 1,
            EnemyGunShot = 2,
            EnemyKill = 3,
            MotherShipKill = 4,
            BarriersHit = 5,
            LevelWin = 6,
            GameOver = 7,
            MenuMove = 8,
        }

        protected override void Initialize()
        {
            base.Initialize();
            LoadSounds();
        }

        protected void LoadSounds()
        {
            ////Backgorund
            m_BgSound = this.Content.Load<SoundEffect>("Sounds/BGMusic").CreateInstance();
            m_BgSound.IsLooped = true;
            m_BgSound.Play();

            ////Menu Sound
            m_MenuMove = this.Content.Load<SoundEffect>("Sounds/MenuMove").CreateInstance();

            ////Effects sounds
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/SSGunShot").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/LifeDie").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/EnemyGunShot").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/EnemyKill").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/MotherShipKill").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/BarrierHit").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/LevelWin").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/GameOver").CreateInstance());
            m_EffectsSounds.Add(this.Content.Load<SoundEffect>("Sounds/MenuMove").CreateInstance());
        }

        public void MuteEffectsSounds()
        {
            m_PrevEfeectsSoundsVolume = EffectsSounds[0].Volume;

            foreach (SoundEffectInstance soundEffectInstance in EffectsSounds)
            {
                soundEffectInstance.Volume = 0;
            }
        }

        public void MuteBGSound()
        {
            m_PrevBGSoundVolume = BackgroundSound.Volume;

            m_BgSound.Volume = 0;
        }

        public void SetBGSound(float i_Volume)
        {
            if (i_Volume >= 0 && i_Volume <= 1)
            {
                m_BgSound.Volume = i_Volume;
            }
        }

        public void SetEffectsSounds(float i_Volume)
        {
            if (i_Volume >= 0 && i_Volume <= 1)
            {
                foreach (SoundEffectInstance soundEffectInstance in EffectsSounds)
                {
                    soundEffectInstance.Volume = i_Volume;
                }
            }
        }

        public void ToogleMuteAllSounds()
        {
            if (m_OnMute)
            {
                SetBGSound(m_PrevBGSoundVolume);
                SetEffectsSounds(m_PrevEfeectsSoundsVolume);
            }
            else
            {
                MuteBGSound();
                MuteEffectsSounds();
            }

            m_OnMute = !m_OnMute;
        }
    }
}
