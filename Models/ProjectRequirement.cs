namespace DefaultNamespace;

public class ProjectRequirement
{
    int idNumber; // id management to be handled by function in Project
    bool isFunctional;
    string description;
    List<EffortEntry> effortEntries;

    public ProjectRequirement(int id, bool isfunc, string desc) {
        idNumber = id;
        isFunctional = isfunc;
        description = desc;
        effortEntries = new List<EffortEntry>();
    }

    // get ID
    public int GetID()
    {
        return idNumber;
    }
    // get all effort entries
    public EffortEntry[] GetEffortEntries()
    {
        return effortEntries.ToArray();
    }
    // add effort entry
    public void AddEffortEntry(int hoursReqsAnalysis, int hoursDesigning, int hoursCoding, int hoursTesting, int hoursProjManagement, int hoursUncategorized)
    {
        EffortEntry entry = new EffortEntry();
        entry.SetEffortByCategory("Reqs Analysis", hoursReqsAnalysis);
        entry.SetEffortByCategory("Designing", hoursDesigning);
        entry.SetEffortByCategory("Coding", hoursCoding);
        entry.SetEffortByCategory("Testing", hoursTesting);
        entry.SetEffortByCategory("Proj Management", hoursProjManagement);
        entry.SetEffortByCategory("Uncategorized", hoursUncategorized);

        effortEntries.Add(entry);
    }
    // remove newest entry
    public void RemoveNewestEntry()
    {
        if (effortEntries.Count > 0) {
            effortEntries.RemoveAt(effortEntries.Count - 1);
        }
    }
    // remove entry by index
    /// <summary>
    /// Removes an entry by its index (0-indexed, please convert if using 1-index).
    /// </summary>
    /// <param name="index"></param>
    /// <returns>true if argument was in range, false otherwise.</returns>
    public bool RemoveEntryByIndex(int index)
    {
        if (index >= 0 && index < effortEntries.Count)
        {
            effortEntries.RemoveAt(index);
            return true;
        }
        return false;
    }
    // get total effort dictionary
    public Dictionary<string, int> totalEffort()
    {
        Dictionary<string, int> totals = new EffortEntry().GetEffortHours();
        foreach (EffortEntry entry in effortEntries)
        {
            Dictionary<string, int> hours = entry.GetEffortHours();
            foreach (KeyValuePair<string, int> kv in hours)
            {
                totals[kv.Key] += kv.Value;
            }
        }
        return totals;
    }
    
    //update effort:
    
    // get total no. hours for each requirement:
    
    // get description (used to update req. descriptions):
    public string GetDescription()
    {
        return description;
    }

    public void SetDescription(string description)
    {
        this.description = description;
    }

    public string ToString()
    {
        if (isFunctional) {
            return "FR-" + idNumber.ToString() + ": " + description;
        }
        // else
        return "NFR-" + idNumber.ToString() + ": " + description;
    }
}