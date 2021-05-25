using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework12.Models;

namespace Homework12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDBContext orderDb;

        public OrdersController(OrderDBContext context)
        {
            orderDb = context;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            IQueryable<Order> query = orderDb.Orders.Include(o => o.orderDetailsList);
            return query.ToList();
        }

        
        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = orderDb.Orders.Include(o => o.orderDetailsList).FirstOrDefault(t => t.OrderId == id);
            if(order ==null)
            {
                return NotFound();
            }
            return order;
        }

        //Get: api/Orders/pageQuery?clientIn=刘雨辛&&orderNameIn=橡皮
        [HttpGet("pageQuery")]
        public ActionResult<List<Order>> queryOrder(string clientIn,string orderNameIn, double orderPriceIn, int orderNumIn)
        {
            IQueryable<Order> query = orderDb.Orders;
            if(clientIn!=null)
            {
                query = query.Include(o => o.orderDetailsList).Where(t => t.client.Contains(clientIn));
            }

            if(orderNameIn!=null)
            {
                query = query.Include(o => o.orderDetailsList)
                    .ThenInclude(d => d.orderName)
                    .Where(order => order.orderDetailsList
                    .Any(item => item.orderName == orderNameIn));
            }

            if(orderPriceIn!=0)
            {
                query = query.Include(o => o.orderDetailsList)
                    .ThenInclude(d => d.orderPrice)
                    .Where(order => order.orderDetailsList
                    .Any(item => item.orderPrice == orderPriceIn));
            }

            if(orderNumIn!=0)
            {
                query = query.Include(o => o.orderDetailsList)
                    .ThenInclude(d => d.orderNum)
                    .Where(order => order.orderDetailsList
                    .Any(item => item.orderNum == orderNumIn));
            }
            
            return query.ToList();
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<Order> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Not found");
            }

            try
            {
                orderDb.Entry(order).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch(Exception e)
            {
                string error = e.Message;
                if(e.InnerException != null)
                {
                    error = e.InnerException.Message;
                }
                return BadRequest(error);
            }
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            try
            {
                orderDb.Orders.Add(order);
                orderDb.SaveChanges();
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return order;
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = orderDb.Orders.Include("orderDetailsList").SingleOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                orderDb.OrderDetails.RemoveRange(order.orderDetailsList);
                orderDb.Orders.Remove(order);
                orderDb.SaveChanges();
            }
            return NoContent();
        }
        
    }
}
