using Assets.Scripts.Other;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
	public class WavesManager : MonoBehaviour
	{
		[HideInInspector]
		public List<Wave> Waves;
		[HideInInspector]
		public int currentWave = 0;

		public static WavesManager Instance;
		private WavesManager()
		{
			Instance = this;
		}

		public delegate void WaveChangeHandler(int enemiesCount, int waveNumber);
		public event WaveChangeHandler WaveChanged;


		private void Start()
		{
			InitializeWavesManager();
		}
		public void InitializeWavesManager()
		{
			SetWavesFromChildren();
			foreach (var wave in Waves)
			{
				wave.InitializeWave();
			}
			DisableWaves();

			Target.Instance.TargetDestroed += RestartLevel;
			Wave.EnemiesAllKilled += ChangeWave;
		

			Waves[0].gameObject.SetActive(true);
			WaveChanged?.Invoke(Waves[0].Enemies.Count, currentWave + 1);
			Waves[currentWave].SpawnEnemies();
		}
		public void DisableWaves()
		{
			for (int i = 0; i < Waves.Count; i++)
			{
				Waves[i].gameObject.SetActive(false);

				foreach (var enemy in Waves[i].Enemies)
				{
					enemy.enabled = false;
				}
			}
		}
		public void ChangeWave()
		{
			if (currentWave > Waves.Count)
				return;

			if (currentWave++ < Waves.Count)
			{
				Waves[currentWave].gameObject.SetActive(true);
				WaveChanged(Waves[currentWave].Enemies.Count, currentWave + 1);
				Waves[currentWave].SpawnEnemies();
			}
			else
			{
				StartCoroutine(EndLevel());
			}
		}
		public IEnumerator EndLevel()
		{
			LevelManager.Instance.ShowEndingPicture();
			yield return new WaitForSeconds(1.3f);
			RestartLevel();
		}
		public void RestartLevel()
		{
			SceneManager.LoadScene(0);
		}
		private void SetWavesFromChildren()
		{
			Waves = this.GetComponentsInChildren<Wave>().ToList();
		}

	}
}