using SkillsGrading.BusinessLogic.Services.Interfaces;

namespace SkillsGrading.BusinessLogic.Services
{
    public class TokenService : ITokenService
    {
        private string _token;
        public string GetToken()
        {
            return _token;
        }

        public void SetToken(string token)
        {
            _token = token;
        }
    }
}
