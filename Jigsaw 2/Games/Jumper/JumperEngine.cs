using System;
using System.Collections.Generic;
using System.Linq;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// Engine for Jumper Game.
    /// </summary>
    public class JumperEngine : Engine
    {
        int numberOfFields;
        int[] combination;

        Random randomSeed;

        public JumperEngine(int numberOfFields = 4)
        {
            this.numberOfFields = numberOfFields;

            combination = new int[numberOfFields];

            randomSeed = new Random(Guid.NewGuid().GetHashCode());

            generateCombination();

        #if DEBUG
            for (int i = 0; i < numberOfFields; i++)
            Console.WriteLine(combination[i]);
        #endif
        }

        /// <summary> Generates a combination to be found by the user. </summary>
        private void generateCombination()
        {
            for (int i = 0; i < numberOfFields; i++)
                combination[i] = randomSeed.Next(1, 7);
        }

        /// <summary> Helper function. Adds a specifed color to a list n times. </summary>
        private void addColors(ref List<string> list, int n, string c)
        {
            int currentListLength = list.Count;

            for (int i = currentListLength; i < n + currentListLength; i++)
                list.Add(c);
        }

        /// <summary> Returns the generated combination. </summary>
        public int[] GetCombination()
        {
            return combination;
        }

        /// <summary> Creates a color list based on the answer the user gives. </summary>
        public string[] CheckFeedback(int[] answer)
        {
            List<int> tempCombination = combination.ToList();
            List<int> tempAnswer = answer.ToList();

            List<string> tempList = new List<string>();

            int greenCount = 0;
            int yellowCount = 0;

            for (int i = 0; i < numberOfFields; i++)
                if (tempCombination[i] == answer[i])
                {
                    greenCount++;

                    tempCombination[i] = -1;
                    tempAnswer.Remove(answer[i]);
                }

            foreach (int a in tempAnswer)
                if (tempCombination.Contains(a))
                {
                    yellowCount++;

                    tempCombination[tempCombination.IndexOf(a)] = -1;
                }

            addColors(ref tempList, greenCount, "Green");
            addColors(ref tempList, yellowCount, "Yellow");
            addColors(ref tempList, numberOfFields - greenCount - yellowCount, "White");

            return tempList.ToArray();
        }

        /// <summary> Function to check if the current answer is correct. </summary>
        public bool Check(int[] answer)
        {
            bool goodAnswer = true;

            for (int i = 0; i < answer.Length; i++)
                if (combination[i] != answer[i])
                {
                    goodAnswer = false;
                    break;
                }

            return goodAnswer;
        }
    }
}
