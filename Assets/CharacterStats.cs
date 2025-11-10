using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData data;

    // buat ngecek langsung
    void Start()
    {
        Debug.Log($"{data.characterName} - Move Range: {data.moveRange}, Skill: {data.skillName}");
    }
}
