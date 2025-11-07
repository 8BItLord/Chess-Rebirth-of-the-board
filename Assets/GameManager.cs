using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;

    private SelectableCharacter selectedCharacter;

    void Start()
    {
        // spawn pemain di tile (0,0)
        Vector3 startPos = new Vector3(0, 0, -1);
        playerInstance = Instantiate(playerPrefab, startPos, Quaternion.identity);
    }

    public void SelectCharacter(SelectableCharacter character)
    {
        // deselect karakter lain dulu
        if (selectedCharacter != null)
            selectedCharacter.SetSelected(false);

        selectedCharacter = character;
        selectedCharacter.SetSelected(true);

        Debug.Log("Character selected!");
    }

    public void MovePlayer(Vector3 targetPos)
    {
        if (selectedCharacter == null) return;

        selectedCharacter.GetComponent<PlayerMovement>().MoveTo(targetPos + new Vector3(0,0,-1));
        StartCoroutine(DeselectAfterMove(targetPos));
    }
    private System.Collections.IEnumerator DeselectAfterMove(Vector3 targetPos)
{
    if (selectedCharacter == null) yield break;

    var mover = selectedCharacter.GetComponent<PlayerMovement>();
    mover.MoveTo(targetPos + new Vector3(0, 0, -1));

    // tunggu sampai sampai ke tujuan
    while (Vector3.Distance(selectedCharacter.transform.position, targetPos + new Vector3(0,0,-1)) > 0.01f)
    {
        yield return null;
    }

    // baru reset warna & deselect
    selectedCharacter.SetSelected(false);
    selectedCharacter = null;
}

}
