using System;

namespace TestConnectDB.Model
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Status { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
    }
}
