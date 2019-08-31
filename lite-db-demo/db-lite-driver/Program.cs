using System;
using LiteDB;

namespace db_lite_driver
{

    // Create your POCO class
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteRepository(@"../../../Customer.litedb"))
            {
                var id = db.Insert(new Customer()
                {
                    Name = "John Doe",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    Age = 39,
                    IsActive = true
                });

                var customer = db.SingleById<Customer>(99);
                customer.Name = "Very Old";
                customer.Age = 500;

                var zz = db.Update(customer);

                

                // query using fluent query
                var result = db.Query<Customer>()
                    .Where(x => x.Age > 499) // used indexes query
                    .ToList();

                
            }
        }
    }
}
