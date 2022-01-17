using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ShortBook.SDK.CSharp
{
    public class LoginClient : ClientBase
    {
        public void Login(string email, string password)
        {
            Parameters.Clear();
            Parameters
                .Add("Email", email)
                .Add("Password", password);
            var response = Post("login", new {Email = email, Password = password});
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ClientException("用户登录名或登录口令错误，请确认。");
            }
        }
    }
}