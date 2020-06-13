using System;
using System.Collections.Generic;
using System.Text;

namespace mobileApp.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string RecipentId { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public virtual Student Sender { get; set; }
        public virtual Student Recipent { get; set; }
    }
}
