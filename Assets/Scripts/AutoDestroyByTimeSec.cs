using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyByTimeSec : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 0.125f;

    private float _timeLine = 0;

    private void Update()
    {
        _timeLine += Time.deltaTime;

        if (_timeLine >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
