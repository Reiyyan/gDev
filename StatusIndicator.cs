using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    
    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR: NO HEALTH BAR OBEJECT REFERENCED!");
        }

        if (healthText == null)
        {
            Debug.LogError("STATUS INDICATOR: NO HEALTH TEXT REFERENCED!");
        }
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);

        healthText.text = _cur + "/" + _max + " HP";

        //Checking if object needs to be deleted
        RemoveObject(_cur);
    }


    void RemoveObject(int checker)
    {
        if (checker <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
