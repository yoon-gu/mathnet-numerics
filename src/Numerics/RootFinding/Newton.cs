// <copyright file="Newton.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// 
// Copyright (c) 2009-2016 Math.NET
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

using System;
using MathNet.Numerics.Properties;

namespace MathNet.Numerics.RootFinding
{
    public static class Newton
    {
        public static double FindRoot(Func<double, double> f, Func<double, double> df, 
                                        double lowerBound, double upperBound, double accuracy = 1e-8, 
                                        int maxIterations = 100)
        {
            double root;
            if (TryFindRoot(f, df, 0.5 * (lowerBound + upperBound), lowerBound, upperBound, accuracy, maxIterations, out root))
            {
                return root;
            }

            throw new NonConvergenceException(Resources.RootFindingFailed);
        }

        public static double FindRootNearGuess(Func<double, double> f, Func<double, double> df, 
                                                double initialGuess, double lowerBound = double.MinValue, 
                                                double upperBound = double.MaxValue, double accuracy = 1e-8, 
                                                int maxIterations = 100)
        {
            double root;
            if (TryFindRoot(f, df, initialGuess, lowerBound, upperBound, accuracy, maxIterations, out root))
            {
                return root;
            }

            throw new NonConvergenceException(Resources.RootFindingFailed);
        }

        public static bool TryFindRoot(Func<double, double> f, Func<double, double> df, 
                                        double initialGuess, double lowerBound, double upperBound, double accuracy, 
                                        int maxIterations, out double root)
        {
            root = initialGuess;
            for (int i = 0; i < maxIterations && root >= lowerBound && root <= upperBound; i++)
            {
                // Evaluation
                double fx = f(root);
                double dfx = df(root);

                // Netwon-Raphson step
                double step = fx/dfx;
                root -= step;

                if (Math.Abs(step) < accuracy && Math.Abs(fx) < accuracy)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
