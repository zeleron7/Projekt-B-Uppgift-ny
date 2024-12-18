﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Models;
using Models.DTO;
using Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuotesController : Controller
    {
        readonly IFriendsService _service = null;
        readonly ILogger<QuotesController> _logger = null;

        //GET: api/quotes/read
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IQuote>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read(string seeded = "true", string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool seededArg = bool.Parse(seeded);
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSizeArg = int.Parse(pageSize);

                _logger.LogInformation($"{nameof(Read)}: {nameof(seededArg)}: {seededArg}, {nameof(flatArg)}: {flatArg}, " +
                    $"{nameof(pageNrArg)}: {pageNrArg}, {nameof(pageSizeArg)}: {pageSizeArg}");
                var resp = await _service.ReadQuotesAsync(seededArg, flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);     
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //GET: api/quotes/readitem
        [HttpGet()]
        [ActionName("ReadItem")]
        [ProducesResponseType(200, Type = typeof(IQuote))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);


                _logger.LogInformation($"{nameof(ReadItem)}: {nameof(idArg)}: {idArg}, {nameof(flatArg)}: {flatArg}");

                var item = await _service.ReadQuoteAsync(idArg, false);
                if (item == null) throw new ArgumentException ($"Item with id {id} does not exist");

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItem)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/quotes/deleteitem/id
        [HttpDelete("{id}")]
        [ActionName("DeleteItem")]
        [ProducesResponseType(200, Type = typeof(IQuote))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
        {   
            try
            {
                var idArg = Guid.Parse(id);
                
                _logger.LogInformation($"{nameof(DeleteItem)}: {nameof(idArg)}: {idArg}");

                var item = await _service.DeleteQuoteAsync(idArg);
                if (item == null) throw new ArgumentException ($"Item with id {id} does not exist");
        
                _logger.LogInformation($"item {idArg} deleted");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteItem)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //GET: api/quotes/readitemdto
        [HttpGet()]
        [ActionName("ReadItemDto")]
        [ProducesResponseType(200, Type = typeof(QuoteCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
        {
            try
            {
                var idArg = Guid.Parse(id);
  
                _logger.LogInformation($"{nameof(ReadItemDto)}: {nameof(idArg)}: {idArg}");
                
                var item = await _service.ReadQuoteAsync(idArg, false);
                if (item == null) throw new ArgumentException ($"Item with id {id} does not exist");

                var dto = new QuoteCUdto(item);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItemDto)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //PUT: api/quotes/updateitem/id
        //Body: csQuoteCUdto in Json
        [HttpPut("{id}")]
        [ActionName("UpdateItem")]
        [ProducesResponseType(200, Type = typeof(IQuote))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] QuoteCUdto item)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(UpdateItem)}: {nameof(idArg)}: {idArg}");
                
                if (item.QuoteId != idArg) throw new ArgumentException("Id mismatch");

                var model = await _service.UpdateQuoteAsync(item);
                _logger.LogInformation($"item {idArg} updated");
               
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateItem)}: {ex.Message}");
                return BadRequest($"Could not update. Error {ex.Message}");
            }
        }

        //POST: api/quotes/createitem
        //Body: csQuoteCUdto in Json
        [HttpPost()]
        [ActionName("CreateItem")]
        [ProducesResponseType(200, Type = typeof(IQuote))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] QuoteCUdto item)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateItem)}:");
               
                var model = await _service.CreateQuoteAsync(item);
                _logger.LogInformation($"item {model.QuoteId} created");

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateItem)}: {ex.Message}");
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        #region constructors
        public QuotesController(IFriendsService service, ILogger<QuotesController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion
    }
}

