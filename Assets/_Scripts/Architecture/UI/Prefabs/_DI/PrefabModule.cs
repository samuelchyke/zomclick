using Zenject;
using UnityEngine;

public class PrefabModule : MonoInstaller<PrefabModule>
{
    public GameObject EnemyPrefab;
    public GameObject ProjectilePrefab;
    public GameObject ProjectileTextPrefab;
    public GameObject BossPrefab;
    public GameObject JohnPrefab;
    public GameObject DoePrefab;
    public GameObject TurretPrefab;


    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("ProjectilePrefab").FromInstance(ProjectilePrefab);
        Container.Bind<GameObject>().WithId("ProjectileTextPrefab").FromInstance(ProjectileTextPrefab);
        Container.Bind<GameObject>().WithId("EnemyPrefab").FromInstance(EnemyPrefab);
        Container.Bind<GameObject>().WithId("BossPrefab").FromInstance(BossPrefab);
        Container.Bind<GameObject>().WithId("JohnPrefab").FromInstance(JohnPrefab);
        Container.Bind<GameObject>().WithId("DoePrefab").FromInstance(DoePrefab);
        Container.Bind<GameObject>().WithId("TurretPrefab").FromInstance(TurretPrefab);
    }
}