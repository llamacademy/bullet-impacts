using UnityEngine;
using UnityEngine.AI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private Bullet BulletPrefab;
    [SerializeField]
    private float ShootDelay = 0.25f;
    [SerializeField]
    private NavMeshAgent Agent;

    private float LastShootTime;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && (LastShootTime + ShootDelay) < Time.time)
        {
            ObjectPool pool = ObjectPool.CreateInstance(BulletPrefab, 5);

            Bullet bullet = pool.GetObject() as Bullet;

            bullet.transform.position = Agent.transform.position + Vector3.up;
            bullet.transform.rotation = Agent.transform.rotation;
            bullet.Spawn(Agent.transform.forward);
            LastShootTime = Time.time;
        }
    }
}
