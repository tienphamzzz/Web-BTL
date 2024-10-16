namespace Web_BTL.Services.Cookie
{
    public class CookieService
    {
        public CookieService() { }
        public void SetCookie(string nameCookie, string valueCookie, int timeLimit, HttpResponse Response)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(timeLimit), // thời gian hết hạn của cookie
                Secure = true, // chỉ truyền qua Https
                HttpOnly = true // chỉ hoạt động phía máy chủ
            };
            Response.Cookies.Append(nameCookie, valueCookie, options);
        }
        public void DeleteCookie(string nameCookie, HttpResponse Response)
        {
            Response.Cookies.Delete(nameCookie);
        }
        public string GetCookie(string nameCookie, HttpRequest Request)
        {
            string tmp = Request.Cookies[nameCookie];
            if (tmp == null) return "";
            return tmp;
        }
    }
}
