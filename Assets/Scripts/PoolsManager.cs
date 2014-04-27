using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoolsManager : MonoBehaviour
{
    public Transform[] prefabs;
    private SpawnPool pool;

    void Start()
    {
        pool = PoolManager.Pools.Create("MapPool");

        pool.group.localPosition = new Vector3(0, 0, 0);
        pool.group.localRotation = Quaternion.identity;

        for (int i = 0; i < prefabs.Length; i++)
        {
            PrefabPool prefabPool = new PrefabPool(prefabs[i]);
            prefabPool.preloadAmount = 1000;
            prefabPool.cullDespawned = true;
            prefabPool.cullAbove = 1000;
            prefabPool.cullDelay = 2;
            prefabPool.limitInstances = false;

            pool.CreatePrefabPool(prefabPool);
        }
    }

    public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
    {
        return pool.Spawn(prefab, pos, rot);
        //return (GameObject.Instantiate(prefab.gameObject, pos, rot) as GameObject).transform;
    }

    public void Despawn(Transform instance)
    {
        pool.Despawn(instance);
        //GameObject.Destroy(instance.gameObject);
    }

    public void DespawnAll()
    {
        pool.DespawnAll();
        //GameObject.Destroy(instance.gameObject);
    }
}
