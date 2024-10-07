using System;

[Serializable]
public class EnemyStatsDto : SeedDto
{
    public string id;
    public int totalHealth;
    public int damage;
    public int attackSpeed;
    public float movementSpeed;
    public int goldDropAmount;

    public SeedEntity toEntity()
    {
        return new EnemyStatsEntity
        {
            id = id,
            totalHealth = totalHealth,
            damage = damage,
            attackSpeed = attackSpeed,
            movementSpeed = movementSpeed,
            goldDropAmount = goldDropAmount
        };
    }
}
