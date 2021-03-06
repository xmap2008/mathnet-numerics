﻿// <copyright file="UserDefinedMatrixTests.cs" company="Math.NET">
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
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.UnitTests.LinearAlgebraTests.Complex
{
    using System;
    using System.Numerics;
    using Distributions;
    using LinearAlgebra.Generic;
    using Threading;

    internal class UserDefinedMatrix : Matrix<Complex>
    {
        private readonly Complex[,] _data;

        public UserDefinedMatrix(int order): base(order, order)
        {
            _data = new Complex[order, order];
        }

        public UserDefinedMatrix(int rows, int columns) : base(rows, columns)
        {
            _data = new Complex[rows, columns];
        }

        public UserDefinedMatrix(Complex[,] data) : base(data.GetLength(0), data.GetLength(1))
        {
            _data = (Complex[,])data.Clone();
        }

        public override Complex At(int row, int column)
        {
            return _data[row, column];
        }

        public override void At(int row, int column, Complex value)
        {
            _data[row, column] = value;
        }

        public override Matrix<Complex> CreateMatrix(int numberOfRows, int numberOfColumns)
        {
            return new UserDefinedMatrix(numberOfRows, numberOfColumns);
        }

        public override Vector<Complex> CreateVector(int size)
        {
            return new UserDefinedVector(size);
        }

        public static UserDefinedMatrix Identity(int order)
        {
            var m = new UserDefinedMatrix(order, order);
            for (var i = 0; i < order; i++)
            {
                m[i, i] = 1.0;
            }

            return m;
        }

        public override void Negate()
        {
            Multiply(-Complex.One);
        }

        public override Matrix<Complex> Random(int numberOfRows, int numberOfColumns, IContinuousDistribution distribution)
        {
            if (numberOfRows < 1)
            {
                throw new ArgumentException("numberOfRows");
            }

            if (numberOfColumns < 1)
            {
                throw new ArgumentException("numberOfColumns");
            }

            var matrix = CreateMatrix(numberOfRows, numberOfColumns);
            CommonParallel.For(
                0,
                ColumnCount,
                j =>
                {
                    for (var i = 0; i < matrix.RowCount; i++)
                    {
                        matrix[i, j] = new Complex(distribution.Sample(), distribution.Sample());
                    }
                });

            return matrix;
        }

        public override Matrix<Complex> Random(int numberOfRows, int numberOfColumns, IDiscreteDistribution distribution)
        {
            if (numberOfRows < 1)
            {
                throw new ArgumentException("numberOfRows");
            }

            if (numberOfColumns < 1)
            {
                throw new ArgumentException("numberOfColumns");
            }

            var matrix = CreateMatrix(numberOfRows, numberOfColumns);
            CommonParallel.For(
                0,
                ColumnCount,
                j =>
                {
                    for (var i = 0; i < matrix.RowCount; i++)
                    {
                        matrix[i, j] = new Complex(distribution.Sample(), distribution.Sample());
                    }
                });

            return matrix;
        }

        protected sealed override Complex AddT(Complex val1, Complex val2)
        {
            return val1 + val2;
        }

        protected sealed override Complex SubtractT(Complex val1, Complex val2)
        {
            return val1 - val2;
        }

        protected sealed override Complex MultiplyT(Complex val1, Complex val2)
        {
            return val1 * val2;
        }

        protected sealed override Complex DivideT(Complex val1, Complex val2)
        {
            return val1 / val2;
        }

        protected sealed override double AbsoluteT(Complex val1)
        {
            return val1.Magnitude;
        }

        public override Matrix<Complex> ConjugateTranspose()
        {
            var ret = CreateMatrix(ColumnCount, RowCount);
            for (var j = 0; j < ColumnCount; j++)
            {
                for (var i = 0; i < RowCount; i++)
                {
                    ret.At(j, i, At(i, j).Conjugate());
                }
            }

            return ret;
        }
    }

    public class UserDefinedMatrixTests : MatrixTests
    {
        protected override Matrix<Complex> CreateMatrix(int rows, int columns)
        {
            return new UserDefinedMatrix(rows, columns);
        }

        protected override Matrix<Complex> CreateMatrix(Complex[,] data)
        {
            return new UserDefinedMatrix(data);
        }

        protected override Vector<Complex> CreateVector(int size)
        {
            return new UserDefinedVector(size);
        }

        protected override Vector<Complex> CreateVector(Complex[] data)
        {
            return new UserDefinedVector(data);
        }
    }
}
