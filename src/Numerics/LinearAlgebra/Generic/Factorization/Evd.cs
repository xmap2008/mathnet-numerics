﻿// <copyright file="Evd.cs" company="Math.NET">
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

namespace MathNet.Numerics.LinearAlgebra.Generic.Factorization
{
    using System;
    using System.Linq;
    using System.Numerics;
    using Generic;
    using Numerics;

    /// <summary>
    /// Eigenvalues and eigenvectors of a real matrix.
    /// </summary>
    /// <remarks>
    /// If A is symmetric, then A = V*D*V' where the eigenvalue matrix D is
    /// diagonal and the eigenvector matrix V is orthogonal.
    /// I.e. A = V*D*V' and V*VT=I.
    /// If A is not symmetric, then the eigenvalue matrix D is block diagonal
    /// with the real eigenvalues in 1-by-1 blocks and any complex eigenvalues,
    /// lambda + i*mu, in 2-by-2 blocks, [lambda, mu; -mu, lambda].  The
    /// columns of V represent the eigenvectors in the sense that A*V = V*D,
    /// i.e. A.Multiply(V) equals V.Multiply(D).  The matrix V may be badly
    /// conditioned, or even singular, so the validity of the equation
    /// A = V*D*Inverse(V) depends upon V.cond().
    /// </remarks>
    /// <typeparam name="T">Supported data types are double, single, <see cref="Complex"/>, and <see cref="Complex32"/>.</typeparam>
    public abstract class Evd<T> : ISolver<T>
    where T : struct, IEquatable<T>, IFormattable
    {
        /// <summary>
        /// Gets or sets a value indicating whether matrix is symmetric or not
        /// </summary>
        public bool IsSymmetric
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the eigen values (λ) of matrix in ascending value.
        /// </summary>
        protected Vector<Complex> VectorEv
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets eigenvectors.
        /// </summary>
        protected Matrix<T> MatrixEv
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the block diagonal eigenvalue matrix.
        /// </summary>
        protected Matrix<T> MatrixD
        {
            get;
            set;
        }

        /// <summary>
        /// Internal method which routes the call to perform the singular value decomposition to the appropriate class.
        /// </summary>
        /// <param name="matrix">The matrix to factor.</param>
        /// <returns>An EVD object.</returns>
        internal static Evd<T> Create(Matrix<T> matrix)
        {
            if (typeof(T) == typeof(double))
            {
                return new LinearAlgebra.Double.Factorization.UserEvd(matrix as Matrix<double>) as Evd<T>;
            }

            // if (typeof(T) == typeof(float))
            // {
            //    return new LinearAlgebra.Single.Factorization.UserEvd(matrix as Matrix<float>, computeVectors) as Evd<T>;
            // }

            // if (typeof(T) == typeof(Complex))
            // {
            //    return new LinearAlgebra.Complex.Factorization.UserEvd(matrix as Matrix<Complex>, computeVectors) as Evd<T>;
            // }

            // if (typeof(T) == typeof(Complex32))
            // {
            //    return new LinearAlgebra.Complex32.Factorization.UserEvd(matrix as Matrix<Complex32>, computeVectors) as Evd<T>;
            // }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the absolute value of determinant of the square matrix for which the EVD was computed.
        /// </summary>
        public virtual double Determinant
        {
            get
            {
                var det = Complex.One;
                for (var i = 0; i < VectorEv.Count; i++)
                {
                    det *= VectorEv[i];
                    if (VectorEv[i].AlmostEqual(Complex.Zero))
                    {
                        return 0;
                    }
                }

                return det.Magnitude;
            }
        }

        /// <summary>
        /// Gets the effective numerical matrix rank.
        /// </summary>
        /// <value>The number of non-negligible singular values.</value>
        public virtual int Rank
        {
            get
            {
                return VectorEv.Count(t => !t.AlmostEqual(Complex.Zero));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is full rank or not.
        /// </summary>
        /// <value><c>true</c> if the matrix is full rank; otherwise <c>false</c>.</value>
        public virtual bool IsFullRank
        {
            get
            {
                for (var i = 0; i < VectorEv.Count; i++)
                {
                    if (VectorEv[i].AlmostEqual(Complex.Zero))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>Returns the eigen values as a <see cref="Vector{T}"/>.</summary>
        /// <returns>The eigen values.</returns>
        public Vector<Complex> EValues()
        {
            return VectorEv.Clone();
        }

        /// <summary>Returns the right eigen vectors as a <see cref="Matrix{T}"/>.</summary>
        /// <returns>The eigen vectors. </returns>
        public Matrix<T> EVectors()
        {
            return MatrixEv.Clone();
        }

        /// <summary>Returns the block diagonal eigenvalue matrix <see cref="Matrix{T}"/>.</summary>
        /// <returns>The block diagonal eigenvalue matrix <see cref="Matrix{T}"/>.</returns>        
        public Matrix<T> D()
        {
            return MatrixD.Clone();
        }

        /// <summary>
        /// Solves a system of linear equations, <b>AX = B</b>, with A SVD factorized.
        /// </summary>
        /// <param name="input">The right hand side <see cref="Matrix{T}"/>, <b>B</b>.</param>
        /// <returns>The left hand side <see cref="Matrix{T}"/>, <b>X</b>.</returns>
        public virtual Matrix<T> Solve(Matrix<T> input)
        {
            // Check for proper arguments.
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var result = MatrixEv.CreateMatrix(MatrixEv.ColumnCount, input.ColumnCount);
            Solve(input, result);
            return result;
        }

        /// <summary>
        /// Solves a system of linear equations, <b>AX = B</b>, with A SVD factorized.
        /// </summary>
        /// <param name="input">The right hand side <see cref="Matrix{T}"/>, <b>B</b>.</param>
        /// <param name="result">The left hand side <see cref="Matrix{T}"/>, <b>X</b>.</param>
        public abstract void Solve(Matrix<T> input, Matrix<T> result);

        /// <summary>
        /// Solves a system of linear equations, <b>Ax = b</b>, with A SVD factorized.
        /// </summary>
        /// <param name="input">The right hand side vector, <b>b</b>.</param>
        /// <returns>The left hand side <see cref="Vector{T}"/>, <b>x</b>.</returns>
        public virtual Vector<T> Solve(Vector<T> input)
        {
            // Check for proper arguments.
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var x = MatrixEv.CreateVector(MatrixEv.ColumnCount);
            Solve(input, x);
            return x;
        }

        /// <summary>
        /// Solves a system of linear equations, <b>Ax = b</b>, with A SVD factorized.
        /// </summary>
        /// <param name="input">The right hand side vector, <b>b</b>.</param>
        /// <param name="result">The left hand side <see cref="Matrix{T}"/>, <b>x</b>.</param>
        public abstract void Solve(Vector<T> input, Vector<T> result);

        #region Simple arithmetic of type T
        /// <summary>
        /// Multiply two values T*T
        /// </summary>
        /// <param name="val1">Left operand value</param>
        /// <param name="val2">Right operand value</param>
        /// <returns>Result of multiplication</returns>
        protected abstract T MultiplyT(T val1, T val2);

        /// <summary>
        /// Gets value of type T equal to one
        /// </summary>
        /// <returns>One value</returns>
        private static T OneValueT
        {
            get
            {
                if (typeof(T) == typeof(Complex))
                {
                    object one = Complex.One;
                    return (T)one;
                }

                if (typeof(T) == typeof(Complex32))
                {
                    object one = Complex32.One;
                    return (T)one;
                }

                if (typeof(T) == typeof(double))
                {
                    object one = 1.0d;
                    return (T)one;
                }

                if (typeof(T) == typeof(float))
                {
                    object one = 1.0f;
                    return (T)one;
                }

                throw new NotSupportedException();
            }
        }

        #endregion
    }
}
