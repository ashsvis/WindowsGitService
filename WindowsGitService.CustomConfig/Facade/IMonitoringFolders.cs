using System.Collections.Generic;

namespace WindowsGitService.CustomConfig
{
    public interface IMonitoringFolders
    {
        List<string> GetFoldersPath();

        List<FolderElement> GetFolderElements();
    }
}
