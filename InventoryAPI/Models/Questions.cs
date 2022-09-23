using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
    }
}