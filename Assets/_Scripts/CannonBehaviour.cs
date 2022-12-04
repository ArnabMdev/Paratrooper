using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class CannonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    [SerializeField] private Transform Cannon;
    private AudioSource audioSourceFire,audioSourceExplode;

    private Animator _animator;
    private bool allowMotion = true;
    void Awake()
    {
        GameManager.PlayerLost += StopGame;
        GameManager.CannonExplode += ExplodeSelf;
        _animator = GetComponentInChildren<Animator>();
        audioSourceFire = GetComponentInChildren<AudioSource>();
        audioSourceExplode = GetComponent<AudioSource>();
    }


    public void PointToMouse(InputAction.CallbackContext context)
    {
        if(!allowMotion)
        {
            return;
        }
        var mouseScreenPos = context.ReadValue<Vector2>();
        var startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void FireShot(InputAction.CallbackContext context)
    {
        if(!allowMotion)
        {
            return;
        }
        if (context.started)
        {
            if (GameManager.instance._score > 0)
            {
                _animator.SetTrigger("Fire");
                audioSourceFire.Play();
                Instantiate(FireBall, Cannon.position, transform.rotation);
                GameManager.instance.UpdateScore(-1);
            }
        }
                
    }

    private void StopGame()
    {
        allowMotion = false;
    }


    private void ExplodeSelf()
    {
        _animator.SetTrigger("Explode");
        audioSourceExplode.Play();
        GameManager.instance.StartCoroutine(GameManager.instance.FullStopGame());
        
    }
}
