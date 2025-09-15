//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TreeEditor;
//using UnityEngine;

//public class SpawnedObject : MonoBehaviour
//{
//    [Serializable]
//    public class SaveSpawnedObjectData
//    {
//        public int objectID;
//        public Vector3 worldPos;

//        public SaveSpawnedObjectData(int _id, Vector3 _worldPos)
//        {
//            objectID = _id;
//            worldPos = _worldPos;
//        }
//    }

//    public int objID;

//    public void SpawnedObjectDestroyed()
//    {
//        transform.parent.GetComponent<ObjectSpawner>().SpawnedObjectDestroyed(this);
//    }
//}
