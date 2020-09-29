using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WindowsGitService.DAL.Interfaces;

namespace WindowsGitService.DAL.FileManagment
{
    public class FileWriter : IChangedFileSaver
    {
        private readonly ILog _log;

        private readonly IFileValidator _fileValidator;

        public FileWriter(ILog log, IFileValidator fileValidator)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _fileValidator = fileValidator ?? throw new ArgumentNullException(nameof(fileValidator));
        }

        /// <summary>
        /// Копирует файл в указаную директорию
        /// </summary>
        /// <param name="file">Данные о копируемом файле</param>
        /// <param name="targetDirectoryPath">Целевая папка</param>
        public void SaveFileСhanges(IEnumerable<FileViewInfo> files, string targetDirectoryPath = @"C:\Navicon\Temp")
        {
            if (_fileValidator.IsValidPath(targetDirectoryPath) == false)
            {
                _log.Error($"Путь {targetDirectoryPath} некорректен");
                throw new ArgumentException("Путь к директории не корректен");
            }
            if (files == null)
            {
                _log.Error($"В метод CopyFile получил null files");
                return;
            }

            foreach (var file in files)
            {
                CopyFile(file, targetDirectoryPath);
            }

            //_log.Info($"Изменения в файлах записаны в {targetDirectoryPath}");
        }

        /// <summary>
        /// Копирует файл в указаную директорию
        /// </summary>
        /// <param name="file">Данные о копируемом файле</param>
        /// <param name="targetDirectoryPath">Целевая папка</param>
        public void CopyFile(FileViewInfo file, string targetDirectoryPath = @"C:\Navicon\Temp")
        {
            if (_fileValidator.IsValidPath(targetDirectoryPath) == false)
            {
                _log.Error($"Путь {targetDirectoryPath} некорректен");
                throw new ArgumentException("Путь к директории не корректен");
            }
            if (file == null)
            {
                _log.Error($"В метод CopyFile получил null file");
                return;
            }

            targetDirectoryPath += $@"\{file.GetFormat()}\{file.Name}";

            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryPath);

            if (targetDirectoryInfo.Exists == false)
            {
                targetDirectoryInfo.Create();
            }

            string targetFilePathInfo = targetDirectoryInfo.FullName + "\\" +
                                        $"{file.Name}-{file.Version}.{file.Format}";

            File.Copy(file.FullPath, targetFilePathInfo, true);

            _log.Info($"Файл {file.Name}.{file.Format} версии: {file.Version} записан в {targetDirectoryInfo.FullName}");
        }

        public void SaveLastUpdate(List<FileViewInfo> lastVersion, string path = @"C:\Navicon\LastUpdated.txt")
        {
            using (StreamWriter myStream = new StreamWriter(path))
            {

                string s = JsonConvert.SerializeObject(lastVersion, Formatting.Indented);

                myStream.WriteLine(s);
            }
        }
    }
}
