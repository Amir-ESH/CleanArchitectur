using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace CleanArchitecture.Application.Common.Helper;

public static class PasswordHelper
{
    public static string EncodePassword(string password) //Encrypt using MD5
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = md5.ComputeHash(inputBytes);
        var stringBuilder = new StringBuilder(hashBytes.Length * 2);
        foreach (var b in hashBytes)
        {
            stringBuilder.Append(b.ToString("X2"));
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// String Password Hash Methods
    /// </summary>
    /// <param name="password">string password</param>
    /// <param name="associatedData">Any extra associated Data to use while hashing the password</param>
    /// <param name="knownSecret">An optional secret to use while hashing the password</param>
    /// <returns>Return a base64 string</returns>
    public static string HashPassword(string password, string? associatedData = null, string? knownSecret = null)
    {
        associatedData ??= "Amir Eslamzadeh";

        knownSecret ??= "09199247713";

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = Encoding.UTF8.GetBytes(EncodePassword(password)),
            DegreeOfParallelism = 1, // The number of cores in the processor
            Iterations = 1,
            MemorySize = 128 * 128, // The size of the memory used by the algorithm
            AssociatedData = Encoding.UTF8.GetBytes(associatedData),
            KnownSecret = Encoding.UTF8.GetBytes(knownSecret)
        };

        var hash = argon2.GetBytes(180); // Returns a 180 byte long binary string

        return Convert.ToBase64String(hash); // Return string as base64
    }
}