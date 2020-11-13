using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    public new TMP_Text name;
    public TMP_Text chat;

    public int id;

    private GameObject child;

    private void Start()
    {
        gameObject.SetActive(false);
    }


    public IEnumerator ChatControl(ChatData chatData)
    {
        gameObject.SetActive(true);

        chat.text = "";

        var length = chatData.chatInfo.Length;

        chat.ForceMeshUpdate();

        // Get # of Visible Character in text object
        int visibleCount = 0;

        chat.maxVisibleCharacters = visibleCount;

        //Works as intended. Do not touch.
        for (var i = 0; i < length; i++)
        {
            chat.text = "";

            visibleCount = 0;
            chat.maxVisibleCharacters = visibleCount;

            chat.text = chatData.chatInfo[i];
            name.text = chatData.personName[i];


            int totalVisibleCharacters = chatData.chatInfo[i].Length;

            while (visibleCount < totalVisibleCharacters)
            {
                visibleCount += 1;
                
                chat.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                yield return new WaitForSeconds(0.1f);
            }

            chat.maxVisibleCharacters += 25;
            chat.text += " (Click to continue...)";

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        }

        gameObject.SetActive(false);
    }
}