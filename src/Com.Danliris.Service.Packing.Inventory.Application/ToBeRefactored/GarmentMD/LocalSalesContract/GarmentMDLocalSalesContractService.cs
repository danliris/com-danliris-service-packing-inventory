using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract
{
    public class GarmentMDLocalSalesContractService : IGarmentMDLocalSalesContractService
    {
        private readonly IGarmentMDLocalSalesContractRepository _repository;
        private readonly IServiceProvider serviceProvider;
        protected readonly ILogHistoryRepository logHistoryRepository;

        public GarmentMDLocalSalesContractService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentMDLocalSalesContractRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
            this.serviceProvider = serviceProvider;
        }

        private string GenerateNo(GarmentMDLocalSalesContractViewModel vm)
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.ToString("MM");

            var prefix = $"DL/GMT/{(vm.transactionType.code ?? "").Trim().ToUpper()}/{month}/{year}";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.SalesContractNo.StartsWith(prefix))
                .OrderByDescending(o => o.SalesContractNo)
                .Select(s => int.Parse(s.SalesContractNo.Replace(prefix, "")))
                .FirstOrDefault();

            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D4")}";

            return invoiceNo;
        }

        private GarmentMDLocalSalesContractViewModel MapToViewModel(GarmentMDLocalSalesContractModel model)
        {
            GarmentMDLocalSalesContractViewModel viewModel = new GarmentMDLocalSalesContractViewModel
            {
                Active = model.Active,
                Id = model.Id,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,

                salesContractNo = model.SalesContractNo,
                salesContractDate = model.SalesContractDate,
                transactionType = new TransactionType
                {
                    id = model.TransactionTypeId,
                    code = model.TransactionTypeCode,
                    name = model.TransactionTypeName
                },
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                    npwp = model.BuyerNPWP,
                    Address = model.BuyerAddress
                },
                isUseVat = model.IsUseVat,
                vat = new Vat
                {
                    id = model.VatId,
                    rate = model.VatRate,
                },
                sellerAddress = model.SellerAddress,
                sellerName = model.SellerName,
                sellerNPWP = model.SellerNPWP,
                sellerPosition = model.SellerPosition,

                comodityName = model.ComodityName,
                isLocalSalesDOCreated = model.IsLocalSalesDOCreated,
                quantity = model.Quantity,
                remainingQuantity = model.RemainingQuantity,
                uom = new UnitOfMeasurement
                {
                    Id = model.UomId,
                    Unit = model.UomUnit
                },

                remark = model.Remark,
                price = model.Price,

                subTotal = model.SubTotal,
            };

            return viewModel;
        }

        private GarmentMDLocalSalesContractModel MapToModel(GarmentMDLocalSalesContractViewModel vm)
        {
            vm.uom = vm.uom ?? new UnitOfMeasurement();
            vm.transactionType = vm.transactionType ?? new TransactionType();
            vm.buyer = vm.buyer ?? new Buyer();
            vm.vat = vm.vat ?? new Vat();

            return new GarmentMDLocalSalesContractModel(GenerateNo(vm), vm.salesContractDate.GetValueOrDefault(), vm.transactionType.id, vm.transactionType.code, vm.transactionType.name, vm.sellerName, vm.sellerPosition, vm.sellerAddress, vm.sellerNPWP, vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.buyer.Address, vm.buyer.npwp, vm.isUseVat, vm.vat.id, vm.vat.rate, vm.subTotal, vm.isLocalSalesDOCreated, vm.comodityName, vm.quantity, vm.remainingQuantity, vm.uom.Id.Value, vm.uom.Unit, vm.price, vm.remark) { Id = vm.Id };
        }

        public Task<int> Create(GarmentMDLocalSalesContractViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
