using System.Collections.Generic;
using Mirror;
using Script.Tools;
using UnityEngine;

namespace Script.Server
{
    public class SpawnObjectsInRandomPosition : SpawnObjectsInAllPositions
    {
        [SerializeField] private int countSpawnObject;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (countSpawnObject > points.Count)
            {
                countSpawnObject = points.Count;
            }
        }

        public override void SpawnObject()
        {
            List <Transform> temp = points.CopyList();
            for (int i = 0; i < countSpawnObject; i++)
            {
                GameObject instance = Instantiate(prefabObject);
                Transform position = temp[Random.Range(0, temp.Count)];
                temp.Remove(position);
                instance.transform.position = position.position;
                instance.transform.rotation = position.rotation;
                Objects.Add(instance);
                NetworkServer.Spawn(instance);
            }
        }
    }
}