using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MuseumDatabase : MonoBehaviour
{
    public MuseumGroupData[] groupData;

    public void DisplayGroupInfo(string groupName, Image targetImage, TextMeshProUGUI targetText)
    {
        MuseumGroupData foundGroup = FindGroup(groupName);

        if (foundGroup != null)
        {
            targetImage.sprite = foundGroup.image;
            targetText.text = foundGroup.description;
        }
        else
        {
            Debug.LogWarning("Group not found: " + groupName);
        }
    }

    private MuseumGroupData FindGroup(string groupName)
    {
        foreach (MuseumGroupData group in groupData)
        {
            if (group.groupName == groupName)
            {
                return group;
            }
        }
        return null;
    }
}
