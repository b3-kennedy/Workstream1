using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScreenManager : MonoBehaviour
{
    public MeshRenderer mr;
    public Image image;
    public List<Sprite> chars = new List<Sprite>();
    public List<Color> colors = new List<Color>(); 
    private int selectedSkin = 0;
    public GameObject playerskin;



    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if(selectedSkin == chars.Count)
        {
            selectedSkin = 0;
        }
        image.sprite = chars[selectedSkin];
        mr.material.color = colors[selectedSkin];
    }
    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = chars.Count -1;
        }
        image.sprite = chars[selectedSkin];
        mr.material.color = colors[selectedSkin];
    }
}
