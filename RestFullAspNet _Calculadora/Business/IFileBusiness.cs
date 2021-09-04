using Microsoft.AspNetCore.Http;
using RestFullAspNet.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestFullAspNet.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailsVO> SaveFileToDIsk(IFormFile file);

        public Task<List<FileDetailsVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
