using System.Collections;
using UnityEngine;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] private Defender defenderPrefab;

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach (var button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(147,147,147, 255);
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab);
    }
    
    
}
