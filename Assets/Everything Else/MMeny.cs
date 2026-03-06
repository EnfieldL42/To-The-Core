using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MMeny : MonoBehaviour
{
    public Animator animatior;
    public float charge;
    public bool used = false;
    
    // Start is called before the first frame update
    void Start()
    {
        charge = Time.time + 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (charge < Time.time)
        {
            animatior.SetBool("Used", true);
            used = true;
        }
        if (used)
        {
            animatior.SetBool("Used", true);
        }
    }
    public void HostGame()
    {
        LobbyManager.Instance.StartNetworkAsHost();
    }

    public void JoinGame()
    {
        LobbyManager.Instance.StartNetworkAsClient();
    }

    public void AddLocalPlayer()
    {
        LobbyManager.Instance.RequestJoin();
    }
}
