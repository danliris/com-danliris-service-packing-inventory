using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CsvHelper.Configuration;
using System.Linq;
using System.Dynamic;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WeftType
{
    public class WeftTypeService : IWeftTypeService
    {
        private readonly IWeftTypeRepository _repository;

        public WeftTypeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IWeftTypeRepository>();
        }

        /* Upload CSV */
        private readonly List<string> Header = new List<string>()
        {
            "Jenis Pakan", "Kode"
        };

        public List<string> CsvHeader => Header;

        public sealed class WeftTypeMap : ClassMap<WeftTypeViewModel>
        {
            public WeftTypeMap()
            {
                Map(c => c.Type).Index(0);
                Map(c => c.Code).Index(1);
            }
        }

        private WeftTypeViewModel MapToViewModel(WeftTypeModel model)
        {
            var vm = new WeftTypeViewModel()
            {
                LastModifiedUtc = model.LastModifiedUtc,
                Type = model.Type,
                Code = model.Code,
                Id = model.Id,
                Active = model.Active,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy
            };

            return vm;
        }


        public Task<int> Create(WeftTypeViewModel viewModel)
        {
            var model = new WeftTypeModel(viewModel.Type, viewModel.Code);

            return _repository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<WeftTypeViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "Type", "Code"
            };

            query = QueryHelper<WeftTypeModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<WeftTypeModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<WeftTypeModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new WeftTypeViewModel()
            {
                Id = s.Id,
                Code = s.Code,
                Type = s.Type,
                LastModifiedUtc = s.LastModifiedUtc
            });

            return new ListResult<WeftTypeViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<WeftTypeViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        public Task<int> Update(int id, WeftTypeViewModel viewModel)
        {
            var model = new WeftTypeModel(viewModel.Type, viewModel.Code);
            return _repository.UpdateAsync(id, model);
        }

        public Task<int> Upload(IEnumerable<WeftTypeViewModel> data)
        {
            var models = data.Select(s => new WeftTypeModel(s.Type, s.Code));

            return _repository.MultipleInsertAsync(models);
        }

        public Tuple<bool, List<object>> UploadValidate(IEnumerable<WeftTypeViewModel> data)
        {
            List<object> ErrorList = new List<object>();
            string ErrorMessage;
            bool Valid = true;


            foreach (var item in data)
            {
                ErrorMessage = "";
                if (string.IsNullOrWhiteSpace(item.Type))
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Jenis Pakan tidak boleh kosong, ");
                }
                else
                {
                    if (_repository.ReadAll().Any(d => d.Type == item.Type))
                    {
                        ErrorMessage = string.Concat(ErrorMessage, "Jenis Pakan sudah terdaftar, ");
                    }
                }

                int code = 0;
                if (string.IsNullOrWhiteSpace(item.Code))
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Kode tidak boleh kosong, ");
                }
                else if (!int.TryParse(item.Code, out code))
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Kode harus numerik, ");
                }
                else if (code <= 0)
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Kode harus lebih besar dari 0, ");
                }
                else if (code >= 100)
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Panjang Kode hanya boleh 2 digit, ");
                }
                else
                {
                    var stringCode = code.ToString().PadLeft(2, '0');
                    if (_repository.ReadAll().Any(d => d.Code == stringCode))
                    {
                        ErrorMessage = string.Concat(ErrorMessage, "Kode sudah terdaftar, ");
                    }
                }

                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    item.Code = code.ToString().PadLeft(2, '0');
                }
                else
                {
                    ErrorMessage = ErrorMessage.Remove(ErrorMessage.Length - 2);
                    var Error = new ExpandoObject() as IDictionary<string, object>;

                    Error.Add("jenis Pakan", item.Type);
                    Error.Add("Kode", item.Code);
                    Error.Add("Error", ErrorMessage);
                    ErrorList.Add(Error);

                }

            }

            if (ErrorList.Count > 0)
            {
                Valid = false;
            }

            return Tuple.Create(Valid, ErrorList);
        }

        public bool ValidateHeader(IEnumerable<string> headers)
        {
            return CsvHeader.SequenceEqual(headers, StringComparer.OrdinalIgnoreCase);
        }
    }
}
