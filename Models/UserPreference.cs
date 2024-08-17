using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class UserPreference
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string PreferenceName { get; set; }
        public string PreferenceValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
