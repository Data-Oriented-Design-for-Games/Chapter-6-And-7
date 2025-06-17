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
    }
}