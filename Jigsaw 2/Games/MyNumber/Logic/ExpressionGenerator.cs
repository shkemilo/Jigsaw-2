using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.MyNumber
{
    internal class ExpressionGenerator
    {
        public static readonly int[] smallNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static readonly int[] mediumNumbers = { 10, 15, 20 };
        public static readonly int[] bigNumbers = { 25, 50, 75, 100 };

        private readonly int numberOfSmall;
        private readonly int numberOfMedium;
        private readonly int numberOfBig;

        private string correctExpression;
        private List<int> numbers;
        private int targetValue;

        public string CorrectExpression => correctExpression;
        public List<int> Numbers => numbers;
        public int TargetValue => targetValue;

        public ExpressionGenerator(int numberOfSmall = 4, int numberOfMedium = 1, int numberOfBig = 1)
        {
            this.numberOfSmall = numberOfSmall;
            this.numberOfMedium = numberOfMedium;
            this.numberOfBig = numberOfBig;

            numbers = new List<int>();

            generateNumbers();

#if DEBUG
            Console.WriteLine(correctExpression);
#endif
        }

        private void generateNumbers()
        {
            numbers.AddRange(generateNumbers(numberOfSmall, smallNumbers));
            numbers.AddRange(generateNumbers(numberOfMedium, mediumNumbers));
            numbers.AddRange(generateNumbers(numberOfBig, bigNumbers));

            targetValue = new Random().Next(100, 1000);

            solve(numbers.ToArray(), targetValue);

            if (correctExpression.Equals(string.Empty))
            {
                generateNumbers();
            }
        }

        private List<int> generateNumbers(int numberOfNumbers, int[] numbers)
        {
            Random rnd = new Random();

            List<int> temp = new List<int>();

            for (int i = 0; i < numberOfNumbers; i++)
            {
                int index = rnd.Next(numbers.Length);

                temp.Add(numbers[index]);
            }

            return temp;
        }

        //out of time constraints i haven't had the time to figure out my own algorithm
        //credit to: http://codinghelmet.com/?path=exercises/expression-from-numbers
        private void solve(int[] numbers, int targetValue)
        {
            int targetKey = (targetValue << numbers.Length) + (1 << numbers.Length) - 1;
            // (value << numbers.Length) represents expression value
            // (1 << numbers.Length) - 1 represents mask with all bits set to 1,
            // i.e. mask in which each input number has been used exactly once
            // to build the expression.

            HashSet<int> solvedKeys = new HashSet<int>();
            // Each number in the collection indicates that corresponding value + mask
            // has been reached using arithmetical operations.

            Dictionary<int, int> keyToLeftParent = new Dictionary<int, int>();
            // For each solved key (value + mask), there is an entry indicating
            // result of the expression on the left side of the arithmetic
            // operator. Missing value indicates that key represents the
            // raw number (taken from the input list), rather than
            // the result of a calculation.

            Dictionary<int, int> keyToRightParent = new Dictionary<int, int>();
            // Same as keyToLeftParent, only indicating the right parent
            // used to build the expression.

            Dictionary<int, char> keyToOperator = new Dictionary<int, char>();
            // Indicates arithmetic operator used to build this node
            // from left and right parent nodes. Missing value for a given key
            // indicates that key is a raw value taken from input array,
            // rather than result of an arithmetic operation.

            Queue<int> queue = new Queue<int>();
            // Keys (value + mask pairs) that have not been processed yet

            // First step is to initialize the structures:
            // Add all input values into corresponding array entries and
            // add them to the queue so that the operation can begin

            for (int i = 0; i < numbers.Length; i++)
            {
                int key = (numbers[i] << numbers.Length) + (1 << i);

                solvedKeys.Add(key);
                queue.Enqueue(key);
            }

            // Now expand entries one at the time until queue is empty,
            // i.e. until there are no new entries populated.
            // Additional stopping condition is that target key has been generated,
            // which indicates that problem has been solved and there is no need to
            // expand nodes any further.

            while (queue.Count > 0 && !solvedKeys.Contains(targetKey))
            {
                int curKey = queue.Dequeue();

                int curMask = curKey & ((1 << numbers.Length) - 1);
                int curValue = curKey >> numbers.Length;

                // Now first take a snapshot of all keys that
                // have been reached because this collection is going to
                // change during the following operation

                int[] keys = new int[solvedKeys.Count];
                solvedKeys.CopyTo(keys);

                for (int i = 0; i < keys.Length; i++)
                {
                    int mask = keys[i] & ((1 << numbers.Length) - 1);
                    int value = keys[i] >> numbers.Length;

                    if ((mask & curMask) == 0)
                    { // Masks are disjoint, i.e. two entries do not use
                      // the same input number twice.
                      // This is sufficient condition to combine the two entries

                        for (int op = 0; op < 6; op++)
                        {
                            char opSign = '\0';
                            int newValue = 0;

                            switch (op)
                            {
                                case 0: // Addition
                                    newValue = curValue + value;
                                    opSign = '+';
                                    break;

                                case 1: // Subtraction - another value subtracted from current
                                    newValue = curValue - value;
                                    opSign = '-';
                                    break;

                                case 2: // Subtraction - current value subtracted from another
                                    newValue = value - curValue;
                                    opSign = '-';
                                    break;

                                case 3: // Multiplication
                                    newValue = curValue * value;
                                    opSign = '*';
                                    break;

                                case 4: // Division - current divided by another
                                    newValue = -1;  // Indicates failure to divide
                                    if (value != 0 && curValue % value == 0)
                                        newValue = curValue / value;
                                    opSign = '/';
                                    break;

                                case 5: // Division - other value divided by current
                                    newValue = -1;  // Indicates failure to divide
                                    if (curValue != 0 && value % curValue == 0)
                                        newValue = value / curValue;
                                    opSign = '/';
                                    break;
                            }

                            if (newValue >= 0)
                            {   // Ignore negative values - they can always be created
                                // the other way around, by subtracting them
                                // from a larger value so that positive value is reached.

                                int newMask = (curMask | mask);
                                // Combine the masks to indicate that all input numbers
                                // from both operands have been used to produce
                                // the resulting expression

                                int newKey = (newValue << numbers.Length) + newMask;

                                if (!solvedKeys.Contains(newKey))
                                {   // We have reached a new entry.
                                    // This expression should now be added
                                    // to data structures and processed further
                                    // in the following steps.

                                    // Populate entries that describe newly created expression
                                    solvedKeys.Add(newKey);

                                    if (op == 2 || op == 5)
                                    {   // Special cases - anti reflexive operations
                                        // with interchanged operands
                                        keyToLeftParent.Add(newKey, keys[i]);
                                        keyToRightParent.Add(newKey, curKey);
                                    }
                                    else
                                    {
                                        keyToLeftParent.Add(newKey, curKey);
                                        keyToRightParent.Add(newKey, keys[i]);
                                    }

                                    keyToOperator.Add(newKey, opSign);

                                    // Add expression to list of reachable expressions
                                    solvedKeys.Add(newKey);

                                    // Add expression to the queue for further expansion
                                    queue.Enqueue(newKey);
                                }
                            }
                        }
                    }
                }
            }

            // Now print the solution if it has been found

            if (!solvedKeys.Contains(targetKey))
                correctExpression = string.Empty;
            else
            {
                printExpression(keyToLeftParent, keyToRightParent, keyToOperator, targetKey, numbers.Length);
                correctExpression += " = " + targetValue.ToString();
            }
        }

        private void printExpression(Dictionary<int, int> keyToLeftParent,
                                    Dictionary<int, int> keyToRightParent,
                                    Dictionary<int, char> keyToOperator,
                                    int key, int numbersCount)
        {
            if (!keyToOperator.ContainsKey(key))
                correctExpression += (key >> numbersCount).ToString();
            else
            {
                correctExpression += "(";

                // Recursively print the left operand
                printExpression(keyToLeftParent, keyToRightParent, keyToOperator,
                                keyToLeftParent[key], numbersCount);

                // Then print the operation sign
                correctExpression += " " + keyToOperator[key] + " ";

                // Finally, print the right operand
                printExpression(keyToLeftParent, keyToRightParent, keyToOperator,
                                keyToRightParent[key], numbersCount);

                correctExpression += ")";
            }
        }
    }
}