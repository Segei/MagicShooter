using System;
using System.Collections.Generic;
using Mirror;
using Script.Interfaces;
using UnityEngine;

namespace Script.Server
{
    public class SpawnObjectsInAllPositions : MonoBehaviour, ISpawnObject, IRegisterPrefab
    {
        [SerializeField] protected GameObject prefabObject;
        [SerializeField] protected List<Transform> points = new List<Transform>();
        public List<GameObject> Objects { get; } = new List<GameObject>();
        
        
        protected virtual void OnValidate()
        {
            if (prefabObject == null)
            {
                Debug.LogError("Prefab to spawn is null.", gameObject);
                return;
            }
            if (!prefabObject.TryGetComponent<NetworkIdentity>(out _))
            {
                Debug.LogError("Prefab without NetworkIdentity.", gameObject);
                return;
            }
        }
        
        public virtual void SpawnObject()
        {
            foreach (var point in points)
            {
                GameObject instance = Instantiate(prefabObject);
                instance.transform.position = point.position;
                instance.transform.rotation = point.rotation;
                Objects.Add(instance);
                NetworkServer.Spawn(instance);
            }
        }

        public void DestroyObject()
        {
            foreach (var @object in Objects)
            {
                Destroy(@object);
            }
            Objects.Clear();
        }

        public void RegisterPrefabToSpawn()
        {
            NetworkClient.RegisterPrefab(prefabObject);
        }
    }
}