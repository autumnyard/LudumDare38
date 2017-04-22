using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEntity : MonoBehaviour
{

    [SerializeField] private GameObject prefabPlayer1;
    private GameObject player1;
    [HideInInspector] public EntityPlayer playerScript1;


    [SerializeField] private GameObject prefabPlayer2;
    private GameObject player2;
    [HideInInspector] public EntityPlayer playerScript2;

    void Awake()
    {
        Director.Instance.managerEntity = this;
    }


    #region Entity Management
    public void SummonPlayer()
    {
        player1 = Instantiate(prefabPlayer1, this.transform) as GameObject;
        playerScript1 = player1.GetComponent<EntityPlayer>();

        player2 = Instantiate(prefabPlayer2, this.transform) as GameObject;
        playerScript2 = player2.GetComponent<EntityPlayer>();
    }

    public void SummonPlayer(int which)
    {
        switch (which)
        {
            default:
            case 1:
                player1 = Instantiate(prefabPlayer1, this.transform) as GameObject;
                playerScript1 = player1.GetComponent<EntityPlayer>();
                break;
            case 2:
                player2 = Instantiate(prefabPlayer2, this.transform) as GameObject;
                playerScript2 = player2.GetComponent<EntityPlayer>();
                break;
        }
    }

    private void RemovePlayers()
    {
        if (player1 != null)
        {
            Destroy(player1);
            player1 = null;
        }

        if (player2 != null)
        {
            Destroy(player2);
            player2 = null;
        }

    }

    private void RemovePlayers(int which)
    {
        switch (which)
        {
            default:
            case 1:
                if (player1 != null)
                {
                    Destroy(player1);
                    player1 = null;
                }
                break;
            case 2:

                if (player2 != null)
                {
                    Destroy(player2);
                    player2 = null;
                }
                break;
        }
    }

    public void Reset()
    {
        RemovePlayers();
    }
    #endregion
}
