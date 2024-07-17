using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        CharacterUnitController character = collider.gameObject.GetComponent<CharacterUnitController>();
        character.Score += 1;
    }
}
