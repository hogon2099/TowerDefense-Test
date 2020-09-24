using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
	public class Target : MonoBehaviour
	{
		[SerializeField] private int _health = 2;
		public static Target Instance;
		private int health
		{
			get => _health;
			set
			{
				if (value <= 0)
				{
					TargetDestroed?.Invoke();
					_health = 0;
				}
				else
				{
					_health = value;
					TargetHealthValueChanged?.Invoke(value);
				}
			}
		}

		public delegate void TargetDestructionHandler();
		public  event TargetDestructionHandler TargetDestroed;

		public delegate void TargetHealthValueHandler(int value);
		public event TargetHealthValueHandler TargetHealthValueChanged;

		private Target()
		{
			Instance = this;
		}
		private void Start()
		{
			TargetHealthValueChanged?.Invoke(health);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<Enemy>())
			{
				health--;
			}
		}
	}
}