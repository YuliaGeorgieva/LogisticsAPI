using LogisticsApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsApi.Dtos
{
    public class ShipmentDetailsDto
    {
        public string TrackingNumber { get; set; }
        public ShipmentStatus ShipmentStatusId { get; set; }
        public string Message { get; set; }
        public int ShipmentId { get; set; }

        public string AddedBy { get; set; }
    }
}
