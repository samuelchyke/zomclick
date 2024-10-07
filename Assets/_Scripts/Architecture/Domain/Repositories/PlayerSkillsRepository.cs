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

        var turret = entities.Find(skill => skill.id == Skill.Turret.id());
        Debug.Log("Shop Repository - ReadPlayerSkills - Turret: " + turret.id);
        var bigBetty = entities.Find(skill => skill.id == Skill.BigBetty.id());
        Debug.Log("Shop Repository - ReadPlayerSkills - BigBetty: " + bigBetty.id);
        var lightningRounds = entities.Find(skill => skill.id == Skill.LightningRounds.id());
        Debug.Log("Shop Repository - ReadPlayerSkills - LightningRounds: " + lightningRounds.id);
        var rallyAllies = entities.Find(skill => skill.id == Skill.RallyAllies.id());
        Debug.Log("Shop Repository - ReadPlayerSkills - RallyAllies: " + rallyAllies.id);
        var incendiaryRounds = entities.Find(skill => skill.id == Skill.IncendiaryRounds.id());
        Debug.Log("Shop Repository - ReadPlayerSkills - IncendiaryRounds: " + incendiaryRounds.id);
        var midasRounds = entities.Find(skill => skill.id == Skill.MidasRounds.id());
        Debug.Log("Shop Repository - ReadPlayerSkills - MidasRounds: " + midasRounds.id);

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
        await playerStatsDao.IncreasePlayerGold(midasRounds.buff);
    }
}