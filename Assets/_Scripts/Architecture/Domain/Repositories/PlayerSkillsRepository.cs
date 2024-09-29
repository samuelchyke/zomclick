using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;

public interface IPlayerSkillsRepository
{
    Task<PlayerSkills> ReadPlayerSkills();

    Task<PlayerSkill> ReadPlayerSkill(string playerSkillId);

    Task ToggleIsSkillActive (string playerSkillId);

    Task IncreasePlayerGold ();
}

public class PlayerSkillsRepositoryImpl : IPlayerSkillsRepository, IInitializable
{
    IPlayerDao playerStatsDao;

    [Inject]
    public PlayerSkillsRepositoryImpl(
        IPlayerDao playerStatsDao
        )
    {
        this.playerStatsDao = playerStatsDao;
    }

    public void Initialize()
    {
        Debug.Log("Shop Repository Initialized");
    }

    public async Task<PlayerSkill> ReadPlayerSkill(string playerSkillId)
    {
        var playerSkill = await playerStatsDao.ReadPlayerSkill(playerSkillId);
        return new PlayerSkillBuilder().ToDomain(playerSkill);
    }

    public async Task<PlayerSkills> ReadPlayerSkills()
    {
        var entities = await playerStatsDao.ReadPlayerSkills();

        var turret = entities.Find(skill => skill.id == "turret_id");
        var bigBetty = entities.Find(skill => skill.id == "big_betty_id");
        var lightningRounds = entities.Find(skill => skill.id == "lightning_rounds_id");
        var rallyAllies = entities.Find(skill => skill.id == "rally_allies_id");
        var incendiaryRounds = entities.Find(skill => skill.id == "incendiary_rounds_id");
        var midasRounds = entities.Find(skill => skill.id == "midas_rounds_id");

        return new PlayerSkillsBuilder().buildFrom(
            turret: turret, 
            bigBetty: bigBetty, 
            lightningRounds: lightningRounds, 
            rallyAllies: rallyAllies, 
            incendiaryRounds: incendiaryRounds, 
            midasRounds: midasRounds
        );
    }

    public async Task ToggleIsSkillActive(string playerSkillId)
    {
        var playerSkill = await playerStatsDao.ReadPlayerSkill(playerSkillId);
        playerSkill.isActive = !playerSkill.isActive;
        Debug.Log("Shop Repository - ToggleIsSkillActive - isSkillActive: " + playerSkill.isActive);
        await playerStatsDao.UpdatePlayerSkill(playerSkill);
    }

    public async Task IncreasePlayerGold()
    {
        var midasRounds = await playerStatsDao.ReadPlayerSkill("midas_rounds_id");
        var playerStats = await playerStatsDao.ReadPlayerStats();
        playerStats.totalGold += midasRounds.buff;
        await playerStatsDao.UpdatePlayerStats(playerStats);
    }
}