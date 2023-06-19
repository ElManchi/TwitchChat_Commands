using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

[AddComponentMenu("Unity Loop Scroller")]
public class Background_Scroller : MonoBehaviour
{
    [Tooltip("Initial Local X Position of the object")]
    [SerializeField] private float initial_X_LocalPosition; 
    [Tooltip("Final Local X Position of the object")]
    [SerializeField] private float final_X_LocalPosition;
    [Tooltip("Local Y Position of the object")]
    [SerializeField] private float const_Y_LocalPosition;
    [Tooltip("Space Between lines of text - rec 1000")]
    [SerializeField] private float const_SpaceBetweenLines;
    [Tooltip("The object's scroll speed")]
    [Range(-1f, 1f)]
    [SerializeField] public float ScrollSpeed = -1f;
    [Tooltip("Offset for the speed")]
    [SerializeField] private float offset = 25f;
    [Tooltip("Object to infinite loop")]
    [SerializeField] private TextMeshProUGUI object_to_Scroll_1;
    [Tooltip("Object to infinite loop")]
    [SerializeField] private TextMeshProUGUI object_to_Scroll_2;

    // The default Starting Position is at X: 2050 and Final Position is at X: -2730, for Y the initial position is 3.549988
    private Vector3 object_Starting_Position = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial and final position of the object
        object_Starting_Position = new Vector3(initial_X_LocalPosition, 0, 0);

        //Instantiate the parent so that all of the Children of the object mode along with the Parent
        object_to_Scroll_1.transform.parent = gameObject.transform;
        object_to_Scroll_2.transform.parent = gameObject.transform;

        //Set the initial position of the text +2100 of the size of the text + 100 of the space bewteen
        object_to_Scroll_2.transform.localPosition = new Vector3(object_to_Scroll_1.transform.localPosition.x + object_to_Scroll_1.rectTransform.sizeDelta.x + 100, object_to_Scroll_1.transform.localPosition.y, object_to_Scroll_1.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Move the Empty Object usinf the Offset to get the spped of the object
        object_to_Scroll_1.transform.localPosition = object_to_Scroll_1.transform.localPosition + new Vector3(offset, 0) * Time.deltaTime * ScrollSpeed;
        object_to_Scroll_2.transform.localPosition = object_to_Scroll_2.transform.localPosition + new Vector3(offset, 0) * Time.deltaTime * ScrollSpeed;

        switch (object_to_Scroll_1.transform.localPosition.x)
        {
            case var expression when object_to_Scroll_1.transform.localPosition.x <= final_X_LocalPosition:
                object_to_Scroll_1.transform.localPosition = object_Starting_Position + new Vector3(object_to_Scroll_2.transform.localPosition.x + const_SpaceBetweenLines, const_Y_LocalPosition);
                break;
        }

        switch (object_to_Scroll_2.transform.position.x)
        {
            case var expression when object_to_Scroll_2.transform.localPosition.x <= final_X_LocalPosition:
                object_to_Scroll_2.transform.localPosition = object_Starting_Position + new Vector3(object_to_Scroll_1.transform.localPosition.x + const_SpaceBetweenLines, const_Y_LocalPosition);
                break;
        }


        //Debug.Log("The X position for Object 1 is: " + object_to_Scroll_1.transform.localPosition.x); 
        //Debug.Log("The X position for Object 2 is: " + object_to_Scroll_2.transform.localPosition.x);
        //Debug.Log(">>>>The Y position for Object 1 is: " + object_to_Scroll_1.transform.localPosition.y);
        //Debug.Log("The Z position for Object 1 is: " + object_to_Scroll_1.transform.localPosition.z);
        //Debug.Log("The Z position for Object 2 is: " + object_to_Scroll_2.transform.localPosition.z);
    }

}

