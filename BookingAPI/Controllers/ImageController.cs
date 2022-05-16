using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Managers;
using BookingAPI.Models;
namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IProprietatiManager _proprietatiManager;
        private readonly IImageManager _imageManager;

        public ImageController(ILogger<ImageController> logger, IWebHostEnvironment environment, IProprietatiManager proprietatiManager, IImageManager imageManager)
        {
            _imageManager = imageManager;
            _proprietatiManager = proprietatiManager;
            _logger = logger;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [HttpPost]
        public async Task<IActionResult> Post(int idProprietate)
        {
            Image imageAdded = new Image() ;
            try
            {
                var httpRequest = HttpContext.Request;
                if (httpRequest.Form.Files.Count > 0)
                {
                    foreach(var file in httpRequest.Form.Files)
                    {
                        var filePath = Path.Combine(_environment.ContentRootPath, "Uploads");
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        if (_imageManager.CheckPhotoUpdated(idProprietate, file.FileName))
                        {
                            return BadRequest();
                        }


                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            System.IO.File.WriteAllBytes(Path.Combine(filePath, file.FileName), memoryStream.ToArray());
                        }
                        filePath = Path.Combine(filePath, file.FileName);
                       
                        if (await _proprietatiManager.FindById(idProprietate))
                        {
                            imageAdded = await _imageManager.AddImage(idProprietate, filePath);
                        }
                        
                        return Ok(imageAdded);
                    }
                }
            }
            catch(Exception e )
            {
                _logger.LogError(e, "Error");
                return new StatusCodeResult(500);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int idProprietate)
        {
            var filePathh = await _imageManager.OneImagePath(idProprietate);
            //var filePath = "C:\\Users\\acer\\Desktop\\Licenta\\Aplicatie\\Aplicatie\\BookingAPI\\Uploads\\IMG_20220507_134458.jpg";
            if (System.IO.File.Exists(filePathh))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePathh);
                return File(b, "image/png");
            }
            return null;
        }

        [HttpGet("img")]
        public async Task<IActionResult> GetImg(int idProprietate)
        {
            string path = _imageManager.ImagineTry(idProprietate);
            return Ok(path);
            //7 as a parameter 
        }
    }
}
