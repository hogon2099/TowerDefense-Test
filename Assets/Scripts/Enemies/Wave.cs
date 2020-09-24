using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefense
{
	public class Wave : MonoBehaviour
	{
		public GameObject SpawnPoint;
		public Route Route;
		public Target Target;
		public float timeBetweenEnemies;

		public List<Enemy> Enemies;

		public delegate void WavesHandler();
		public static event WavesHandler EnemiesAllKilled;

		public delegate void EnemiesCountHandler(int count);
		public static event EnemiesCountHandler EnemiesCountChanged;

		public void InitializeWave()
		{
			this.Route.SetRoutePointsFromChildren();
			GetEnemiesFromChildren();
			SetRouteForEachEnemy();
			foreach (var enemy in Enemies)
			{
				enemy.EnemyKilled += UpdateEnemiesList;
			}
		}
		private void UpdateEnemiesList(Enemy enemy)
		{
			Enemies.Remove(enemy);
			enemy.gameObject.SetActive(false);

			EnemiesCountChanged?.Invoke(Enemies.Count);

			if (Enemies.Count <= 0)
			{
				EnemiesAllKilled.Invoke();
			}
		}

		private void GetEnemiesFromChildren()
		{
			Enemies = this.GetComponentsInChildren<Enemy>().ToList();
		}

		private void SetRouteForEachEnemy()
		{
			this.Route.RoutePoints.Add(Target.GetComponent<RoutePoint>());

			foreach (var enemy in Enemies)
			{
				//Debug.Log("wave = " + this.gameObject);
				//Debug.Log("enemy " + enemy + "; root is set");
				enemy.RoutePoints = this.Route.RoutePoints;
			}
			Debug.Log("Route was set for each enemy in wave" + this.gameObject);
		}
		public void SpawnEnemies()
		{
			EnemiesCountChanged?.Invoke(Enemies.Count);

			for (int i = 0; i < Enemies.Count; i++)
			{
				StartCoroutine(SpawnEnemy(Enemies[i], i));
			}
		}
		private IEnumerator SpawnEnemy(Enemy enemy, int seconds)
		{
			yield return new WaitForSeconds(seconds);
			enemy.enabled = true;
			enemy.transform.position = SpawnPoint.transform.position;
		}
	}
}