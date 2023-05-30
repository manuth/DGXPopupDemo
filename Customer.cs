using System;
using System.Net.Mail;

namespace DGXPopupDemo
{
    public class Customer
    {
        public int? ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public MailAddress? Email { get; set; }
        public bool IsMember { get; set; }
        public OrderStatus Status { get; set; }
    }
}
