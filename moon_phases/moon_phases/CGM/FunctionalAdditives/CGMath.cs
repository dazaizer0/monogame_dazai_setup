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
            // Normalizacja kątów do zakresu [0, 2 * Pi]
            from = (from + MathHelper.TwoPi) % MathHelper.TwoPi;
            to = (to + MathHelper.TwoPi) % MathHelper.TwoPi;

            // Interpolacja kątów
            float result = from + (to - from) * amount;

            // Ponowna normalizacja wyniku
            result = (result + MathHelper.TwoPi) % MathHelper.TwoPi;

            return result;
        }
    }
}
