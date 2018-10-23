using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace memo.Models
{
    public class Memo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int memoId { get; set; }

        public string OwnerId { get; set; } //this field is used to tie to the login user

        [Required(ErrorMessage = "Please enter date and time")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter title of memo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the description")]
        public string Details { get; set; }
    }

}
