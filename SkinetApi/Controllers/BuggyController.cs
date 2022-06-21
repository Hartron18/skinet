using Microsoft.AspNetCore.Mvc;
using SkinetApi.Entities;
using SkinetApi.Errors;

namespace SkinetApi.Controllers
{
    public class BuggyController: BaseApiController
    {
        private readonly StoreDbContext context;

        public BuggyController(StoreDbContext context)
        {
            this.context = context;
        }

        [HttpGet("notfounderror")]
       public ActionResult<Product> GetNotFoundError()
        {
            var thing = context.Products.Find(42);

            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult<Product> GetBadRequestError()
        {
            return BadRequest( new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult<Product> GetValidationError(int id)
        {
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult<Product> GetServerError()
        {
            var thing = context.Products.Find(42);

            var thingToReturn = thing.ToString();

            return Ok();
        }
    }
}
