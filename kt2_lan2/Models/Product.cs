namespace kt2_lan2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [DisplayName("mã sản phẩm")]
        public int Pid { get; set; }
        [DisplayName("mã danh mục")]
        public int Categoryid { get; set; }

        [Required]
        [DisplayName("tên sản phẩm")]
        [StringLength(250)]
        public string ProdName { get; set; }

        [StringLength(50)]
        [DisplayName("tiêu đề sản phẩm")]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        [DisplayName("mô tả")]
        public string Description { get; set; }

        [Required]
        [DisplayName("ảnh sản phẩm")]
        [StringLength(550)]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        
        [DisplayName("giá sản phẩm")]
        public decimal Price { get; set; }
    }
}
