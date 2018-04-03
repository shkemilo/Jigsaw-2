using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.Couplings
{
    class CouplingGenerator //TODO: Would be nice if i knew how to make a database :)
    {
        Dictionary<string, string> couplings;

        public CouplingGenerator()
        {
            couplings = new Dictionary<string, string>();

            string[] allCouplings;
            allCouplings = (Properties.Resources.CouplingsList).Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

            Random rnd = new Random();
            string[] currentCoupling = allCouplings[rnd.Next(allCouplings.Length)].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            try
            {
                for (int i = 0; i < currentCoupling.Length; i += 2)
                    couplings.Add(currentCoupling[i + 1], currentCoupling[i]);
            }
            catch (Exception)
            {
                foreach (string s in currentCoupling)
                    Console.WriteLine(s);

                throw new Exception("This shouldn't happen lol");
            }

            foreach (KeyValuePair<string, string> s in couplings)
                Console.WriteLine(s.ToString());
        }

        public Dictionary<string, string> GetCouplings()
        {
            return couplings;
        }
    }
}
