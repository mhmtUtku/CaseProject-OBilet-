﻿using CaseProject.Helper;
using CaseProject.Interface;
using CaseProject.Model.Session;

namespace CaseProject.Service
{
    public class UserSessionService : IUserSessionService
    {
        private static string token = Constant.Token;

        public async Task<UserSessionResponse> GetUserSessionInfo(UserSessionRequest request)
        {
            var getSessionUrl = Constant.GetSessionUrl;

            Dictionary<string, string> headerParams = new Dictionary<string, string>();
            headerParams.Add("Authorization", token);

            var apiResponse = await HttpHelper.HttpPost<UserSessionResponse, UserSessionRequest>(getSessionUrl, request, headerParams);
            
            return apiResponse;
        }
    }
}
