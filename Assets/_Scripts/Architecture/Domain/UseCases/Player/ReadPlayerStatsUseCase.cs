using System.Threading.Tasks;
using Zenject;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public interface IReadPlayerStatsUseCase {
    public Task<PlayerStats> Invoke();
}

public class ReadPlayerStatsUseCaseImpl : IReadPlayerStatsUseCase, IInitializable
{
    IPlayerRepository playerRepository;

    [Inject]
    public ReadPlayerStatsUseCaseImpl(IPlayerRepository playerRepository)
    {
        this.playerRepository = playerRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadPlayerStatsUseCaseImpl Initialized");
    }

    public Task<PlayerStats> Invoke()
    {
        return playerRepository.ReadPlayerStats();
    }
}