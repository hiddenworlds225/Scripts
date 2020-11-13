using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public ItemData item;

    public Texture2D cursorType;

    private GameController gameController;

    public int id;
    public ChatData chat;

    public Button Button;


    public enum Calltypes
    {
        Move,
        Interact,
        Chat
    }

    public Calltypes calling;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (Button != null)
        {
            Button.onClick.AddListener(ButtonPressed);
        }
    }

    private void OnMouseDown()
    {
        switch (calling)
        {
            case Calltypes.Move:
                gameController.ChangeScene(id);
                break;
            case Calltypes.Interact:
                gameController.StartChat(chat);
                break;
            case Calltypes.Chat:
                throw new ArgumentOutOfRangeException();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnMouseEnter()
    {
        gameController.ActivateTooltip(item);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameController.DeactivateTooltip();
    }

    private void ButtonPressed()
    {
        OnMouseDown();
    }
}