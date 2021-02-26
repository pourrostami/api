using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stone.Core.DTOs.AdminDTO;
using Stone.Core.DTOs.UserDTO;
using Stone.Core.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AdminLogin(AdminViewModelDto adminViewModelDto)
        {

            if (adminViewModelDto == null)
            {
                return BadRequest(new { message = "Please Enter The Admin Mobile and Password" });
            }
            var admin = await _adminService.Login(adminViewModelDto);
            return Ok(admin);
        }


        [HttpPost("[action]")]
        public IActionResult CreatNewProduct(ProductViewModelDto productViewModelDto)
        {
            if (productViewModelDto == null)
                return null;
            var products = _adminService.CreatNewProduct(productViewModelDto);

            return Ok(products);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> ListProductViewModel()
        {
            return Ok(await _adminService.ListProductViewModel());
        }


        [HttpGet("[action]/{productId}")]
        public IActionResult GetProduct(int productId)
        {
            return Ok(_adminService.GetProduct(productId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateProduct(ProductViewModelDto productViewModelDto)
        {
            if (productViewModelDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!await _adminService.UpdateProduct(productViewModelDto))
            {
                ModelState.AddModelError("", "خطا در زمان بروزرسانی محصول {productViewModelDto.ProductTitle}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("[action]/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (await _adminService.DeleteProduct(productId))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "خطا هنگام حذف کردن");
            return StatusCode(500, ModelState);

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreatNewSubProduct(SubProductViewModelDto subProductViewModelDto)
        {
            if (subProductViewModelDto == null)
            {
                return null;
            }
            var subProductList = await _adminService.CreatNewSubProduct(subProductViewModelDto);
            return Ok(subProductList);
        }

        [HttpGet("[action]/{productId}")]
        public async Task<IActionResult> GetSubProductList(int productId)
        {
            var subProductList = await _adminService.SubProductList(productId);
            return Ok(subProductList);
        }

        [HttpGet("[action]/{subProductId}")]
        public async Task<IActionResult> GetSubProduct(int subProductId)
        {
            return Ok(await _adminService.GetSubProduct(subProductId));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateSubProductViewModel(SubProductViewModelDto subProductViewModelDto)
        {
            if (subProductViewModelDto == null)
            {
                return BadRequest(ModelState);
            }
            
            if (!await _adminService.UpdateSubProduct(subProductViewModelDto))
            {
                ModelState.AddModelError("", "خطا در زمان بروزرسانی زیر محصول {productViewModelDto.ProductTitle}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("[action]/{subProductId}")]
        public async Task<IActionResult> DeleteSubProduct(int subProductId)
        {
            if (await _adminService.DeleteSubProduct(subProductId))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "خطا هنگام حذف کردن");
            return StatusCode(500, ModelState);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateNewEslimi(EslimiViewModelDto eslimiViewModelDto)
        {
            if (eslimiViewModelDto == null)
                return null;
            var EslimiSuccess = await _adminService.CreateNewEslimi(eslimiViewModelDto);

            return Ok(EslimiSuccess);
        }
    }
}
