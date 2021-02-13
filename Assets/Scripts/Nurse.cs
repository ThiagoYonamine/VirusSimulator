using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : Person
{
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
     
        if (collision.gameObject.CompareTag(GameTags.PersonTag))
        {
            var personCollision = collision.gameObject.GetComponent<Person>();
            if (personCollision.CureDelayTime > 0 || !personCollision.IsInfected) return;

            personCollision.CureDelayTime = GameConfig.CureDelayTime;
            TryToCureInfected(personCollision);
        }

    }
    private void TryToCureInfected(Person person)
    {
        if (Random.Range(0, 100) <= GameConfig.CureRate)
        {
            person.Cure();
        }
    }


}
