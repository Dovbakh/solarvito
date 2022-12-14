using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Domain
{
    public class AdvertisementImage
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

        /// <summary>
        /// Обьявление, к которому относится изображение.
        /// </summary>
        public Advertisement Advertisement { get; set; }
    }
}
