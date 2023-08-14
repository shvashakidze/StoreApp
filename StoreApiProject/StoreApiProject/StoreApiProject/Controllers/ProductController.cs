using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApiProject.Model;
using StoreApiProject.Repositories;
using System.Security.Claims;

namespace StoreApiProject.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			this._productRepository = productRepository;
		}
		[HttpPost]
		[Authorize]
		public IActionResult SaveProduct(Product product )

		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);
			var terminalNumber = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value);

			if (product == null)
			{
				return BadRequest("product data is empty");
			}

			
			var newProduct = new Product
				{

				productName = product.productName,
				barCode = product.barCode,
				price = product.price,
				quantity = product.quantity,
				goodsIn = product.goodsIn,
				size = product.size,
				unit = product.unit,
				terminalNumber = terminalNumber,
				userID = userId,
				date = DateTime.UtcNow
			};

				_productRepository.SaveProduct(newProduct);
			 

			return Ok(newProduct);
		
		
}

		

		[HttpDelete]
		[Authorize]
		public IActionResult DeleteProduct(int id)
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);
			var terminalNumber = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value);
			try { 
			_productRepository.DeleteProduct(id, userId, terminalNumber);

			return Ok();
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetAllProducts()
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);
			
			try { 
			List<Product> productList = _productRepository.GetAllProducts(userId);

			return Ok(productList);
		}
		catch
		  {
				return StatusCode(500, "system error");
	      }
}

		[HttpGet]
		[Authorize]
		public IActionResult GetProducts()
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);
			var terminalNumber = User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value;

			try { 
			List<Product> productList = _productRepository.GetProductlist(userId, terminalNumber);

			return Ok(productList);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetProductById(int id )
		{
			var terminalNumber = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value);
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);

			try { 
			List<Product> productList = _productRepository.GetProductById(userId, id, terminalNumber);

			return Ok(productList);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpPut]
		[Authorize]
		public IActionResult UpdateProduct(Product  product )
		{
			var terminalNumber = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value);
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);

			try { 
			var productList = new Product
			{

				id = product.id,
				quantity = product.quantity,
				terminalNumber = terminalNumber,
				userID = userId
			};


			_productRepository.UpdateProduct(productList);

			return Ok();
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetAllProductsInList()
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);


			try { 
			List<Product> productList = _productRepository.GetAllProductsInList(userId);

			return Ok(productList);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetAllProductsOutList()
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);


			try { 
			List<Product> productList = _productRepository.GetAllProductsOutList(userId);

			return Ok(productList);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpPost]
		[Authorize]
		public IActionResult SaveProductIn(Product product)

		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);
			var terminalNumber = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value);

			if (product == null)
			{
				return BadRequest("product data is empty");
			}
			try { 
			var updateProduct = new Product
			{

				id = product.id,
				quantity = product.quantity,
				terminalNumber = terminalNumber,
				userID = userId,
				productName = product.productName,
				barCode = product.barCode,
				price = product.price,
				goodsIn = product.goodsIn,
				size = product.size,
				unit = product.unit,
				date = DateTime.UtcNow
			};


			_productRepository.UpdateProduct(updateProduct);

			

			return Ok();
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpPost]
		[Authorize]
		public IActionResult SaveProductOut(Product product)

		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value);
			var terminalNumber = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.SerialNumber).Value);

			if (product == null)
			{
				return BadRequest("product data is empty");
			}

			try
			{
				var updateProduct = new Product
				{

					id = product.id,
					quantity = product.quantity,
					terminalNumber = terminalNumber,
					userID = userId
				};

				var productOut = new Product
				{

					productName = product.productName,
					barCode = product.barCode,
					price = product.price,
					quantity = product.goodsOut,
					size = product.size,
					unit = product.unit,
					terminalNumber = terminalNumber,
					userID = userId,
					date = DateTime.UtcNow
				};

				_productRepository.UpdateProduct(updateProduct);

				_productRepository.SaveProductOut(productOut);

				return Ok(productOut);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}


	}
}




