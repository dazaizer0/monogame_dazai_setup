using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.Classes
{

    internal class Component
    {
        public bool Enabled;

        public Component(bool enabled)
        {
            Enabled = enabled;
        }
    }

    internal class Animation : Component
    {
        public List<Texture2D> Frames = new List<Texture2D>();
        public Texture2D ActualFrame;
        public int CurrentFrame = 0;

        public float ElapsedTime;
        public float FrameDuration;

        public Animation(List<Texture2D> frames, float elapsed_time, float frame_duration, bool enabled) : base(enabled)
        {
            Frames = frames;
            ElapsedTime = elapsed_time;
            FrameDuration = frame_duration;
        }

        public void PlayAnimation(SpriteBatch sprite_batch, PlayerObject player)
        {
            /*
             * UPDATE: 
             * ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
             * if (ElapsedTime >= FrameDuration)
             * {
             *    CurrentFrame = (CurrentFrame + 1) % Frames.Size();
             *    ActualFrame = Frames[CurrentFrame];
             *    ElapsedTime = 0;
             * }
             * 
             * DRAW: DRAWIT
            */
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
