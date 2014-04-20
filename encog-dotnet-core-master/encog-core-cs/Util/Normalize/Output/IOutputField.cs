//
// Encog(tm) Core v3.2 - .Net Version
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
//
namespace Encog.Util.Normalize.Output
{
    /// <summary>
    /// An output field, this represents the actual output from the 
    /// normalization.  Output from the normalization class is usually
    /// input to a neural network.
    ///
    /// An output field may contain several subfields that will be
    /// generated.  Call getSubfieldCount to determine how many fields
    /// will be generated.  A simple field will return 1, indicating that 
    /// this is a single field.
    /// </summary>
    public interface IOutputField
    {
        /// <summary>
        /// The numebr of fields that will actually be generated by 
        /// this field. For a simple field, this value is 1.
        /// </summary>
        int SubfieldCount { get; }

        /// <summary>
        /// Is this field part of the ideal data uses to train the
        /// neural network.
        /// </summary>
        bool Ideal { get; set; }

        /// <summary>
        /// Calculate the value for this field.  Specify subfield of zero
        /// if this is a simple field.
        /// </summary>
        /// <param name="subfield"> The subfield index.</param>
        /// <returns>The calculated value for this field.</returns>
        double Calculate(int subfield);

        /// <summary>
        /// Init this field for a new row.
        /// </summary>
        void RowInit();
    }
}
