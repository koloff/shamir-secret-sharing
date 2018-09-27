namespace SecretSharing.Polynoms
{
    using Helpers;

    class PolynomSolver
    { 

        public static uint SolveWithFiniteField(ushort[] coefficients, ulong x)
        {
            ulong y = 0;

            for (int i = 0; i < coefficients.Length; i++)
            {
                y += (coefficients[i] * Mod.PowMod(x, (ulong)(coefficients.Length - i - 1), Mod.MOD)) % Mod.MOD;
                y %= Mod.MOD;
            }

            return (uint)y;
        }

        //public static int SolveWithoutFiniteField(int[] coefficients, int x)
        //{
        //    int y = 0;

        //    for (int i = 0; i < coefficients.Length; i++)
        //    {
        //        y += coefficients[i] * Convert.ToInt32(Math.Pow(x, coefficients.Length - i - 1));
        //    }

        //    return y;
        //}

        public static uint[,] GetRandomPoints(ushort[] coefficients, int count)
        {
            var pointsArr = new uint[count, 2];
            var point = new uint[2];

            for (int i = 0; i < count; i++)
            {
                pointsArr[i, 0] =  PolynomGenerator.GetRandomX(PolynomGenerator.lowerXBound, PolynomGenerator.upperXBound);
                pointsArr[i, 1] = SolveWithFiniteField(coefficients, pointsArr[i, 0]);

                for (int j = 0; j < i; j++)
                {
                    // ensure random point
                    if (pointsArr[i, 0] == pointsArr[j, 0])
                    {
                        i--;
                    }
                }
            }

            return pointsArr;
        }
    }
}
