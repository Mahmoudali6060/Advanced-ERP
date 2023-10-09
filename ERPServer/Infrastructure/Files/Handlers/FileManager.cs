using Infrastructure.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Handlers
{
    public class FileManager : IFileManager
    {
        private ILoggerManager _logger;
        public FileManager(ILoggerManager logger)
        {
            _logger = logger;
        }

        public bool UploadFile(string fileName, FileStream file)
        {
            try
            {
                var folderName = Path.Combine("Images/Users");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public bool UploadImageBase64(string filePath, string imageBase64)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(imageBase64))
                {
                    string[] convert = imageBase64.Split(",");
                    if (convert.Length > 1)
                    {
                        File.WriteAllBytes(filePath, Convert.FromBase64String(convert[1]));
                    }
                    else
                    {
                        File.WriteAllBytes(filePath, Convert.FromBase64String(imageBase64));
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }
}
