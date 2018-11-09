using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dtc.IO.Extensions
{
    public static class DirectoryInfoExtensions
    {

        ///// <summary>
        ///// TODO vyhodit  
        ///// </summary>
        ///// <param name="directoryInfo"></param>
        ///// <param name="extensions"></param>
        ///// <param name="stringComparison"></param>
        ///// <returns></returns>
        //public static IEnumerable<FileInfo> GetFilesWithExtension(this DirectoryInfo directoryInfo,
        //                                                          string[] extensions,
        //                                                          StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        //{
        //    bool ExtensionOk(FileInfo fileInfo)
        //    {
        //        var ok = extensions.Any(p => string.Equals(fileInfo?.Extension, p, stringComparison));
        //        return ok;
        //    }

        //    var result = directoryInfo?
        //                    .GetFiles()
        //                        .Where(p => ExtensionOk(p));
        //    return result;
        //}


        public static List<FileInfo> GetFilesByExt(this DirectoryInfo directoryInfo,
                                                    List<string> extensions,
                                                    SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (directoryInfo != null)
            {
                // extesnion list prepared for searching
                var preparedExtension = extensions.Select(p => $".{p.ToLower()}").ToList();

                var allFiles = directoryInfo.GetFiles("*.*", searchOption).ToList();
                var result = allFiles.Where(p => preparedExtension.Contains(p.Extension?.ToLower())).ToList();
                return result;
            }

            return new List<FileInfo>();
        }


        public static List<FileInfo> GetFilesByExt(this DirectoryInfo directoryInfo,
                                                    string extension,
                                                    SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return GetFilesByExt(directoryInfo, new List<string>() { extension }, searchOption);
        }



        ///// <summary>
        ///// dohleda v adresari soubory majici jednu z extensions
        ///// </summary>
        ///// <param name="directoryInfo">zdrojova cesta</param>
        ///// <param name="extensionList">seznam pripon souboru ... napr "jpg;jpeg;gif"</param>
        ///// <param name="extensionListSeparator">oddelovac v extension listu</param>
        ///// <param name="searchOption">SearchOption - hledani jen v adresari vs hledani i ve vsech podadresarich</param>
        ///// <returns>Seznam dohledanych souboru</returns>
        //public static List<FileInfo> GetFilesByExt(this DirectoryInfo directoryInfo, 
        //                                           string extensionList, 
        //                                           char extensionListSeparator, 
        //                                           SearchOption searchOption = SearchOption.TopDirectoryOnly)
        //{
        //    if (directoryInfo != null)
        //    {
        //        var extensionSet = extensionList.Split(new char[] { extensionListSeparator })
        //                                            .Select(p => "." + p.ToLower())
        //                                                .ToList();

        //        var allFiles = directoryInfo.GetFiles("*.*", searchOption).ToList();
        //        var result = allFiles.Where(p => extensionSet.Contains(p?.Extension?.ToLower())).ToList();
        //        return result;
        //    }
        //    return new List<FileInfo>();
        //}



        //public static List<FileInfo> GetFilesByExtRecursiveAllDirectories(this DirectoryInfo directoryInfo, 
        //                                                                  string extensionList, 
        //                                                                  char extensionListSeparator)
        //{
        //    return GetFilesByExt(directoryInfo, extensionList, extensionListSeparator, SearchOption.AllDirectories);
        //}



    }
}


