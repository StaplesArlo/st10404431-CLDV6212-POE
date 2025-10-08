#nullable disable

using System.ComponentModel.DataAnnotations;

namespace ABCRetailers.Models
{
    public class FileUploadModel
    {
        [Required]
        [Display(Name = "Proof of Payment")]
        public IFormFile ProofOfPayment { get; set; }

        [Display(Name = "Order ID")]
        public string OrderID { get; set; } = string.Empty;

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Contract File")]
        public IFormFile File { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Please enter a valid contract name.")]
        [Display(Name = "Contract Name")]
        public string FileName
        {
            get; set;
        }
}
    }