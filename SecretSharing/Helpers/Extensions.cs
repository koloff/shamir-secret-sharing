namespace SecretSharing.Helpers
{
    using System;

    public static class Extensions
    { 
        public static T[] GetRow<T>(this T[,] matrix, int index)
        {
            int matrixWidth = matrix.GetLength(1);

            T[] result = new T[matrixWidth];

            for (int i = 0; i < matrixWidth; i++)
            {
                result[i] = matrix[index, i];
            }

            return result;
        }

        public static void SetRow<T>(this T[,] matrix, int index, T[] row)
        {
            if (row.Length > matrix.Length)
            {
                throw new ArgumentException("The row size must not be biger than the destination row.");
            }

            for (int i = 0; i < row.Length; i++)
            {
                matrix[index, i] = row[i];
            }
        }

        public static void SwapRows<T>(this T[,] matrix, int firstIndex, int secondIndex)
        {
            var tempRow = matrix.GetRow(firstIndex);
            matrix.SetRow(firstIndex, matrix.GetRow(secondIndex));
            matrix.SetRow(secondIndex, tempRow);
        }

        public static void Divide(this double[] row, int index, double value)
        {
            if (value == 0)
            {
                throw new System.ArgumentException("You can't devide by 0.");
            }
            for (int i = index; i < row.Length; i++)
            {
                row[i] /= value;
            }
        }
    }
}
