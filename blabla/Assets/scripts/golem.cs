using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem : MonoBehaviour
{
    private bool attacking = false;
    private bool die = false;

    private Animator animator;

    private SpriteRenderer sprite;

    private wing_wepon wood_wepon;

    public bool Sprite
    {
        set { sprite.flipX = value; }
    }
    private GolemState state
    {
        get { return (GolemState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        wood_wepon = Resources.Load<wing_wepon>("Wood");
    }
    private void Update()
    {
        if (!attacking && !die)
            state = GolemState.idle;
    }

    private IEnumerator AttackAnim()
    {
        attacking = true;
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }

    public void ThrowWepon()
    {
        Vector3 position = transform.position; position.x +=  sprite.flipX ? 0.5f : -0.5f;
        wing_wepon Newthrow_wepon =  Instantiate(wood_wepon, position, wood_wepon.transform.rotation) as wing_wepon;
        Newthrow_wepon.Direction = Newthrow_wepon.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }
    public void Attack()
    {
        state = GolemState.attack;
    }
   public  void Die()
    {
        die = true;
        state = GolemState.die;
        Destroy(gameObject, 1.1f);
    }

}
public enum GolemState
{
    idle,
    attack,
    die
}