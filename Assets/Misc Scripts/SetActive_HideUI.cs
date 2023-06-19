using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lexone.UnityTwitchChat;
using UnityEditor;
using System;

[AddComponentMenu("Hide UI Elements")]
public class SetActive_HideUI : MonoBehaviour
{
    [Tooltip("Twitch Command Object to Show")]
    [SerializeField] private List<GameObject> elementToShowWithCommand;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject element in elementToShowWithCommand)
        {
            element.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
