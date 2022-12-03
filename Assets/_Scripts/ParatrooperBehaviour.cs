using UnityEngine;

public class ParatrooperBehaviour : MonoBehaviour
{
    private bool isGrounded = false;
    [SerializeField] private Sprite _groundedSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground" && !isGrounded)
        {
            GroundSoldier();
        }
    }

    private void GroundSoldier()
    {
        this.gameObject.tag = "GroundedSoldier";
        isGrounded = true;
        GetComponent<SpriteRenderer>().sprite = _groundedSprite;
        var collider = GetComponent<CapsuleCollider2D>();
        collider.offset = new Vector2(collider.offset.x, 0);
        if(transform.position.x < 0)
        {
            GameManager.instance.LandSoldier(true);
        }
        else
        {
            GameManager.instance.LandSoldier(false);

        }

    }

    private void OnDestroy()
    {
        GameManager.instance.UpdateScore(5);
    }
}
