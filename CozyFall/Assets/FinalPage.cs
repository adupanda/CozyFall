using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPage : MonoBehaviour
{
    [SerializeField]
    public Button nextButton;
    [SerializeField]

    public GameObject[] images;

    private int currentPage = 0;

    public void ButtonClicked()
    {
        if (currentPage < 2)
        {
            currentPage++;
            images[currentPage].SetActive(true);

        }
        else
        {
            return;
        }
    }
}
