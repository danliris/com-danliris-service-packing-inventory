using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.DTOs
{
    public class IdAndCodeDto
    {
        public IdAndCodeDto(int id, string code)
        {
            Id = id;
            Code = code;
        }
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
