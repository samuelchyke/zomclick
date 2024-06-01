using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Collections.Generic;

public interface IAllyRepository
{
    Task<AllyStats> ReadAllyStats(string allyId);
    Task<List<AllyStats>> ReadAlliesStats();
    Task<List<AllySkill>> ReadAllySkills(string allyId);

    Task UnlockAlly(string allyId);
    Task UpgradeAllyStats (string allyId);
}

public class AllyRepositoryImpl : IAllyRepository, IInitializable
{
    private IAllyDao allyDao;
    private IPlayerStatsDao playerDao;

    [Inject]
    public AllyRepositoryImpl(
        IAllyDao allyDao,
        IPlayerStatsDao playerDao
        )
    {
        this.allyDao = allyDao;
        this.playerDao = playerDao;
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

    public async Task<List<AllySkill>> ReadAllySkills(string allyId)
    {
        var entities = await allyDao.ReadAllySkills(allyId);
        var allySkillsList = entities.Select(item => new AllySkillsBuilder().ToDomain(item)).ToList();
        return allySkillsList;
    }

    public async Task UnlockAlly(string allyId)
    {   
        var ally = await allyDao.ReadAllyEntity(allyId);
        var playerStats = await playerDao.ReadPlayerStats();

        if (playerStats.totalGold >= ally.unlockCost)
        {
            ally.isUnlocked = true;
            playerStats.totalGold -= ally.unlockCost;
        }
        
        await allyDao.UpdateAllyStats(ally);
        await playerDao.UpdatePlayerStats(playerStats);
    }
    
    public async Task UpgradeAllyStats(string allyId)
    {   
        var ally = await allyDao.ReadAllyEntity(allyId);
        var playerStats = await playerDao.ReadPlayerStats();

        if (playerStats.totalGold >= ally.upgradeCost)
        {
            playerStats.totalGold -= ally.upgradeCost;
            ally.upgradeCost += 10;
            ally.totalDamage += 1;
            ally.level += 1;

            await UnlockSkill(ally);
        }
        
        await allyDao.UpdateAllyStats(ally);
        await playerDao.UpdatePlayerStats(playerStats);
    }

    public async Task UnlockSkill(AllyStatsEntity ally)
    {   
        var allySkills = await allyDao.ReadAllySkills(ally.id);

        foreach (var skill in allySkills)
        {
            if (skill.unlockLevel == ally.level)
            {
                skill.isUnlocked = true;
                await allyDao.UpdateAllySkill(skill);
                break;
            }
        }
    }
}