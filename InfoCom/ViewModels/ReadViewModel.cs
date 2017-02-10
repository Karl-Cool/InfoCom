using System.ComponentModel;
using DataAccess.Models;

namespace InfoCom.ViewModels
{
    public class ReadViewModel
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public User Author { get; set; }
        [DisplayName("Write a comment")]
        public string Content  { get; set; }
    }
}