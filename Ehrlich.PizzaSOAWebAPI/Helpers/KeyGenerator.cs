using System.Security.Cryptography;

namespace Ehrlich.PizzaSOA.WebAPI.Helpers;

public static class KeyGenerator
{
    public static string GenerateKey(int lengthInBytes)
    {
        using var rng = new RNGCryptoServiceProvider();
        var key = new byte[lengthInBytes];
        rng.GetBytes(key);
        return Convert.ToBase64String(key);
    }
}
