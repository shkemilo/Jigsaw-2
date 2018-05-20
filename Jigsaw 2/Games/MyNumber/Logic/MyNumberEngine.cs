using Jigsaw_2.Abstracts;
using NCalc;
using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.MyNumber
{
    internal class MyNumberEngine : Engine
    {
        private readonly List<int> numbers;
        private readonly int target;
        private readonly string correctExpression;

        public MyNumberEngine()
        {
            ExpressionGenerator expressionGenerator = new ExpressionGenerator();

            numbers = expressionGenerator.Numbers;
            target = expressionGenerator.TargetValue;
            correctExpression = expressionGenerator.CorrectExpression;

            Console.WriteLine(target);
        }

        public List<int> Numbers => numbers;

        public int Target => target;

        public string CorrectExpression => correctExpression;

        public int Check(string expression)
        {
            if (expression == string.Empty)
            {
                return -1;
            }

            Expression e = new Expression(expression);

            if (!e.HasErrors())
            {
                return Convert.ToInt32(e.Evaluate());
            }
            else
            {
                return -1;
            }
        }

        public List<string> GetAllNumbers()
        {
            List<string> temp = decomposeNumber(target);

            temp.AddRange(intsToStrings(numbers));

            return temp;
        }

        private List<string> decomposeNumber(int number)
        {
            List<string> temp = new List<string>();
            while (number > 0)
            {
                temp.Add((number % 10).ToString());

                number /= 10;
            }

            temp.Reverse();

            return temp;
        }

        private List<string> intsToStrings(List<int> numbers)
        {
            List<string> temp = new List<string>();
            foreach (int element in numbers)
            {
                temp.Add(element.ToString());
            }

            return temp;
        }
    }
}