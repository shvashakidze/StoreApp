using StoreApiProject.Model;
using System.Data.SqlClient;
using System.Data;

namespace StoreApiProject.Repositories
{

	public interface IUserRepository
	{
		public void CreateOrganization(User user);

		List<Employee> GetEmployees(int adminId);

		List<Employee> GetUsers(string adminId);

		List<Employee> GetUserById(string adminId, int id);
		public void CreateEmployee(Employee employee);

		public void DeleteEmployee(int id, int userID);

		public void UpdateEmployee(Employee employee, int userID);

		public Token Login(LoginAdmin user);
	}
	public class userRepository : IUserRepository
	{

		public IConfiguration _configuration { get; set; }
		public string connection;

		public userRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			connection = _configuration.GetConnectionString("DefaultConnection");
		}
		public void CreateOrganization(User user)
		{

			var terminalNumber = 1;
			var role = 1;

			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.CreateOrg";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = user.firstName;
				cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = user.lastName;
				cmd.Parameters.Add("@OrgAddress", SqlDbType.NVarChar).Value = user.orgAddress;
				cmd.Parameters.Add("@orgName", SqlDbType.NVarChar).Value = user.organName;
				cmd.Parameters.Add("@orgEmail", SqlDbType.NVarChar).Value = user.orgEmail;
				cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.username;
				cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.password;
				cmd.Parameters.Add("@role", SqlDbType.Int).Value = role;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = terminalNumber;

				cmd.ExecuteNonQuery();
			}
		}

		

		public List<Employee> GetEmployees(int adminId)
		{
			List<Employee> employeeList = new List<Employee>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetEmployees";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@adminId", SqlDbType.Int).Value = adminId;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Employee employee = new();
					employee.id = Convert.ToInt32(reader["id"].ToString());
					employee.firstname = reader["firstname"].ToString();
					employee.lastname = reader["lastname"].ToString();
					employee.role = Convert.ToInt32(reader["role"].ToString());
					employee.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());

					employeeList.Add(employee);
				}

			}

			return employeeList;
		}

		public void CreateEmployee(Employee employee)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.CreateEmployee";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = employee.firstname;
				cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = employee.lastname;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = employee.adminID;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = employee.terminalNumber;
				cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = employee.username;
				cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = employee.password;
				cmd.Parameters.Add("@role", SqlDbType.Int).Value = employee.role;

				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteEmployee(int id, int userID)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.DeleteEmployee";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = userID;

				cmd.ExecuteNonQuery();

			}
		}

		public void UpdateEmployee(Employee employee, int userID)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.UpdateEmployee";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = employee.id;
                cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = employee.firstname;
				cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = employee.lastname;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = userID;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = employee.terminalNumber;
				cmd.Parameters.Add("@role", SqlDbType.Int).Value = employee.role;


				cmd.ExecuteNonQuery();

			}
		}

		public List<Employee> GetUsers(string adminId)
		{
			List<Employee> employeeList = new List<Employee>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetUsers";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = adminId;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Employee employee = new();
					employee.id = Convert.ToInt32(reader["id"].ToString());
					employee.firstname = reader["firstname"].ToString();
					employee.lastname = reader["lastname"].ToString();
					employee.role = Convert.ToInt32(reader["role"].ToString());
					employee.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());

					employeeList.Add(employee);
				}

			}

			return employeeList;
		}

		public Token Login(LoginAdmin user)
		{
			Token userData = null;

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				command.CommandText = "dbo.LoginAdmin";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@username", user.username);
				command.Parameters.AddWithValue("@password", user.password);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						userData = new Token
						{
							id = reader.GetInt32(reader.GetOrdinal("id")),
							adminID = reader.GetInt32(reader.GetOrdinal("adminID")),
							terminalNumber = reader.GetInt32(reader.GetOrdinal("terminalNumber")),
							role = reader.GetInt32(reader.GetOrdinal("role"))
							
						};
					}
				}

				reader.Close();
			}

			return userData;
		}

		public List<Employee> GetUserById(string adminId, int id)
		{
			List<Employee> userById = new List<Employee>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetUsersById";
				cmd.CommandType = CommandType.StoredProcedure;
				
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = adminId;
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Employee user = new();
					
					user.firstname = reader["firstName"].ToString();
					user.lastname = reader["lastname"].ToString();
					


					userById.Add(user);
				}
			}

			return userById;
		}
	}
}
