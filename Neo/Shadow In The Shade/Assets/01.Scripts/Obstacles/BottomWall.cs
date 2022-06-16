using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BottomWall : MonoBehaviour
{
    private Tilemap tile;
    public Tilemap Tile
    {
        get 
        {
            if(tile == null)
                tile = GetComponent<Tilemap>();
            return tile; 
        }
    }

    private readonly float a = 0.5f;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & LayerMask.GetMask("Player")) > 0)
        {
            Tile.color = new Color(Tile.color.r, Tile.color.g, Tile.color.b, a);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Tile.color.a != 1)
        {
            Tile.color = new Color(Tile.color.r, Tile.color.g, Tile.color.b, 1);
        }
    }

}
