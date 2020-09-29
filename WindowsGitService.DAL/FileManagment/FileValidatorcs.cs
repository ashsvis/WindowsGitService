using System;
using System.IO;
using WindowsGitService.DAL.Interfaces;

namespace WindowsGitService.DAL.FileManagment
{
    public class FileValidator : IFileValidator
    {
        /// <summary>
        /// Проверяет путь к файлу на корректность использования
        /// </summary>
        /// <param name="path">Путь к файлу в файловой системе Windows</param>
        /// <returns></returns>
        public bool IsValidPath(string path)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                string root = Path.GetPathRoot(path);

                isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
