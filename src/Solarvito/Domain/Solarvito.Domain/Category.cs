﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Domain
{
    public class Category
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public Category Parent { get; set; }

        /// <summary>
        /// Коллекция подкатегорий.
        /// </summary>
        public ICollection<Category> Children { get; set; }

        /// <summary>
        /// Коллекция обьявлений в категории.
        /// </summary>
        public ICollection<Advertisement> Advertisements { get; set; }


    }
}
