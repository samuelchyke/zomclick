using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Collections.Generic;

public interface IAllyRepository
{
    Task<AllyStats> ReadAllyStats(string allyId);
    Task<List<AllyStats>> ReadAlliesStats();
    Task UpdateAllyStats(AllyStats allyStats);
}

public class AllyRepositoryImpl : IAllyRepository, IInitializable
{
    private IAllyDao allyDao;

    [Inject]
    public AllyRepositoryImpl(IAllyDao allyDao)
    {
        this.allyDao = allyDao;
    }

    public void Initialize()
    {
        Debug.Log("Ally Repository Initialized");
    }

    public async Task<AllyStats> ReadAllyStats(string allyId)
    {   
        var entity = await allyDao.ReadAllyEntity(allyId);
        Debug.Log("Ally Repository - ReadAllyStats: " + entity.id);
        return new AllyStatsBuilder().ToDomain(entity);
    }

    public async Task<List<AllyStats>> ReadAlliesStats()
    {
        var entities = await allyDao.ReadAlliesStats();
        var allyStatsList = entities.Select(item => new AllyStatsBuilder().ToDomain(item)).ToList();
        return allyStatsList;
    }

    public async Task UpdateAllyStats(AllyStats allyStats)
    {
        var entity = new AllyStatsBuilder().ToEntity(allyStats);
        await allyDao.UpdateAllyStats(entity);
        Debug.Log("Ally Repository - UpdateAllyStats: Updated stats for ID " + entity.id);
    }
}