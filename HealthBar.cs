using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {

    public Player playerReference;

    int currentHP;
    int maxHP;
    //Image hBar;

    [SerializeField]
    private RectTransform healthBarRect;

    // Use this for initialization
    // void Start(){

    //   hBar = GetComponent<Image>();

    // }

    void FindPlayerRef()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

       // Debug.LogError("Value is" + _cur);

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        
    }

    // Update is called once per frame
    void Update () {


        if (playerReference == null)
            FindPlayerRef();


        currentHP = playerReference.stats.curHealth;
        maxHP = playerReference.stats.maxHealth;

      SetHealth(currentHP, maxHP);

        // hBar.fillAmount = (currentHP / maxHP);
    }
}
