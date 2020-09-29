using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGitService.DAL
{
    public class FileChangesTracker : IFileChangesTracker
    {
        private readonly ILog _log;

        public List<FileViewInfo> lastVersion { get; set; }

        public FileChangesTracker(ILog log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            lastVersion = new List<FileViewInfo>();
        }

        public IEnumerable<FileViewInfo> GetChangedFiles(IEnumerable<FileViewInfo> oldFiles, IEnumerable<FileViewInfo> newFiles)
        {
            _log.Info($"На сравнение пришло {oldFiles.Count()} старых файлов " +
                      $"и {newFiles.Count()} актуальных");

            HashSet<FileViewInfo> newHashfiles = new HashSet<FileViewInfo>(newFiles);

            newHashfiles.ExceptWith(oldFiles);

            _log.Info($"В сравнении обнаружено {newHashfiles.Count} измененных файлов");

            // разница между новыми и старыми объектами = измененные объекты
            return newHashfiles.ToList();
        }

        public List<FileViewInfo> UpdateLastVersion(List<FileViewInfo> changedFiles)
        {
            foreach (var changedFile in changedFiles)
            {
                if (lastVersion.Contains(changedFile))
                {
                    foreach (var file in lastVersion)
                    {
                        if (file.Equals(changedFile))
                        {
                            file.Version += 1;

                            changedFile.UpdateTo(file);

                            changedFile.Version = file.Version;

                            break;
                        }
                    }
                }
                else
                {
                    lastVersion.Add(changedFile);
                }
            }
            return changedFiles;
        }
    }
}
