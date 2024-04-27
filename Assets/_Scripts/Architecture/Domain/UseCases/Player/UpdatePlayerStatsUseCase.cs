using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdatePlayerStatsUseCase {
    public Task Invoke(PlayerStats playerStats);
}

public class UpdatePlayerStatsUseCaseImpl : IUpdatePlayerStatsUseCase, IInitializable
{
    IPlayerRepository playerRepository;

    [Inject]
    public UpdatePlayerStatsUseCaseImpl(IPlayerRepository playerRepository)
    {
        this.playerRepository = playerRepository;
    }

    public void Initialize()
    {
        Debug.Log("UpdateAndFetchPlayerStatsUseCaseImpl Initialized");
    }

    public async Task Invoke(PlayerStats playerStats)
    {
        await playerRepository.UpdatePlayerStats(playerStats);
    }
}
