using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoSingleton<ResourceManager> {

    public List<Sprite> numberResources;

    void Awake()
    {
        //numberResources = new List<Sprite>();
        LoadSpriteFromFile(ref numberResources, "Images/BackGround/Number");
        Debug.Log("Resources: " +  numberResources.Count);
    }

    void LoadSpriteFromFile(ref List<Sprite> _spriteResources, string _path)
    {
        numberResources = new List<Sprite>(Resources.LoadAll<Sprite>(_path));
    }

    public Sprite GetSpriteByName(string spriteName)
    {
        return numberResources.Find(x => x.name.Equals(spriteName));
    }
}
