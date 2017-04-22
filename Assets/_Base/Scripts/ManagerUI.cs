﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private PanelBase panelMenu;
    [SerializeField]
    private PanelBase panelHUD;
    [SerializeField]
    private PanelBase panelLoading;

    // Panel HUD
    [Header("Ingame HUD"), SerializeField] private UnityEngine.UI.Text health1;
    [SerializeField] private UnityEngine.UI.Text health2;
    [SerializeField] private UnityEngine.UI.Text score;
    [SerializeField] private UnityEngine.UI.Text enemycount;

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
                break;

            case Structs.GameScene.Ingame:
                panelMenu.Hide();
                panelHUD.Show();
                panelLoading.Hide();
                break;

            case Structs.GameScene.LoadingGame:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Show();
                break;

            default:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Hide();
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
                    health1.text = "Health: --";
                }
                else
                {
                    health1.text = "Health: " + newHealth.ToString("00");
                }
                break;
            case 2:
                if (newHealth < 0)
                {
                    health2.text = "Health: --";
                }
                else
                {
                    health2.text = "Health: " + newHealth.ToString("00");
                }
                break;
        }
    }
    #endregion
}
