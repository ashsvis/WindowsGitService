using System.Collections.Generic;

namespace WindowsGitService.DAL
{
    public interface IChangedFileSaver
    {
        void CopyFile(FileViewInfo file, string targetDirectoryPath = "C:\\Navicon\\Temp");

        void SaveFileСhanges(IEnumerable<FileViewInfo> files, string targetDirectoryPath = @"C:\Navicon\Temp");

        void SaveLastUpdate(List<FileViewInfo> lastVersion, string path = @"C:\Navicon\LastUpdated.txt");
    }
}