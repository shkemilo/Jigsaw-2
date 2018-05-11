using System;
using System.Windows;

namespace Jigsaw_2.Helpers
{
    /// <summary>
    /// Helper class used for fetching resource dictionaries
    /// </summary>
    public static class ResourceDictionaryManager
    {
        #region Public Static Fields

        private static readonly ResourceDictionary resources;

        #endregion Public Static Fields

        #region Constructors

        static ResourceDictionaryManager()
        {
            resources = new ResourceDictionary
            {
                Source = new Uri("Resources\\Icons.xaml", UriKind.Relative)
            };
        }

        #endregion Constructors

        #region Public Static Methods

        /// <summary> Returns the resource dictionary. </summary>
        public static ResourceDictionary GetResources()
        {
            return resources;
        }

        #endregion Public Static Methods
    }
}