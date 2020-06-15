using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CsvHelper.Configuration;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Linq;
using System.Dynamic;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.Grade
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _repository;

        public GradeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGradeRepository>();
        }

        /* Upload CSV */
        private readonly List<string> Header = new List<string>()
        {
            "Grade", "Kode"
        };

        public List<string> CsvHeader => Header;

        public sealed class GradeMap : ClassMap<GradeViewModel>
        {
            public GradeMap()
            {
                Map(c => c.Type).Index(0);
                Map(c => c.Code).Index(1);
            }
        }

        private GradeViewModel MapToViewModel(GradeModel model)
        {
            var vm = new GradeViewModel()
            {
                LastModifiedUtc = model.LastModifiedUtc,
                Type = model.Type,
                Code = model.Code,
                IsAvalGrade = model.IsAvalGrade,
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

        public Task<int> Create(GradeViewModel viewModel)
        {
            var model = new GradeModel(viewModel.Type, viewModel.Code, viewModel.IsAvalGrade);

            return _repository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<GradeViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "Type", "Code"
            };

            query = QueryHelper<GradeModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GradeModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GradeModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new GradeViewModel()
            {
                Id = s.Id,
                Code = s.Code,
                IsAvalGrade = s.IsAvalGrade,
                Type = s.Type,
                LastModifiedUtc = s.LastModifiedUtc
            });

            return new ListResult<GradeViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<GradeViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        public Task<int> Update(int id, GradeViewModel viewModel)
        {
            var model = new GradeModel(viewModel.Type, viewModel.Code, viewModel.IsAvalGrade);
            return _repository.UpdateAsync(id, model);
        }

        public Task<int> Upload(IEnumerable<GradeViewModel> data)
        {
            var models = data.Select(s => new GradeModel(s.Type, s.Code, s.IsAvalGrade));

            return _repository.MultipleInsertAsync(models);
        }

        public Tuple<bool, List<object>> UploadValidate(IEnumerable<GradeViewModel> data)
        {
            List<object> ErrorList = new List<object>();
            string ErrorMessage;
            bool Valid = true;


            foreach (var item in data)
            {
                ErrorMessage = "";
                if (string.IsNullOrWhiteSpace(item.Type))
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Grade tidak boleh kosong, ");
                }
                else
                {
                    if (_repository.ReadAll().Any(d => d.Type == item.Type))
                    {
                        ErrorMessage = string.Concat(ErrorMessage, "Grade sudah terdaftar, ");
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
                else if (code >= 10)
                {
                    ErrorMessage = string.Concat(ErrorMessage, "Panjang Kode hanya boleh 3 digit, ");
                }
                else
                {
                    if (_repository.ReadAll().Any(d => d.Code == item.Code))
                    {
                        ErrorMessage = string.Concat(ErrorMessage, "Kode sudah terdaftar, ");
                    }
                }

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    ErrorMessage = ErrorMessage.Remove(ErrorMessage.Length - 2);
                    var Error = new ExpandoObject() as IDictionary<string, object>;

                    Error.Add("Jenis Lusi", item.Type);
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

        public MemoryStream DownloadTemplate()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {

                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        foreach (var item in CsvHeader)
                        {
                            csvWriter.WriteField(item);
                        }
                        csvWriter.NextRecord();
                    }
                }
                return stream;
            }
        }
    }
}
