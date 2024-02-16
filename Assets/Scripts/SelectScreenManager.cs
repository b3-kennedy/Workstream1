using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject collum;
    public List<GameObject> list = new List<GameObject>();
    private int selectedSkin = 0;
    private GameObject tempGO;
    private void Start()
    {
        tempGO = collum;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            NextOption();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            BackOption();
        }
    }
    public void NextOption()
    {
    
        selectedSkin = selectedSkin + 1;
       tempGO = list[selectedSkin];
        tempGO.SetActive(true);
    }
    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        tempGO = list[selectedSkin];
        tempGO.SetActive(false);
    }
}
