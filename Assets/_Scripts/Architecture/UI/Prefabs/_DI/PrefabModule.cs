using Zenject;
using UnityEngine;

public class PrefabModule : MonoInstaller<PrefabModule>
{
    public GameObject EnemyPrefab;
    public GameObject ProjectilePrefab;
    public GameObject BossPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("ProjectilePrefab").FromInstance(ProjectilePrefab);
        Container.Bind<GameObject>().WithId("EnemyPrefab").FromInstance(EnemyPrefab);
        Container.Bind<GameObject>().WithId("BossPrefab").FromInstance(BossPrefab);
    }
}