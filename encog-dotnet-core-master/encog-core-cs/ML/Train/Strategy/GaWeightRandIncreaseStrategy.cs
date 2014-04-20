using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encog.ML.Train.Strategy
{
    public class GaWeightRandIncreaseStrategy : IStrategy
    {
        private static Random _myRandom= new Random();
        public const double DefaultMinImprovement = 0.0000001d;

        private IMLTrain _train;
        #region member IStrategy

        public void Init(IMLTrain train)
        {
            _train = train;
        }

        public void PreIteration()
        {

        }

        public void PostIteration()
        {

        } 
        #endregion
    }
}
