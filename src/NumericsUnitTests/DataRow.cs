// <copyright file="DataRow.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2010 Math.NET
//
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
namespace MathNet.Numerics.UnitTests
{
    using System;

    /// <summary>
    /// A row of test data
    /// </summary>
    public class DataRow
    {
        /// <summary>
        /// The test data.
        /// </summary>
        private readonly object[] _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRow"/> class.
        /// </summary>
        /// <param name="data">The test data.</param>
        public DataRow(params object[] data)
        {
            _data = new object[data.Length];
            Array.Copy(data, 0, _data, 0, data.Length);
        }

        /// <summary>
        /// A value of from the row as an int.
        /// </summary>
        /// <param name="index">The index to retrieve.</param>
        /// <returns>The requested value as an int.</returns>
        public int AsInt(int index)
        {
            return Convert.ToInt32(_data[index]);
        }

        /// <summary>
        /// A value of from the row as a long.
        /// </summary>
        /// <param name="index">The index to retrieve.</param>
        /// <returns>The requested value as a long.</returns>
        public long AsLong(int index)
        {
            return Convert.ToInt64(_data[index]);
        }

        /// <summary>
        /// A value of from the row as a double.
        /// </summary>
        /// <param name="index">The index to retrieve.</param>
        /// <returns>The requested value as a double.</returns>
        public double AsDouble(int index)
        {
            return Convert.ToDouble(_data[index]);
        }
    }
}
