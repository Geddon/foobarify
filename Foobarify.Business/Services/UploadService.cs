using System;
using System.IO;
using System.Text;
using Foobarify.Business.Helpers;
using Foobarify.Business.ServiceInterfaces;

namespace Foobarify.Business.Services
{
    public class UploadService : IUploadService
    {

        public string ReadTextFileContent(Stream filestream, int contentLength, Encoding encoding = null)
        {
            if (filestream.Length < 1)
            {
                throw new Exception("File is empty!");
            }

            string fileContent;
            using (var reader = new System.IO.BinaryReader(filestream, Encoding.ASCII))
            {
                var binData = reader.ReadBytes(contentLength);

                fileContent = encoding != null ? encoding.GetString(binData) : Encoding.UTF8.GetString(binData);

                if (StreamHelper.IsBinary(fileContent))
                {
                    throw new Exception("File is not a recognized text file");
                }
            }

            return fileContent;
        }

        public string ReadTextFileContent(string filePath, Encoding encoding = null)
        {
            string fileContent = "";

            using (var filestream = new FileStream(filePath, FileMode.Open))
            {
                if (filestream.Length < 1)
                {
                    throw new Exception("File is empty!");
                }

                using (var reader = new System.IO.BinaryReader(filestream, Encoding.ASCII))
                {
                    var binData = reader.ReadBytes((int) filestream.Length);

                    fileContent = encoding != null ? encoding.GetString(binData) : Encoding.UTF8.GetString(binData);

                    if (StreamHelper.IsBinary(fileContent))
                    {
                        throw new Exception("File is not a recognized text file");
                    }
                }
            }

            return fileContent;
        }
    }
}