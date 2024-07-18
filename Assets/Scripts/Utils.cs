using UnityEngine;
using Random = System.Random;
using System.Security.Cryptography;
using System.Text;

public static class Utils
{
    public static T AddPrefabGameObject<T>(GameObject parent, GameObject prefab)
    {
        return AddPrefabGameObject<T>(parent, prefab, 0f, 0f, 0f);
    }

    public static T AddPrefabGameObject<T>(GameObject parent, GameObject prefab, float x, float y, float z)
    {
        GameObject go = GameObject.Instantiate(prefab, parent.transform);
        go.transform.localPosition = new Vector3(x, y, z);
        return (T)(object)go.GetComponent<T>();
    }

    public static void DestroyGameObject(GameObject target)
    {
        if (target == null)
        {
            return;
        }

        target.SetActive(false);
        target.transform.SetParent(null);
        GameObject.Destroy(target);
        object obj = (object)target;
        obj = null;
    }

    public static string GetHashValue(string seed = null)
    {
        seed = seed == null ? CreateRandomString(10) : seed;

        byte[] ar1 = Encoding.UTF8.GetBytes(seed);
        byte[] ar2 =
            new SHA256CryptoServiceProvider().ComputeHash(ar1);

        // バイト配列 → 16進数文字列
        var ret = new StringBuilder();
        foreach (byte b1 in ar2)
        {
            ret.Append(b1.ToString("x2"));
        }
        return ret.ToString();
    }

    private static string CreateRandomString(int length)
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var charsarr = new char[length];
        var random = new Random();

        for (int i = 0; i < charsarr.Length; i++)
        {
            charsarr[i] = characters[random.Next(characters.Length)];
        }
        return new string(charsarr);
    }

    public static void SetCanvasOrder(GameObject go, int order)
    {
        Canvas canvas = go.transform.GetChild(0).GetComponent<Canvas>();
        canvas.sortingOrder = order;
    }
}