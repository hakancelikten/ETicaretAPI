using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new (){Id=Guid.NewGuid(), Name="Product 1", Price=100, CreatedDate=DateTime.UtcNow,Stock=10},
            //    new (){Id=Guid.NewGuid(), Name="Product 2", Price=200, CreatedDate=DateTime.UtcNow,Stock=20},
            //    new (){Id=Guid.NewGuid(), Name="Product 3", Price=300, CreatedDate=DateTime.UtcNow,Stock=130}
            //});

            //var count = await _productWriteRepository.SaveAsync();

            /*
             * burada tracking false gönderdiğimiz için mehmet değişikliği olmayacaktır.
             * aynı scope içerisinde savechange yaptığımızda bu değişiklik yansımayacaktır.
            */
            Product p = await _productReadRepository.GetByIdAsync("2ca111cf-cac1-4ec0-9cf4-186638d6d1d7", false);
            p.Name = "Mehmet";
            await _productWriteRepository.SaveAsync();
        }
    }
}
