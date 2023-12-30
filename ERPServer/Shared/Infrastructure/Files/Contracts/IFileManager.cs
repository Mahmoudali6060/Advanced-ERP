using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Contracts
{
    public interface IFileManager
    {
        bool UploadFile(string fileName, FileStream file);
        bool UploadImageBase64(string filePath, string imageBase64);
    }
}
