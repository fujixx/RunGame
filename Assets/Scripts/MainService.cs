using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UniRx;

public class MainService : BaseService
{
    [SerializeField] GameObject prefabField;
    [SerializeField] GameObject parentField;
    [SerializeField] GameObject prefabCube;

    private List<Field> _fields;
    private List<Cube> _cubes;
    private IDisposable _timerDisposable;

    void Start()
    {
        _fields = new();
        _cubes = new();

        CreateFieldObject(0, 0, 0);
        CreateFieldObject(0, 0, 10);
        CreateFieldObject(0, 0, 20);
        CreateFieldObject(0, 0, 30);
        CreateFieldObject(0, 0, 40);
        CreateFieldObject(0, 0, 50);
        CreateFieldObject(0, 0, 60);
        _timerDisposable = Observable.Interval(TimeSpan.FromSeconds(2))
            .StartWith(0)
            .Subscribe(_ => CreateFieldObject(0, 0, 70))
            .AddTo(this);

        _timerDisposable = Observable.Interval(TimeSpan.FromSeconds(5))
            .StartWith(0)
            .Subscribe(_ => CreateCubeObject())
            .AddTo(this);
    }

    void OnDestroy()
    {
        _timerDisposable.Dispose();
    }

    private void CreateFieldObject(float x, float y, float z)
    {
        Field field = (Field)Utils.AddPrefabGameObject<Field>(parentField, prefabField, x, y, z);
        _fields.Add(field);

        foreach (Field f in _fields)
        {
            if (f != null)
            {
                MoveBy(f.gameObject, new Vector3(0, 0, -10), 2.0f).Forget();
            }
        }
    }

    private void CreateCubeObject()
    {
        Cube cube = (Cube)Utils.AddPrefabGameObject<Cube>(parentField, prefabCube);
        _cubes.Add(cube);

        foreach (Cube c in _cubes)
        {
            if (c != null)
            {
                MoveBy(c.gameObject, new Vector3(0, 0, -25), 5.0f).Forget();
            }
        }
    }

    private async UniTaskVoid MoveBy(GameObject obj, Vector3 deltaPosition, float duration)
    {
        Vector3 initialPosition = obj.transform.position;
        Vector3 targetPosition = initialPosition + deltaPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration && obj != null)
        {
            obj.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        if (obj != null)
        {
            obj.transform.position = targetPosition;
        }
    }
}
