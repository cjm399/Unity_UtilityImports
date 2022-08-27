using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class PlaceItemsInGrid : MonoBehaviour
    {
        public bool fillTopToBottom = true;
        public Vector3 initialGridPositionOffset = Vector3.zero;
        public float verticalSpacing = 10f;
        public float horizontalSpacing = 10f;
        [Min(1)]
        public uint maxItemsPerLine = 10;
        public bool tryCenterHorizontal = true;
        public bool tryCenterVertical = true;

        private void OnValidate()
        {
            SpaceOutChildren();
        }

        public void SpaceOutChildren()
        {
            bool myFillTopToBottom = fillTopToBottom;
            Vector3 myInitialGridPositionOffset = initialGridPositionOffset;
            float myVerticalSpacing = verticalSpacing;
            float myHorizontalSpacing = horizontalSpacing;
            uint myMaxItemsPerLine = maxItemsPerLine;
            bool myTryCenterHorizontal = tryCenterHorizontal;
            bool myTryCenterVertical = tryCenterVertical;


            bool isCanvas = this.GetComponent<RectTransform>() != null;

            List<GameObject> children = gameObject.GetAllChildren();

            Vector3 initialPos = transform.position + myInitialGridPositionOffset;

            if(isCanvas)
            {
                initialPos = GetComponent<RectTransform>().anchoredPosition;
                initialPos += myInitialGridPositionOffset;
            }

            if (myFillTopToBottom)
            {
                myVerticalSpacing *= -1;
            }

            if (myTryCenterHorizontal)
            {
                int numHor = Mathf.Min(children.Count, (int)myMaxItemsPerLine)-1;
                float totalHorizontalSpacing = numHor * myHorizontalSpacing;
                initialPos.x -= totalHorizontalSpacing / 2f;
            }
            
            if (myTryCenterVertical)
            {
                int numVer = (children.Count-1) / (int)myMaxItemsPerLine;
                float totalVerticalSpacing = numVer * myVerticalSpacing;
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
                currPos.x += myHorizontalSpacing;
                if (++itemsOnLine == myMaxItemsPerLine)
                {
                    itemsOnLine = 0;
                    currPos.x = initialPos.x;
                    currPos.y += myVerticalSpacing;
                }
            }
        }
    }
}