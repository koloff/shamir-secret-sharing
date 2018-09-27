namespace SecretSharing.Helpers
{
    public static class Mod
    {
        public const uint MOD = 2147483647;

        public static ulong PowMod(ulong value, ulong exp, ulong mod)
        {
            if (exp == 0) return 1;
            if (exp == 1) return value % MOD;

            ulong res = PowMod(value, exp / 2, mod);
            res *= res;
            res %= MOD;

            if ((exp & 1) == 1)
            {
                res *= value;
                res %= mod;
            }

            return res;
        }
    }
}
