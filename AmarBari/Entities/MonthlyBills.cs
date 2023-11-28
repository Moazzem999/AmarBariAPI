using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarBari.Entities
{
    public class MonthlyBills
    {
        [Key]
        public long Id { get; set; }
        public long RenterId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MonthOfBill { get; set; }

        [Precision(18, 2)]
        public decimal HouseRent { get; set; } = 0;

        [Precision(18, 2)]
        public decimal CurrentMonthUnit { get; set; } = 0;

        [Precision(18, 2)]
        public decimal PreviousMonthUnit { get; set; } = 0;

        [Precision(18, 2)]
        public decimal UnitUsed { get; set; } = 0;

        [Precision(18, 2)]
        public decimal BillPerUnit { get; set; } = 0;

        [Precision(18, 2)]
        public decimal ElectricityBill { get; set; } = 0;

        [Precision(18, 2)]
        public decimal GasBill { get; set; } = 0;

        [Precision(18, 2)]
        public decimal WaterBill { get; set; } = 0;

        [Precision(18, 2)]
        public decimal ServiceCharge { get; set; } = 0;

        [Precision(18, 2)]
        public decimal OthersBill { get; set; } = 0;

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; } = 0;

        [Column(TypeName = "nvarchar(20)")]
        public string PaymentStatus { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("RenterId")]
        public virtual Renters Renter { get; set; } = null!;
    }
}
