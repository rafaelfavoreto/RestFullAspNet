using Microsoft.AspNetCore.Http;
using RestFullAspNet.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;
            return File.ReadAllBytes(filePath);
        }

        public async Task<List<FileDetailsVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailsVO> list = new List<FileDetailsVO>();
            foreach (var file in files)
            {
                list.Add(await SaveFileToDIsk(file));
            }
            return list;
        }

        public async Task<FileDetailsVO> SaveFileToDIsk(IFormFile file)
        {
            FileDetailsVO fileDatatil = new FileDetailsVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if(fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);
                if(file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDatatil.DocumentName = docName;
                    fileDatatil.DocType = fileType;
                    fileDatatil.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDatatil.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return fileDatatil;
        }
    }
}
