using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{
    [Header("Object Prefab")]
    [SerializeField] GameObject prefab;
    [Header("PoolPosition")]
    [SerializeField] GameObject poolPosition;
    [Header("SetObjectPool")]
    public int maxPoolSize;
    public int stackDefaultCapacity;
    IObjectPool<Enemy> _pool;
    public IObjectPool<Enemy> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Enemy>(CreatedPoolItem,
                                              OnTakeFromPool,
                                              OnReturnedToPool,
                                              OnDestroyPoolObject,
                                              true,
                                              stackDefaultCapacity,
                                              maxPoolSize);
            }
            return _pool;
        }
    }
    private Enemy CreatedPoolItem()
    {
        //SpawObject
        GameObject _enemyInstan = Instantiate(prefab);
        Enemy _enemy = _enemyInstan.GetComponent<Enemy>();
        _enemy.Pool = Pool;
        return _enemy;
    }
    private void OnReturnedToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    private void OnDestroyPoolObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
    void Start()
    {
        SpawnPool();
    }
    void SpawnPool()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            Enemy enemy = Pool.Get();
            enemy.transform.position = poolPosition.transform.position;
        }
    }
}
