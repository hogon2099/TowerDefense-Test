using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UIManager : MonoBehaviour
    {
        public Text Health;
        public Text Money;
        public Text EnemiesCount;
        public Text WavesCount;
        private int enemiesInCurrentWave;

        void Awake()
        {
            InitializeUIManager();
        }

        private void InitializeUIManager()
        {
            // DontDestroyOnLoad(this);

            Wave.EnemiesCountChanged += this.SetEnemiesCurrentCount;
            WavesManager.Instance.WaveChanged += this.SetEnemiesInWaveCount;
            WavesManager.Instance.WaveChanged += this.SetWavesCount;
            Target.Instance.TargetHealthValueChanged += this.SetHealthValue;
            MoneyManager.Instance.MoneyChanged += this.SetMoney;
        }
        public void SetHealthValue(int value)
        {
            if (Health != null)
            {
                Health.text = value.ToString();
            }
        }
        public void SetEnemiesCurrentCount(int count)
        {
            if (EnemiesCount != null)
            {
                EnemiesCount.text = count.ToString() + "/" + enemiesInCurrentWave.ToString();
            }
        }
        public void SetEnemiesInWaveCount(int enemiesCount, int wavesNumber)
        {
            enemiesInCurrentWave = enemiesCount;
        }
        public void SetWavesCount(int enemiesCount, int waveNumber)
        {
            WavesCount.text = waveNumber + "/" + WavesManager.Instance.Waves.Count;
        }
        public void SetMoney(int money)
		{
            Money.text = money.ToString();
		}      
    }
}