using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthText : MonoBehaviour {

    public Player playerReference;
    //public GameObject player1;
    int currentHP;
    int maxHP;
    //Image hBar;

    [SerializeField]
    private Text healthText;


    void FindPlayerRef()
    {
      playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    public void SetHealth(int _cur, int _max)
    {
          healthText.text = _cur + "/" + _max + " HP";
    }

    // Update is called once per frame
    void Update()
    {

        if (playerReference == null)
            FindPlayerRef();
        
        currentHP = playerReference.stats.curHealth;

        //Debug.LogError("Current HP is " + currentHP);

        maxHP = playerReference.stats.maxHealth;

        SetHealth(currentHP, maxHP);
    }
}
