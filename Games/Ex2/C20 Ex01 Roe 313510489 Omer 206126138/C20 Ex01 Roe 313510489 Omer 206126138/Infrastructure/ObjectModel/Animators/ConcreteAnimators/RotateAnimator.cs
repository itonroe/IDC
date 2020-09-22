using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class RotateAnimator : SpriteAnimator
    {
        private TimeSpan m_AnimationLength;
        private TimeSpan m_TimeLeftForRotate;
        private double m_LastRotationTime;
        const float k_AngularVelocity = (float)MathHelper.TwoPi;
        const float k_MaxAngle = ((float)MathHelper.TwoPi);

        public TimeSpan RotateLength
        {
            get { return m_AnimationLength; }
            set { m_AnimationLength = value; }
        }

        // CTORs
        public RotateAnimator(string i_Name, TimeSpan i_RotateLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_AnimationLength = i_AnimationLength;
            this.m_TimeLeftForRotate = i_RotateLength;
            this.m_LastRotationTime = 0;
        }

        public RotateAnimator(TimeSpan i_RotateLength, TimeSpan i_AnimationLength)
            : this("Rotate", i_RotateLength, i_AnimationLength)
        {
            this.m_AnimationLength = i_RotateLength;
            this.m_TimeLeftForRotate = i_RotateLength;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForRotate -= i_GameTime.ElapsedGameTime;

            if (i_GameTime.TotalGameTime.TotalSeconds - m_LastRotationTime >= m_AnimationLength.TotalSeconds)
            {
                this.BoundSprite.RotationOrigin = new Vector2(this.BoundSprite.Width/8, this.BoundSprite.Height/8);
                this.BoundSprite.Rotation += (float)(MathHelper.TwoPi * m_TimeLeftForRotate.TotalSeconds *1/100);
                this.BoundSprite.AngularVelocity = k_AngularVelocity;
            }

            if (m_TimeLeftForRotate.TotalSeconds < 0)
            {
                // we have elapsed, so blink
                this.BoundSprite.Visible = !this.BoundSprite.Visible;
                m_TimeLeftForRotate = m_AnimationLength;
            }

            

        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
        }
    }
}
