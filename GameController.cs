using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TooltipData tooltipData;
    public ChatController chatCon;

    public Animator transition;

    public float transitionTime = 1f;

    private static readonly int start = Animator.StringToHash("Start");

    public void ActivateTooltip(ItemData itemData)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        tooltipData.gameObject.SetActive(true);
        tooltipData.tooltipText.text = itemData.itemName;
    }

    public void DeactivateTooltip()
    {
        tooltipData.gameObject.SetActive(false);
    }

    public void ChangeScene(int id)
    {
        StartCoroutine(LoadScene(id));
    }

    IEnumerator LoadScene(int id)
    {
        transition.SetTrigger(start);
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(id);
    }

    public void StartChat(ChatData chat)
    {
        StartCoroutine(chatCon.ChatControl(chat));
    }

    private void Start()
    {

    }
}