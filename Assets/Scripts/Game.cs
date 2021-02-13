using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	static Game instance;
	
	[SerializeField]
	PersonFactory personFactory = default;

	[SerializeField]
	SpawnPoint[] spwanPoints;

	private GameBehaviorCollection _peopleList = new GameBehaviorCollection();

	void OnEnable()
	{
		instance = this;
	}

	public Person SpawnPerson()
	{
		Person person = personFactory.Person;
		person.SpawnOn(spwanPoints[Random.Range(0, spwanPoints.Length)].GetSpawnLocation());
		instance._peopleList.Add(person);
		return person;
	}

	public Nurse SpawnNurse()
	{
		Nurse nurse = personFactory.Nurse;
		nurse.SpawnOn(spwanPoints[Random.Range(0, spwanPoints.Length)].GetSpawnLocation());
		instance._peopleList.Add(nurse);
		return nurse;
	}

	public Doctor SpawnDoctor()
	{
		Doctor doctor = personFactory.Doctor;
		doctor.SpawnOn(spwanPoints[Random.Range(0, spwanPoints.Length)].GetSpawnLocation());
		instance._peopleList.Add(doctor);
		return doctor;
	}


	private void Start()
    {
        for (int i =0; i< 10; i++)
        {
			SpawnPerson();
		}
	}

    private void Update()
    {
		_peopleList.GameUpdate();

        if (Input.GetKeyUp(KeyCode.Q))
        {
			SpawnPerson();
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			SpawnNurse();
		}

		if (Input.GetKeyUp(KeyCode.E))
		{
			SpawnDoctor();
		}
	}
}
