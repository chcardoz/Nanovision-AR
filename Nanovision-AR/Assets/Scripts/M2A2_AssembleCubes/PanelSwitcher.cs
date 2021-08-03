using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] GameObject spawnButton;
    [SerializeField] GameObject startButton;

    void Start()
    {
        startButton.SetActive(false);
        spawnButton.SetActive(true);
    }

    public void ShowStart()
    {
        spawnButton.SetActive(false);
        startButton.SetActive(true);
    }
}
