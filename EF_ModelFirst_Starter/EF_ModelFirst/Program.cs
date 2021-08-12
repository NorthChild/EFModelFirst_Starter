using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EF_ModelFirst
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SouthwindContext())
            {

                Console.WriteLine("Creating some customers");

                //db.Add(new Customer() { CustomerId = "MART", City = "London", ContactName = "Martin", PostalCode = "N1" });
                //db.Add(new Customer() { CustomerId = "CATH", City = "Birmingham", ContactName = "Cathy", PostalCode = "AB1" });
                //db.SaveChanges();

                Console.WriteLine("Querying a customer");

                var customerQuery =
                    db.Customers.OrderBy(c => c.ContactName);

                var orderQuery =
                    db.Orders.OrderBy(c => c.OrderId);


                //var customer = customerQuery.First();
                var CustomerList = customerQuery.ToArray();

                //Console.WriteLine($"First Customer {customer.ContactName} who lives in {customer.City}");

                //customer.City = "New London";

                //Console.WriteLine($"First Customer {customer.ContactName} who lives in {customer.City}");


                // find cust with id of mart and add orders with that custId and now date with ship country of france and brazil 

                var findCust = db.Customers.Find("MART");

                //findCust.Orders.Add(new Order()
                //{
                //    CustomerId = "MART",
                //    OrderDate = new DateTime(2020),
                //    ShippedDate = new DateTime(2021),
                //    ShipCountry = "Italy"
                //});

                //findCust.Orders.Add(new Order()
                //{
                //    CustomerId = "MART",
                //    OrderDate = new DateTime(2020),
                //    ShippedDate = new DateTime(2022),
                //    ShipCountry = "Germany"
                //});

                //db.SaveChanges();


                foreach (var el in customerQuery.Include(c => c.Orders))
                {
                    Console.WriteLine($"{el.ContactName} lives in {el.City}");

                    if (el.Orders.Count > 0)
                    {
                        
                        foreach (var order in el.Orders)
                        {
                            Console.WriteLine($"Orders {order.OrderId} by {order.Customer.ContactName} made on {order.OrderDate.Value.Date}");
                        }


                    }
                }

                var customers = customerQuery.Include(c => c.Orders).ToArray();



                // DELETE

                Console.WriteLine($"Delete all Customers");

                //db.RemoveRange(customerQuery);
                //db.RemoveRange(orderQuery);
                //db.SaveChanges();

                Console.WriteLine($"there should be {db.Customers.Count()} Customers left and {db.Orders.Count()} Orders left");
              
            }
        }
    }
}
