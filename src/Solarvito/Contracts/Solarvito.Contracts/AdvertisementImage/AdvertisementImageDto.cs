using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.AdvertisementImage
{
    public class AdvertisementImageDto
    {
        /// <summary>
        /// Идентификатор изображения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя файла-изображения.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Идентификатор обьявления.
        /// </summary>
        public int AdvertisementId { get; set; }

    }
}
