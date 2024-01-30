using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.Inventory
{
    [Produces("application/json")]
    [Route("v1/inventory/inventory-packings")]
    [Authorize]
    public class InventoryPackingController : Controller
    {
    }
}
