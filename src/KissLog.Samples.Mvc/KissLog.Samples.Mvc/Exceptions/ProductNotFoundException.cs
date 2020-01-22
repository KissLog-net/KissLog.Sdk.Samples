using System;

namespace KissLog.Samples.Mvc.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public int ProductId { get; set; }

        public ProductNotFoundException(int productId) : base(ErrorMessage())
        {
            ProductId = productId;
        }

        private static string ErrorMessage()
        {
            return "Product was not found";
        }
    }
}