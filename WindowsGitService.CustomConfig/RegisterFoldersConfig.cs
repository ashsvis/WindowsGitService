using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsGitService.CustomConfig
{
    public class RegisterFoldersConfig : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        [ConfigurationCollection(typeof(FolderElementCollection))]
        public FolderElementCollection FolderElementCollection
        {
            get
            {
                return (FolderElementCollection)this["Folders"];
            }
        }
    }
}
