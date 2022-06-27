using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaceItemsInGrid : MonoBehaviour
{
    public Vector3 initialGridPositionOffset = Vector3.zero;
    public float verticalSpacing = 10f;
    public float horizontalSpacing = 10f;
    public int maxItemsPerLine = 10;

    public void SpaceOutChildren()
    {
        List<GameObject> children = gameObject.GetAllChildren();

        Vector3 initialPos = transform.position + initialGridPositionOffset;
        Vector3 currPos = initialPos;
        int itemsOnLine = 0;

        for(int childIndex = 0; childIndex < children.Count; ++childIndex)
        {
            children[childIndex].transform.position = currPos;
            currPos.x += horizontalSpacing;
            if(++itemsOnLine == maxItemsPerLine)
            {
                itemsOnLine = 0;
                currPos.x = initialPos.x;
                currPos.y += verticalSpacing;
            }
        }
    }
}
