using ICities;
using UnityEngine;
using System.Collections;
using ColossalFramework;
using ColossalFramework.UI;

namespace FavoriteCims
{
	public class WindowController : UIDragHandle 
	{
		Vector3 MousePos;
		float maX;
		float maY;
		float deltaX;
		float deltaY;

		public UIComponent ComponentToMove;
		public bool Stop = false;

		public override void Start ()
		{
			this.maX = Input.mousePosition.x;
			this.maY = Input.mousePosition.y;
			this.deltaX = maX - ComponentToMove.absolutePosition.x;
			this.deltaY = maY - ComponentToMove.absolutePosition.y * -1;
		}

		public override void Update () 
		{
			if(Stop)
				return;

			if (Input.GetMouseButton (0)) 
			{
				this.maX = Input.mousePosition.x;
				this.maY = Input.mousePosition.y;
				this.MousePos = new Vector3 (this.maX-this.deltaX, ((this.maY) * -1) + this.deltaY);
				ComponentToMove.absolutePosition = this.MousePos;
			}
		}
	}
}
