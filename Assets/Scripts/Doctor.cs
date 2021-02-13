using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : Nurse
{
    [SerializeField] private GameObject power;
    private float _powerTime = 0;

    public override bool GameUpdate()
    {
        _powerTime -= Time.deltaTime;
        if (_powerTime < 0) UsePower();
        if (_powerTime < 1) power.SetActive(false);

            return base.GameUpdate();
    }

    private void UsePower()
    {
        _powerTime = 2f;
        power.SetActive(true);
        AcquireTarget();

    }

	protected bool AcquireTarget()
	{
        var collider = power.GetComponent<Collider2D>();
        List<Collider2D> hits = new List<Collider2D>();
        ContactFilter2D mask = new ContactFilter2D();
        mask.SetLayerMask(GameConfig.PeopleLayerMask);
        var numHits = collider.OverlapCollider(mask, hits);

        for (int i=0; i < numHits; i++)
        {
            var target = hits[i].GetComponent<Person>();
            if (target != null)
            {
                //Debug.Log(target.gameObject.name);
                target.Cure();
            }
        }
		
		return false;
	}
}
