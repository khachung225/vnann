//
// Encog(tm) Core v3.2 - .Net Version (Unit Test)
// http://www.heatonresearch.com/encog/
//
// Copyright 2008-2013 Heaton Research, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//   
// For more information on Heaton Research copyrights, licenses 
// and trademarks visit:
// http://www.heatonresearch.com/copyright
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encog.Parse.Expression.Common;
using Encog.ML.Prg;

namespace Encog.ML.Prg.Train.Crossover
{
    [TestClass]
    public class TestSubtreeCrossover
    {
        [TestMethod]
        public void TestCrossoverOperation()
        {
            RenderCommonExpression render = new RenderCommonExpression();
            EncogProgram prg = new EncogProgram("1+2");
            EncogProgram prg2 = new EncogProgram("4+5");
            ProgramNode node = prg.FindNode(2);
            prg.ReplaceNode(node, prg2.RootNode);
            Assert.AreEqual("(1+(4+5))", render.Render(prg));
        }
    }
}
