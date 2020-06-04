using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListService : IGarmentPackingListService
    {
        public Task<int> Create(GarmentPackingListViewModel viewModel)
        {
            return Task.FromResult(1);
        }

        public ListResult<GarmentPackingListViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var data = new List<GarmentPackingListViewModel>()
            {
                new GarmentPackingListViewModel()
            };

            return new ListResult<GarmentPackingListViewModel>(data, 1, 1, 1);
        }

        public Task<GarmentPackingListViewModel> ReadById(int id)
        {
            var data = new GarmentPackingListViewModel()
            {
                Items = new List<GarmentPackingListItemViewModel>
                    {
                        new GarmentPackingListItemViewModel
                        {
                            Details = new List<GarmentPackingListDetailViewModel>
                            {
                                new GarmentPackingListDetailViewModel
                                {
                                    Sizes = new List<DetailSize>
                                    {
                                        new DetailSize()
                                    }
                                }
                            }
                        }
                    },
                Measurements = new List<GarmentPackingListMeasurementViewModel>
                    {
                        new GarmentPackingListMeasurementViewModel()
                    }
            };

            return Task.FromResult(data);
        }

        public Task<int> Update(int id, GarmentPackingListViewModel viewModel)
        {
            return Task.FromResult(1);
        }

        public Task<int> Delete(int id)
        {
            return Task.FromResult(1);
        }
    }
}
