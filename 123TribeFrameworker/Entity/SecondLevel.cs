namespace _123TribeFrameworker.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SecondLevel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int orderId { get; set; }
        [MaxLength(50)]
        public string title { get; set; }
        [MaxLength(50)]
        public string url { get; set; }
        [MaxLength(50)]
        public string open { get; set; }
        public int firstLevelId { get; set; }
        [MaxLength(50)]
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;
        [MaxLength(50)]
        public string lastUpdatedBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public int activityFlag { get; set; } = 1;
        [ForeignKey("firstLevelId")]
        public virtual FirstLevel FirstLevel { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
