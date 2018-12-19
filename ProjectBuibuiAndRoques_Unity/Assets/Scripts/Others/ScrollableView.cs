using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableView : MonoBehaviour
{
    public Transform movingTransform;
    public float sensitivity = 100;

    // Start is called before the first frame update
    void Start()
    {
        movingTransform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            movingTransform.position += new Vector3(0, -Input.GetAxis("Mouse ScrollWheel") * sensitivity, 0);
        }

        if (movingTransform.localPosition.y < 0)
        {
            movingTransform.localPosition = new Vector3(0, 0, 0);
        }
        if(movingTransform.localPosition.y + ((RectTransform)transform).rect.height > ((RectTransform)movingTransform).rect.height)
        {
            movingTransform.localPosition = new Vector3(0, ((RectTransform)movingTransform).rect.height - ((RectTransform)transform).rect.height, 0);
        }

        
    }
}
