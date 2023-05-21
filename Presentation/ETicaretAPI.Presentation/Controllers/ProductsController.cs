using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.AccessControl;

namespace ETicaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();

            return Ok(new
            {
                totalCount,
                products
            });

            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new (){Id=Guid.NewGuid(), Name="Product 1", Price=100, Stock=10},
            //    new (){Id=Guid.NewGuid(), Name="Product 2", Price=200, Stock=20},
            //    new (){Id=Guid.NewGuid(), Name="Product 3", Price=300, Stock=130}
            //});

            //var count = await _productWriteRepository.SaveAsync();

            /*
             * burada tracking false gönderdiğimiz için mehmet değişikliği olmayacaktır.
             * aynı scope içerisinde savechange yaptığımızda bu değişiklik yansımayacaktır.
            */
            //Product p = await _productReadRepository.GetByIdAsync("2ca111cf-cac1-4ec0-9cf4-186638d6d1d7", false);
            //p.Name = "Mehmet";
            //await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            // Burada ilgili model'i direk çekerek yaptık ve update metodunu kullanmadık. Eğer tracking kullanmıyorsak update metodunu kullanabiliriz. Tracking mekanizmasını kullanıyorsak update metodunu kullanmamıza gerek yoktur. Track edildiği için direk savechanges diyerek güncellemeyi yapabilmekteyiz.
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
