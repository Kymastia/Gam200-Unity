using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class JsonReader : ScriptableObject
{
    //Abandon for now
    [SerializeField]
    private string fullPath;

    [SerializeField]
    private bool _synchronise = true;

    private Task _synchronisationTask = null;

    string jsonString;

    public class ListWrapper
    {
        public List<string> items;
    }

    protected async Task<IDictionary<string, string>> SeekID(string id)
    {
        // Assert that the file exists at the full path
        Debug.Assert(File.Exists(fullPath), "Path does not exist: " + fullPath, this);

        // Open the file asynchronously
        using var file = new StreamReader(File.OpenRead(fullPath));

        jsonString = await file.ReadToEndAsync();

        ListWrapper jsonRowList = JsonUtility.FromJson<ListWrapper>(jsonString);

        IDictionary<string, string> dataDictionary = new Dictionary<string, string>();

        foreach (string item in jsonRowList.items)
        {
            // Assuming each item in the list is formatted as "id: value"
            string[] splitItem = item.Split(':');
            if (splitItem.Length == 2)
            {
                string key = splitItem[0].Trim();
                string value = splitItem[1].Trim();

                if (key == id)  // Match the desired ID
                {
                    dataDictionary[key] = value;
                }
            }
        }

        return dataDictionary;
}

// Virtual method for synchronisation, can be overridden in derived classes
// The default implementation completes immediately
protected virtual async Task Synchronise() => await Task.CompletedTask;

    // Method called when the script is validated in the editor
    // This ensures that synchronisation happens as needed
    protected void OnValidate()
    {
        // If synchronisation is not needed, return immediately
        if (!_synchronise)
        {
            return;
        }

        // If the file doesn't exist or synchronisation is already in progress, do nothing
        if (!File.Exists(fullPath) || (_synchronisationTask is not null && !_synchronisationTask.IsCompleted))
        {
            return;
        }

        // Start the synchronisation task
        _synchronisationTask = Synchronise();
    }

    // Method called when the script is enabled
    // This ensures that synchronisation happens when the script is enabled
    protected void OnEnable()
    {
        OnValidate();  // Call OnValidate to ensure synchronisation happens when enabled
    }
}