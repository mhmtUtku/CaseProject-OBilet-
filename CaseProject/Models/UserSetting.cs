namespace CaseProject.Models
{
    [Serializable]
    public class UserSetting
    {
        public string SessionId { get; set; }
        public string DeviceId { get; set; }

        public string CookieName => SessionId.Substring(0, 10);
    }
}
