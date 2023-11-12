using UnityEngine;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    public Button restartButton; 

    void Start()
    {

        restartButton.onClick.AddListener(ButtonClickHandler);
    }

    // The method to handle the button click
    public void ButtonClickHandler()
    {
        Debug.Log("Button clicked!");
        // Add your button click logic here
    }
}


