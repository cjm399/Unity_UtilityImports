using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class PlaceItemsInGrid : MonoBehaviour
    {
        public Vector3 initialGridPositionOffset = Vector3.zero;
        public float verticalSpacing = 10f;
        public float horizontalSpacing = 10f;
        [Min(1)]
        public uint maxItemsPerLine = 10;
        public bool tryCenterHorizontal = false;
        public bool tryCenterVertical = false;

        private void OnValidate()
        {
            SpaceOutChildren();
        }

        public void SpaceOutChildren()
        {
            bool isCanvas = this.GetComponent<RectTransform>() != null;

            List<GameObject> children = gameObject.GetAllChildren();

            Vector3 initialPos = transform.position + initialGridPositionOffset;

            if(isCanvas)
            {
                initialPos = GetComponent<RectTransform>().anchoredPosition;
                initialPos += initialGridPositionOffset;
            }

            if(tryCenterHorizontal)
            {
                int numHor = Mathf.Min(children.Count, (int)maxItemsPerLine)-1;
                float totalHorizontalSpacing = numHor * horizontalSpacing;
                initialPos.x -= totalHorizontalSpacing / 2f;
            }
            
            if (tryCenterVertical)
            {
                int numVer = (children.Count-1) / (int)maxItemsPerLine;
                float totalVerticalSpacing = numVer * verticalSpacing;
                initialPos.y -= totalVerticalSpacing / 2f;
            }

            Vector3 currPos = initialPos;
            int itemsOnLine = 0;

            for (int childIndex = 0; childIndex < children.Count; ++childIndex)
            {
                if (isCanvas)
                {
                    RectTransform rt = children[childIndex].GetComponent<RectTransform>();
                    if(rt != null)
                    {
                        rt.anchoredPosition = currPos;
                    }
                }
                else
                {
                    children[childIndex].transform.position = currPos;
                }
                currPos.x += horizontalSpacing;
                if (++itemsOnLine == maxItemsPerLine)
                {
                    itemsOnLine = 0;
                    currPos.x = initialPos.x;
                    currPos.y += verticalSpacing;
                }
            }
        }
    }
}