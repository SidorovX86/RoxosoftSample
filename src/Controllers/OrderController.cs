using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoxosoftSample.Model;
using Microsoft.EntityFrameworkCore;

namespace RoxosoftSample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : Controller
	{
		private RoxosoftDbContext dbContext;

		public OrderController(RoxosoftDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet()]
		public IActionResult GetOrderList()
		{
			return new ObjectResult(new SuccessRequestResult()
			{
				Result = dbContext.Orders
			});
		}

		[HttpGet("{orderId:int}")]
		public async Task<IActionResult> GetOrderDetails(int orderId)
		{
			if (orderId <= 0)
			{
				return BadRequest(new ErrorRequestResult() { Message = "������������� ������ ������ �������." });
			}

			Order order = await dbContext.Orders.Include(x => x.Entries).FirstOrDefaultAsync(x => x.Id == orderId);

			if (order == null)
			{
				return NotFound(new ErrorRequestResult() { Message = "����� �� ������." });
			}

			return new ObjectResult(new SuccessRequestResult()
			{
				Result = order
			});
		}
	}
}
