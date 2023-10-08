using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pages : MonoBehaviour
{
    [SerializeField]
    public Button nextButton;
    [SerializeField]
    public Image[] images;
    
    private int currentPage = 0;

    public void ButtonClicked()
    {
        if(currentPage < 5)
        {
            currentPage++;
            images[currentPage].SetEnabled(true);

        }
        else
        {
            SceneManager.LoadScene("boss1");
        }
    }


}
