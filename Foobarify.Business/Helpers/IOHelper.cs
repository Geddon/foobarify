using System.IO;

namespace Foobarify.Business.Helpers
{
    public static class IOHelper
    {
        public static string GetTempDirectory()
        {
            var tempFolder = "C:\\Temp"; //TODO add to .config

            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            return tempFolder;
        }
    }
}
