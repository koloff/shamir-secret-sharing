namespace SecretSharing.Helpers
{
    using System.Text;
    using System;

    public class HexConverter
    {
        public static string NumbersArrToHexString(uint[] arr, char separator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i].ToString("X"));
                if (i != arr.Length - 1)
                {
                    sb.Append(separator);
                }
            }

            return sb.ToString();
        }

        public static uint[] HexStringToNumbersArr(string str, char separator)
        {
            var strArr = str.Split(separator);
            var res = new uint[strArr.Length];

            for (int i = 0; i < strArr.Length; i++)
            {
                res[i] = (uint)Convert.ToInt32(strArr[i], 16);
            }

            return res;
        }
    }
}
