namespace DataAccessLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_MainOrders
    {
        [Key]
        [Column(Order = 0)]
        public Guid MainOrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public Guid? CustomerId { get; set; }

        [StringLength(500)]
        public string Customer { get; set; }

        [StringLength(500)]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string PO { get; set; }

        [StringLength(500)]
        public string PMIWorkNumber { get; set; }

        [StringLength(500)]
        public string ProductType { get; set; }

        [StringLength(500)]
        public string Purity { get; set; }

        [StringLength(500)]
        public string Shape { get; set; }

        [StringLength(500)]
        public string Dimension { get; set; }

        public double? Quantity { get; set; }

        [StringLength(500)]
        public string Unit { get; set; }

        public DateTime? DeliveryDateExpect { get; set; }

        [StringLength(500)]
        public string Consignee { get; set; }

        public bool? IsDeliveryFinished { get; set; }

        public DateTime? DeliveryDateFact { get; set; }

        public bool? IsPlanFinished { get; set; }

        public int? Priority { get; set; }

        [StringLength(50)]
        public string OrderState { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [Column(Order = 1)]
        [StringLength(500)]
        public string CustomerName { get; set; }
    }
}
