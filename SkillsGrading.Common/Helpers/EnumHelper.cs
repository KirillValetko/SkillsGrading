using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.Common.Models;

namespace SkillsGrading.Common.Helpers
{
    public class EnumHelper<T> : IEnumHelper<T> where T : Enum
    {
        public List<EnumDto> GetAllEnumValues()
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(e => new EnumDto { Key = Convert.ToInt32(e), Name = e.ToString() })
                .ToList();
        }
    }
}
