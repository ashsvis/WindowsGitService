using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WindowsGitService.DAL
{
    public interface IFileViewConverter
    {
        IEnumerable<FileViewInfo> ConvertToFileInfo(IEnumerable<FileInfo> files);
    }
}