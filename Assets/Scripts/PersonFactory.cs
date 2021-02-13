using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PersonFactory : GameObjectFactory
{
	[SerializeField]
	Person personPrefab = default;

	[SerializeField]
	Nurse nursePrefab = default;

	[SerializeField]
	Doctor doctorPrefab = default;

	public Person Person => Get(personPrefab);

	public Nurse Nurse => Get(nursePrefab);

	public Doctor Doctor => Get(doctorPrefab);

	T Get<T>(T prefab) where T : Person
	{
		T instance = CreateGameObjectInstance(prefab);
		instance.OriginFactory = this;
		instance.Initialize();
		return instance;
	}
	/*
	public Person Get()
	{
		Person instance = CreateGameObjectInstance(personPrefab);
		instance.OriginFactory = this;
		instance.Initialize();
		return instance;
	}*/

	public void Reclaim(Person entity)
	{
		Debug.Assert(entity.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(entity.gameObject);
	}
}
