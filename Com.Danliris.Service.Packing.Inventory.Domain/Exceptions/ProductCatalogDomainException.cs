using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Domain.Exceptions
{
    public class ProductCatalogDomainException : Exception
    {
        public ProductCatalogDomainException()
        { }

        public ProductCatalogDomainException(string message)
            : base(message)
        { }

        public ProductCatalogDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
