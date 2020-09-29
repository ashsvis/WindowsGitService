using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsGitService.DAL
{
    public class FileChangesFacade : IFileChangesFacade
    {
        private readonly IFileAccessor _fileAccessor;

        private readonly IChangedFileSaver _changedFileSaver;

        private readonly IFileViewConverter _fileModelBuilder;

        private readonly IFileChangesTracker _fileChangesTracker;

        private readonly ILog _log;

        public FileChangesFacade(IFileAccessor      fileAccessor, IChangedFileSaver   changedFileSaver,
                                 IFileViewConverter modelBuilder, IFileChangesTracker fileChangesTracker,
                                 ILog log)
        {
            if (log == null)
            {
                throw new ArgumentException(nameof(log));
            }

            _log = log;

            if (fileAccessor == null)
            {
                _log.Error($"FileChangesDirector get null {nameof(fileAccessor)} constructor param");
                throw new ArgumentException(nameof(fileAccessor));
            }
            if (changedFileSaver == null)
            {
                _log.Error($"FileChangesDirector get null {nameof(changedFileSaver)} constructor param");
                throw new ArgumentException(nameof(changedFileSaver));
            }
            if (modelBuilder == null)
            {
                _log.Error($"FileChangesDirector get null {nameof(modelBuilder)} constructor param");
                throw new ArgumentException(nameof(modelBuilder));
            }
            if (fileChangesTracker == null)
            {
                _log.Error($"FileChangesDirector get null {nameof(fileChangesTracker)} constructor param");
                throw new ArgumentException(nameof(fileChangesTracker));
            }

            _fileAccessor = fileAccessor;
            _changedFileSaver = changedFileSaver;
            _fileModelBuilder = modelBuilder;
            _fileChangesTracker = fileChangesTracker;
        }

        /// <summary>
        /// Заполняет lastVersion списком начальных данных
        /// </summary>
        public void InitializePreviouslyCopiedFiles()
        {
            IEnumerable<FileViewInfo> deserializedProduct = _fileAccessor.GetLastUpdate();
         
            if (deserializedProduct != null)
            {
                _fileChangesTracker.lastVersion = (List<FileViewInfo>)deserializedProduct;
                _log.Info($"Директор иниц иализировал начальный список из {_fileChangesTracker.lastVersion.Count} ранее скопированых файлов");
            }
            else
            {
                _log.Warn("Ранее сканированные файлы отсутствуют");
            }
        }

        /// <summary>
        /// Сохраняет lastVersion списком начальных данных
        /// </summary>
        public void SaveCurrentFileVersionState()
        {
            const string path = @"C:\Navicon\LastUpdated.txt";

            _changedFileSaver.SaveLastUpdate(_fileChangesTracker.lastVersion, path);

            _log.Warn($"Выполненно сохранение данных о файлах в {path}");
        }

        /// <summary>
        /// Выполнение сравнения актуальных файлов с сохраненными в lastVersion данными
        /// и сохраняет измененные файлы в директории
        /// сравнивает файлы из всех папок в конфиге
        /// </summary>
        public void MakeFilesCompare()
        {
            _log.Warn($"Директор начинает сравнение всех доступных в App.config папок");

            IReadOnlyCollection<FileInfo> actualFiles = (IReadOnlyCollection<FileInfo>)_fileAccessor.GetFiles();

            _log.Info($"Директор получил список из {actualFiles.Count} актуальных файлов");

            IEnumerable<FileViewInfo> actualViewFiles;

            actualViewFiles = _fileModelBuilder.ConvertToFileInfo(actualFiles);

            // _log.Info($"Директор получил преобразование файлов в FileViewInfo");

            List<FileViewInfo> changedFiles = (List<FileViewInfo>)_fileChangesTracker.GetChangedFiles(_fileChangesTracker.lastVersion, actualViewFiles);

            _log.Info($"Директор получил список из {changedFiles.Count} измененных файлов");

            // обновление списка "lastversion" актуальных файлов
            changedFiles = _fileChangesTracker.UpdateLastVersion(changedFiles);

            // сохранение измененных файлов
            _changedFileSaver.SaveFileСhanges(changedFiles);

            _log.Warn($"Директор завершил операцию по сохранению изменений");
        }

        /// <summary>
        /// Выполнение сравнения актуальных файлов с сохраненными в lastVersion данными
        /// и сохраняет измененные файлы в директории
        /// по срабатыванию таймера
        /// </summary>
        /// <param name="folders">IEnumerable<FileInfo> список сканируемых папок</param>
        public void MakeFilesCompare(object folders)
        {
            IEnumerable<string> scanningFolders = folders as IEnumerable<string>;

            if(scanningFolders == null)
            {
                _log.Error("MakeFilesCompare(object folders) null argument param");
                return;
            }

            MakeFilesCompare(scanningFolders);
        }

        /// <summary>
        /// Выполнение сравнения актуальных файлов с сохраненными в lastVersion данными
        /// и сохраняет измененные файлы в директории
        /// </summary>
        /// <param name="folders">Список сканируемых папок</param>
        public void MakeFilesCompare(IEnumerable<string> folders)
        {
            _log.Warn($"Директор начинает сравнение файлов из ${string.Join(" ", folders)}");

            var actualFiles = (IReadOnlyCollection<FileInfo>)_fileAccessor.GetFiles(folders);

            _log.Info($"Директор получил список из {actualFiles.Count} актуальных файлов");

            IEnumerable<FileViewInfo> actualViewFiles = _fileModelBuilder.ConvertToFileInfo(actualFiles);

            // _log.Info($"Директор получил преобразование файлов в FileViewInfo");

            var changedFiles = (List<FileViewInfo>)_fileChangesTracker.GetChangedFiles(_fileChangesTracker.lastVersion, actualViewFiles);

            _log.Info($"Директор получил список из {changedFiles.Count} измененных файлов");

            // обновление списка "lastversion" актуальных файлов
            changedFiles = _fileChangesTracker.UpdateLastVersion(changedFiles);

            // сохранение измененных файлов
            _changedFileSaver.SaveFileСhanges(changedFiles);

            _log.Warn($"Директор завершил операцию по сохранению изменений");
        }
    }
}