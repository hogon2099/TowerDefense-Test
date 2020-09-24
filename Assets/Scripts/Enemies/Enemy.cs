using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [HideInInspector]
        public List<RoutePoint> RoutePoints;
        public Vector2 currentDirection = Vector2.zero;
        public int nextRoutePoint = 0;
        public float speed;


        [SerializeField] private int _health;
        public int health
        {
            get => _health;
            set
            {
                if (value <= 0)
                {
                    EnemyKilled?.Invoke(this);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    _health = value;
                }
            }
        }
        private int currentHealth;
        private float walkingSpeed;

        public delegate void EnemiesKillsHandler(Enemy enemy);
        public event EnemiesKillsHandler EnemyKilled;

        private void SetDirection()
        {
            if (RoutePoints.Count <= 0) return;
            Vector2 currentPosition = this.transform.position;
            currentDirection = (Vector2)RoutePoints[nextRoutePoint].transform.position - currentPosition;
        }

        private void Start()
        {
            if (RoutePoints == null)
            {
                this.enabled = false;
            }

            SetDirection();
        }

        private void Update()
        {
            SetDirection();
            this.transform.Translate(currentDirection.normalized * Time.deltaTime * speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet!=null)
            {
                health -= bullet.damage;
            }

            if (collision.GetComponent<Target>())
            {
                EnemyKilled(this);
                this.gameObject.SetActive(false);
            }

            RoutePoint foundRoutePoint = collision.GetComponent<RoutePoint>();
            if (foundRoutePoint != null && (RoutePoints[nextRoutePoint] == foundRoutePoint) && nextRoutePoint < (RoutePoints.Count - 1))
            {
                nextRoutePoint++;
            }
        }
    }
}