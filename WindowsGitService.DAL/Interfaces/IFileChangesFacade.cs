using System;
using System.Collections.Generic;

namespace WindowsGitService.DAL
{
    public interface IFileChangesFacade
    {
        void InitializePreviouslyCopiedFiles();
        void SaveCurrentFileVersionState();

        void MakeFilesCompare();

        void MakeFilesCompare(object scanningFolders);

        void MakeFilesCompare(IEnumerable<string> scanningFolders);
    }
}