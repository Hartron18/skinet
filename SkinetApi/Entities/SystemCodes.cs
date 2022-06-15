using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SkinetApi.Entities
{
    public class SystemCodes
    {
        [Key]
        public int OID { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "System Code")]
        public string Code { get; set; }
        
        [Required]
        [StringLength(250)]
        [Display(Name ="Setup Type")]
        public string SetupType { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }            

        
        public decimal? CodeOrderApplication { get; set; }

    }
}
