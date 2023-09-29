using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinManager", menuName = "GameResources/SkinManager")]
public class SkinManager : ScriptableObject
{
    public List<CharacterSkin> Skins = new List<CharacterSkin>();

    public int Count { get { return Skins.Count; } }

    public RuntimeAnimatorController GetSkinById(int id)
    {
        return Skins[id].Controller;
    }
}

[Serializable]
public class CharacterSkin
{
    public RuntimeAnimatorController Controller;
}