using System;
using System.IO;
using Foobarify.Business.Extensions;

namespace Foobarify.Business.Models
{
    public class UploadTextFile
    {
        public string FileName { get; set; }
        
        private string _originalContent;
        public string OriginalContent
        {
            get
            {
                return _originalContent;
            }
            set
            {
                _originalContent = value;
                _fooBarifiedContent = "";
            }
        }

        private string _fooBarifiedContent;
        public string FooBarifiedContent
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_fooBarifiedContent))
                {
                    _fooBarifiedContent = OriginalContent.FooBarify();
                }

                return _fooBarifiedContent;
            }
        }

        public string FileExtension
        {
            get { return Path.GetExtension(FileName); }
        }

        public UploadTextFile(string fileName, string fileContent)
        {
            FileName = fileName;
            OriginalContent = fileContent;
        }
    }
}