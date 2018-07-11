namespace _123TribeFrameworker.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class FirstLevel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int orderId { get; set; }
        [MaxLength(50)]
        public string beforContent { get; set; }
        [MaxLength(50)]
        public string midContent { get; set; }
        [MaxLength(50)]
        public string afterContent { get; set; }
        public int activityFlag { get; set; } = 1;
        [MaxLength(20)]
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;
        [MaxLength(20)]
        public string lastUpdatedBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
