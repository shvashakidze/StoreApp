namespace StoreApiProject.Model
{
	public class User
	{
		
		public string? organName { get; set; }

		public string? orgAddress { get; set; }

		public string? firstName { get; set; }

		public string? lastName { get; set; }

		public string? orgEmail { get; set; }

		public string? password { get; set; }

		public string? username { get; set; }


	}

	public class LoginAdmin
	{
		public string? username { get; set; }

		public string? password { get; set; }
        
		
	}

	public class LoginEmployee
	{
		public string? username { get; set; }

		public string? password { get; set; }

		public string? role { get; set; }
	}

	public class Employee
	{
		public int? id { get; set; }

		public int adminID { get; set; }

		public int terminalNumber { get; set; }


		public string? username { get; set;}
       
		public string? password { get; set; }

		public string? firstname { get; set; }

		public string? lastname { get; set;}

		public int role { get; set; }
	}
	public class Token
	{
		public int id { get; set; }
		public int adminID { get; set; }
		public int terminalNumber { get; set; }
		public int role { get; set; }
		
	}

}
