using Azure;
using Azure.Data.Tables;
using System;

using System.ComponentModel.DataAnnotations;

namespace ABCRetailers.Models
{
    public enum OrderStatus
    {
        Submitted,   // First created
        Processing,  // Reviewed by company
        Completed,   // Delivered to customer
        Cancelled    // Cancelled by company
    }
    public partial class Order : ITableEntity
    {
        public string PartitionKey { get; set; } = "Order";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        public ETag ETag { get; set; }

        [Display(Name = "Order ID")]
        public string OrderID { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer")]
        public string CustomerID { get; set; } = string.Empty;

        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product")]
        public string ProductID { get; set; } = string.Empty;

        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset? OrderDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

       
       

        [Required]
        [Display(Name = "Payment Method")]
        public string Payment { get; set; } = "Submitted";

        [Required]
        [Display(Name = "Status")]
        public OrderStatus Status { get; set; } = OrderStatus.Submitted;
        public DateTimeOffset? Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}