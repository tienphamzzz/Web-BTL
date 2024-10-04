using MimeKit;

namespace Web_BTL.Services.EmailServices
{
    public class SendEmail
    {
        private readonly EmailSetting _email;
        public SendEmail(EmailSetting email)
        {
            _email = email;
        }
        public bool SendOTPEmail(string _to, string otpCode)
        {
            bool check = false;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Đây là mã OTP của bạn", _email.SenderEmail));
            message.To.Add(new MailboxAddress("", _to));
            message.Subject = "Đây là mã OTP";
            message.Body = new TextPart("plain")
            {
                Text = $"Xin chào, \n\nMã OTP của bạn là: {otpCode}\n\nVui lòng sử dụng mã này để hoàn tất quá trình xác thực email: {_to}"
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_email.SmtpServer, _email.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(_email.SenderEmail, _email.SenderPassword);
                    client.Send(message);
                    Console.WriteLine("Da gui thanh cong " + otpCode);
                    check = true;
                }catch (Exception ex)
                {
                    Console.WriteLine("Khong gui duoc");
                    Console.WriteLine(ex.ToString());
                    check = false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
            return check;
        }
        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
