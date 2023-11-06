using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

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

        public void PlayAnimation(SpriteBatch sprite_batch, PlayerObject player, GameTime game_time)
        {
            // IN DEVELOPMENT | PROTOTYPE | CONSPECT
            ElapsedTime += (float)game_time.ElapsedGameTime.TotalSeconds;
            if (ElapsedTime >= this.FrameDuration)
            {
                CurrentFrame = (CurrentFrame + 1) % Frames.Count;
                ActualFrame = Frames[CurrentFrame];
                ElapsedTime = 0;
            }
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
