using System.Text.Json;

namespace Shin_Megami_Tensei;

public class UnitRepository
{
    private readonly List<SamuraiJsonDto> _samuraiDtos;
    private readonly List<MonsterJsonDto> _monsterDtos;
    private readonly List<SkillJsonDto> _skillDtos;

    public UnitRepository()
    {
        _samuraiDtos = LoadFromJson<SamuraiJsonDto>(GameConstants.SamuraiFilePath);
        _monsterDtos = LoadFromJson<MonsterJsonDto>(GameConstants.MonsterFilePath);
        _skillDtos = LoadFromJson<SkillJsonDto>(GameConstants.SkillsFilePath);
    }

    private List<T> LoadFromJson<T>(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonContent)!;
    }

    public SamuraiJsonDto GetSamurai(string samuraiName)
    {
        return _samuraiDtos.First(samuraiDto =>
            samuraiDto.Name.Equals(samuraiName, StringComparison.OrdinalIgnoreCase));
    }

    public MonsterJsonDto GetMonster(string monsterName)
    {
        return _monsterDtos.First(monsterDto =>
            monsterDto.Name.Equals(monsterName, StringComparison.OrdinalIgnoreCase));
    }

    public SkillJsonDto GetSkill(string skillName)
    {
        return _skillDtos.First(skillDto =>
            skillDto.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
    }
}