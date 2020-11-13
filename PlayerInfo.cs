using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    //Base currency
    public TMP_Text moneyText;
    public float cash = 0;

    //Player Health
    private Animator anim;
    public GameObject damageScreen;
    public Slider healthBar;
    public TMP_Text healthText;
    private SysController gameController;
    private bool SelfHurt = false;

    public float health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        //Health Related
        Mathf.Clamp(health, 0, 100);
        anim = damageScreen.GetComponent<Animator>();

        //no clue atm.
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<SysController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void Update()
    {
        //Money
        moneyText.SetText("Money: $" + cash);


        //Health
        healthBar.value = health;
        healthText.SetText("Health: " + health);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!SelfHurt)
            {
                health = health - 10;
                anim.SetTrigger("GetHurt");
                SelfHurt = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            SelfHurt = false;


        }
    }
    //Money Based collecting
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Currency")
        {
            var tempCash = other.collider.GetComponent<Money>().value;

            cash += tempCash;

            Destroy(other.gameObject);
        }
    }

    public void Damaged(float takeDamage)
    {

        health -= takeDamage;
    }
}

