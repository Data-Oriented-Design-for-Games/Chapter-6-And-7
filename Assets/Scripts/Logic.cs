using Unity.VisualScripting;
using UnityEngine;

namespace Survivor
{
    public static class Logic
    {
        public static void AllocateGameData(GameData gameData, Balance balance)
        {
            gameData.EnemyPosition = new Vector2[balance.NumEnemies];
            gameData.EnemyDirection = new Vector2[balance.NumEnemies];
            gameData.EnemyVelocity = new float[balance.NumEnemies];
        }

        public static void Init(MetaData metaData)
        {
            metaData.MenuState = MENU_STATE.NONE;
        }

        public static void StartGame(GameData gameData, Balance balance, float cameraSize, float screenRatio)
        {
            gameData.InGame = true;

            gameData.PlayerPosition = Vector2.zero;
            gameData.PlayerDirection = Vector2.zero;

            gameData.BoardBounds.x = cameraSize * screenRatio;
            gameData.BoardBounds.y = cameraSize;

            for (int i = 0; i < balance.NumEnemies; i++)
            {
                float randomX = Random.value *
                    gameData.BoardBounds.x * 2.0f -
                    gameData.BoardBounds.x;

                float randomY = Random.value *
                    gameData.BoardBounds.y * 2.0f -
                    gameData.BoardBounds.y;

                gameData.EnemyPosition[i] = new Vector2(randomX, randomY);

                gameData.EnemyDirection[i] = new Vector2(
                    Random.value - 0.5f,
                    Random.value - 0.5f
                    ).normalized;

                gameData.EnemyVelocity[i] = (
                    Random.value *
                    (balance.EnemyVelocityMax - balance.EnemyVelocityMin)) +
                    balance.EnemyVelocityMin;
            }

            gameData.GameTime = 0.0f;
        }

        public static void Tick(
            MetaData metaData,
            GameData gameData,
            Balance balance,
            float dt,
            out bool gameOver)
        {
            gameOver = false;
            for (int i = 0; i < balance.NumEnemies; i++)
            {
                Vector2 position = gameData.EnemyPosition[i] +
                gameData.EnemyDirection[i] *
                gameData.EnemyVelocity[i] * dt;
                Vector2 direction = gameData.EnemyDirection[i];

                checkEnemyWallCollision(gameData.BoardBounds, ref position, ref direction);

                gameData.EnemyDirection[i] = direction;
                gameData.EnemyPosition[i] = position;
            }

            Vector2 newPlayerPosition = gameData.PlayerPosition + gameData.PlayerDirection * dt * balance.PlayerVelocity;
            gameData.PlayerPosition = checkPlayerWallCollision(gameData.BoardBounds, newPlayerPosition);

            gameData.GameTime += dt;

            gameOver = checkGameOver(metaData, gameData, balance);
        }

        public static void MouseMove(GameData gameData, Vector2 mouseCurrentPos, Vector2 mouseDownPos)
        {
            gameData.PlayerDirection = (mouseCurrentPos - mouseDownPos).normalized;
        }

        public static void MouseUp(GameData gameData)
        {
            gameData.PlayerDirection = Vector2.zero;
        }

        static void checkEnemyWallCollision(Vector2 boardBounds, ref Vector2 position, ref Vector2 direction)
        {
            if (position.x < -boardBounds.x)
            {
                position.x = -boardBounds.x;
                direction.x = -direction.x;
            }
            else if (position.x > boardBounds.x)
            {
                position.x = boardBounds.x;
                direction.x = -direction.x;
            }
            if (position.y < -boardBounds.y)
            {
                position.y = -boardBounds.y;
                direction.y = -direction.y;
            }
            else if (position.y > boardBounds.y)
            {
                position.y = boardBounds.y;
                direction.y = -direction.y;
            }
        }

        static Vector2 checkPlayerWallCollision(Vector2 boardBounds, Vector2 position)
        {
            if (position.x < -boardBounds.x)
                position.x = -boardBounds.x;

            if (position.x > boardBounds.x)
                position.x = boardBounds.x;

            if (position.y < -boardBounds.y)
                position.y = -boardBounds.y;

            if (position.y > boardBounds.y)
                position.y = boardBounds.y;

            return position;
        }

        static bool checkGameOver(MetaData metaData, GameData gameData, Balance balance)
        {
            for (int i = 0; i < balance.NumEnemies; i++)
                if (Vector2.Distance(gameData.EnemyPosition[i], gameData.PlayerPosition) < balance.MinCollisionDistance)
                {
                    if (gameData.GameTime > metaData.BestTime)
                        metaData.BestTime = gameData.GameTime;

                    gameData.InGame = false;

                    return true;
                }
            return false;
        }

        public static void SetMenuState(MetaData metaData, MENU_STATE newMenuState)
        {
            metaData.MenuState = newMenuState;
        }
    }
}