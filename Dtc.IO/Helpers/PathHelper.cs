using Dtc.Common.Extensions;
using System;
using System.IO;

namespace Dtc.IO.Helpers
{
    public static class PathHelper
    {
        public static string GetRelativePath(string filespec, string rootFolder)
        {
            var pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!rootFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                rootFolder += Path.DirectorySeparatorChar;
            }
            var folderUri = new Uri(rootFolder);
            var result = Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
            return result;
        }



   


        public static string GetRelativePath2(string fileName, string rootFolder)
        {
            fileName = fileName.RemoveEndText(Path.DirectorySeparatorChar);
            rootFolder = rootFolder.RemoveEndText(Path.DirectorySeparatorChar);

            var relative = fileName.Substring(rootFolder.Length).RemoveStartText(Path.DirectorySeparatorChar);
            return relative;
        }
    }

}

