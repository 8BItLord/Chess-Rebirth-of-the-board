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
        sr.sprite = idleSprite; // mulai dari idle
    }

    private void OnMouseDown()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.SelectCharacter(this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        sr.sprite = selected ? selectedSprite : idleSprite;
    }
}
