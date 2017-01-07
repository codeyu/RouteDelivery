using System;
using System.ComponentModel.DataAnnotations;
namespace RouteDelivery.Models
{
    public enum RequestStatus
    {
        Waiting,
        Processing,
        Complete,
        Failed
    }
    public class OptimizationRequest : IEntity
    {

        public OptimizationRequest()
        {
            RequestDate = System.DateTime.Now;
            Status = RequestStatus.Waiting;
            ScheduleDate = System.DateTime.Now.Date;
        }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate { get; set; }
        [Display(Name = "Status")]
        public string StatusText { get { return Status.ToString(); } }

        public RequestStatus Status { get; set; }
        public int ID { get; set; }
    }
}
