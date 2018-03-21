using System;

namespace Jigsaw_2.Helpers
{
    /// <summary>
    /// Static class used for Shuffling elements of an array.
    /// </summary>
    static class Shuffler
    {
        /// <summary> Shuffles the array. </summary>
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
