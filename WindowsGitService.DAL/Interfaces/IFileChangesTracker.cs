using System.Collections.Generic;

namespace WindowsGitService.DAL
{
    public interface IFileChangesTracker
    {
        List<FileViewInfo> lastVersion { get; set; }

        IEnumerable<FileViewInfo> GetChangedFiles(IEnumerable<FileViewInfo> oldFiles, IEnumerable<FileViewInfo> newFiles);

        List<FileViewInfo> UpdateLastVersion(List<FileViewInfo> changedFiles);
    }
}