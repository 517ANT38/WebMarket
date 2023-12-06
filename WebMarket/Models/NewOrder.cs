using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebMarket.Infrastructure;

namespace WebMarket.Models
{
    public class NewOrder
    {




        public DateTime DateOrder { get; } = DateTime.Now.Date;


        [DisplayName("Дата доставки")]
        [FutureDate(ErrorMessage ="Дата должна быть больше текущей")]
        [Required(ErrorMessage ="Поле должно быть заполнено")]
        [DataType(DataType.Date)]
        public DateTime DateDelivery { get; set; }

        [DisplayName("Cпособ получения доставки")]
        [Required]
        public string MethodOfIssue { get; set; }
    }
}