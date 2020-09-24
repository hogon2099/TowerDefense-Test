using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{ 
	public class Route : MonoBehaviour
	{
		[HideInInspector]
		public List<RoutePoint> RoutePoints;

		public void SetRoutePointsFromChildren()
		{
			RoutePoints = this.GetComponentsInChildren<RoutePoint>().ToList();
		}
	}
}