using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Foobarify.Business.Extensions;
using Foobarify.Business.Helpers;
using Foobarify.Business.ServiceInterfaces;
using Foobarify.Business.Services;

namespace Foobarify.Api.Controllers
{
    public class FilesController : ApiControllerBase
    {
        public HttpResponseMessage Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            try
            {
                var streamProvider = new MultipartFormDataStreamProvider(IOHelper.GetTempDirectory());
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                
                var fileName = streamProvider.FileData.First().LocalFileName;
                 
                var fileContent = _uploadService.ReadTextFileContent(fileName);

                //TODO Persist file in DB

                File.Delete(fileName);
                
                return await CreateResponseMessageAsync(fileContent.FooBarify(@"<span style='color:red;font-weight:bold'>", @"</span>"));
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }

    public class ApiControllerBase : ApiController
    {
        protected readonly IUploadService _uploadService;

        public ApiControllerBase()
        {
            _uploadService = new UploadService();
        }

        public HttpResponseMessage CreateResponseMessage(string content)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, content);
            
            return response;
        }

        public async Task<HttpResponseMessage> CreateResponseMessageAsync(string content)
        {
            return
                await
                    Task<HttpResponseMessage>.Factory.StartNew(
                        () => Request.CreateResponse(HttpStatusCode.OK, content));
            
        }
    }
}
