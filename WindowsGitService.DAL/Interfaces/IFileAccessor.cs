using System.Collections.Generic;
using System.IO;

namespace WindowsGitService.DAL
{
    public interface IFileAccessor
    {
        IEnumerable<FileInfo> GetFiles();
        IEnumerable<FileInfo> GetFiles(string path = "C:\\Navicon\\JsonSerializer");
        IEnumerable<FileInfo> GetFiles(IEnumerable<string> paths);

        IEnumerable<FileViewInfo> GetLastUpdate(string path = @"C:\Navicon\LastUpdated.txt");
    }
}