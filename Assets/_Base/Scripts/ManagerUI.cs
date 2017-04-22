using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Panel HUD
    [Header("Ingame HUD"), SerializeField] private UnityEngine.UI.Text health1;
    [SerializeField] private UnityEngine.UI.Text health2;
    [SerializeField] private UnityEngine.UI.Text score;
    [SerializeField] private UnityEngine.UI.Text enemycount;
    private string healthText = "Lifes: ";

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
                break;
            case Structs.GameScene.Ingame:
                panelMenu.Hide();
                panelHUD.Show();
                panelLoading.Hide();
                panelScore.Hide();
                break;

            case Structs.GameScene.Score:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Show();
                break;

            case Structs.GameScene.LoadingGame:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Show();
                panelScore.Hide();
                break;

            default:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Hide();
                break;
        }
    }
    #endregion

    #region Inagem HUD management
    public void SetHealth(int id, int newHealth)
    {
        switch(id)
        {
            default:
            case 1:
                if (newHealth < 0)
                {
                    health1.text = healthText+" --";
                }
                else
                {
                    health1.text = healthText + newHealth.ToString("0");
                }
                break;
            case 2:
                if (newHealth < 0)
                {
                    health2.text = healthText+" --";
                }
                else
                {
                    health2.text = healthText + newHealth.ToString("0");
                }
                break;
        }
    }
    #endregion
}
