using System;
using AutoMapper;
using CleanArchitecture.Demo.Application;
using CleanArchitecture.Demo.Contracts.v1;
using CleanArchitecture.Demo.WebApi.Presenters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Demo.WebApi.Controllers
{
    [Route("/api/v1.0/products")]
    public class ProductController : Controller
    {

        private readonly IUseCaseFactory _useCaseFactory;
        private readonly IAddProductPresenter _addProductPresenter;
        private readonly IMapper _mapper;

        public ProductController(IUseCaseFactory useCaseFactory, IAddProductPresenter addProductPresenter, IMapper mapper)
        {
            _useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            _addProductPresenter = addProductPresenter ?? throw new ArgumentNullException(nameof(addProductPresenter));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Add Product Api
        /// </summary>
        /// <param name="addProductRequest">Add Product Request</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult AddProduct([FromBody]AddProductRequest addProductRequest)
        {
            try
            {
                var addProductInteractor = _useCaseFactory.GetUseCase<AddProductRequestModel, AddProductResponseModel>();
                var req = _mapper.Map<AddProductRequestModel>(addProductRequest);
                var useCaseResponseModel = addProductInteractor.Execute(req);
                var response = _addProductPresenter.Execute(useCaseResponseModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error - {ex.Message}");
            }

        }
    }
}