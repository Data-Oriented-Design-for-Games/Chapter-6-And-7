using System;
using System.IO;
using UnityEngine;

namespace Survivor
{
    public static class GameDataIO
    {
        public static void Save(GameData gameData, Balance balance)
        {
            Debug.LogFormat("SaveGame()");

            if (!Directory.Exists(Application.persistentDataPath + "/DODSurvivor"))
                Directory.CreateDirectory(Application.persistentDataPath + "/DODSurvivor");

            string fileName = Application.persistentDataPath + "/DODSurvivor/gamedata.dat";
            using (FileStream fs = File.Create(fileName))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                int version = 2;
                bw.Write(version);

                bw.Write(gameData.InGame);

                bw.Write(balance.NumEnemies);
                for (int i = 0; i < balance.NumEnemies; i++)
                {
                    bw.Write(gameData.EnemyPosition[i].x);
                    bw.Write(gameData.EnemyPosition[i].y);
                }

                for (int i = 0; i < balance.NumEnemies; i++)
                {
                    bw.Write(gameData.EnemyDirection[i].x);
                    bw.Write(gameData.EnemyDirection[i].y);
                }

                for (int i = 0; i < balance.NumEnemies; i++)
                    bw.Write(gameData.EnemyVelocity[i]);

                bw.Write(gameData.PlayerPosition.x);
                bw.Write(gameData.PlayerPosition.y);

                bw.Write(gameData.PlayerDirection.x);
                bw.Write(gameData.PlayerDirection.y);

                bw.Write(gameData.BoardBounds.x);
                bw.Write(gameData.BoardBounds.y);

                bw.Write(gameData.GameTime);
            }
        }

        public static void Load(GameData gameData)
        {
            string fileName = Application.persistentDataPath + "/DODSurvivor/gamedata.dat";
            if (File.Exists(fileName))
            {
                using (FileStream stream = File.Open(fileName, FileMode.Open))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    int version = br.ReadInt32();

                    if (version >= 2)
                        gameData.InGame = br.ReadBoolean();

                    int numEnemies = br.ReadInt32();
                    for (int i = 0; i < numEnemies; i++)
                    {
                        gameData.EnemyPosition[i].x = br.ReadSingle();
                        gameData.EnemyPosition[i].y = br.ReadSingle();
                    }

                    for (int i = 0; i < numEnemies; i++)
                    {
                        gameData.EnemyDirection[i].x = br.ReadSingle();
                        gameData.EnemyDirection[i].y = br.ReadSingle();
                    }

                    for (int i = 0; i < numEnemies; i++)
                        gameData.EnemyVelocity[i] = br.ReadSingle();

                    gameData.PlayerPosition.x = br.ReadSingle();
                    gameData.PlayerPosition.y = br.ReadSingle();

                    gameData.PlayerDirection.x = br.ReadSingle();
                    gameData.PlayerDirection.y = br.ReadSingle();

                    gameData.BoardBounds.x = br.ReadSingle();
                    gameData.BoardBounds.y = br.ReadSingle();

                    gameData.GameTime = br.ReadSingle();
                }
            }
        }

        public static bool SaveGameExists()
        {
            bool inGame = false;
            string fileName = Application.persistentDataPath + "/DODSurvivor/gamedata.dat";
            if (File.Exists(fileName))
            {
                using (FileStream stream = File.Open(fileName, FileMode.Open))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    int version = br.ReadInt32();

                    inGame = br.ReadBoolean();
                }
            }
            return inGame;
        }
    }
}