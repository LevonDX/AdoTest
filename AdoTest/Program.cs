using System.Data.SqlClient;

namespace AdoTest
{
	internal class Program
	{
		const string ConnectionString = "Data Source=localhost;Initial Catalog=TestRecordDB;Integrated Security=True;Encrypt=False";

		static void Main(string[] args)
		{
			string name = "john";
			string surname = "Babayan";
			decimal salary = 2000;
			DateTime birthDate = new DateTime(1985, 08, 15);

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				//string insertSql = "Insert into Record ([name], surname, birthDate) values (@nm, @srn, @bt)";

				//using SqlCommand insertCmd = new SqlCommand(insertSql, connection);
				//insertCmd.Parameters.AddWithValue("@nm", name);
				//insertCmd.Parameters.AddWithValue("@srn", surname);
				//insertCmd.Parameters.AddWithValue("@bt", birthDate);

				//insertCmd.ExecuteNonQuery();

				string sql = $"Select ID from Record where [name]='{name}' and surname='{surname}'" +
					$"and salary > @sal";

				using SqlCommand cmd = new SqlCommand(sql, connection);
				cmd.Parameters.AddWithValue("@sal", 700);

				List<int> idsToDelete = new List<int>();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						int id = (int)reader["ID"];

						//id = reader.GetInt32(reader.GetOrdinal("ID")); // alternative method to read values from reader

						idsToDelete.Add(id);

						//string recordName = reader["name"].ToString()!;
						//string? recordSurname = reader["surname"]?.ToString();

						//decimal? recordSalary = reader["salary"] as decimal?;

						//Console.WriteLine($"{recordName}\t |\t {recordSurname}\t |\t {recordSalary}");
					}
				}

				foreach (int id in idsToDelete)
				{
					sql = $"Delete from Record where ID={id}";

					using SqlCommand deleteCmd = new SqlCommand(sql, connection);

					deleteCmd.ExecuteNonQuery();
				}
			}
		}
	}
}