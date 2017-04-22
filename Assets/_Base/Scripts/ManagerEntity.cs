using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEntity : MonoBehaviour
{

    [SerializeField] private GameObject prefabPlayer;
    private GameObject player;
    [HideInInspector] public EntityBase playerScript;


    void Awake()
    {
        Director.Instance.managerEntity = this;
    }


    #region Entity Management
    public void SummonPlayer()
    {
        player = Instantiate(prefabPlayer, this.transform) as GameObject;

        playerScript = player.GetComponent<EntityBase>();
    }

    private void RemovePlayer()
    {
        if (player != null)
        {
            Destroy(player);
            player = null;
        }
    }

    public void Reset()
    {

        RemovePlayer();
    }
    #endregion
}
