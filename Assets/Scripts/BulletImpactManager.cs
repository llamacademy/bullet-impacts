using System.Collections.Generic;
using UnityEngine;

public class BulletImpactManager : MonoBehaviour
{
    public List<BulletImpact> BulletImpacts = new List<BulletImpact>();
    public Dictionary<Material, ObjectPool> BulletImpactDictionary = new Dictionary<Material, ObjectPool>();
    [SerializeField]
    private int ParticleSystemBuffer = 5;

    private static BulletImpactManager _instance;
    public static BulletImpactManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple BulletImpactManagers! Destroying the newest one: " + this.name);
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        foreach(BulletImpact impact in BulletImpacts)
        {
            BulletImpactDictionary.Add(impact.Material, ObjectPool.CreateInstance(impact.CollisionParticleSystem, ParticleSystemBuffer));
        }
    }

    public void SpawnBulletImpact(Vector3 Position, Vector3 Forward, Material HitMaterial)
    {
        if (BulletImpactDictionary.ContainsKey(HitMaterial))
        {
            DoSpawnBulletImpact(Position, Forward, BulletImpactDictionary[HitMaterial].GetObject());
        }
        else
        {
            DoSpawnBulletImpact(Position, Forward, BulletImpactDictionary[BulletImpacts[0].Material].GetObject());
        }
    }

    private void DoSpawnBulletImpact(Vector3 Position, Vector3 Forward, PoolableObject ParticleSystem)
    {
        ParticleSystem.transform.position = Position;
        ParticleSystem.transform.forward = Forward;
    }


    [System.Serializable]
    public class BulletImpact
    {
        public Material Material;
        public PoolableObject CollisionParticleSystem;
    }
}
