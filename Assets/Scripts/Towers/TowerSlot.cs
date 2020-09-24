using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense
{
	public class TowerSlot : MonoBehaviour, IDropHandler
	{
		public bool IsSlotTaken;
		public void OnDrop(PointerEventData eventData)
		{
			if (eventData.pointerDrag != null && 
				!IsSlotTaken &&
				MoneyManager.Instance.Money >= eventData.pointerDrag.GetComponent<TowerContainer>().Tower.GetComponent<Tower>().Price)
			{
				IsSlotTaken = true;
				MoneyManager.Instance.Money -= eventData.pointerDrag.GetComponent<TowerContainer>().Tower.GetComponent<Tower>().Price;

				GameObject newTower = Instantiate(eventData.pointerDrag.GetComponent<TowerContainer>().Tower, this.transform.position, Quaternion.identity);
				newTower.GetComponent<Tower>().InitializeTower();

				Debug.Log("tower = " + eventData.pointerDrag.GetComponent<RectTransform>().position);
				Debug.Log("slot = " + this.GetComponent<RectTransform>().position);
			}
		}
	}
}