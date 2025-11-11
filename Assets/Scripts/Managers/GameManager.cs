using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    PlacementP1,
    PlacementP2,
    Reveal,
    Battle
}

public class GameManager : MonoBehaviour
{
    [Header("Player 1 & 2 Character Prefabs")]
    public List<SelectableCharacter> player1Characters; // isi di Inspector (5 prefab)
    public List<SelectableCharacter> player2Characters; // isi di Inspector (5 prefab)

    [Header("Current Game Phase")]
    public GamePhase currentPhase = GamePhase.PlacementP1;

    [Header("Placement Settings")]
    public int currentPlacementIndex = 0;
    public int currentPlayer = 1;
    private List<SelectableCharacter> activeRoster;
    private List<SelectableCharacter> allCharacters = new();

    private SelectableCharacter selectedCharacter;

    private void Start()
    {
        Debug.Log("=== Hidden Deployment Start ===");
        StartPlacementPhase(1);
    }

    // Mulai fase placement
    void StartPlacementPhase(int player)
    {
        currentPlayer = player;
        currentPlacementIndex = 0;
        currentPhase = (player == 1) ? GamePhase.PlacementP1 : GamePhase.PlacementP2;
        activeRoster = (player == 1) ? player1Characters : player2Characters;

        Debug.Log($"Player {player} Placement Phase Started. Place your {activeRoster.Count} units.");
    }

    // Dipanggil dari SelectableCharacter
    public void OnCharacterClicked(SelectableCharacter character)
    {
        if (currentPhase == GamePhase.Battle)
        {
            if (selectedCharacter != null)
                selectedCharacter.SetSelected(false);

            selectedCharacter = character;
            selectedCharacter.SetSelected(true);
            Debug.Log($"Selected {character.name}");
        }
    }

    // Dipanggil dari Tile.cs
    public void OnTileClicked(Tile tile)
    {
        switch (currentPhase)
        {
            case GamePhase.PlacementP1:
            case GamePhase.PlacementP2:
                PlaceCharacter(tile);
                break;

            case GamePhase.Battle:
                if (selectedCharacter != null)
                    MoveSelectedCharacter(tile);
                break;
        }
    }

    void PlaceCharacter(Tile tile)
{
    if (tile == null || activeRoster == null || currentPlacementIndex >= activeRoster.Count)
        return;

    // Ambil prefab karakter dari roster
    SelectableCharacter prefabToSpawn = activeRoster[currentPlacementIndex];
    if (prefabToSpawn == null) return;

    // Instansiasi prefab ke posisi tile
    Vector3 spawnPos = tile.transform.position;
    SelectableCharacter charToPlace = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    charToPlace.gameObject.SetActive(true);

    Debug.Log($"Player {currentPlayer} placed {charToPlace.name} on Tile ({tile.x},{tile.y})");

    currentPlacementIndex++;

    // Cek apakah semua karakter player ini sudah ditempatkan
    if (currentPlacementIndex >= activeRoster.Count)
    {
        if (currentPlayer == 1)
        {
            HidePlayerCharacters(1);
            StartPlacementPhase(2);
        }
        else
        {
            currentPhase = GamePhase.Reveal;
            RevealAllCharacters();
            StartBattlePhase();
        }
    }
}


    void HidePlayerCharacters(int player)
    {
        var list = (player == 1) ? player1Characters : player2Characters;
        foreach (var c in list)
            c.gameObject.SetActive(false);
        Debug.Log($"Player {player} characters hidden.");
    }

    void RevealAllCharacters()
    {
        foreach (var c in player1Characters)
        {
            c.gameObject.SetActive(true);
            allCharacters.Add(c);
        }

        foreach (var c in player2Characters)
        {
            c.gameObject.SetActive(true);
            allCharacters.Add(c);
        }

        Debug.Log("All characters revealed!");
    }

    void StartBattlePhase()
    {
        currentPhase = GamePhase.Battle;
        Debug.Log("=== Battle Phase Started ===");
    }

    void MoveSelectedCharacter(Tile tile)
    {
        Vector3 newPos = new Vector3(tile.x, tile.y, 0);
        selectedCharacter.transform.position = newPos;
        Debug.Log($"{selectedCharacter.name} moved to Tile ({tile.x},{tile.y})");
    }
}
