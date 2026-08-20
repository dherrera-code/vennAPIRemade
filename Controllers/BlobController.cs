using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Services;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly BlobServices _blobServices;
        public BlobController(BlobServices blobServices)
        {
            _blobServices = blobServices;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult> Upload(IFormFile file, [FromForm] string fileName)
        {
            if (file == null || file.Length == 0) return BadRequest("Invalid File");
            try
            {
                using var stream = file.OpenReadStream();
                var fileUrl = await _blobServices.UploadFileAsync(stream, fileName);
                return Ok(new { FileUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}