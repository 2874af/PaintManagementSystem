using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paint;
using Paint.Models;

namespace PaintAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class PaintProductController : ControllerBase
    {
        [HttpGet]
        public PaintProduct GetPaintProduct()
        {
            //assume data from database
            PaintProduct paintProduct = new PaintProduct();

            return paintProduct;
        }
    }
}