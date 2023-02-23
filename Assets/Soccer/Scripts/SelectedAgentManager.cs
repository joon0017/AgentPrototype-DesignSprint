using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAgentManager : MonoBehaviour
{
    public GameObject char1;
    public GameObject char2;

    public bool clicked = false;

    void Update()
    {
        
    }

    public void AgentSelected()
    {
        if (clicked == false)
        {
            clicked = true;

            if (char1.transform.childCount == 0)
            {
                this.transform.SetParent(char1.transform);
                GetComponent<RectTransform>().anchoredPosition = new Vector3(150, -150, 0);
            }

            else if (char2.transform.childCount == 0)
            {
                this.transform.SetParent(char2.transform);
                GetComponent<RectTransform>().anchoredPosition = new Vector3(150, -150, 0);
            }
        }
    }
}
