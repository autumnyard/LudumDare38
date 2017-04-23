using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerUI : MonoBehaviour
{
    [Header("Components")]
    //, SerializeField]
    public PanelMenu panelMenu;
    //[SerializeField]
    public PanelBase panelHUD;
    //[SerializeField]
    public PanelBase panelLoading;
    //[SerializeField]
    public PanelScore panelScore;
    //[SerializeField]
    public PanelBase panelPause;

    // Panel HUD
    const int maxPlayers = 3;
    [Header("Ingame HUD"), SerializeField] private UnityEngine.UI.Text[] health = new UnityEngine.UI.Text[maxPlayers];
    private string healthText = "Lifes left: ";
    [SerializeField] private Image[] playerSprite = new Image[3];


    void Awake()
    {
        Director.Instance.managerUI = this;
    }

    private void Update()
    {
        if (Director.Instance.currentScene == Structs.GameScene.Ingame)
        {

        }
    }

    #region Panel management
    public void SetPanels()
    {
        switch (Director.Instance.currentScene)
        {
            case Structs.GameScene.Menu:
                panelMenu.Show();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Hide();
                panelPause.Hide();
                break;

            case Structs.GameScene.Ingame:
                panelMenu.Hide();
                panelHUD.Show();
                panelLoading.Hide();
                panelScore.Hide();
                panelPause.Hide();

                if (Director.Instance.currentGameMode == Structs.GameMode.Multi3players)
                {
                    health[2].transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    health[2].transform.parent.gameObject.SetActive(false);

                }
                break;

            case Structs.GameScene.Score:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Show();
                panelPause.Hide();
                break;

            case Structs.GameScene.LoadingGame:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Show();
                panelScore.Hide();
                panelPause.Hide();
                break;

            default:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Hide();
                panelPause.Hide();
                break;
        }
    }
    #endregion

    #region Ingame HUD management
    public void SetHealth(int id, int newHealth)
    {
        if (newHealth < 0)
        {
            health[id - 1].text = healthText + " --";
        }
        else
        {
            health[id - 1].text = healthText + newHealth.ToString("0");
        }
    }
    
    public void SetPlayerSprite(int index, Sprite spr)
    {
        playerSprite[index].sprite = spr;
    }
    #endregion


}
