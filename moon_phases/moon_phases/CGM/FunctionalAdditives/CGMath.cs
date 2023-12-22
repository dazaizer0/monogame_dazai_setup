using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moon_phases.CGM.FunctionalAdditives
{
    internal class CGMath
    {
        public static float LerpAngle(float from, float to, float amount)
        {
            from = (from + MathHelper.TwoPi) % MathHelper.TwoPi; // Normalizacja kątów do zakresu [0, 2 * Pi]
            to = (to + MathHelper.TwoPi) % MathHelper.TwoPi;

            float result = from + (to - from) * amount; // INTERPOLATION

            result = (result + MathHelper.TwoPi) % MathHelper.TwoPi; // NORMALIZATION

            return result;
        }
    }
}
