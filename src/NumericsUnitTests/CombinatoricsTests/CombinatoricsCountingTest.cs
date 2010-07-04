// <copyright file="CombinatoricsCountingTest.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
// Copyright (c) 2009-2010 Math.NET
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.UnitTests.CombinatoricsTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for <see cref="Combinatorics"/> counting.
    /// </summary>
    [TestClass]
    public class CombinatoricsCountingTest
    {
        /// <summary>
        /// Determines whether we can count variations.
        /// </summary>
        [TestMethod]
        public void CanCountVariations()
        {
            var data = new[]
                       {
                           new DataRow(0, 0, 1), 
                           new DataRow(1, 0, 1), 
                           new DataRow(10, 0, 1), 
                           new DataRow(10, 2, 90), 
                           new DataRow(10, 4, 5040), 
                           new DataRow(10, 6, 151200), 
                           new DataRow(10, 9, 3628800), 
                           new DataRow(10, 10, 3628800)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);
                var expected = dataRow.AsDouble(2);

                Assert.AreEqual(
                    expected, 
                    Combinatorics.Variations(n, k), 
                    String.Format("Count the number of variations without repetition: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether out the of range variations return zero.
        /// </summary>
        [TestMethod]
        public void OutOfRangeVariationsMustCountToZero()
        {
            var data = new[]
                       {
                           new DataRow(0, 1), 
                           new DataRow(10, 11), 
                           new DataRow(0, -1), 
                           new DataRow(1, -1), 
                           new DataRow(-1, 0), 
                           new DataRow(-1, 1)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);

                Assert.AreEqual(
                    0, 
                    Combinatorics.Variations(n, k), 
                    String.Format("The number of variations without repetition but out of the range must be 0: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether we can count variations with repetition.
        /// </summary>
        [TestMethod]
        public void CanCountVariationsWithRepetition()
        {
            var data = new[]
                       {
                           new DataRow(0, 0, 1), 
                           new DataRow(1, 0, 1), 
                           new DataRow(10, 0, 1), 
                           new DataRow(10, 2, 100), 
                           new DataRow(10, 4, 10000), 
                           new DataRow(10, 6, 1000000), 
                           new DataRow(10, 9, 1000000000), 
                           new DataRow(10, 10, 10000000000), 
                           new DataRow(10, 11, 100000000000)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);
                var expected = dataRow.AsDouble(2);

                Assert.AreEqual(
                    expected, 
                    Combinatorics.VariationsWithRepetition(n, k), 
                    String.Format("Count the number of variations with repetition: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether out of range variations with repetition return zero.
        /// </summary>
        [TestMethod]
        public void OutOfRangeVariationsWithRepetitionMustCountToZero()
        {
            var data = new[]
                       {
                           new DataRow(0, 1), 
                           new DataRow(0, -1), 
                           new DataRow(1, -1), 
                           new DataRow(-1, 0), 
                           new DataRow(-1, 1)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);

                Assert.AreEqual(
                    0, 
                    Combinatorics.VariationsWithRepetition(n, k), 
                    String.Format("The number of variations with repetition but out of the range must be 0: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether we can count combinations.
        /// </summary>
        [TestMethod]
        public void CanCountCombinations()
        {
            var data = new[]
                       {
                           new DataRow(0, 0, 1), 
                           new DataRow(1, 0, 1), 
                           new DataRow(10, 0, 1), 
                           new DataRow(10, 2, 45), 
                           new DataRow(10, 4, 210), 
                           new DataRow(10, 6, 210), 
                           new DataRow(10, 9, 10), 
                           new DataRow(10, 10, 1)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);
                var expected = dataRow.AsDouble(2);

                Assert.AreEqual(
                    expected, 
                    Combinatorics.Combinations(n, k), 
                    String.Format("Count the number of combinations without repetition: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether out of range combinations return zero.
        /// </summary>
        [TestMethod]
        public void OutOfRangeCombinationsMustCountToZero()
        {
            var data = new[]
                       {
                           new DataRow(0, 1), 
                           new DataRow(10, 11), 
                           new DataRow(0, -1), 
                           new DataRow(1, -1), 
                           new DataRow(-1, 0), 
                           new DataRow(-1, 1)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);

                Assert.AreEqual(
                    0, 
                    Combinatorics.Combinations(n, k), 
                    String.Format("The number of combinations without repetition but out of the range must be 0: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether we can count combinations with repetition.
        /// </summary>
        [TestMethod]
        public void CanCountCombinationsWithRepetition()
        {
            var data = new[]
                       {
                           new DataRow(0, 0, 1), 
                           new DataRow(1, 0, 1), 
                           new DataRow(10, 0, 1), 
                           new DataRow(10, 2, 55), 
                           new DataRow(10, 4, 715), 
                           new DataRow(10, 6, 5005), 
                           new DataRow(10, 9, 48620), 
                           new DataRow(10, 10, 92378), 
                           new DataRow(10, 11, 167960)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);
                var expected = dataRow.AsDouble(2);

                Assert.AreEqual(
                    expected, 
                    Combinatorics.CombinationsWithRepetition(n, k), 
                    String.Format("Count the number of combinations with repetition: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether out the of range combinations with repetition return zero.
        /// </summary>
        [TestMethod]
        public void OutOfRangeCombinationsWithRepetitionMustCountToZero()
        {
            var data = new[]
                       {
                           new DataRow(0, 1), 
                           new DataRow(0, -1), 
                           new DataRow(1, -1), 
                           new DataRow(-1, 0), 
                           new DataRow(-1, 1)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var k = dataRow.AsInt(1);

                Assert.AreEqual(
                    0, 
                    Combinatorics.CombinationsWithRepetition(n, k), 
                    String.Format("The number of combinations with repetition but out of the range must be 0: n={0}, k={1}", n, k));
            }
        }

        /// <summary>
        /// Determines whether we can count permutations.
        /// </summary>
        [TestMethod]
        public void CanCountPermutations()
        {
            var data = new[]
                       {
                           new DataRow(0, 1), 
                           new DataRow(1, 1), 
                           new DataRow(2, 2), 
                           new DataRow(8, 40320)
                       };
            foreach (var dataRow in data)
            {
                var n = dataRow.AsInt(0);
                var expected = dataRow.AsDouble(1);
                Assert.AreEqual(
                    expected, 
                    Combinatorics.Permutations(n), 
                    String.Format("Count the number of permutations: n={0}", n));
            }
        }
    }
}
