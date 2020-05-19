using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    private Defender defenderPrefab;

    public void SetSelectedDefender(Defender defenderPrefab)
    {
        this.defenderPrefab = defenderPrefab;
    }

    private void OnMouseDown()
    {
        TryToPlaceDefender(GetSquareClicked());
    }
    
    private void TryToPlaceDefender(Vector2 gridPos)
    {
        var starDisplay = FindObjectOfType<StarsDisplay>();
        var defenderCost = defenderPrefab.GetStarCost();
        if (starDisplay.HaveEnoughStars(defenderCost))
        {
            SpawnDefender(gridPos);
            starDisplay.SpendStars(defenderCost);
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 gridPos)
    {
        Defender defender = Instantiate(defenderPrefab, gridPos, Quaternion.identity);
    }
}
