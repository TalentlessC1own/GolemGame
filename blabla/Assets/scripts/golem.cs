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
    private wing_wepon axe_wepom;
    private wing_wepon dubinka_wepom;

    private wing_wepon selected_wepon;

    public bool Sprite
    {
        set { sprite.flipX = value; }
    }
    private GolemState state
    {
        get { return (GolemState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }

    private void Start()
    {
        SelectWeapon();
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        wood_wepon = Resources.Load<wing_wepon>("Wood");
        axe_wepom = Resources.Load<wing_wepon>("Axe");
        dubinka_wepom = Resources.Load<wing_wepon>("Dubinka");
    } 
    private void Update()
    {
        if (!attacking && !die)
            state = GolemState.idle;
    }

    private IEnumerator AttackAnim()
    {
        state = GolemState.attack;
        attacking = true;
        yield return new WaitForSeconds(0.8f);
        attacking = false;
    }
    private IEnumerator RangeAttackAnim()
    {
        state = GolemState.range_attack;
        attacking = true;
        yield return new WaitForSeconds(0.8f);
        ThrowWepon();
        attacking = false;
    }

    private void  SelectWeapon()
    {
        switch (gameObject.name)
        {
            case "StoneGolem(Clone)":
                selected_wepon = dubinka_wepom;
                break;
            case "EarthGolem(Clone)":
                selected_wepon = axe_wepom;
                break;
            case "WoodGolem(Clone)":
                selected_wepon =wood_wepon;
                break;

        }
    }
    public void RangeAttack()
    {
        StartCoroutine(RangeAttackAnim());
    }
    private void ThrowWepon()
    {
        Vector3 position = transform.position; position.x +=  sprite.flipX ? -1.5f : 1.5f;
        wing_wepon Newthrow_wepon =  Instantiate(selected_wepon, position, wood_wepon.transform.rotation) as wing_wepon;
        Newthrow_wepon.Direction = Newthrow_wepon.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }
    public void Attack()
    {
        StartCoroutine(AttackAnim());
    }
   public  void Die()
    {
        die = true;
        
        state = GolemState.die;
       
        
        Destroy(gameObject, 1.5f);
    }

}
public enum GolemState
{
    idle,
    attack,
    die,
    range_attack,
    
}