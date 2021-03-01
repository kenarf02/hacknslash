using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemScript : MonoBehaviour
{
    public Item item;

    #region cursor change on interaction
    public CursorMode cursorMode = CursorMode.Auto;
    public Texture2D cursorTexture;
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);

    }
    #endregion
    private void OnDestroy()
    {
        OnMouseExit();
    }
}
