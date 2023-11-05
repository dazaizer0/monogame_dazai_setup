using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moon_phases.Classes
{
    internal class Animation
    {
        public List<Texture2D> Frames = new List<Texture2D>();
        public Texture2D ActualFrame;
        public int CurrentFrame = 0;

        public float ElapsedTime;
        public float FrameDuration;

        public Animation(List<Texture2D> frames, float elapsed_time, float frame_duration)
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
}
