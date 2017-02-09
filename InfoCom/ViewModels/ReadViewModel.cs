using DataAccess.Models;

namespace InfoCom.ViewModels
{
    public class ReadViewModel
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public User Author { get; set; }
        public string Content  { get; set; }
    }
}