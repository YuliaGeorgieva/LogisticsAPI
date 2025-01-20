namespace LogisticsApi.Dtos
{
    public class ShipmentDto
    {
        public int Id { get; set; }
        public string? TrackingNumber { get; set; }

        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderEmail { get; set; }
        public string? SenderCity { get; set; }
        public string? SenderState { get; set; }
        public string SenderCountry { get; set; }
        public string? SenderPhoneNumber { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverEmail { get; set; }
        public string? ReceiverCity { get; set; }
        public string? ReceiverState { get; set; }
        public string ReceiverCountry { get; set; }
        public string? ReceiverPhoneNumber { get; set; }

        public string? Description { get; set; }
        public string Weight { get; set; }
        public int Quantity { get; set; }


        public DateTime ShippingDate { get; set; } //when the order shipped out to the customer, or when the product leave the supplier warehouse
        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveredDate { get; set; } // when products reached to Recepient doorstep.
        public string DeliveryAddress { get; set; }
        public decimal DeliveryCost { get; set; }

        public string ShippingAddress { get; set; }
        public string PaymentStatus { get; set; }

        public decimal? Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal? PaidAmount { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } // => ShippingDate
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int BranchId { get; set; }
        public string CourierId { get; set; }

    }
}
