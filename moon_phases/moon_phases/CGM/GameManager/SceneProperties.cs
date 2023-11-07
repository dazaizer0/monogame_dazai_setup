using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.GameManager
{
    internal class SceneProperties
    {
        public int GridSize;
        public int MapSize;

        public SceneProperties(int grid_size, int map_size)
        {
            GridSize = grid_size;
            MapSize = map_size;
        }
    }
}
