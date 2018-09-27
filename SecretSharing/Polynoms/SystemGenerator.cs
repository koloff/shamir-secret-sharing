namespace SecretSharing.Polynoms
{
    using Helpers;

    class SystemGenerator
    {
        public static ulong[,] GenerateSystemMatrixWithFiniteField(uint[,] points)
        {
            int pointsCount = points.GetLength(0);

            var matrix = new ulong[pointsCount, pointsCount + 1];

            for (int i = 0; i < pointsCount; i++)
            {
                var currentX = (ulong)points[i, 0];
                for (int j = 0; j < pointsCount; j++)
                {
                    matrix[i, j] = Mod.PowMod(currentX, (ulong)(pointsCount - j - 1), Mod.MOD);
                }
                matrix[i, pointsCount] = points[i, 1];
            }

            return matrix;
        }

        //public static double[,] GenerateSystemMatrixWithoutFiniteField(int[,] points)
        //{
        //    int pointsCount = points.GetLength(0);

        //    var matrix = new double[pointsCount, pointsCount + 1];

        //    for (int i = 0; i < pointsCount; i++)
        //    {
        //        var currentX = points[i, 0];
        //        for (int j = 0; j < pointsCount; j++)
        //        {
        //            matrix[i, j] = (double)Math.Pow(currentX, pointsCount - j - 1);
        //        }
        //        matrix[i, pointsCount] = points[i, 1];
        //    }

        //    return matrix;
        //}
    }
}
