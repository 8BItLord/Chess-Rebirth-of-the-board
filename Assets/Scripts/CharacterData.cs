using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string characterName;

    [Header("Stats")]
    public int maxHP;
    public int attack;
    public int defense;

    [Header("Movement")]
    public int moveRange;
    public string moveType; // "orthogonal", "diagonal", "free"

    [Header("Basic Attack")]
    public int attackRange;     // berapa tile jangkauan basic attack
    public int attackDamage;    // damage basic attack

    [Header("Skill")]
    public string skillName;
    [TextArea(2, 4)]
    public string skillDescription;
    public int skillDamage;     // damage skill
    public int skillRange;      // jangkauan tile target skill
    public int skillCooldown;
}
