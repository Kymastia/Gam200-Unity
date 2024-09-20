using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CSVReader : ScriptableObject
{
    // Path to the CSV file
    [SerializeField]
    private string _path;

    // Delimiter used in CSV files, default is a comma
    [SerializeField]
    private string delimiter = ",";

    // Bool to indicate if synchronisation is needed
    [SerializeField]
    private bool _synchronise = true;

    // Task for handling asynchronous synchronisation
    private Task _synchronisationTask = null;

    // Property to get the full path of the CSV file
    // This combines the application data path with the specified path
    public string FullPath => string.Join('/', Application.dataPath, _path);

    // Method to seek a row by its number in the CSV file
    // The method returns a dictionary where Key value is the column header, while the values are the data of each row under the column
    // Dictionary is finding and returning two values, Key and Data, both in string format
    // Okay so Async, you can think of it as a coroutine
    // Task<> Function() is how its formatted
    // Tasks are part of the package for Async, its basically where you fetch the data from
    protected async Task<IDictionary<string, string>> SeekRow(int rowNumber)
    {
        // Open the file asynchronously
        using var file = new StreamReader(File.OpenRead(FullPath));

        // Reads the first row, aka the header row asynchronously
        // ReadLineAsync() is a built in method, it just reads the line of characters
        // File was defined the line above
        var headerRow = await file.ReadLineAsync();

        // Split the header row, the line it just read into column headers using the specified delimiter, this case, the (,)
        var columnHeaders = headerRow.Split(delimiter);

        // Skip rows until the desired row number is reached
        // The idea is that if its row 5, its gonna keep minusing till either its row 1, or it runs out of rows
        while (!file.EndOfStream && rowNumber > 0)
        {
            // Read each line asynchronously
            await file.ReadLineAsync();
            rowNumber--;
        }

        // If the end of the file is reached before the row number, you messed up and it returns null
        if (file.EndOfStream || rowNumber > 0)
        {
            return null;
        }

        // Read the desired row asynchronously
        var row = await file.ReadLineAsync();

        // Split the row data using the specified delimiter
        var rowData = row.Split(delimiter);

        // Return the row data as a dictionary, pairing column headers with row data, the key and value
        return new Dictionary<string, string>(
            columnHeaders.Zip(rowData, KeyValuePair.Create)
        );
    }

    // Method to seek a row by its ID in the CSV file
    // This method returns a dictionary where the keys are column headers and values are the corresponding data in the row
    protected async Task<IDictionary<string, string>> SeekRow(string id)
    {
        // Assert that the file exists at the full path
        Debug.Assert(File.Exists(FullPath), "Path does not exist: " + FullPath, this);

        // Open the file asynchronously
        using var file = new StreamReader(File.OpenRead(FullPath));

        // Read the header row asynchronously
        // ReadLineAsync() is a built in method, it just reads the line of characters
        var headerRow = await file.ReadLineAsync();

        // Split the header row into column headers using the specified delimiter
        var columnHeaders = headerRow.Split(delimiter);

        // Find the index of the "ID" column
        var rowNameColumnIndex = Array.IndexOf(columnHeaders, "ID");

        // Iterate through the rows to find the row with the matching ID
        while (!file.EndOfStream)
        {
            // Read each row asynchronously
            var row = await file.ReadLineAsync();

            // Split the row data using the specified delimiter
            var rowData = row.Split(delimiter);

            // Get the value of the "ID" column for the current row
            var rowName = rowData[rowNameColumnIndex];

            // If the ID matches, return the row data as a dictionary
            if (rowName == id)
            {
                return new Dictionary<string, string>(
                    columnHeaders.Zip(rowData, KeyValuePair.Create)
                );
            }
        }

        // If no matching ID is found, return null
        return null;
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
        if (!File.Exists(FullPath) || (_synchronisationTask is not null && !_synchronisationTask.IsCompleted))
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
