namespace cms_pract.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public string Email { get; set; }
        //public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }

}
