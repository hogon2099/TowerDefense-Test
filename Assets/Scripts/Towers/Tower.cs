using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        
        public GameObject bulletPrefab;
        public int Price;

        [SerializeField] private int damagePerBullet;
        [SerializeField] private float shootingDistanceMinimal;
        [SerializeField] private float shootingDistanceMaximum;
        [SerializeField] private float shootingRate;
        [SerializeField] private float shootingScatter;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletSize;

        private List<Enemy> currentWaveEnemies;

        private void Start()
        {
            InitializeTower();
        }
        public void InitializeTower()
		{
            currentWaveEnemies = GetEnemiesList();
            StartCoroutine(Shoot());
        }
        private List<Enemy> GetEnemiesList()
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            List<Enemy> enemiesList = new List<Enemy>();
            foreach (var enemy in enemies)
            {
                var kek = enemy.GetComponent<Enemy>();
                enemiesList.Add(kek);
            }

            return enemiesList;
        }
        private Enemy GetEnemyWithinShootingDistance()
        {
            currentWaveEnemies = GetEnemiesList();

            (Enemy, float) closestEnemy = (null, float.MaxValue);

            foreach (var enemy in currentWaveEnemies)
            {
                float distanceToEnemy = (enemy.transform.position - this.transform.position).magnitude;

                if ((distanceToEnemy > shootingDistanceMinimal && distanceToEnemy < shootingDistanceMaximum))
                {
                    closestEnemy = (enemy, distanceToEnemy);
                }
            }

            return closestEnemy.Item1;
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                Enemy enemy = GetEnemyWithinShootingDistance();

                if (enemy != null)
                {
                    SpawnBullets(enemy);
                    yield return new WaitForSeconds(1 / shootingRate);
                }
                else
                {
                    yield return null;
                }
            }
        }

        public virtual void SpawnBullets(Enemy enemy)
		{
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = (enemy.transform.position - this.transform.position).normalized * bulletSpeed;
            bullet.GetComponent<Bullet>().damage = this.damagePerBullet;
            bullet.transform.localScale = new Vector2(this.bulletSize, this.bulletSize);
        }

    }
}