using StoreApiProject.Model;
using System.Data.SqlClient;
using System.Data;

namespace StoreApiProject.Repositories
{

	public interface ITerminalRepository
	{
		public void DeleteTerminal(int id, int userID);
		public void CreateTerminal(Terminal terminal);
        List<Terminal> GetTerminallist(int userId);
	}
	public class TerminalRepository : ITerminalRepository
	{
		public IConfiguration _configuration { get; set; }
		public string connection;

		public TerminalRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			connection = _configuration.GetConnectionString("DefaultConnection");
		}
		public void CreateTerminal(Terminal terminal)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.CreateTerminal";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = terminal.terminalNumber;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = terminal.adminID;
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteTerminal(int id, int userID)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.DeleteTerninal";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = userID;

				cmd.ExecuteNonQuery();

			}
		}

		public List<Terminal> GetTerminallist(int userId)
		{
			List<Terminal> employeeList = new List<Terminal>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetTerminals";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@adminId", SqlDbType.Int).Value = userId;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Terminal terminal = new();
					terminal.id = Convert.ToInt32(reader["id"].ToString());
					terminal.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());

					employeeList.Add(terminal);
				}

			}

			return employeeList;
		}
	}
}
