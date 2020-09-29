using System;
using System.Configuration;

namespace WindowsGitService.CustomConfig
{
    public class FolderElementCollection : ConfigurationElementCollection
    {
        public FolderElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as FolderElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new FolderElement this[string responseString]
        {
            get { return (FolderElement)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)element).Path;
        }
    }
}
