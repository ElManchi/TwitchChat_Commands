using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lexone.UnityTwitchChat;
using UnityEditor;
using System;

[AddComponentMenu("Unity Chat Listener")]
public class Twitch_Listener : MonoBehaviour
{
    [Header("Chatter Information")]
    public Chatter chatterObject;

    [Tooltip("Twitch Command Object to Show")]
    [SerializeField] private List<GameObject> elementToShowWithCommand;


    //// Start is called before the first frame update
    private void Start()
    {
        IRC.Instance.OnChatMessage += OnChatMessage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnChatMessage(Chatter chatter)
    {
        Debug.Log($"<color=#fef83e><b>[CHAT LISTENER]</b></color> New chat message from {chatter.tags.displayName}");

        // This is just to show the latest chatter object in the inspector
        chatterObject = chatter;

        StartCoroutine(RemoveAfterSeconds(chatter, 20));

    }

    IEnumerator RemoveAfterSeconds(Chatter chatter, int seconds)
    {
        switch (chatter)
        {
            case var expression when (((chatter.HasBadge("moderator") == true) || (chatter.HasBadge("broadcaster") == true)) && (chatter.message.ToLower().Contains("!sc") == true)):
                foreach (GameObject element in elementToShowWithCommand)
                {
                    element.SetActive(true);
                    yield return new WaitForSeconds(seconds);
                    element.SetActive(false);
                }
                break;
        }
    }
}