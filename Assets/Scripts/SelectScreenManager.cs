using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject collum;
    public List<Sprite> chars = new List<Sprite>();
    public List<Color> colors = new List<Color>(); 
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
        if(selectedSkin == chars.Count)
        {
            selectedSkin = 0;
        }
        tempGO.GetComponentInChildren<Image>().sprite = chars[selectedSkin];
        tempGO.GetComponent<Image>().material.color = colors[selectedSkin];
        Instantiate(tempGO, this.transform);
    }
    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = chars.Count -1;
        }
        tempGO.GetComponentInChildren<Image>().sprite = chars[selectedSkin];
        tempGO.GetComponent<Image>().material.color = colors[selectedSkin];
        int numchild = this.transform.childCount;
        Destroy(this.transform.GetChild(numchild - 1).gameObject);
    }
}
