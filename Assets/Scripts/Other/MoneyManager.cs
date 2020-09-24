using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefense
{
	class MoneyManager: MonoBehaviour
	{
		[SerializeField] private int _money;
		public int Money
		{
			get => _money;

			set
			{
				_money = value;
				MoneyChanged(value);
			}
		}
		public static MoneyManager Instance;

		public delegate void MoneyHandler(int money);
		public event MoneyHandler MoneyChanged;
		
		private void Start()
		{
			MoneyChanged(Money);
		}
		private MoneyManager()
		{
			Instance = this;
		}
	}
}
