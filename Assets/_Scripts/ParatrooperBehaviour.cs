using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ParatrooperBehaviour : MonoBehaviour
{
    private bool isGrounded = false;
    private SpriteRenderer spr;
    private Rigidbody2D rbd;
    private float _parachuteBurstPoint;
    private bool _hasParachute = true;
    private float lerpDuration = 5;
    
    [SerializeField] private Sprite _groundedSprite;
    [SerializeField] private float _velocity;
    [SerializeField] private Transform _cannonPos;

    
    private void Awake()
    {
        GameManager.PlayerLost += MoveTowardsCannon;
    }
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rbd = GetComponent<Rigidbody2D>();
        rbd.velocity = new Vector2(0f, -1 * _velocity);
        _cannonPos = GameManager.instance.GetCannonPosition();
    }
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
        if(_hasParachute)
            ParacuteBurst();    
        var collider = GetComponent<CapsuleCollider2D>();
        collider.offset = new Vector2(collider.offset.x, 0);
        if(_parachuteBurstPoint >= -3f)
        {
            Destroy(this.gameObject);
            return;
        }
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
        GameManager.PlayerLost -= MoveTowardsCannon;

    }

    public void ParacuteBurst()
    {
        _hasParachute = false;
        spr.sprite = _groundedSprite;
        rbd.isKinematic = false;
        _parachuteBurstPoint = transform.position.y;
        if(transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        
    }

    
     

    private void MoveTowardsCannon()
    {
        StartCoroutine(MoveSoldierTowardsCannon());
    }

    private IEnumerator MoveSoldierTowardsCannon()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration) 
        {
            transform.position = Vector3.Lerp(transform.position, _cannonPos.position, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null; 
        }
        transform.position = _cannonPos.position;
        GameManager.instance.SoldierArriveAtCannon();
        yield break;
    }
}
