using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Engine class used for Game logic in the Couplings Game.
    /// </summary>
    internal class CouplingsEngine : Engine
    {
        #region Private Fields

        private int numberOfFields;

        private Dictionary<string, string> couplings;
        private string couplingText;

        private int[] offset;

        #endregion Private Fields

        #region Constructors

        public CouplingsEngine(int numberOfFields = 8)
        {
            this.numberOfFields = numberOfFields;

            CouplingGenerator couplingGenerator = new CouplingGenerator();

            couplings = couplingGenerator.GetCouplings();
            couplingText = couplingGenerator.GetCouplingText();

            offset = new int[numberOfFields];
        }

        #endregion Constructors

        #region Public Methods

        public string GetCouplingText()
        {
            return couplingText;
        }

        /// <summary> Shuffles and returns the Couplings as an array of Tuples. </summary>
        public Tuple<string, string>[] GetCouplings()
        {
            string[] tempValues = new string[numberOfFields];
            string[] tempKeys = new string[numberOfFields];

            dictionaryToArrays(ref tempKeys, ref tempValues);

            new Random(Guid.NewGuid().GetHashCode()).Shuffle(tempValues);

            setOffset(couplings.Values.ToList(), tempValues.ToList());

            return arraysToTupleArray(tempValues, tempKeys);
        }

        /// <summary> Gets how much did the elements move when they were shuffled. </summary>
        public int[] GetOffset()
        {
            return offset;
        }

        /// <summary> Checks if the Coupling was pair correctly. </summary>
        public bool Check(string hit, string target)
        {
            if (couplings[target.ToLower()] == hit.ToLower())
                return true;

            return false;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary> Calculates how many positions did the elements move based on their original position. </summary>
        private void setOffset(List<string> original, List<string> shuffled)
        {
            for (int i = 0; i < numberOfFields; i++)
                offset[i] = shuffled.IndexOf(original[i]) - i;
        }

        /// <summary> Converts a dictionary to 2 arrays, one containing the keys, and other the values. </summary>
        private void dictionaryToArrays(ref string[] keys, ref string[] vals)
        {
            int z = 0;
            foreach (KeyValuePair<string, string> pair in couplings)
            {
                vals[z] = pair.Value;
                keys[z++] = pair.Key;
            }
        }

        /// <summary> Merges 2 arrays into a array of tuples. </summary>
        private Tuple<string, string>[] arraysToTupleArray(string[] array1, string[] array2)
        {
            Tuple<string, string>[] temp = new Tuple<string, string>[numberOfFields];

            for (int i = 0; i < numberOfFields; i++) // Organizes the keys and values into tuples
                temp[i] = new Tuple<string, string>(array1[i], array2[i]);

            return temp;
        }

        #endregion Private Methods
    }
}