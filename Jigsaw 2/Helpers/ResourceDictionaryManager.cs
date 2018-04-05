using System;
using System.Windows;

namespace Jigsaw_2.Helpers
{
    /// <summary>
    /// Helper class used for fetching resource dictionaries
    /// </summary>
    public static class ResourceDictionaryManager
    {
        private static ResourceDictionary resources;

        static ResourceDictionaryManager()
        {
            resources = new ResourceDictionary();
            resources.Source = new Uri("Resources\\Icons.xaml", UriKind.Relative);
        }

        /// <summary> Returns the resource dictionary. </summary>
        public static ResourceDictionary GetResources()
        {
            return resources;
        }
    }
}