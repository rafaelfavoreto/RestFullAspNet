using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullAspNet.Business;
using RestFullAspNet.Data.VO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestFullAspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailsVO))]   
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploudOneFile([FromForm] IFormFile file)
        {
            FileDetailsVO detail = await _fileBusiness.SaveFileToDIsk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultipleFile")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailsVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploudManyFile([FromForm] List<IFormFile> files)
        {
            List<FileDetailsVO> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(FileDetailsVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string filename)
        {
            byte[] buffer = _fileBusiness.GetFile(filename);
            if(buffer != null)
            {
                // a linha abaixo perde a extensão , e sem ela abre até a imagem no POstman
                //HttpContext.Response.ContentType = $"application/{Path.GetExtension(filename).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }

    }
}
