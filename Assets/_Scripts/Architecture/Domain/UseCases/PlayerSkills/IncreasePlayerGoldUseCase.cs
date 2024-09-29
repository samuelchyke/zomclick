using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IIncreasePlayerGoldUseCase
{
    Task Invoke();
}

public class IncreasePlayerGoldUseCaseImpl : IIncreasePlayerGoldUseCase, IInitializable
{
    private readonly IPlayerSkillsRepository _playerSkillsRepository;

    [Inject]
    public IncreasePlayerGoldUseCaseImpl(IPlayerSkillsRepository playerSkillsRepository)
    {
        _playerSkillsRepository = playerSkillsRepository;
    }

    public void Initialize()
    {
        Debug.Log("IncreasePlayerGoldUseCase Initialized");
    }

    public Task Invoke()
    {
        return _playerSkillsRepository.IncreasePlayerGold();
    }
}