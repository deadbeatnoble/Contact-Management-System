namespace cms_pract.Models
{
    public class LinkedInContactsInfoViewModel
    {
        public string removeEmail { get; set; } = new string("");
        public string email { get; set; } = "";
        public List<EmailModel> emails { get; set; } = new List<EmailModel>();
        public string selectedEmail { get; set; } = "";
        public string unselectedEmail { get; set; } = "";



        public List<string> selectedEmails { get; set; } = new List<string>();
    }

    public class EmailModel 
    { 
        public string email { get; set; }
        public bool isSelected { get; set; }
    }
}
