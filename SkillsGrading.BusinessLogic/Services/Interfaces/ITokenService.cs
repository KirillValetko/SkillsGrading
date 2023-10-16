namespace SkillsGrading.BusinessLogic.Services.Interfaces
{
    public interface ITokenService
    {
        string GetToken();
        void SetToken(string token);
    }
}
