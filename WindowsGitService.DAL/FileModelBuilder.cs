using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsGitService.DAL
{
    public class FileViewModelBuilder : IFileViewConverter
    {
        public IEnumerable<FileViewInfo> ConvertToFileInfo(IEnumerable<FileInfo> files)
        {
            List<FileViewInfo> fileViewInfos = new List<FileViewInfo>();

            using (var md5 = MD5.Create())
            {
                foreach (var file in files)
                {
                    FileViewInfo fileView = ConvertToFileInfo(file, md5);

                    fileViewInfos.Add(fileView);
                }
            }

            return fileViewInfos;
        }

        private FileViewInfo ConvertToFileInfo(FileInfo file, MD5 md5)
        {
            FileViewInfo fileView = new FileViewInfo();

            fileView.Name = file.Name.Split('.').First();
            fileView.FileName = file.Name;
            fileView.Format = file.Extension.Remove(0, 1);
            fileView.Version = 1;
            fileView.Created = file.CreationTime;
            fileView.Path = file.DirectoryName;
            fileView.FullPath = file.FullName;
            fileView.LastChange = file.LastWriteTime;
            fileView.Hash = BitConverter.ToInt32(GetFileHash(file, md5), 0);

            return fileView;
        }

        private byte[] GetFileHash(FileInfo file, MD5 md5)
        {
            byte[] hash; 

            using (FileStream stream = file.OpenRead())
            {
                hash = md5.ComputeHash(stream);
            }

            return hash;
        }
    }
}
