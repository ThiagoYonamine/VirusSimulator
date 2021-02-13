using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : GameBehavior
{
    [SerializeField] private float speed;
    [SerializeField] private bool isInfected;
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody2D _rigidbody;
    private float _reInfectTime = 0;
    private float _infectionTime = 0;
    private float _cureDelayTime = 0;
    public float CureDelayTime 
    {
        get => _cureDelayTime;
        set => _cureDelayTime = value;
    }
    public bool IsInfected => isInfected;

    PersonFactory originFactory;

    public PersonFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "Redefined origin factory!");
            originFactory = value;
        }
    }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 360));
        _rigidbody.velocity = transform.right * speed;
        
        //todo change this
        TouchInfected();

        if (isInfected) GetInfected();
    }

    public void SpawnOn(Vector2 position)
    {
        transform.position = position;
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
         transform.rotation *= Quaternion.Euler(0, 0, Random.Range(90, 270));

        if (collision.gameObject.CompareTag(GameTags.PersonTag))
        {
            if (collision.gameObject.GetComponent<Person>().isInfected)
            {
                TouchInfected();
            }
        }

        _rigidbody.velocity = transform.right * speed;
    }

    private void TouchInfected()
    {
        if (_reInfectTime > 0 || isInfected) return ;

        _reInfectTime = GameConfig.ReInfectionTime;
        if (Random.Range(0, 100) <= GameConfig.InfectionRate)
        {
            GetInfected();
        }
    }

    private void GetInfected()
    {
        isInfected = true;
        sprite.color = Color.green;
    }

    public void Cure()
    {
        isInfected = false;
        sprite.color = Color.white;
        _infectionTime = 0;
    }

    public override bool GameUpdate()
    {
        float deltaTime = Time.deltaTime;
        if (_reInfectTime > 0) _reInfectTime -= deltaTime;

        if (isInfected) _infectionTime += deltaTime;

        if (_cureDelayTime > 0) _cureDelayTime -= deltaTime;

        if (_infectionTime > GameConfig.MaxInfectionTime)
        {
            originFactory.Reclaim(this);
            return false;
        }

        return true;
    }


    private void OnValidate()
    {
        if (isInfected) GetInfected();
    }
}
