using System.Security.Cryptography;
using System.Text;

namespace TechTask.AA.Core.Helpers;

public static class HashHelper
{
    public static string ComputeHash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);

        var hashedBytes = SHA256.Create().ComputeHash(inputBytes);

        return BitConverter.ToString(hashedBytes);
    }
}
