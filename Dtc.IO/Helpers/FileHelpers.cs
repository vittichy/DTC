using Dtc.Common.Extensions;
using System;
using System.IO;

namespace Dtc.IO.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// nacteni casti souboru do byte[]
        /// </summary>
        public static byte[] ReadBytes(string filename, int length)
        {
            if (File.Exists(filename))
            {
                var fileLen = new FileInfo(filename).Length;
                var resultLen = Math.Min(fileLen, length).ToInt();
                var result = new byte[resultLen];
                using (var reader = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    reader.Seek(resultLen, SeekOrigin.Begin);
                    reader.Read(result, 0, resultLen);
                }
                return result;
            }
            return null;
        }

    }
}
