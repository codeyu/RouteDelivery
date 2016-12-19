
using System.ComponentModel.DataAnnotations;
namespace RouteDelivery.Models
{
    public class Delivery
    {
        [Display(Name = "Customer No")]
        public int CustomerID { get; set; }
        [Display(Name = "Package No")]
        public int ID { get; set; }
        [Display(Name = "Transport Type")]
        public TransportType TransportType { get; set; }
    }
}