using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class pokeWebRequest : MonoBehaviour
{
    public RawImage pokeRawImage;
    public TextMeshProUGUI pokeText;

    private readonly string basePokeUrl = "https://pokeapi.co/api/v2/";

    // Start is called before the first frame update
    void Start(){
        pokeRawImage.texture = Texture2D.blackTexture;
        pokeText.text = " ";
        
    }

   public void OnButtonRandomPokemon(){
        int randomPokeIndex = Random.Range(1, 127);
        pokeRawImage.texture = Texture2D.blackTexture;
        pokeText.text = "Loading...";

        StartCoroutine(GetPokemonAtIndex(randomPokeIndex));
    }

    IEnumerator GetPokemonAtIndex(int pokeIndex){
        string pokemonUrl = basePokeUrl + "pokemon/" + pokeIndex.ToString();

        UnityWebRequest pokeInfoRequest = UnityWebRequest.Get(pokemonUrl);
        yield return pokeInfoRequest.SendWebRequest();

        if (pokeInfoRequest.isNetworkError || pokeInfoRequest.isHttpError){
            Debug.LogError(pokeInfoRequest.error);
            yield break;
        }

        JSONNode pokeInfo = JSON.Parse(pokeInfoRequest.downloadHandler.text);

        string pokeName = pokeInfo["name"];
        string pokeSpriteUrl = pokeInfo["sprites"]["front_default"];

        UnityWebRequest pokeSpriteRequest = UnityWebRequestTexture.GetTexture(pokeSpriteUrl);
        yield return pokeSpriteRequest.SendWebRequest();

        if (pokeSpriteRequest.isNetworkError || pokeSpriteRequest.isHttpError){
            Debug.LogError(pokeSpriteRequest.error);
            yield break;
        }

        pokeRawImage.texture = DownloadHandlerTexture.GetContent(pokeSpriteRequest);
        pokeRawImage.texture.filterMode = FilterMode.Point;

        pokeText.text = pokeName;





    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
