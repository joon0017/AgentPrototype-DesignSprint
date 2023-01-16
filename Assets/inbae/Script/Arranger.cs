using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arranger : MonoBehaviour
{
    List<Transform> children;
    void Start()
    {
        children = new List<Transform>();

        UpdateChildren();
    }

    public void UpdateChildren()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            if(i == children.Count)
            {
                children.Add(null);
            }

            var child = transform.GetChild(i);

            if(child != children[i])
            {
                children[i] = child;
            }
        }

        children.RemoveRange(transform.childCount, children.Count - transform.childCount);
    }

    public int GetIndexByPosition(Transform icon, int skipIndex = -1)
    {
        int result = 0;
        for(int i = 0; i < children.Count; i++)
        {
            if(icon.position.x < children[i].position.x)
            {
                break;
            }
            else if(skipIndex != i)
            {
                result++;
            }
        }

        return result;
    }

    public void SwapIconIndex(int index1, int index2)
    {
        Central.SwapIcons(children[index1], children[index2]);
        UpdateChildren();
    }

    public void InsertIcon(Transform icon, int index)
    {
        children.Add(icon);
        icon.SetSiblingIndex(index);
        UpdateChildren();
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.name == "Character1" && this.name == "Layout1")
    //    {
    //        Debug.Log("길버트 ON");
    //        gilbertUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640, 180);
    //    }
    //}
}
