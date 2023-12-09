using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.GameManager
{
    internal class CGMManager
    {
        public static int SceneNumber = 0;
    }

    internal struct GLobalSceneVariables
    {
        public Vector2 GlobalScreenCenter;
        public Vector2 MouseClickPosition;

        public static Vector2 Gravity;
    }
}


