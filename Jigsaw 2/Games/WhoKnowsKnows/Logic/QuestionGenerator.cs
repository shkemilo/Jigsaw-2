using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class QuestionGenerator : Generator
    {
        public Queue<Question> Questions { get; }

        private readonly int numberOfGames;
        private readonly int numberOfFields;

        public QuestionGenerator(int numberOfFields = 4, int numberOfGames = 10)
        {
            Questions = new Queue<Question>();

            this.numberOfGames = numberOfGames;
            this.numberOfFields = numberOfFields;

            setQuestions();
        }

        private void setQuestions()
        {
            string query = "SELECT QuestionText, Answer1, Answer2, Answer3, Answer4, CorrectIndex FROM Questions q INNER JOIN Answers a ON q.QuestionId = a.Id; ";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString))
            {
                DataTable dataTable = new DataTable();

                int length = adapter.Fill(dataTable);

                List<int> indexes = uniqueRandomGenerator(numberOfGames, length);

                foreach (int index in indexes)
                {
                    DataRow dataRow = dataTable.Select()[index];

                    string questionText = dataRow[0] as string;

                    List<string> answers = new List<string>();
                    for (int i = 1; i < numberOfFields + 1; i++)
                    {
                        string answer = dataRow[i] as string;
                        answers.Add(answer.ToUpperInvariant());
                    }

                    int correctIndex = Convert.ToInt32(dataRow[numberOfFields + 1]);

                    Questions.Enqueue(new Question(answers, questionText, correctIndex));
                }
            }
        }

        private List<int> uniqueRandomGenerator(int n, int maxNumber)
        {
            List<int> temp = new List<int>();

            Random rnd = new Random();

            int i = 0;
            while (i < 10)
            {
                int newNumber = rnd.Next(maxNumber);

                if(!temp.Contains(newNumber))
                {
                    temp.Add(newNumber);
                    i++;
                }
            }

            return temp;
        }
    }
}