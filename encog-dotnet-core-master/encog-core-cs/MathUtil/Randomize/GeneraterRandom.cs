using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encog.MathUtil.Randomize
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneraterRandom  
    {
         private static Random _random = new Random((int) (DateTime.Now.Ticks*10000000));

        /// <summary>
        /// Construct a random number generator with a random(current time) seed. If
        /// you want to set your own seed, just call "getRandom().setSeed".
        /// </summary>
        ///
         public GeneraterRandom()
        {
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static int GetRandomIndex(int startIndex, int endIndex)
        {
            return _random.Next(startIndex, endIndex);
        }

        public static double GetRandomValue()
        {
            return _random.NextDouble() * 0.001;
        }
    }
}
