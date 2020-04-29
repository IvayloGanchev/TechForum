namespace TechForum.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
