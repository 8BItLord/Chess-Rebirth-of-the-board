using UnityEngine;

public class SelectableCharacter : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite selectedSprite;
    public bool isSelected = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }

    private void OnMouseDown()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            gm.OnCharacterClicked(this);
        }
        else
        {
            Debug.LogWarning("GameManager not found in scene!");
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        sr.sprite = selected ? selectedSprite : idleSprite;
    }
}
