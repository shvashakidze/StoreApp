namespace StoreApiProject.Model
{
	public class Product
	{
		
		public int id { get; set; }
		public string? productName { get; set; }

		public string? barCode { get; set; }

		public decimal price { get; set; }

		public int quantity { get; set; }


		public int goodsIn { get; set; }

		public int goodsOut { get; set; }

		public string? size { get; set; }

		public string? unit { get; set; }

		public int terminalNumber { get; set; }

		public int userID { get; set; }

		public DateTime date { get; set; }

	}
}
