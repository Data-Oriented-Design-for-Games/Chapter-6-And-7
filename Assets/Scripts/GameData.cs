using UnityEngine;
using System.IO;

namespace Survivor
{
public class GameData
{
    public Vector2[] EnemyPosition;
    public Vector2[] EnemyDirection;
    public float[] EnemyVelocity;

    public Vector2 PlayerPosition;
    public Vector2 PlayerDirection;

    public Vector2 BoardBounds;

    public float GameTime;
}
}