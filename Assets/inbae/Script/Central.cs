using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Central : MonoBehaviour
{
    public Transform ivisibleIcon;
    List<Arranger> arrangers;
    Arranger workArranger;
    int originIdx;

    void Start()
    {
        arrangers = new List<Arranger>();

        var arrs = transform.GetComponentsInChildren<Arranger>();

        for(int i=0; i<arrs.Length; i++)
        {
            arrangers.Add(arrs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SwapIcons(Transform sour, Transform dest)
    {
        Transform sourParent = sour.parent;
        Transform destParent = dest.parent;
        int sourIndex = sour.GetSiblingIndex();
        int destIndex = dest.GetSiblingIndex();

        sour.SetParent(destParent);
        sour.SetSiblingIndex(destIndex);

        dest.SetParent(sourParent);
        dest.SetSiblingIndex(sourIndex);
    }

    void SwapIcon(Transform sour, Transform dest)
    {
        SwapIcons(sour, dest);

        arrangers.ForEach(t => t.UpdateChildren());
    }

    bool ContainPos(RectTransform rt, Vector2 pos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rt, pos);
    }

    void BeginDrag(Transform icon)
    {
        workArranger = arrangers.Find(t => ContainPos(t.transform as RectTransform, icon.position));
        originIdx = icon.GetSiblingIndex();

        SwapIcon(ivisibleIcon, icon);
    }

    void Drag(Transform icon)
    {
        var currentArranger = arrangers.Find(t => ContainPos(t.transform as RectTransform, icon.position));

        if(currentArranger == null)
        {
            bool updateChildren = transform != ivisibleIcon.parent;

            ivisibleIcon.SetParent(transform);

            if(updateChildren)
            {
                arrangers.ForEach(t => t.UpdateChildren());
            }
        }

        else
        {
            bool insert = ivisibleIcon.parent == transform;
            if(insert)
            {
                int index = currentArranger.GetIndexByPosition(icon);

                ivisibleIcon.SetParent(currentArranger.transform);
                currentArranger.InsertIcon(ivisibleIcon, index);
            }
            else
            {
                int invisibleIconIndex = ivisibleIcon.GetSiblingIndex();
                int targetIndex = currentArranger.GetIndexByPosition(icon, ivisibleIcon.GetSiblingIndex());

                if(invisibleIconIndex != targetIndex)
                {
                    currentArranger.SwapIconIndex(invisibleIconIndex, targetIndex);
                }
            }
            
            
        }
    }

    void EndDrag(Transform icon)
    {
        if(ivisibleIcon.parent == transform)
        {
            workArranger.InsertIcon(icon, originIdx);
            workArranger = null;
            originIdx = -1;
        }
        else
        {
            SwapIcon(ivisibleIcon, icon);
        }
        
    }
}
