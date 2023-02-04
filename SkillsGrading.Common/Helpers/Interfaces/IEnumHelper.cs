using SkillsGrading.Common.Models;

namespace SkillsGrading.Common.Helpers.Interfaces
{
    public interface IEnumHelper<T> where T : Enum
    {
        List<EnumDto> GetAllEnumValues();
    }
}
