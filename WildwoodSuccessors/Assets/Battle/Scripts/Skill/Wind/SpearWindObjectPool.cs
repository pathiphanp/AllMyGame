using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpearWindObjectPool : MonoBehaviour
{
    [Header("PoolPosition")]
    public GameObject poolPosition;
    [Header("SetObjectPool")]
    public int maxPoolSize;
    public int stackDefaultCapacity;
    IObjectPool<SpearWind> _pool;
    public IObjectPool<SpearWind> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<SpearWind>(CreatedPoolItem,
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
    private SpearWind CreatedPoolItem()
    {
        GameObject prefab = Resources.Load<GameObject>("SpearWind");
        //SpawObject
        GameObject _spearWindInstan = Instantiate(prefab);
        SpearWind _spearWind = _spearWindInstan.GetComponent<SpearWind>();
        _spearWind.Pool = Pool;
        return _spearWind;
    }

    private void OnReturnedToPool(SpearWind spearWind)
    {
        spearWind.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(SpearWind spearWind)
    {
        spearWind.gameObject.SetActive(true);
    }
    private void OnDestroyPoolObject(SpearWind spearWind)
    {
        Destroy(spearWind.gameObject);
    }
    void Start()
    {
        SpawnPool();
    }
    void SpawnPool()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            SpearWind spearWind = Pool.Get();
            spearWind.transform.position = poolPosition.transform.position;
        }
    }
}
