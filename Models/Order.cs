using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Simple.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        /*public int UserID { get; set; }*/
        public int RtrID { get; set; }
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền ngày đến")]
        public DateTime ArrivalDate { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền giờ đến")]
        public TimeSpan ArrivalTime { get; set; }
        public DateTime BookTime { get; set; }
        public int Quantity { get; set; }
        public string OrderNote { get; set; }
        public string RtrMessage { get; set; }
        public bool Happen { get; set; }
        public string UserAction { get; set; }
        public string RtrAction { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        /*public virtual User User { get; set; }*/
        public virtual Restaurant Restaurant { get; set; }
        public virtual Customer Customer { get; set; }

        [NotMapped] // Không ánh xạ thuộc tính này vào cơ sở dữ liệu
        public DateTime DateTime
        {
            get { return new DateTime(ArrivalDate.Year, ArrivalDate.Month, ArrivalDate.Day, ArrivalTime.Hours, ArrivalTime.Seconds, 0); }
            set
            {
                
            }
        }

    }
}