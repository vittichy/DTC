using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dtc.IO.Extensions
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// get files by extension
        /// </summary>
        /// <param name="directoryInfo">DirectoryInfo</param>
        /// <param name="extensions">set of extensions</param>
        /// <param name="searchOption">SearchOption</param>
        /// <returns></returns>
        public static List<FileInfo> GetFilesByExt(this DirectoryInfo directoryInfo,
                                                    List<string> extensions,
                                                    SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (directoryInfo == null)
                return new List<FileInfo>();

            // extesnion list prepared for searching
            var preparedExtension = extensions.Select(p => $".{p.ToLower()}").ToList();
            var allFiles = directoryInfo.GetFiles("*.*", searchOption).ToList();
            var result = allFiles.Where(p => preparedExtension.Contains(p.Extension?.ToLower())).ToList();
            return result;
        }


        /// <summary>
        /// get files by extension
        /// </summary>
        /// <param name="directoryInfo">DirectoryInfo</param>
        /// <param name="extension">extension to search</param>
        /// <param name="searchOption">SearchOption</param>
        /// <returns></returns>
        public static List<FileInfo> GetFilesByExt(this DirectoryInfo directoryInfo,
                                                    string extension,
                                                    SearchOption searchOption = SearchOption.TopDirectoryOnly) 
            => GetFilesByExt(directoryInfo, new List<string>() { extension }, searchOption);
    }
}


