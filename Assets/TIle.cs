using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;

    private SpriteRenderer sr;
    private Color baseColor;
    private Color hoverColor = new Color(0.6f, 0.9f, 1f, 1f);
    private Color movableColor = new Color(0.7f, 1f, 0.7f, 1f);
    private Color invalidColor = new Color(1f, 0.4f, 0.4f, 1f);

    private GameManager gm;
    public bool isMovable = false;
    private bool isFlashing = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        gm = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject)
            {
                if (isMovable && gm != null)
                {
                    gm.MovePlayer(new Vector3(x, y, 0));
                }
                else if (!isMovable && !isFlashing)
                {
                    StartCoroutine(FlashInvalid());
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (isFlashing) return; // ⛔ Jangan ubah warna pas lagi merah
        if (!isMovable)
            sr.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (isFlashing) return; // ⛔ Jangan ubah warna pas lagi merah
        sr.color = isMovable ? movableColor : baseColor;
    }

    public void SetMovable(bool state)
    {
        isMovable = state;
        if (!isFlashing)
            sr.color = state ? movableColor : baseColor;
    }

    public void ResetColor()
    {
        sr.color = baseColor;
        isMovable = false;
    }

    private IEnumerator FlashInvalid()
    {
        isFlashing = true;
        Color prev = sr.color;

        // merah cepat
        sr.color = invalidColor;
        yield return new WaitForSeconds(0.2f);

        // pastikan warna balik sesuai status tile
        sr.color = isMovable ? movableColor : baseColor;
        isFlashing = false;
    }
}
