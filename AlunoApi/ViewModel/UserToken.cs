using System;

/*
{
   "email": "marcomasson@gmail.com",
  "password": "Numsey#2021"
}
*/

namespace AlunoApi.ViewModel
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }
}

