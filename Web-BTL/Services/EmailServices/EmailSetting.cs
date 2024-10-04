namespace Web_BTL.Services.EmailServices
{
    public class EmailSetting
    {
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderPassword { get; set; }
    }
}
