using Application.Common.Interfaces;

namespace IntegrationTests.Fakes
{
    public class CurrentUserServiceFake : ICurrentUserService
    {
        public string FakedUserId { private get; set; } = "1234";
        public string UserId => FakedUserId;
    }
}