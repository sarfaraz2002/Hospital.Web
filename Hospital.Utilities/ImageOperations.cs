using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Utilities
{
    public class ImageOperations
    {
        IWebHostEnvironment _environment;
        public ImageOperations(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string ImageUpload(IFormFile file)
        {
            string filename = null;
            if(file!=null)
            {
                string fileDirectory = Path.Combine(_environment.WebRootPath, "Images");
                filename = Guid.NewGuid() + "_" + file.FileName;
                string filepath =Path.Combine(fileDirectory, filename);
                using (FileStream fs = new FileStream(filepath,FileMode.Create))
                {
                    file.CopyToAsync(fs);
                }
            }
            return filename;
        }
    }
}
