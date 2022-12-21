using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.AdvertisementImage
{
    public class ImageCachedDto
    {
        public string FileName { get; set; }

        public byte[] FileBytes { get; set; } 
    }
}
