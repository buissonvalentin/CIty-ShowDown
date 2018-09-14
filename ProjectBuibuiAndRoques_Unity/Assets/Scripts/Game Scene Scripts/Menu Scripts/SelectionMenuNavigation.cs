using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuNavigation : MonoBehaviour {

    public Button left;
    public Button right;
    List<GameObject> list;
    Transform container;
    int index;

	// Use this for initialization
	void Start () {
        container = transform.Find("Container");
        index = 0;
        left.onClick.AddListener(() =>
        {
            index--;
            if (index < 0) index = 0;
        });
        right.onClick.AddListener(() =>
        {
            index++;
            if (index + 6 >= container.childCount) index = container.childCount  - 6;
        });
    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < container.childCount; i++)
        {
            if(i >= index && i < index + 6)
            {
                container.GetChild(i).gameObject.SetActive(true);
            }
            else container.GetChild(i).gameObject.SetActive(false);
        }
	}
}
