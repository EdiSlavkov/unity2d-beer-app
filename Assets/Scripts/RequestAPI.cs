using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class RequestAPI
{
    private const string Url = "https://api.punkapi.com/v2/beers?";

    public static IEnumerator MakeBeersRequest(string filters, Action<List<Beer>> callback, Action<bool> connectionErrorCallback)
    {
        UnityWebRequest beerRequest = UnityWebRequest.Get($"{Url}{filters}");
        yield return beerRequest.SendWebRequest();
        bool hasError = beerRequest.result == UnityWebRequest.Result.ConnectionError;
        if (!hasError)
        {
            List<Beer> response = JsonConvert.DeserializeObject<List<Beer>>(beerRequest.downloadHandler.text);
            callback.Invoke(response);
        }
        connectionErrorCallback?.Invoke(hasError);
    }

    public static IEnumerator MakeBeerSpriteRequest(string url, Action<Sprite> callback)
    {
        UnityWebRequest spriteRequest = UnityWebRequestTexture.GetTexture(url);
        yield return spriteRequest.SendWebRequest();
        if (spriteRequest.result != UnityWebRequest.Result.ConnectionError)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(spriteRequest);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            callback.Invoke(sprite);
        }
        else
        {
            callback.Invoke(null);
        }
    }
}