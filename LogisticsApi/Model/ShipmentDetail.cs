using LogisticsApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsApi.Model
{
    // Dependent (child)
    public class ShipmentDetail
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public ShipmentStatus ShipmentStatusId { get; set; }
        public string Message { get; set; }


        [ForeignKey("ShipmentId")]
        public int ShipmentId { get; set; }// Required foreign key property
        //public virtual Shipment Shipment { get; set; }// Required reference navigation to principal

        public bool IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}