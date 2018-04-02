using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jigsaw_2.Games.Couplings
{
    class CouplingsEngine : Engine
    { 
        int numberOfFields;

        Dictionary<string, string> couplings;

        int[] offset;

        public CouplingsEngine(int numberOfFields = 8)
        {
            this.numberOfFields = numberOfFields;

            couplings = new CouplingGenerator().GetCouplings();

            offset = new int[numberOfFields];
        }

        public Tuple<string, string>[]  GetCouplings()
        {
            string[] tempValues = new string[numberOfFields];
            string[] tempKeys = new string[numberOfFields];

            int z = 0;
            foreach (KeyValuePair<string, string> s in couplings) //Puts the keys and values of the dictionary into string arrays TODO: Encapsulate
            {
                tempValues[z] = s.Value;
                tempKeys[z] = s.Key;
                z++;
            }

            new Random(Guid.NewGuid().GetHashCode()).Shuffle(tempValues);

            setOffset(couplings.Values.ToList(), tempValues.ToList());

            Tuple<string, string>[] temp = new Tuple<string, string>[numberOfFields];

            for (int i = 0; i < numberOfFields; i++) //Organizes the keys and values into tuples
                temp[i] = new Tuple<string, string>(tempValues[i], tempKeys[i]);
            
            return temp;
        }

        public Dictionary<string, string> GetRealCouplings()
        {
            return couplings;
        }

        private void setOffset(List<string> original, List<string> shuffled)
        {
            for (int i = 0; i < numberOfFields; i++)
                offset[i] = shuffled.IndexOf(original[i]) - i;

            for (int i = 0; i < offset.Length; i++)
                Console.WriteLine(offset[i]);
        }

        public int[] GetOffset()
        {
            return offset;
        }

        public bool Check(string hit, string target)
        {
            if (couplings[target.ToLower()] == hit.ToLower())
                return true;

            return false;
        }
    }
}
