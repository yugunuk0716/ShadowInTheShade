using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    public Texture2D deafultCursor;
    public Texture2D clickCursor;
    public Texture2D canClickCursor;


    private void Awake()
    {
		cursorSet(deafultCursor);

	}

	void cursorSet(Texture2D tex)
	{
		CursorMode mode = CursorMode.Auto;
		float xspot = tex.width / 2;
		float yspot = tex.height / 2;
		Vector2 hotSpot = new Vector2(xspot, yspot);
		Cursor.SetCursor(tex, hotSpot, mode);
	}
	/*
		private void Update()
		{
			UpdateMouseCursor();

		}

		void UpdateMouseCursor()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f))
			{
				if (hit.collider.gameObject.layer != LayerMask.GetMask("UI"))
				{
					Cursor.SetCursor(deafultCursor, Vector2.zero, CursorMode.Auto);
				}
				else
				{
					Cursor.SetCursor(canClickCursor, Vector2.zero, CursorMode.Auto);
				}
			}
		}*/
}
