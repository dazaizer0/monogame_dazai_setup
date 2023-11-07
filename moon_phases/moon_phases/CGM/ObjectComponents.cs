using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

namespace moon_phases.CGM
{
    internal class Component
    {
        public bool Enabled;

        public Component(bool enabled)
        {
            Enabled = enabled;
        }
    }

    internal class SoundPlayer : Component
    {
        public SoundEffect Sound;

        public SoundPlayer(SoundEffect sound_effect, bool enabled) : base(enabled)
        {
            Sound = sound_effect;
        }

        public void PlayIt()
        {
            if (this.Enabled)
                this.Sound.Play();
        }
    }
}
