using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : InteractableBehaviour
{
    [SerializeField] private TextMeshProUGUI doorTextUI;
    [SerializeField] private String connectingSceneName;
    [SerializeField] private Vector2 connectingDoorLocation;
    private String doorText = "Press B to enter";

    private void OnTriggerEnter2D(Collider2D other)
    {
        doorTextUI.text = doorText;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        doorTextUI.text = "";
    }

    public override void interactWith()
    {
        FindObjectOfType<DoorConnectionManager>().setPos(connectingDoorLocation);
        SceneManager.LoadSceneAsync(connectingSceneName);
    }
}