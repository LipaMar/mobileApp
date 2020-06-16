using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mobileApp.Web.Models
{
    public class Message
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string RecipentId { get; set; }
        [Required]
        public string Content { get; set; }

        public string Date { get; set; }
        [ForeignKey("SenderId")]
        public virtual Student Sender { get; set; }
        [ForeignKey("RecipentId")]
        public virtual Student Recipent { get; set; }
    }
}
