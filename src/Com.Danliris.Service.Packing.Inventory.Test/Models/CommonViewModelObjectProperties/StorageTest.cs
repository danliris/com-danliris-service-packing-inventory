using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.CommonViewModelObjectProperties
{
   public class StorageTest
    {
        [Fact]
        public void should_Success_Instantiate_Storage()
        {
            var unitStorage = new UnitStorage();
            Storage storage = new Storage()
            {
                Unit = unitStorage
            };

            Assert.Equal(unitStorage, storage.Unit);

        }

        [Fact]
        public void should_Success_Instantiate_UnitStorage()
        {
            DivisionStorage divison = new DivisionStorage();
            UnitStorage unit = new UnitStorage()
            {
                Name = "Name",
                Division = divison
            };

            Assert.Equal("Name", unit.Name);
            Assert.Equal(divison, unit.Division);
            
        }

        [Fact]
        public void should_Success_Instantiate_DivisionStorage()
        {
            DivisionStorage divison = new DivisionStorage()
            {
                Name = "Name"
            };

            Assert.Equal("Name", divison.Name);

        }
    }
}
