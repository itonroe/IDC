using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    class FaderAnimator : SpriteAnimator
    {
        public FaderAnimator(String i_Name, TimeSpan i_AnimationLength) 
            : base (i_Name, i_AnimationLength) { }

        protected override void DoFrame(GameTime i_GameTime)
        {
            this.BoundSprite.Opacity -= (float)(m_OriginalSpriteInfo.Opacity / AnimationLength.TotalSeconds) * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }
    }
}
