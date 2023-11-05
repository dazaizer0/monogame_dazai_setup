using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moon_phases.Classes
{
    struct GLobalVariables
    {
        public Vector2 GlobalScreenCenter;
        public Vector2 MouseClickPosition;
    }

    class GameScene
    {
        public int GridSize;
        public int MapSize;

        public GameScene(int grid_size, int map_size) 
        {
            GridSize = grid_size; 
            MapSize = map_size;
        }
    }
}
