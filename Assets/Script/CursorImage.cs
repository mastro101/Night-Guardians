using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorImage : MonoBehaviour {

    public Texture2D cursorImage;


	// Use this for initialization
	void Start () {

        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);

	}

}

