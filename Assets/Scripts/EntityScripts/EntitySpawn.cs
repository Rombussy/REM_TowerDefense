using UnityEngine;
using System.Collections.Generic;

public class EntitySpawn : MonoBehaviour
{
    [System.Serializable]
    public class EntityPool
    {
        public EntityType type;
        public GameObject entityPrefab;
        public int poolSize = 10;
        public List<GameObject> spawnList = new List<GameObject>();
    }

    public EntityPool[] entityPools;

    private void Start()
    {
        foreach (EntityPool pool in entityPools)
        {
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject entity = Instantiate(pool.entityPrefab, transform);
                entity.SetActive(false);
                pool.spawnList.Add(entity);
            }
        }
    }

    public GameObject SpawnEntity(EntityType type, BezierSegment[] path)
    {
        foreach (EntityPool pool in entityPools)
        {
            if (pool.type == type)
            {
                foreach (GameObject entity in pool.spawnList)
                {
                    if (!entity.activeInHierarchy)
                    {
                        entity.SetActive(true);
                        EntityMovementManager movement = entity.GetComponent<EntityMovementManager>();
                        if (movement != null)
                        {
                            movement.Initialize(path);
                        }
                        return entity;
                    }
                }
            }
        }
        return null;
    }
}
