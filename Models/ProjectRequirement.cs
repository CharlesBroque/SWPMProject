namespace Models;

/// <summary>
/// ProjectRequirement Class: manages an individual functional / non-functional project requirement
/// and its associated effort entries.
/// </summary>
public class ProjectRequirement
{
    private int idNumber; // id management to be handled by function in Project
    private bool isFunctional;
    private string description;
    private List<EffortEntry> effortEntries;
    private Dictionary<string, int> estimatedEffortHours;

    /// <summary>
    /// ProjectRequirement Constructor: Initializes a new project requirement.
    /// </summary>
    /// <param name="id">The assigned requirement ID.</param>
    /// <param name="isfunc">Indicates whether the requirement is functional or non-functional</param>
    /// <param name="desc">The requirement description.</param>
    public ProjectRequirement(int id, bool isfunc, string desc) {
        this.idNumber = id;
        this.isFunctional = isfunc;
        this.description = desc;
        this.effortEntries = new List<EffortEntry>();
		estimatedEffortHours = new Dictionary<string, int>(new EffortEntry().GetEffortHours());
    }

    /// <summary>
    /// GetID: Gets the requirement ID.
    /// </summary>
    public int GetID()
    {
        return idNumber;
    }
    
    /// <summary>
    /// GetEffortEntries: Gets all effort entries associated with the requirement.
    /// </summary>
    /// <returns>An array of the requirement's effort entries</returns>
    public EffortEntry[] GetEffortEntries()
    {
        return effortEntries.ToArray();
    }
    
    /// <summary>
    /// AddEffortEntry: Creates and adds a new effort entry for the requirement. 
    /// </summary>
    /// <param name="hoursReqsAnalysis">Hours spent on requirements analysis.</param>
    /// <param name="hoursDesigning">Hours spent designing.</param>
    /// <param name="hoursCoding">Hours spent coding.</param>
    /// <param name="hoursTesting">Hours spent testing.</param>
    /// <param name="hoursProjManagement">Hours spent on project management.</param>
    /// <param name="hoursUncategorized">Hours spent on other tasks.</param>
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
    
    /// <summary>
    /// RemoveNewestEntry: Removes the most recently added effort entry.
    /// </summary>
    public void RemoveNewestEntry()
    {
        if (effortEntries.Count > 0) 
        {
            effortEntries.RemoveAt(effortEntries.Count - 1);
        }
    }
    
    /// <summary>
    /// RemoveEntryByIndex: Removes an entry by its index (0-indexed, please convert if using 1-index).
    /// </summary>
    /// <param name="index">The zero-based index of the entry to remove.</param>
    /// <returns>True if argument was in range, false otherwise.</returns>
    public bool RemoveEntryByIndex(int index)
    {
        if (index >= 0 && index < effortEntries.Count)
        {
            effortEntries.RemoveAt(index);
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// totalEffort: Calculates the total hours recorded  in each effort category
    /// </summary>
    /// <returns>A dictionary containing the total hrs for each category.</returns>
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
    
    /// <summary>
    /// GetDescription: Gets the requirement description.
    /// </summary>
    public string GetDescription()
    {
        return description;
    }

    /// <summary>
    /// SetDescription: Updates the requirement description.
    /// </summary>
    /// <param name="description">The new requirement description.</param>
    public void SetDescription(string description)
    {
        this.description = description;
    }

    /// <summary>
    /// SetEstimatedEffort: Sets the estimated effort hours for a specified effort category.
    /// </summary>
    public void SetEstimatedEffortByCategory(string categoryName, int hours)
    {
        if (hours < 0)
        {
            throw new ArgumentException("Estimated effort cannot be negative.", nameof(hours));
        }

        if (!estimatedEffortHours.ContainsKey(categoryName))
        {
            throw new ArgumentException("Invalid effort category.", nameof(categoryName));
        }
        estimatedEffortHours[categoryName] = hours;
    }

    /// <summary>
    /// GetEstimatedEffortHours: Gets the estimated effort hours for every effort category.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> GetEstimatedEffortHours()
    {
        return new Dictionary<string, int>(estimatedEffortHours);
    }
    
	/// <summary>
	/// GetEffortDifference: Calculates the difference between actual effort and estimated effort for each category.
	/// (actual - estimated)
	/// </summary>
	/// <returns>A dictionary of effort differences by category.</returns>
	public Dictionary<string, int> GetEffortDifference() 
	{
		Dictionary<string, int> actualTotals = totalEffort();
		Dictionary<string, int> difference = new Dictionary<string, int>();

		foreach (KeyValuePair<string, int> estimate in estimatedEffortHours) 
		{
			variance[estimate.Key] = actualTotals[estimate.Key] - estimate.Value;
		}
		return difference;
	}

    /// <summary>
    /// ToString: Returns a formatted representation of the requirement.
    /// </summary>
    public override string ToString()
    {
        if (isFunctional) {
            return "FR-" + idNumber.ToString() + ": " + description;
        } // else
        return "NFR-" + idNumber.ToString() + ": " + description;
    }
}