using UnityEngine;
using UnityEngine.InputSystem;


public class CannonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    [SerializeField] private Transform Cannon;
    
    private Animator _animator;
    private bool allowMotion = true;
    void Awake()
    {
        GameManager.PlayerLost += StopGame;
        _animator = GetComponentInChildren<Animator>();
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
                Instantiate(FireBall, Cannon.position, transform.rotation);
                GameManager.instance.UpdateScore(-1);
            }
        }
                
    }

    private void StopGame()
    {
        allowMotion = false;
        Debug.Log("Lost");
    }


}
