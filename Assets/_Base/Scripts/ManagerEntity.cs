using System.Collections.Generic;
using UnityEngine;

public class ManagerEntity : MonoBehaviour
{
    // Players
    const int maxPlayers = 3;
    [SerializeField] private GameObject prefabPlayer;
    private GameObject[] players = new GameObject[maxPlayers];
    [HideInInspector] public EntityPlayer[] playersScript = new EntityPlayer[maxPlayers];

    // Holes
    [SerializeField] private GameObject prefabHole;
    private List<GameObject> holes;

    // Flags
    [SerializeField] Sprite[] flags;

    void Awake()
    {
        Director.Instance.managerEntity = this;

        holes = new List<GameObject>();
        //player = new List<GameObject>();
        //playerScript = new List<EntityPlayer>();
    }


    #region Entity Management
    public void SummonPlayers()
    {
        SummonPlayer(1);
        SummonPlayer(2);

        if (Director.Instance.currentGameMode == Structs.GameMode.Multi3players)
        {
            SummonPlayer(3);
        }
    }

    public void SummonPlayer(int which)
    {
        // Instantiate
        Vector2 position = Vector2.zero;
        players[which - 1] = Instantiate(prefabPlayer, position, Quaternion.identity, this.transform) as GameObject;
        playersScript[which - 1] = players[which - 1].GetComponent<EntityPlayer>();

        // Pick a flag
        Sprite flag = flags[Random.Range(0, flags.Length)];
        ///Debug.Log("Player " + which + " se le asigna la bandera " + flag.name);

        // Settings
        playersScript[which - 1].Set(which,flag);
    }

    private void RemovePlayers()
    {
        RemovePlayer(1);
        RemovePlayer(2);
        RemovePlayer(3);
    }

    public void RemovePlayer(int which)
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
        holes.Add(Instantiate(prefabHole, position, Quaternion.identity) as GameObject);
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
