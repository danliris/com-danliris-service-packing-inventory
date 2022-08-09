using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Utilities
{
    public class CurrencyToTextTest
    {
        [Fact]
        public void Should_Success_ConvertWithoutDecimal()
        {
            var currency = CurrencyToText.ToWords(123123);
            Assert.NotEmpty(currency);
        }

        [Fact]
        public void Should_Success_ConvertWithDecimal()
        {
            var currency = CurrencyToText.ToWords((decimal)30.45);
            Assert.NotEmpty(currency);
        }

        [Fact]
        public void Should_Success_ConvertWithDecimal1()
        {
            var currency = CurrencyToText.ToWords((decimal)1730.70);
            Assert.NotEmpty(currency);
        }
    }
}
