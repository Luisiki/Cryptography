using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using Krypto.Operations.Crypto_Operations;
using Krypto.Operations.File_Operations;
using Krypto.Operations.Mode_testing;
using Microsoft.VisualBasic.CompilerServices;


namespace Krypto.Krypto // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private FactorizationSelection[] Selections = new[]
        {
            FactorizationSelection.Brute, FactorizationSelection.BabyStepGiantStep,
            FactorizationSelection.LenstraEllipticCurveFactorization, FactorizationSelection.PollarRho
        };

        static void Main(string[] args)
        {
            Testing.FactorTesting(2, 100, FactorizationSelection.BabyStepGiantStep, "testingBSG");

        }
    }
}