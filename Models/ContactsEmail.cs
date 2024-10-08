﻿namespace cms_pract.Models
{
    public class ContactsEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsSelected { get; set; } = false;
        public string UserProfileId { get; set; }
        public CmsPractUser UserProfile { get; set; }
    }
}
