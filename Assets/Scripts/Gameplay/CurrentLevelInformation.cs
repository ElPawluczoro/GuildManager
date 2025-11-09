using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Gameplay
{
    public class CurrentLevelInformation : MonoBehaviour
    {
        private Vector3 characterPosition;

        private Transform characterTransform;
        
        private List<Encounter> encounters = new List<Encounter>();

        struct Encounter : IEquatable<Encounter>
        {
            public GameObject gameObject;
            public Vector3 position;

            public Encounter(GameObject gameObject, Vector3 position)
            {
                this.gameObject = gameObject;
                this.position = position;
            }

            public bool Equals(Encounter other)
            {
                return Equals(gameObject, other.gameObject) && position.Equals(other.position);
            }

            public override bool Equals(object obj)
            {
                return obj is Encounter other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(gameObject, position);
            }
        }
        
        private void Start()
        {
            characterTransform = GameObject.Find("Player").GetComponent<Transform>();
        }

        public void SaveLevel(GameObject encounter)
        {
            Debug.Log("Saving level");
            characterPosition = characterTransform.position;
            encounters.Clear();
            List<GameObject> encountersGO = GameObject.FindGameObjectsWithTag("Encounter").ToList();
            foreach (GameObject o in encountersGO)
            {
                encounters.Add(new Encounter(o, o.transform.position));
            }
            RemoveEncounterByGameObject(encounter);
        }

        public void LoadLevel()
        {
            Debug.Log("Loading Level");
            GameObject.FindGameObjectWithTag("Player").transform.position = characterPosition;
            Debug.Log(encounters.Count);
            foreach (Encounter encounter in encounters)
            {
                Debug.Log("loading encounter");
                Instantiate(encounter.gameObject, encounter.position, Quaternion.identity);
            }
        }

        public void RemoveEncounterByGameObject(GameObject encounterGo)
        {
            Encounter toRemove = new Encounter(null, Vector3.zero);
            foreach (Encounter encounter in encounters)
            {
                if (encounter.gameObject == encounterGo)
                {
                    toRemove = encounter;
                }
            }

            if (toRemove.gameObject == null)
            {
                Debug.LogError("No encounter found to remove");
                return;
            }
            
            encounters.Remove(toRemove);
        }
    }
}