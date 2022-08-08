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
    public class GarmentShippingInvoiceControllerGetWHPdfTest : GarmentShippingInvoiceControllerTest
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
                            Quantity=102,
                            Amount=(decimal)1326.25,
                            Price=13,
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
                            Quantity=120,
                            Amount=(decimal)1440.34,
                            Price=12,
                            CMTPrice=(decimal)12.15,
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
                            Quantity=110,
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
                            AdjustmentValue=650,
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
                            Amount=(decimal)1250.70,
                            Price=1332,
                            CMTPrice=(decimal)11.35,
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
                            AdjustmentValue=650,
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
                            Amount=(decimal)1222.40,
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
                            Amount=(decimal)1150.30,
                            Price=1332,
                            CMTPrice=(decimal)12.25,
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
                            AdjustmentValue=650,
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
                            Quantity=100,
                            Amount=(decimal)1200.55,
                            Price=12,
                            CMTPrice=(decimal)7.85,
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
                            AdjustmentValue=350,
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
                            Quantity=102,
                            Amount=(decimal)1326.35,
                            Price=13,
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
                            Quantity=105,
                            Amount=(decimal)1260.48,
                            Price=12,
                            CMTPrice=0,
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
                            AdjustmentValue=650,
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
                            Quantity=102,
                            Amount=(decimal)1236.52,
                            Price=13,
                            CMTPrice=(decimal)8.48,
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
                            Quantity=102,
                            Amount=(decimal)1236.52,
                            Price=13,
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
                            Quantity=120,
                            Amount=(decimal)1560.48,
                            Price=13,
                            CMTPrice=0,
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
                            AdjustmentValue=350,
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
                            Quantity=105,
                            Amount=(decimal)1260.58,
                            Price=12,
                            CMTPrice=(decimal)7.54,
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
                            AdjustmentValue=350,
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
                            Quantity=102,
                            Amount=(decimal)1236.59,
                            Price=13,
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
                            Quantity=120,
                            Amount=(decimal)1440.28,
                            Price=12,
                            CMTPrice=0,
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
                return new GarmentPackingListViewModel()
                {
                    Remark = "aa",
                    SideMark = "aa",
                    ShippingMark = "aa",
                    GrossWeight = 12,
                    NettWeight = 12,
                    ShippingMarkImageFile = "/9j/4AAQSkZJRgABAQEAYABgAAD/4RDaRXhpZgAATU0AKgAAAAgABAE7AAIAAAAFAAAISodpAAQAAAABAAAIUJydAAEAAAAKAAAQyOocAAcAAAgMAAAAPgAAAAAc6gAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHVzZXIAAAAFkAMAAgAAABQAABCekAQAAgAAABQAABCykpEAAgAAAAM3OQAAkpIAAgAAAAM3OQAA6hwABwAACAwAAAiSAAAAABzqAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAyMDoxMTowNCAwODozNzoyMAAyMDIwOjExOjA0IDA4OjM3OjIwAAAAdQBzAGUAcgAAAP/hCxdodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+DQo8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIj48cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSJ1dWlkOmZhZjViZGQ1LWJhM2QtMTFkYS1hZDMxLWQzM2Q3NTE4MmYxYiIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIi8+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iPjx4bXA6Q3JlYXRlRGF0ZT4yMDIwLTExLTA0VDA4OjM3OjIwLjc4NzwveG1wOkNyZWF0ZURhdGU+PC9yZGY6RGVzY3JpcHRpb24+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPjxkYzpjcmVhdG9yPjxyZGY6U2VxIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpsaT51c2VyPC9yZGY6bGk+PC9yZGY6U2VxPg0KCQkJPC9kYzpjcmVhdG9yPjwvcmRmOkRlc2NyaXB0aW9uPjwvcmRmOlJERj48L3g6eG1wbWV0YT4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgPD94cGFja2V0IGVuZD0ndyc/Pv/bAEMABwUFBgUEBwYFBggHBwgKEQsK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",
                    SideMarkImageFile = "/9j/4AAQSkZJRgABAQEAYABgAAD/4RDaRXhpZgAATU0AKgAAAAgABAE7AAIAAAAFAAAISodpAAQAAAABAAAIUJydAAEAAAAKAAAQyOocAAcAAAgMAAAAPgAAAAAc6gAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHVzZXIAAAAFkAMAAgAAABQAABCekAQAAgAAABQAABCykpEAAgAAAAM3OQAAkpIAAgAAAAM3OQAA6hwABwAACAwAAAiSAAAAABzqAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAyMDoxMTowNCAwODozNzoyMAAyMDIwOjExOjA0IDA4OjM3OjIwAAAAdQBzAGUAcgAAAP/hCxdodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+DQo8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIj48cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSJ1dWlkOmZhZjViZGQ1LWJhM2QtMTFkYS1hZDMxLWQzM2Q3NTE4MmYxYiIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIi8+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iPjx4bXA6Q3JlYXRlRGF0ZT4yMDIwLTExLTA0VDA4OjM3OjIwLjc4NzwveG1wOkNyZWF0ZURhdGU+PC9yZGY6RGVzY3JpcHRpb24+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPjxkYzpjcmVhdG9yPjxyZGY6U2VxIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpsaT51c2VyPC9yZGY6bGk+PC9yZGY6U2VxPg0KCQkJPC9kYzpjcmVhdG9yPjwvcmRmOkRlc2NyaXB0aW9uPjwvcmRmOlJERj48L3g6eG1wbWV0YT4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgPD94cGFja2V0IGVuZD0ndyc/Pv/bAEMABwUFBgUEBwYFBggHBwgKEQsK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",
                    RemarkImageFile = "/9j/4AAQSkZJRgABAQEAYABgAAD/4RDaRXhpZgAATU0AKgAAAAgABAE7AAIAAAAFAAAISodpAAQAAAABAAAIUJydAAEAAAAKAAAQyOocAAcAAAgMAAAAPgAAAAAc6gAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHVzZXIAAAAFkAMAAgAAABQAABCekAQAAgAAABQAABCykpEAAgAAAAM3OQAAkpIAAgAAAAM3OQAA6hwABwAACAwAAAiSAAAAABzqAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAyMDoxMTowNCAwODozNzoyMAAyMDIwOjExOjA0IDA4OjM3OjIwAAAAdQBzAGUAcgAAAP/hCxdodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+DQo8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIj48cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSJ1dWlkOmZhZjViZGQ1LWJhM2QtMTFkYS1hZDMxLWQzM2Q3NTE4MmYxYiIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIi8+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iPjx4bXA6Q3JlYXRlRGF0ZT4yMDIwLTExLTA0VDA4OjM3OjIwLjc4NzwveG1wOkNyZWF0ZURhdGU+PC9yZGY6RGVzY3JpcHRpb24+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPjxkYzpjcmVhdG9yPjxyZGY6U2VxIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpsaT51c2VyPC9yZGY6bGk+PC9yZGY6U2VxPg0KCQkJPC9kYzpjcmVhdG9yPjwvcmRmOkRlc2NyaXB0aW9uPjwvcmRmOlJERj48L3g6eG1wbWV0YT4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgPD94cGFja2V0IGVuZD0ndyc/Pv/bAEMABwUFBgUEBwYFBggHBwgKEQsK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",
                    Measurements = new List<GarmentPackingListMeasurementViewModel>()
                    {
                        new GarmentPackingListMeasurementViewModel
                        {
                            Width=1,
                            Height=1,
                            CartonsQuantity=1,
                            Length=1,

                        }
                    }
                };
            }
        }

        private GarmentPackingListViewModel packingListVMLC
        {
            get
            {
                return new GarmentPackingListViewModel()
                {
                    Remark = "aa",
                    SideMark = "aa",
                    ShippingMark = "aa",
                    GrossWeight = 12,
                    NettWeight = 12,
                    PaymentTerm = "LC",
                    LCDate = DateTimeOffset.Now,
                    Measurements = new List<GarmentPackingListMeasurementViewModel>()
                    {
                        new GarmentPackingListMeasurementViewModel
                        {
                            Width=1,
                            Height=1,
                            CartonsQuantity=1,
                            Length=1,

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
                    Id = 1,
                    Name = "aa",
                    Code = "aa",
                    Country = "aa",
                    Address = "aa",
                };
            }
        }

        private BankAccount bankVm
        {
            get
            {
                return new BankAccount()
                {
                    id = 1,
                    accountName = "aa",
                    Currency = new Currency
                    {
                        Id = 1,
                        Code = "aa",
                    },
                    AccountNumber = "21231",
                    bankAddress = "asdasda",
                    swiftCode = "jskha"
                };
            }
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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "fob");

            Assert.NotNull(response);
        }
        // INVOICE DS FOB LC
        [Fact]
        public async Task Should_Success_GetPDF_DS_LC()
        {
            //
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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "fob");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "cmt");

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
            var response = await controller.GetWHPDF(1, "fob");

            Assert.NotNull(response);
        }
    }
}