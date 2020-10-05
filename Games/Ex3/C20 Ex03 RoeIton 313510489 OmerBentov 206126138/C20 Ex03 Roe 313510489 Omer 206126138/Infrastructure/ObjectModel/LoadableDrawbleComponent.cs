//*** Guy Ronen � 2008-2011 ***//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Infrastructure.ServiceInterfaces;

namespace Infrastructure.ObjectModel
{
    public abstract class LoadableDrawableComponent : DrawableGameComponent
    {
        protected string m_AssetName;

        // used to load the sprite:
        protected ContentManager ContentManager
        {
            get { return this.Game.Content; }
        }

        public event PositionChangedEventHandler PositionChanged;
        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this);
            }
        }

        public string AssetName
        {
            get { return m_AssetName; }
            set { m_AssetName = value; }
        }

        public LoadableDrawableComponent(
            string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_Game)
        {
            this.AssetName = i_AssetName;
            this.UpdateOrder = i_UpdateOrder;
            this.DrawOrder = i_DrawOrder;
        }

        public LoadableDrawableComponent(
            string i_AssetName,
            Game i_Game,
            int i_CallsOrder)
            : this(i_AssetName, i_Game, i_CallsOrder, i_CallsOrder)
        { }

        public override void Initialize()
        {
            base.Initialize();
            
            if (this is ICollidable)
            {
                ICollisionsManager collisionMgr =
                    this.Game.Services.GetService(typeof(ICollisionsManager))
                        as ICollisionsManager;

                if (collisionMgr != null)
                {
                    collisionMgr.AddObjectToMonitor(this as ICollidable);
                }
            }

            // After everything is loaded and initialzied,
            // lets init graphical aspects:
            InitBounds();   // a call to an abstract method;
        }

#if DEBUG
        protected bool m_ShowBoundingBox = true;
#else
        protected bool m_ShowBoundingBox = false;
#endif

        public bool ShowBoundingBox
        {
            get { return m_ShowBoundingBox; }
            set { m_ShowBoundingBox = value; }
        }

        protected abstract void InitBounds();

        public override void Draw(GameTime gameTime)
        {
            DrawBoundingBox();
            base.Draw(gameTime);
        }

        protected abstract void DrawBoundingBox();
    }
}