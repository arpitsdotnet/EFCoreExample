using System.ComponentModel.DataAnnotations;

namespace EFCoreExample.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    
    [Required]
    public string CategoryName { get; set; }
    
    public int CategoryDisplayOrder { get; set; }
}
