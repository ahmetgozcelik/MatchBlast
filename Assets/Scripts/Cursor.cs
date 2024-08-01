using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cursor : Singleton<Cursor>
{
    private SpriteRenderer spriteRenderer;

    private Matchable[] selected;

    [SerializeField]
    private Vector2Int verticalStrech = new Vector2Int(1, 2),
    horizontalStrech = new Vector2Int(2, 1);

    [SerializeField]
    private Vector3 halfUp = Vector3.up / 2,
                    halfDown = Vector3.down / 2,
                    halfLeft = Vector3.left / 2,
                    halfRight = Vector3.right / 2;
    protected override void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;

        selected = new Matchable[2];
    }

    public void SelectFirst(Matchable toSelect)
    {
        selected[0] = toSelect;

        if (!enabled || selected[0] == null)
        {
            return;
        }

        transform.position = toSelect.transform.position;

        spriteRenderer.size = Vector2.one;
        spriteRenderer.enabled = true;
    }
    public void SelectSecond(Matchable toSelect)
    {
        selected[1] = toSelect;

        if (!enabled || selected[0] == null || selected[1] == null || !selected[0].Idle || !selected[1].Idle || selected[0] == selected[1])
        {
            return;
        }

        if (SelectedAreAdjacent())
        {
            print("Swapping matchables at positions : (" + selected[0].position.x + ", " + selected[0].position.y + " ) and ( " + selected[1].position.x + ", " + selected[1].position.y + ")");
        }
        SelectFirst(null);
    }

    private bool SelectedAreAdjacent()
    {
        if (selected[0].position.x == selected[1].position.x)
        {
            if (selected[0].position.y == selected[1].position.y + 1)
            {
                spriteRenderer.size = verticalStrech;
                transform.position += halfDown;
                return true;
            }
            else if (selected[0].position.y == selected[1].position.y - 1)
            {
                spriteRenderer.size = verticalStrech;
                transform.position += halfUp;
                return true;
            }
        }
        else if (selected[0].position.y == selected[1].position.y)
        {
            if (selected[0].position.x == selected[1].position.x + 1)
            {
                spriteRenderer.size = horizontalStrech;
                transform.position += halfLeft;
                return true;
            }
            else if (selected[0].position.x == selected[1].position.x - 1)
            {
                spriteRenderer.size = horizontalStrech;
                transform.position += halfRight;
                return true;
            }
        }

        return false;
    }
}
