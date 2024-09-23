using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManageAPI.DTO;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;

namespace ProductManageAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository m_productsRepository;
        private readonly PmsContext m_context;

        public ProductsController(IProductsRepository productsRepository, PmsContext pmsContext)
        {
            m_context = pmsContext;
            m_productsRepository = productsRepository;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts(int page, int pageSize, string? searchedProduct)
        {
            try
            {
                List<ProductsDTO> result = new List<ProductsDTO>();

                if (searchedProduct == null)
                {
                    result = await m_productsRepository.searchProducts(null);
                }
                else
                {
                    result = await m_productsRepository.searchProducts(searchedProduct);
                }

                int offset = (page - 1) * pageSize;
                page = page - 1;

                if (result != null && result.Count > 0)
                {

                    var rowCount = result.Count();
                    result = result.Skip(offset).Take(pageSize).OrderBy(q => q.ProductId).ToList();

                    var response = new
                    {
                        Response = result,
                        RowCount = rowCount,
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        Response = result,
                        RowCount = 0,
                    };

                    return Ok(response);
                }
            }

            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Message = "Internal Server Error",
                    ErrorDetails = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

            finally
            {

            }
        }

        [HttpPost("products")]
        public async Task<IActionResult> PostProducts(PostProductDTO products, int createdBy)
        {
            try
            {
                int result = await m_productsRepository.postProducts(products,createdBy);

                if (result == 0)
                {
                    var errorResponse = new
                    {
                        Message = "Internal Server Error",
                        ErrorDetails = "Databese is not updated"
                    };

                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }

                return Ok(new
                {
                    status_code = 200,
                    status = "Success",
                    message = "Product is created successfully"
                });

            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Message = "Internal Server Error",
                    ErrorDetails = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
            finally
            {

            }
        }
    }
}
