#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace abcRetail.Models;

public partial class Administrator
{
    [Key]
    public int AdminID { get; set; }

    [Required]
    [StringLength(15)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [StringLength(15)]
    [Unicode(false)]
    public string Password { get; set; }
}