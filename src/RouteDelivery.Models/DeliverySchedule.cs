using System;
using System.ComponentModel.DataAnnotations;

namespace RouteDelivery.Models
{
    public class DeliverySchedule: IEntity
    {
        [Display(Name = "Optimization Request ID")]
        public int OptimizationRequestID { get; set; }
        [Display(Name = "Customer No")]
        public int CustomerID { get; set; }
        [Display(Name = "Package No")]
        public int PackageID { get; set; }
        [Display(Name = "Transport Type")]
        public TransportType TransportType { get; set; }
        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }
        [Display(Name = "Estimated Time")]
        public DateTime EstimatedTime { get; set; }
        public int ID { get; set; }
    }
}