using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEntity : MonoBehaviour
{

    //[SerializeField] private GameObject prefabPlayer1;
    //private GameObject player1;
    //[HideInInspector] public EntityPlayer playerScript1;


    //[SerializeField] private GameObject prefabPlayer2;
    //private GameObject player2;
    //[HideInInspector] public EntityPlayer playerScript2;

    const int maxPlayers = 4;

    [SerializeField] private GameObject[] prefabPlayer = new GameObject[maxPlayers];
    //private List<GameObject> player;
    //[HideInInspector] public List<EntityPlayer> playerScript;
    private GameObject[] players = new GameObject[maxPlayers];
    [HideInInspector] public EntityPlayer[] playersScript = new EntityPlayer[maxPlayers];


    [SerializeField] private GameObject prefabHole;
    private List<GameObject> holes;


    void Awake()
    {
        Director.Instance.managerEntity = this;

        holes = new List<GameObject>();
        //player = new List<GameObject>();
        //playerScript = new List<EntityPlayer>();
    }


    #region Entity Management
    public void SummonPlayer()
    {
        SummonPlayer(1);
        SummonPlayer(2);
    }

    public void SummonPlayer(int which)
    {

        players[which - 1] = Instantiate(prefabPlayer[which - 1], this.transform) as GameObject;
        playersScript[which - 1] = players[which - 1].GetComponent<EntityPlayer>();
        /*
        
        switch (which)
        {
            default:
            case 1:
                players[0] = Instantiate(prefabPlayer1, this.transform) as GameObject;
                playersScript[0] = player1.GetComponent<EntityPlayer>();
                //player.Add( Instantiate(prefabPlayer1, this.transform) as GameObject);
                //playerScript.Add(player1.GetComponent<EntityPlayer>() );
                break;
            case 2:
                //player2 = Instantiate(prefabPlayer2, this.transform) as GameObject;
                //playerScript2 = player2.GetComponent<EntityPlayer>();
                players[1] = Instantiate(prefabPlayer1, this.transform) as GameObject;
                playersScript[1] = player1.GetComponent<EntityPlayer>();
                break;
        }
        */
    }

    private void RemovePlayers()
    {
        RemovePlayers(1);
        RemovePlayers(2);
    }

    private void RemovePlayers(int which)
    {

        if (players[which - 1] != null)
        {
            Destroy(players[which - 1]);
            players[which - 1] = null;
        }
        /*
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
        */
    }

    #endregion


    #region Holes management
    public void SummonHole(Vector2 position)
    {
        holes.Add(Instantiate(prefabHole, this.transform) as GameObject);
    }

    public void RemoveHoles()
    {
        if (holes.Count > 0)
        {
            var holesArray = holes.ToArray();
            for (int i = 0; i < holesArray.Length; i++)
            {
                Destroy(holesArray[i]);
            }
            holes.Clear();
        }
    }
    #endregion


    public void Reset()
    {
        RemoveHoles();
        RemovePlayers();
    }
}
