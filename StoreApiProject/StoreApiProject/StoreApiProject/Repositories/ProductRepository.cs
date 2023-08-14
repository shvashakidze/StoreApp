using StoreApiProject.Model;
using System.Data.SqlClient;
using System.Data;

namespace StoreApiProject.Repositories
{
	public interface IProductRepository
	{
		List<Product> GetProductlist(int userID, string terminalNumber);

		List<Product> GetAllProducts(int userID);

		List<Product> GetAllProductsInList(int userID);

		public List<Product> GetAllProductsOutList(int userID);
		public void SaveProduct(Product product);


		public void SaveProductOut(Product product);

		public void DeleteProduct(int id, int userID, int terminalNumber);

		public void UpdateProduct(Product product);

		List<Product> GetProductById(int userID, int id, int terminalNumber);
	}
	public class ProductRepository : IProductRepository
	{
		public IConfiguration _configuration { get; set; }
		public string connection;

		public ProductRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			connection = _configuration.GetConnectionString("DefaultConnection");
		}
		public void DeleteProduct(int id, int userID, int terminalNumber)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.DeleteProduct";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
				cmd.Parameters.Add("@adminID", SqlDbType.Int).Value = userID;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = terminalNumber;

				cmd.ExecuteNonQuery();

			}
		}

		public List<Product> GetAllProducts(int userID)
		{

			List<Product> productList = new List<Product>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetAllProducts";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Product product = new();
					product.id = Convert.ToInt32(reader["id"].ToString());
					product.productName = reader["productName"].ToString();
					product.barCode = reader["barCode"].ToString();
					product.price = Convert.ToDecimal(reader["price"].ToString());
					product.quantity = Convert.ToInt32(reader["quantity"].ToString());
					product.size = reader["size"].ToString();
					product.unit = reader["unit"].ToString();
					product.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());

					productList.Add(product);
				}

			}

			return productList;
		}

		public List<Product> GetProductlist(int userID, string terminalNumber)
		{

			List<Product> productList = new List<Product>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetProducts";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.NVarChar).Value = terminalNumber;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Product product = new();
					product.id = Convert.ToInt32(reader["id"].ToString());
					product.productName = reader["productName"].ToString();
					product.barCode = reader["barCode"].ToString();
					product.price = Convert.ToDecimal(reader["price"].ToString());
					product.quantity = Convert.ToInt32(reader["quantity"].ToString());
					product.size = reader["size"].ToString();
					product.unit = reader["unit"].ToString();
					product.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());

					productList.Add(product);
				}

			}

			return productList;
		}

		public void SaveProduct(Product product)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.CreateProduct";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@productName", SqlDbType.NVarChar).Value = product.productName;
				cmd.Parameters.Add("@barCode", SqlDbType.NVarChar).Value = product.barCode;
				cmd.Parameters.Add("@price", SqlDbType.Int).Value = product.price;
				cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = product.quantity;
				cmd.Parameters.Add("@goodsIn", SqlDbType.Int).Value = product.goodsIn;
				cmd.Parameters.Add("@size", SqlDbType.NVarChar).Value = product.size;
				cmd.Parameters.Add("@unit", SqlDbType.NVarChar).Value = product.unit;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.VarChar).Value = product.terminalNumber;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = product.userID;
				cmd.Parameters.Add("@date", SqlDbType.Date).Value = product.date;

				cmd.ExecuteNonQuery();
			}
		}

		public void UpdateProduct(Product product )
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.UpdateProduct";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = product.id;
				cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = product.quantity;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = product.terminalNumber;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = product.userID;
				cmd.Parameters.Add("@productName", SqlDbType.NVarChar).Value = product.productName;
				cmd.Parameters.Add("@barCode", SqlDbType.NVarChar).Value = product.barCode;
				cmd.Parameters.Add("@price", SqlDbType.Int).Value = product.price;
				cmd.Parameters.Add("@goodsIn", SqlDbType.Int).Value = product.goodsIn;
				cmd.Parameters.Add("@size", SqlDbType.NVarChar).Value = product.size;
				cmd.Parameters.Add("@unit", SqlDbType.NVarChar).Value = product.unit;
				cmd.Parameters.Add("@date", SqlDbType.Date).Value = product.date;

				cmd.ExecuteNonQuery();

			}
		}

		public List<Product> GetProductById(int userID, int id, int terminalNumber)
		{
			List<Product> productList = new List<Product>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetProductById";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.Int).Value = terminalNumber;
				cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Product product = new();
					product.id = Convert.ToInt32(reader["id"].ToString());
					product.productName = reader["productName"].ToString();
					product.barCode = reader["barCode"].ToString();
					product.price = Convert.ToDecimal(reader["price"].ToString());
					product.quantity = Convert.ToInt32(reader["quantity"].ToString());
					product.size = reader["size"].ToString();
					product.unit = reader["unit"].ToString();


					productList.Add(product);
				}
			}
			
			   return productList;
		}

		public List<Product> GetAllProductsInList(int userID)
		{
			List<Product> productList = new List<Product>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetAllProductsInList";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Product product = new();
					product.id = Convert.ToInt32(reader["id"].ToString());
					product.productName = reader["productName"].ToString();
					product.barCode = reader["barCode"].ToString();
					product.price = Convert.ToDecimal(reader["price"].ToString());
					product.quantity = Convert.ToInt32(reader["quantity"].ToString());
					product.size = reader["size"].ToString();
					product.unit = reader["unit"].ToString();
					product.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());
					DateTime dateValue = reader.GetDateTime(reader.GetOrdinal("date"));
					product.date = dateValue;

					productList.Add(product);
				}

			}

			return productList;
		}

		public List<Product> GetAllProductsOutList(int userID)
		{
			List<Product> productList = new List<Product>();

			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.GetAllProductsOutList";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Product product = new();
					product.id = Convert.ToInt32(reader["id"].ToString());
					product.productName = reader["productName"].ToString();
					product.barCode = reader["barCode"].ToString();
					product.price = Convert.ToDecimal(reader["price"].ToString());
					product.quantity = Convert.ToInt32(reader["quantity"].ToString());
					product.size = reader["size"].ToString();
					product.unit = reader["unit"].ToString();
					product.terminalNumber = Convert.ToInt32(reader["terminalNumber"].ToString());
					DateTime dateValue = reader.GetDateTime(reader.GetOrdinal("date"));
					product.date = dateValue;

					productList.Add(product);
				}

			}

			return productList;
		}

		

		public void SaveProductOut(Product product)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{

				conn.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "dbo.ProductOut";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@productName", SqlDbType.NVarChar).Value = product.productName;
				cmd.Parameters.Add("@barCode", SqlDbType.NVarChar).Value = product.barCode;
				cmd.Parameters.Add("@price", SqlDbType.Int).Value = product.price;
				cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = product.quantity;
				cmd.Parameters.Add("@size", SqlDbType.NVarChar).Value = product.size;
				cmd.Parameters.Add("@unit", SqlDbType.NVarChar).Value = product.unit;
				cmd.Parameters.Add("@terminalNumber", SqlDbType.VarChar).Value = product.terminalNumber;
				cmd.Parameters.Add("@userID", SqlDbType.Int).Value = product.userID;
				cmd.Parameters.Add("@date", SqlDbType.Date).Value = product.date;

				cmd.ExecuteNonQuery();
			}
		}
	}
}
