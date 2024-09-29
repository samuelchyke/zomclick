using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IToggleSkillActiveUseCase
{
    Task Invoke(string playerSkillId);
}

public class ToggleSkillActiveUseCaseImpl : IToggleSkillActiveUseCase, IInitializable
{
    private readonly IPlayerSkillsRepository _playerSkillsRepository;

    [Inject]
    public ToggleSkillActiveUseCaseImpl(IPlayerSkillsRepository playerSkillsRepository)
    {
        _playerSkillsRepository = playerSkillsRepository;
    }

    public void Initialize()
    {
        Debug.Log("ToggleIsSkillActiveUseCase Initialized");
    }

    public Task Invoke(string playerSkillId)
    {
        return _playerSkillsRepository.ToggleIsSkillActive(playerSkillId);
    }
}
