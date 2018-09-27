namespace SecretSharing.SecretEncryption
{
    using System;
    using Polynoms;
    using Helpers;

    public class SharesManager
    {
        public static string[] SplitKey(ushort[] key, int players, int required)
        {
            int keyLengthInBits = key.Length * 16;
            int polynomsCount = key.Length;

            var polynoms = PolynomGenerator.GeneratePolynomsMatrix(key, polynomsCount, required - 1);

            //Console.WriteLine("Generated matrix:");
            //for (int i = 0; i < polynoms.GetLength(0); i++)
            //{
            //    for (int j = 0; j < polynoms.GetLength(1); j++)
            //    {
            //        Console.Write("{0} ", polynoms[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            var playerPoints = new uint[players, polynomsCount, 2];
            var currentPolynomPoints = new uint[players, 2];
            var currentPolynom = new ushort[required];

            for (int i = 0; i < polynomsCount; i++)
            {
                currentPolynom = polynoms.GetRow(i);
                currentPolynomPoints = PolynomSolver.GetRandomPoints(currentPolynom, players);
                for (int j = 0; j < players; j++)
                {
                    playerPoints[j, i, 0] = currentPolynomPoints[j, 0];
                    playerPoints[j, i, 1] = currentPolynomPoints[j, 1];
                }
            }

            //for (int i = 0; i < playerPoints.GetLength(0); i++)
            //{
            //    for (int j = 0; j < playerPoints.GetLength(1); j++)
            //    {
            //        Console.Write("({0}, {1})  ", playerPoints[i, j, 0], playerPoints[i, j, 1]);
            //    }
            //    Console.WriteLine();
            //}

            var geneneratedHexes = new string[players];

            var currentPlayerPoints = new uint[polynomsCount * 2];
            var counter = 0;
            for (int i = 0; i < playerPoints.GetLength(0); i++)
            {
                for (int j = 0; j < playerPoints.GetLength(1); j++)
                {
                    currentPlayerPoints[counter] = playerPoints[i, j, 0];
                    counter++;
                    currentPlayerPoints[counter] = playerPoints[i, j, 1];
                    counter++;
                }
                counter = 0;
                geneneratedHexes[i] = HexConverter.NumbersArrToHexString(currentPlayerPoints, '-');
                //Console.WriteLine(geneneratedHexes[i]);
                //Console.WriteLine();
            }

            return geneneratedHexes;
        }

        public static ushort[] CombineKey(string[] playerPoints)
        {
            var polynomsCount = (playerPoints[0].Split('-').Length - 1) / 2 + 1;
            var required = playerPoints.Length;

            var convertedPoints = new uint[polynomsCount, required, 2];

            var testingPlayersPoints = new uint[polynomsCount];
            var counter = 0;

            var currentPlayerPoints = new uint[polynomsCount];

            for (int i = 0; i < required; i++)
            {
                currentPlayerPoints = HexConverter.HexStringToNumbersArr(playerPoints[i], '-');

                for (int j = 0; j < polynomsCount; j++)
                {
                    convertedPoints[j, i, 0] = currentPlayerPoints[counter];
                    counter++;
                    convertedPoints[j, i, 1] = currentPlayerPoints[counter];
                    counter++;
                }
                counter = 0;
            }

            //for (int i = 0; i < convertedPoints.GetLength(0); i++)
            //{
            //    for (int j = 0; j < convertedPoints.GetLength(1); j++)
            //    {
            //        Console.Write("({0}, {1})", convertedPoints[i, j, 0], convertedPoints[i, j, 1]);
            //    }
            //    Console.WriteLine();
            //}

            var testingSolved = CoefficientsSolver.GetCoefficients(convertedPoints);

            return testingSolved;
        }
    }
}
