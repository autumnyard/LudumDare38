using UnityEngine;
using UnityEngine.UI;

public class PanelMenu : PanelBase
{
    [SerializeField] private Text healthValueObject;
    [HideInInspector] public int healthValue = 6;
    private int healthValueMax = 9;
    private int healthValueMin = 3;

    public TweenMove tweenCreditsOpen;
    public TweenMove tweenCreditsClose;
    private Vector3 creditsInitPosition;

    private void Start()
    {
        healthValueObject.text = Director.Instance.managerEntity.lifes.ToString();
        creditsInitPosition = tweenCreditsOpen.transform.localPosition;


    }
    public void ButtonPlay2()
    {
        Director.Instance.GameBegin(2);
    }
    public void ButtonPlay3()
    {
        Director.Instance.GameBegin(3);
    }

    public void ButtonHealthMore()
    {
        int currentLifeValue = Director.Instance.managerEntity.lifes;
        currentLifeValue++;
        if (currentLifeValue > healthValueMax)
        {
            currentLifeValue = healthValueMax;
        }
        healthValueObject.text = currentLifeValue.ToString();
        Director.Instance.managerEntity.lifes = currentLifeValue;
    }
    public void ButtonHealthLess()
    {
        int currentLifeValue = Director.Instance.managerEntity.lifes;
        currentLifeValue--;
        if (currentLifeValue < healthValueMin)
        {
            currentLifeValue = healthValueMin;
        }
        healthValueObject.text = currentLifeValue.ToString();
        Director.Instance.managerEntity.lifes = currentLifeValue;
    }

    public void ButtonCreditsOpen()
    {
        // tweenCreditsClose.enabled = false;
        //tweenCreditsOpen.enabled = true;
        tweenCreditsClose.Stop();
        tweenCreditsOpen.transform.localPosition = creditsInitPosition;
        tweenCreditsOpen.Play();
        tweenCreditsOpen.GetComponent<Button>().interactable = false;
        tweenCreditsClose.GetComponent<Button>().interactable = true;
    }
    public void ButtonCreditsClose()
    {
        //tweenCreditsClose.enabled = true;
        //tweenCreditsOpen.enabled = false;
        tweenCreditsOpen.Stop();
        tweenCreditsClose.Play();
        tweenCreditsOpen.GetComponent<Button>().interactable = true;
        tweenCreditsClose.GetComponent<Button>().interactable = false;
    }

    public void ButtonExit()
    {
        Director.Instance.Exit();
    }
}
