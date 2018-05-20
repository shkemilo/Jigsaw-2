using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationGenerator : Generator
    {
        private readonly List<Association> associations;
        private string finalAnswer;

        private readonly int numberOfFields;

        public AssociationGenerator(int numberOfFields = 4)
        {
            associations = new List<Association>();
            this.numberOfFields = numberOfFields;

            setAssociaons();
        }

        private void setAssociaons()
        {
            string query = "SELECT ac.A1, ac.A2, ac.A3, ac.A4, ac.A, bc.B1, bc.B2, bc.B3, bc.B4, bc.B, cc.C1, cc.C2, cc.C3, cc.C4, cc.C, dc.D1, dc.D2, dc.D3, dc.D4, dc.D, a.Answer FROM Associations a INNER JOIN AColumns ac ON a.AColumnId = ac.Id INNER JOIN BColumns bc ON a.BColumnId = bc.Id INNER JOIN CColumns cc ON a.CColumnId = cc.Id INNER JOIN DColumns dc ON a.DColumnId = dc.Id; ";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();

                int length = adapter.Fill(dataTable);

                DataRow dataRow = dataTable.Select()[new Random().Next(length)];

                for (int i = 0; i < (numberOfFields + 1) * numberOfFields; i += numberOfFields + 1)
                {
                    List<string> temp = new List<string>();

                    for (int j = 0; j < numberOfFields; j++)
                    {
                        temp.Add(dataRow[i + j] as string);
                    }

                    associations.Add(new Association(temp, dataRow[i + numberOfFields] as string));
                }

                finalAnswer = dataRow[(numberOfFields + 1) * numberOfFields] as string;
            }
        }

        public string FinalAnswer => finalAnswer;

        public List<Association> Associations => associations;
    }
}