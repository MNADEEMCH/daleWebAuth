using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace daleWebAuth.Services
{
    public interface IAccountService
    {
        string CreateAuthorizationRequest();

        Task<Tuple<helpers.Common.CallStatus, TokenResponse>> ExchangeCodeForToken(string code);

    }
}
