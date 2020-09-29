using System.Configuration;

namespace WindowsGitService.CustomConfig
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)base["path"];
            }
            set
            {
                base["path"] = value;
            }
        }

        [ConfigurationProperty("folderType", IsRequired = false)]
        public string FolderType
        {
            get
            {
                return (string)base["folderType"];
            }
            set
            {
                base["folderType"] = value;
            }
        }

        [ConfigurationProperty("monitoringPeriod", DefaultValue = "1000", IsRequired = true)]
        public string МonitoringPeriod
        {
            get
            {
                return (string)base["monitoringPeriod"];
            }
            set
            {
                base["monitoringPeriod"] = value;
            }
        }
    }
}