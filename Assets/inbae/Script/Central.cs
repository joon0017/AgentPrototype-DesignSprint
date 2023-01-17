using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Central : MonoBehaviour
{
    public Transform ivisibleIcon;
    public GameObject infoUI;
    GameObject plusButton1;
    GameObject plusButton2;
    GameObject plusButton3;
    List<Arranger> arrangers;
    Arranger workArranger;
    Arranger endArranger;
    int originIdx;

    void Start()
    {
        arrangers = new List<Arranger>();

        var arrs = transform.GetComponentsInChildren<Arranger>();

        for(int i=0; i<arrs.Length; i++)
        {
            arrangers.Add(arrs[i]);
        }

        plusButton1 = GameObject.Find("PlusButton1");
        plusButton2 = GameObject.Find("PlusButton2");
        plusButton3 = GameObject.Find("PlusButton3");
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

        GameObject thisIcon = GameObject.Find(icon.name);
        Image iconImage = thisIcon.GetComponent<Image>();
        var onColor = new Color(255/ 255f, 255/ 255f, 255/ 255f, 255/ 255f);

        iconImage.color = onColor;

        Image buttonImage;

        string infoName = icon.name + "Info";
        infoUI = GameObject.Find(infoName);

        if(workArranger.name == "Layout1")
        {
            infoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(1280, 180);
            buttonImage = plusButton1.GetComponent<Image>();
            buttonImage.enabled = true;
        }
        else if(workArranger.name == "Layout2")
        {
            infoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(1280, 180);
            buttonImage = plusButton2.GetComponent<Image>();
            buttonImage.enabled = true;
        }
        else if(workArranger.name == "Layout3")
        {
            infoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(1280, 180);
            buttonImage = plusButton3.GetComponent<Image>();
            buttonImage.enabled = true;
        }

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
        endArranger = arrangers.Find(t => ContainPos(t.transform as RectTransform, icon.position));

        string currName = icon.name + "Name";
        GameObject imageName = GameObject.Find(currName);
        Text imageText = imageName.GetComponent<Text>();

        GameObject thisIcon = GameObject.Find(icon.name);
        Image iconImage = thisIcon.GetComponent<Image>();
        var noneColor = new Color(255/ 255f, 255/ 255f, 255/ 255f, 0);
        var onColor = new Color(255/ 255f, 255/ 255f, 255/ 255f, 255/ 255f);

        Image buttonImage;

        if(endArranger.name == "Layout1")
        {
            buttonImage = plusButton1.GetComponent<Image>();
            buttonImage.enabled = false;
            infoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640, 180);
            imageText.enabled = false;
            iconImage.color = noneColor;
        }
        else if(endArranger.name == "Layout2")
        {
            buttonImage = plusButton2.GetComponent<Image>();
            buttonImage.enabled = false;
            infoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 180);
            imageText.enabled = false;
            iconImage.color = noneColor;
        }
        else if(endArranger.name == "Layout3")
        {
            buttonImage = plusButton3.GetComponent<Image>();
            buttonImage.enabled = false;
            infoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(640, 180);
            imageText.enabled = false;
            iconImage.color = noneColor;
        }
        else
        {
            imageText.enabled = true;
        }

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
