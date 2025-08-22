using System;
using System.Collections;
using System.Collections.Generic;
using CommonTools;
using UnityEngine;

namespace Survivor
{
    public class AssetManager : Singleton<AssetManager>
    {
        [SerializeField] GameObject m_playerPrefab = null;
        [SerializeField] GameObject m_enemyPrefab = null;

        [SerializeField] GameObject m_UIInGame = null;
        [SerializeField] GameObject m_UIMainMenu = null;
        [SerializeField] GameObject m_UIGameOver = null;
        [SerializeField] GameObject m_UIPauseMenu = null;

        public GameObject GetPlayerGameObject(Transform enemyParent)
        {
            return Instantiate(m_playerPrefab, enemyParent);
        }

        public GameObject GetEnemyGameObject(Transform enemyParent)
        {
            return Instantiate(m_enemyPrefab, enemyParent);
        }

        public GameObject GetInGameUI()
        {
            return Instantiate(m_UIInGame);
        }

        public GameObject GetMainMenuUI()
        {
            return Instantiate(m_UIMainMenu);
        }

        public GameObject GetGameOverUI()
        {
            return Instantiate(m_UIGameOver);
        }

        public GameObject GetPauseMenuUI()
        {
            return Instantiate(m_UIPauseMenu);
        }
    }
}