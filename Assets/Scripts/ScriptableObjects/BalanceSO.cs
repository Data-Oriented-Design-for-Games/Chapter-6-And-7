using UnityEngine;

namespace Survivor
{
    [CreateAssetMenu(fileName = "BalanceSO", menuName = "DOD/BalanceSO", order = 1)]
    public class BalanceSO : ScriptableObject
    {
        public int NumEnemies;
        public float EnemyVelocityMin;
        public float EnemyVelocityMax;
        public float PlayerVelocity;
        public float MinCollisionDistance;
    }
}