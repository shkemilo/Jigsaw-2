using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Jigsaw_2.Helpers
{ 

    public static class ResourceDictionaryManager
    {
        static ResourceDictionary resources;

        static ResourceDictionaryManager()
        {
            resources = new ResourceDictionary();
            resources.Source = new Uri("Resources\\Icons.xaml", UriKind.Relative);
        }

        public static ResourceDictionary GetResources()
        {
            return resources;
        }
    }
}
