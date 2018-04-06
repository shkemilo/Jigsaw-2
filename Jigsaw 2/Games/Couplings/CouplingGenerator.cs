﻿using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.Couplings
{
    // TODO: Would be nice if i knew how to make a database :)
    internal class CouplingGenerator
    {
        #region Private Fields

        private Dictionary<string, string> couplings;
        private string couplingText;

        #endregion Private Fields

        #region Constructors

        public CouplingGenerator()
        {
            couplings = new Dictionary<string, string>();

            string[] allCouplings;
            allCouplings = Properties.Resources.CouplingsList.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

            Random rnd = new Random();
            string[] currentCoupling = allCouplings[rnd.Next(allCouplings.Length)].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            couplingText = currentCoupling[0];

            try
            {
                for (int i = 1; i < currentCoupling.Length; i += 2)
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

        #endregion Constructors

        #region Public Methods

        public Dictionary<string, string> GetCouplings()
        {
            return couplings;
        }

        public string GetCouplingText()
        {
            return couplingText;
        }

        #endregion Public Methods
    }
}