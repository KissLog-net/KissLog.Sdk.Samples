using System;

namespace KissLog.Samples.NetCore20.Exceptions
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
