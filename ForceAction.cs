using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ForceAction : MonoBehaviour
{
     public GameController GameController;
     public ChatData chat;

     public int id;
     
    

    public void Move()
    {
        GameController.ChangeScene(id);
    }


    public void ActivateChat()
    {
        StartCoroutine(Chatty(chat));
    }

    IEnumerator Chatty(ChatData chat)
    {
        //I hate my life rn
        yield return new WaitForSeconds(2f);
         GameController.StartChat(chat);
        
    }

    private void Start()
    {
        ActivateChat();
    }
}
