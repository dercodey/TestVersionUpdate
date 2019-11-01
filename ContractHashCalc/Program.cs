using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ContractHashCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(args[0]).AsEnumerable();
            lines = lines.Select(RemoveComments).Where(line => !string.IsNullOrEmpty(line)).ToList();
            lines = RemoveVersions(lines);
            var allLines = string.Join("\n", lines);
            var hash = ComputeSha256Hash(allLines);
            Console.WriteLine("Hash of assembly is {0}", hash);
        }

        static string RemoveComments(string line)
        {
            var indexComment = line.IndexOf("//");
            if (indexComment >= 0)
            {
                return line.Substring(0, indexComment).TrimEnd();
            }
            return line;
        }

        static IEnumerable<string> RemoveVersions(IEnumerable<string> fromLines)
        {
            foreach (var line in fromLines)
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith(".custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string)")
                    || trimmed.StartsWith(".ver")
                    || trimmed.StartsWith("// MVID: {")
                    || trimmed.StartsWith("// Image base: 0x"))
                {
                    continue;
                }
                yield return line;
            }
        }

        static string ComputeSha256Hash(string allLines)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(allLines));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
