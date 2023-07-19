using CaseProject.Model.Session;

namespace CaseProject.Interface
{
    public interface IUserSessionService
    {
        Task<UserSessionResponse> GetUserSessionInfo(UserSessionRequest request);
    }
}
