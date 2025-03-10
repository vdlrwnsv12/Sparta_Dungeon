using UnityEngine.UI; 
using UnityEngine; 


public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Text healthText;
    public Image uiBar;


    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        //ui
        uiBar.fillAmount = GetPercentage();
        if(healthText != null)
        {
        healthText.text = $"{(int)curValue}/{(int)maxValue}";
        }
    }
    float GetPercentage()
    {
        return curValue / maxValue;
    }
    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }
    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
