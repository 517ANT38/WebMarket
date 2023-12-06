using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMarket.Models
{
    public class NewProduct
    {
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [Required]
        [DisplayName("Стоимость ед товара")]
        public decimal Price { get; set; }
    }
}