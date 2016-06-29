using System.IO;
using System.Text;

namespace Foobarify.Business.ServiceInterfaces
{
    public interface IUploadService
    {
        string ReadTextFileContent(Stream filestream, int contentLength, Encoding encoding = null);
        string ReadTextFileContent(string filepath, Encoding encoding = null);
    }
}
