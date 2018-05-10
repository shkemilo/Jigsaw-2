using System.Configuration;
using System.Data.SqlClient;

namespace Jigsaw_2.Abstracts
{
    public abstract class Generator
    {
        protected string connectionString;

        protected SqlConnection connection;

        protected Generator()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Jigsaw_2.Properties.Settings.JigsawDatabaseConnectionString"].ConnectionString;
        }
    }
}