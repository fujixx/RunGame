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

    private List<Field> _fields;
    private IDisposable _timerDisposable;

    void Start()
    {
        _fields = new();

        _timerDisposable = Observable.Interval(TimeSpan.FromSeconds(2))
            .StartWith(0)
            .Subscribe(_ => ExecuteEveryTenSeconds())
            .AddTo(this);
    }

    void OnDestroy()
    {
        _timerDisposable.Dispose();
    }

    private void ExecuteEveryTenSeconds()
    {
        Field field = (Field)Utils.AddPrefabGameObject<Field>(parentField, prefabField);
        _fields.Add(field);

        foreach (Field x in _fields)
        {
            MoveBy(x.gameObject, new Vector3(0, 0, -10), 2.0f).Forget();
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
