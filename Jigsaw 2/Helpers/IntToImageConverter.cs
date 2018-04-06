using System;
using System.Windows.Media.Imaging;

namespace Jigsaw_2.Helpers
{
    /// <summary>
    /// Helper class used for converting int's specified Images.
    /// </summary>
    public sealed class IntToImageConverter
    {
        #region Private Static Fields

        private static IntToImageConverter instance = null;
        private static readonly object padlock = new object();

        #endregion Private Static Fields

        #region Private Fields

        private BitmapImage[] images;

        #endregion Private Fields

        #region Constructors

        private IntToImageConverter()
        {
            images = new BitmapImage[] { null,
                                         new BitmapImage(new Uri("/Jigsaw 2;component/Resources/Graphics/Jumper/Logo64.png", UriKind.Relative)),
                                         new BitmapImage(new Uri("/Jigsaw 2;component/Resources/Graphics/Jumper/Club64.png", UriKind.Relative)),
                                         new BitmapImage(new Uri("/Jigsaw 2;component/Resources/Graphics/Jumper/Spade64.png", UriKind.Relative)),
                                         new BitmapImage(new Uri("/Jigsaw 2;component/Resources/Graphics/Jumper/Heart64.png", UriKind.Relative)),
                                         new BitmapImage(new Uri("/Jigsaw 2;component/Resources/Graphics/Jumper/Diamond64.png", UriKind.Relative)),
                                         new BitmapImage(new Uri("/Jigsaw 2;component/Resources/Graphics/Jumper/Star64.png", UriKind.Relative)) };
        }

        #endregion Constructors

        #region Public Static Properties

        public static IntToImageConverter Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IntToImageConverter();
                    }
                    return instance;
                }
            }
        }

        #endregion Public Static Properties

        #region Public Methods

        /// <summary> Converts and array of int's to an array of Images. </summary>
        public BitmapImage[] Convert(int[] target)
        {
            BitmapImage[] temp = new BitmapImage[target.Length];

            for (int i = 0; i < target.Length; i++)
                temp[i] = images[target[i]];

            return temp;
        }

        /// <summary> Converts an int to an Image. </summary>
        public BitmapImage Convert(int target)
        {
            return images[target];
        }

        #endregion Public Methods
    }
}