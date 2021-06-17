using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Xunit;
using System.Threading.Tasks;
using Moq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using System.Net;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceControllerGetPdfTest : GarmentShippingInvoiceControllerTest
    {
        public override GarmentShippingInvoiceViewModel ViewModel
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DL/210001",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }

        private GarmentShippingInvoiceViewModel ViewModel2
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DL/210002",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }
        //
        public GarmentShippingInvoiceViewModel ViewModel3
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "SM/210001",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }

        private GarmentShippingInvoiceViewModel ViewModel4
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "SM/210002",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }
         //
        public GarmentShippingInvoiceViewModel ViewModel5
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DS/210001",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }

        private GarmentShippingInvoiceViewModel ViewModel6
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DS/210002",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }
        //
        public GarmentShippingInvoiceViewModel ViewModel7
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DLR/210001",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }

        private GarmentShippingInvoiceViewModel ViewModel8
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DLR/210002",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }
       
        public GarmentShippingInvoiceViewModel ViewModel9
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DL/211002",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    //GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    //{
                    //    new GarmentShippingInvoiceAdjustmentViewModel
                    //    {
                    //        AdjustmentValue=1000,
                    //        AdjustmentDescription="AA",

                    //    }
                    //}
                };
            }
        }

        private GarmentPackingListViewModel packingListVM
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "SM/210001",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }

        private GarmentShippingInvoiceViewModel ViewModel4
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "SM/210002",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10,
                            Amount=99999999999,
                            Price=1332,
                            CMTPrice=1,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }
        //
        public GarmentShippingInvoiceViewModel ViewModel5
        {
            get
            {
                return new GarmentShippingInvoiceViewModel()
                {
                    InvoiceDate = DateTimeOffset.Now,
                    InvoiceNo = "DS/210001",
                    BuyerAgent = new BuyerAgent
                    {
                        Id = '1',
                        Code = "aa",
                        Name = "aa"
                    },
                    BankAccount = "aa",
                    BankAccountId = 1,
                    CO = "aa",
                    Description = "aa",
                    LCNo = "aa",
                    PackingListId = 1,
                    ShippingPer = "aa",
                    From = "aa",
                    To = "aa",

                    Items = new List<GarmentShippingInvoiceItemViewModel>()
                    {
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aa",
                            Quantity=1002,
                            Amount=(decimal)12222.01,
                            Price=1332,
                            RONo="roNo",
                            CMTPrice=0,
                            Uom= new UnitOfMeasurement
                            {
                                Id=1,
                                Unit="aa"
                            },
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=(decimal)1222.01,
                            Price=1332,
                            CMTPrice=129,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        },
                        new GarmentShippingInvoiceItemViewModel
                        {
                            ComodityDesc="aad",
                            Quantity=10021,
                            Amount=0,
                            Price=0,
                            CMTPrice=0,
                            RONo="roNo1",
                            Uom= new UnitOfMeasurement
                            {
                                Id=2,
                                Unit="abaa"
                            }
                        }
                    },
                    GarmentShippingInvoiceAdjustments = new List<GarmentShippingInvoiceAdjustmentViewModel>()
                    {
                        new GarmentShippingInvoiceAdjustmentViewModel
                        {
                            AdjustmentValue=1000,
                            AdjustmentDescription="AA",

                        }
                    }
                };
            }
        }

        private Buyer buyerVm
        {
            get
            {
                return new Buyer()
                {
                    Id=1,
                    Name="aa",
                    Code="aa",
                    Country="aa",
                    Address="aa",
                };
            }
        }

        private BankAccount bankVm
        {
            get
            {
                return new BankAccount()
                {
                   id=1,
                   accountName="aa",
                   Currency=new Currency
                   {
                       Id=1,
                       Code="aa",
                   },
                   AccountNumber="21231",
                   bankAddress="asdasda",
                   swiftCode="jskha"
                };
            }
        }

        [Fact]
        public async Task Should_Success_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1,"fob");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_CMT()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel2);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_CMT_MINUS()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_LongAmount()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            ViewModel.Items.First().Amount = (decimal)123456789012345612.007;
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Exception_GetPDF()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(default(GarmentShippingInvoiceViewModel));
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_GetPDF()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetPDF_LC()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetPDF_CMT_LC()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel2);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //  INVOICE SM FOB TT
        [Fact]
        public async Task Should_Success_GetPDF_SM()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel3);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }
        // INVOICE SM FOB LC
        [Fact]
        public async Task Should_Success_GetPDF_SM_LC()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel3);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }

        //  INVOICE SM CMT TT
        [Fact]
        public async Task Should_Success_GetPDF_SM_CMT()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel4);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //  INVOICE SM CMT LC
        [Fact]
        public async Task Should_Success_GetPDF_SM_CMT_LC()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel4);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //  INVOICE DS FOB TT
        [Fact]
        public async Task Should_Success_GetPDF_DS()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel5);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }
        // INVOICE DS FOB LC
        [Fact]
        public async Task Should_Success_GetPDF_DS_LC()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel5);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }

        //  INVOICE DS CMT TT
        [Fact]
        public async Task Should_Success_GetPDF_DS_CMT()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel6);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //  INVOICE DS CMT LC
        [Fact]
        public async Task Should_Success_GetPDF_DS_CMT_LC()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel6);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //
        //  INVOICE DLR FOB TT
        [Fact]
        public async Task Should_Success_GetPDF_DLR()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel7);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }
        // INVOICE DLR FOB LC
        [Fact]
        public async Task Should_Success_GetPDF_DLR_LC()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel7);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }

        //  INVOICE DLR CMT TT
        [Fact]
        public async Task Should_Success_GetPDF_DLR_CMT()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel8);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //  INVOICE DLR CMT LC
        [Fact]
        public async Task Should_Success_GetPDF_DLR_CMT_LC()
        {
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel8);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVMLC);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "cmt");

            Assert.NotNull(response);
        }
        //
        [Fact]
        public async Task Should_Success_GetPDF_FOB_WITHOUT_DHL()
        {
            //v
            var serviceMock = new Mock<IGarmentShippingInvoiceService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel9);
            serviceMock.Setup(s => s.GetBank(It.IsAny<int>())).Returns(bankVm);
            serviceMock.Setup(s => s.GetBuyer(It.IsAny<int>())).Returns(buyerVm);
            var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(packingListVM);
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, packingListService, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetPDF(1, "fob");

            Assert.NotNull(response);
        }
    }
}
