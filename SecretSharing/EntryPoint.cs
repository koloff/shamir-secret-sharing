namespace SecretSharing
{
    using SecretEncryption;
    using Polynoms;
    using Helpers;
    using System;

    class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("1: Split\n2: Combine");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Players: ");
                string playerStr = Console.ReadLine();

                Console.WriteLine("Required: ");
                string requiredStr = Console.ReadLine();

                Console.WriteLine("Message:");
                string message = Console.ReadLine();

                int players = int.Parse(playerStr);
                int required = int.Parse(requiredStr);

                int polynomsCount = 3;

                var byteKey = KeyGenerator.GenerateKey(polynomsCount * 16);
                var key = KeyGenerator.GenerateDoubleBytesKey(byteKey);
                var hexKey = KeyGenerator.GetHexKey(key);

                var encrypted = Encryption.Encrypt(message, hexKey);

                Console.WriteLine("\nEncrypted message:\n{0}", encrypted);

                Console.WriteLine("\nShares: ");

                var splitted = SharesManager.SplitKey(key, players, required);
                for (int i = 0; i < splitted.Length; i++)
                {
                    Console.WriteLine(splitted[i]);
                }
                Console.WriteLine();
            }
            else if (choice == "2")
            {

                Console.WriteLine("Encrypted message: ");
                string message = Console.ReadLine();

                Console.WriteLine("Number of shares: ");
                string sharesCountStr = Console.ReadLine();
                int sharesCount = int.Parse(sharesCountStr);

                
                var shares = new string[sharesCount];

                for (int i = 0; i < sharesCount; i++)
                {
                    Console.WriteLine("Share {0}:", i + 1);
                    shares[i] = Console.ReadLine();
                }

                var generatedKey = SharesManager.CombineKey(shares);
                var hexKey = KeyGenerator.GetHexKey(generatedKey);

                var decrypted = Encryption.Decrypt(message, hexKey);
                Console.WriteLine("\nDecrypted message:");
                Console.WriteLine(decrypted);
                Console.WriteLine();
            }

            var a = Console.ReadLine();
        }
    }
}
