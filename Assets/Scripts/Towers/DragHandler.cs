using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public static GameObject ItemBeingDragged;
		private Vector3 startPosition;
		private RectTransform rectTransform;
		private CanvasGroup canvasGroup;

		private void Awake()
		{
			rectTransform = this.GetComponent<RectTransform>();
			canvasGroup = this.GetComponent<CanvasGroup>();
		}
		public void OnBeginDrag(PointerEventData eventData)
		{
			ItemBeingDragged = this.gameObject;
			startPosition = this.transform.position;
			canvasGroup.blocksRaycasts = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			rectTransform.anchoredPosition += eventData.delta;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			ItemBeingDragged = null;
			this.transform.position = startPosition;
			canvasGroup.blocksRaycasts = true;
		}


	}
}