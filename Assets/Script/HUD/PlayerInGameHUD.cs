using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInGameHUD : MonoBehaviour
{
    
    public UIDocument uiDocument;
    
    // Start is called before the first frame update
    void Start()
    {
        VisualElement HUD = uiDocument.rootVisualElement.Q("MainMenu");

        if (HUD != null)
        {
            
            
            // Button startBTN = panel.Q<Button>("Start");
            // Button QuitBTN = panel.Q<Button>("Quit");
            
            // startBTN?.RegisterCallback<ClickEvent>(LoadGame);
            // QuitBTN?.RegisterCallback<ClickEvent>(QuitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSPeed(float speed)
    {
        
    }
    
}
