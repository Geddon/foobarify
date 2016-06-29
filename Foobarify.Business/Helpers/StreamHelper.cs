using System.IO;

namespace Foobarify.Business.Helpers
{
    public static class StreamHelper
    {
        public static bool IsBinary(string fileContents)
        {
            using (var stream = new StreamReader(GenerateStreamFromString(fileContents)))
            {
                int ch;
                while ((ch = stream.Read()) != -1)
                {
                    if (IsControlChar(ch))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static bool IsControlChar(int ch)
        {
            return (ch > Chars.NUL && ch < Chars.BS)
                || (ch > Chars.CR && ch < Chars.SUB);
        }

        private class Chars
        {
            public static char NUL = (char)0; // Null char
            public static char BS = (char)8; // Back Space
            public static char CR = (char)13; // Carriage Return
            public static char SUB = (char)26; // Substitute
        }
    }
}
