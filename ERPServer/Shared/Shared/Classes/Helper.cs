using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shared.Interfaces;
using Shared.Entities.Shared;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;

namespace Shared.Classes
{
    public static class Helper
    {
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
        }
        public static void DetachLocal<T>(this DbContext context, T entity) where T : class, IIDInterface
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
        }
        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "E:\\Logs\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }

    }
}


