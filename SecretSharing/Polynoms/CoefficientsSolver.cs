namespace SecretSharing.Polynoms
{
    public class CoefficientsSolver
    {
        public static ushort[] GetCoefficients(uint[,,] points)
        {
            int polynomsCount = points.GetLength(0);
            int pointsCount = points.GetLength(1);
            var coefficients = new ushort[polynomsCount];

            var currentPoints = new uint[pointsCount, 2];

            var systemMatrix = new ulong[polynomsCount, polynomsCount + 1];
            var solved = new ulong[polynomsCount];

            for (int i = 0; i < polynomsCount; i++)
            {
                for (int j = 0; j < pointsCount; j++)
                {
                    currentPoints[j, 0] = points[i, j, 0];
                    currentPoints[j, 1] = points[i, j, 1];
                }

                systemMatrix = SystemGenerator.GenerateSystemMatrixWithFiniteField(currentPoints);
                solved = LinearEquationsSystemSolver.SolveWithFiniteField(systemMatrix);

                coefficients[i] = (ushort)solved[0];
            }

            return coefficients;
        }
    }
}
