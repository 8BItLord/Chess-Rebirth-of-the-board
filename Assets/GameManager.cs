using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;

    void Start()
    {
        // spawn pemain di tile (0,0)
        Vector3 startPos = new Vector3(0, 0, -1);  // z=-1 biar di atas tile
        playerInstance = Instantiate(playerPrefab, startPos, Quaternion.identity);
        playerInstance.AddComponent<PlayerMovement>();
    }

    public void MovePlayer(Vector3 targetPos)
{
    if (playerInstance != null)
        playerInstance.GetComponent<PlayerMovement>().MoveTo(targetPos + new Vector3(0,0,-1));

    // reset warna semua tile setelah klik
    foreach (Tile t in FindObjectsOfType<Tile>())
    {
        t.ResetColor();
    }
}

}
    