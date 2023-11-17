using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "MuseumGroupData", menuName = "New_Museum_Data/Museum Data")]
public class MuseumGroupData : ScriptableObject
{
    public string groupName;
    public Sprite image;
    [TextArea(3, 10)] public string description;
}
