using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // === PREFABS ===
    public GameObject[] characterPrefabs;
    public Vector3[] spawnPositions;

    // === SELECTION ===
    private SelectableCharacter selectedCharacter;

    void Start()
    {
        SpawnAllCharacters();
    }

    void SpawnAllCharacters()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0) return;

        if (spawnPositions == null || spawnPositions.Length < characterPrefabs.Length)
        {
            spawnPositions = new Vector3[characterPrefabs.Length];
            for (int i = 0; i < characterPrefabs.Length; i++)
                spawnPositions[i] = new Vector3(i, 0, -1);
        }

        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            if (characterPrefabs[i] == null) continue;
            Vector3 pos = spawnPositions[i];
            GameObject inst = Instantiate(characterPrefabs[i], pos, Quaternion.identity);
            inst.name = characterPrefabs[i].name + "_Instance";
        }
    }

    // === CHARACTER SELECTION ===
    public void SelectCharacter(SelectableCharacter character)
{
    if (selectedCharacter != null)
    {
        selectedCharacter.SetSelected(false);
        ResetAllTiles();
    }

    selectedCharacter = character;
    selectedCharacter.SetSelected(true);

    Debug.Log("Selected: " + selectedCharacter.name);

    HighlightMoveRangeForCharacter(selectedCharacter);
}
    public void ResetAllTiles()
{
    foreach (Tile t in FindObjectsOfType<Tile>())
        t.ResetColor();
}


    // === MOVEMENT ===
    public void MovePlayer(Vector3 targetPos)
    {
        if (selectedCharacter == null) return;

        selectedCharacter.GetComponent<PlayerMovement>().MoveTo(targetPos + new Vector3(0, 0, -1));
        DeselectAfterMove();
    }

    private void DeselectAfterMove()
    {
        selectedCharacter.SetSelected(false);
        selectedCharacter = null;
    }

    // Pindahkan method ke dalam class
    public void HighlightMoveRangeForCharacter(SelectableCharacter character)
    {
        var stats = character.GetComponent<CharacterStats>();
        if (stats == null || stats.data == null) return;

        int range = stats.data.moveRange;
        string moveType = stats.data.moveType.ToLower();

        Vector2 charPos = new Vector2(
            Mathf.RoundToInt(character.transform.position.x),
            Mathf.RoundToInt(character.transform.position.y)
        );

        foreach (Tile t in FindObjectsOfType<Tile>())
        {
            int dx = Mathf.Abs(t.x - (int)charPos.x);
            int dy = Mathf.Abs(t.y - (int)charPos.y);

            bool inRange = false;

            if (moveType == "orthogonal")
                inRange = (dx + dy) <= range && (dx == 0 || dy == 0);
            else if (moveType == "diagonal")
                inRange = (dx == dy && dx <= range);
            else // "free"
                inRange = (dx + dy) <= range;

            t.SetMovable(inRange);
        }
    }
}