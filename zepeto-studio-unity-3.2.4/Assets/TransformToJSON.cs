using UnityEngine;
using System.IO;
using LitJson;

public class TransformToJSON : MonoBehaviour 
{
    private Transform[] transformsToSave;
    [SerializeField] private string filePath = "transformData.json";

    public void SaveTransformDataToJson()
    {
        // Create a JSON array to store the transform data
        JsonData transformsData = new JsonData();

        transformsToSave = GetComponentsInChildren<Transform>();

        // Loop through all the transforms and add them to the array
        foreach (Transform transformToSave in transformsToSave)
        {
            JsonData transformData = new JsonData();
           
            transformData["name"] = transformToSave.name + "\n";
            if (transformToSave.localScale.x != 1f)
            {
                transformData["scaleX"] = transformToSave.localScale.x + "\n";
            }
            if (transformToSave.localScale.y != 1f)
            {
                transformData["scaleX"] = transformToSave.localScale.x + "\n";
            }
            if (transformToSave.localScale.z != 1f)
            {
                transformData["scaleX"] = transformToSave.localScale.x + "\n";
            }
            
            
            transformsData.Add(transformData);
            transformsData.Add("\n");
        }

        // Convert the transforms data to a JSON string
        string jsonData = transformsData.ToJson().Replace("\\n", "\n");

        // Write the JSON data to a file
        File.WriteAllText(filePath, jsonData);
    }
}


