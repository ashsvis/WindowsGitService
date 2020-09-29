using System;
using System.Configuration;
using System.Collections.Generic;

namespace WindowsGitService.CustomConfig
{
    public class FolderElementsAccessor : IMonitoringFolders
    {
        public List<string> GetFoldersPath()
        {
            var sageConfig = (RegisterFoldersConfig)ConfigurationManager
                                                .GetSection("StartupFolders");

            List<string> paths = new List<string>();

            foreach (FolderElement instance in sageConfig.FolderElementCollection)
            {
                paths.Add(instance.Path);
            }

            return paths;
        }

        public List<FolderElement> GetFolderElements()
        {
            var sageConfig = (RegisterFoldersConfig)ConfigurationManager
                                                .GetSection("StartupFolders");

            List<FolderElement> paths = new List<FolderElement>();

            foreach (FolderElement instance in sageConfig.FolderElementCollection)
            {
                paths.Add(instance);
            }

            return paths;
        }
    }
}