using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour
{
    private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        resolutionDropdown = GetComponent<TMP_Dropdown>();
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        HashSet<string> uniqueResolutions = new HashSet<string>(); 
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (uniqueResolutions.Add(option))
            {
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = options.Count - 1; 
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex; 
        resolutionDropdown.RefreshShownValue(); 

        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    public void ChangeResolution(int index)
    {
        string[] resolutionParts = resolutionDropdown.options[index].text.Split('x');
        int width = int.Parse(resolutionParts[0].Trim());
        int height = int.Parse(resolutionParts[1].Trim());

        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}
