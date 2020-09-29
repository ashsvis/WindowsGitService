using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsGitService.DAL.Interfaces
{
    public interface IFileValidator
    {
        bool IsValidPath(string path);
    }
}
