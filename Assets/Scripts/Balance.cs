using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Survivor
{
    [Serializable]
    public class Balance
    {
        public int NumEnemies;
        public float EnemyVelocityMin;
        public float EnemyVelocityMax;
        public float PlayerVelocity;
        public float MinCollisionDistance;

        public void LoadBalance()
        {
            TextAsset asset = Resources.Load("balance") as TextAsset;
            LoadBalance(asset.bytes);
        }

        public void LoadBalance(byte[] array)
        {
            Stream s = new MemoryStream(array);
            using (BinaryReader br = new BinaryReader(s))
            {
                int version = br.ReadInt32();

                NumEnemies = br.ReadInt32();
                EnemyVelocityMin = br.ReadSingle();
                EnemyVelocityMax = br.ReadSingle();
                PlayerVelocity = br.ReadSingle();
                MinCollisionDistance = br.ReadSingle();

                int magic = br.ReadInt32();
                Debug.Log(magic);
            }
        }
    }
}