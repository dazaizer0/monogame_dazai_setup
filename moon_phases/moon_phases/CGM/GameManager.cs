using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM
{
    #region GLOBAL GAME STRUCTURES
    internal struct GLobalSceneVariables
    {
        public Vector2 GlobalScreenCenter;
        public Vector2 MouseClickPosition;
    }

    internal class GameSceneProperties
    {
        public int GridSize;
        public int MapSize;

        public GameSceneProperties(int grid_size, int map_size)
        {
            GridSize = grid_size;
            MapSize = map_size;
        }
    }
    #endregion
}
