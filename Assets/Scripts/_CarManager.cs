using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CarManager : MonoBehaviour
{
    #region Singleton
    public static _CarManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public GameObject player;
    private GameObject playerChild;
    [SerializeField] GameObject m_meshManager;
    _MeshManager mm;
    GameObject ActiveCar;

    [SerializeField]
    public Car[] cars;

   
    private void Start()
    {
        mm = m_meshManager.GetComponent<_MeshManager>();
        playerChild = player.transform.GetChild(1).gameObject;
        SelectCar();
    }
    public void SelectCar()
    {
        switch (PlayerPrefs.GetInt("Selected_Car"))
        {
            case 0:
                playerChild.GetComponent<MeshFilter>().mesh = cars[0].mesh;
                playerChild.GetComponent<MeshRenderer>().material = cars[0].materials[PlayerPrefs.GetInt("Mesh_Select")]; 
                break;
            case 1:
                playerChild.GetComponent<MeshFilter>().mesh = cars[1].mesh;
                playerChild.GetComponent<MeshRenderer>().material = cars[1].materials[PlayerPrefs.GetInt("Mesh_Select")];
                break;
            case 2:
                playerChild.GetComponent<MeshFilter>().mesh = cars[2].mesh;
                playerChild.GetComponent<MeshRenderer>().material = cars[2].materials[PlayerPrefs.GetInt("Mesh_Select")];
                break;
            case 3:
                playerChild.GetComponent<MeshFilter>().mesh = cars[3].mesh;
                playerChild.GetComponent<MeshRenderer>().material = cars[3].materials[PlayerPrefs.GetInt("Mesh_Select")];
                break;
            case 4:
                playerChild.GetComponent<MeshFilter>().mesh = cars[4].mesh;
                playerChild.GetComponent<MeshRenderer>().material = cars[4].materials[PlayerPrefs.GetInt("Mesh_Select")];
                break;
        }

    }
}


