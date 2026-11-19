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
    /// <returns>true on success, false if date is invalid.</returns>
    public bool AddEffortEntry(string date, int hoursReqsAnalysis, int hoursDesigning, int hoursCoding, int hoursTesting, int hoursProjManagement, int hoursUncategorized)
    {
        EffortEntry entry = new EffortEntry();
        if (!entry.SetDatestamp(date))
        {
            return false;
        }
        
        entry.SetEffortByCategory("Reqs Analysis", hoursReqsAnalysis);
        entry.SetEffortByCategory("Designing", hoursDesigning);
        entry.SetEffortByCategory("Coding", hoursCoding);
        entry.SetEffortByCategory("Testing", hoursTesting);
        entry.SetEffortByCategory("Proj Management", hoursProjManagement);
        entry.SetEffortByCategory("Uncategorized", hoursUncategorized);

        effortEntries.Add(entry);
        effortEntries.Sort((e1, e2) => e1.GetDatestamp().CompareTo(e2.GetDatestamp())); // sorts effort entry list by date
        return true;
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
    /// UpdateEffortEntryByIndex: Updates an effort entry by index. All parameters except the index are optional.
    /// </summary>
    /// <returns>True if the update succeeds and false if it doesn't.</returns>
    public bool UpdateEffortEntryByIndex(int index, int? hoursReqsAnalysis=null, int? hoursDesigning=null, int? hoursCoding=null, int? hoursTesting=null, int? hoursProjManagement=null, int? hoursUncategorized=null)
    {
        if (index >= 0 && index < effortEntries.Count)
        {
            EffortEntry entry = effortEntries[index];
            // categories: Reqs Analysis, Designing, Coding, Testing, Proj Management, Uncategorized
            entry.SetEffortByCategory("Reqs Analysis", hoursReqsAnalysis ?? entry.GetEffortByCategory("Reqs Analysis"));
            entry.SetEffortByCategory("Reqs Analysis", hoursDesigning ?? entry.GetEffortByCategory("Designing"));
            entry.SetEffortByCategory("Reqs Analysis", hoursCoding ?? entry.GetEffortByCategory("Coding"));
            entry.SetEffortByCategory("Reqs Analysis", hoursTesting ?? entry.GetEffortByCategory("Testing"));
            entry.SetEffortByCategory("Reqs Analysis", hoursProjManagement ?? entry.GetEffortByCategory("Proj Management"));
            entry.SetEffortByCategory("Reqs Analysis", hoursUncategorized ?? entry.GetEffortByCategory("Uncategorized"));
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

    /// <summary>
    /// SplitToTimeBuckets: Sorts effort entries into buckets based on their date.
    /// Buckets have the form of dummy ProjectRequirement objects so that totalEffort()
    /// (and thereby UserInterface.PrintRequirementActualEffort()) can be called on them easily.
    /// </summary>
    /// <param name="weekly">Whether to use weekly bucketing or not.</param>
    /// <returns>An array of dummy ProjectRequirement objects where each element has effort entries restricted to the bucket interval.</returns>
    public ProjectRequirement[] SplitToTimeBuckets(bool weekly=false)
    {
        // start with dictionary to enforce uniqueness
        Dictionary<DateOnly, List<EffortEntry>> buckets_dict = [];
        foreach(EffortEntry entry in effortEntries)
        {
            DateOnly date_key = weekly ? entry.GetWeekstamp() : entry.GetDatestamp();
            if (buckets_dict.ContainsKey(date_key)) {
                buckets_dict[date_key].Add(entry);
            }
            else
            {
                buckets_dict[date_key] = [];
                buckets_dict[date_key].Add(entry);
            }
        }
        
        // convert to list of tuples, static-ify internal lists
        List<(DateOnly, EffortEntry[])> buckets_list = [];
        foreach(KeyValuePair<DateOnly, List<EffortEntry>> kv in buckets_dict)
        {
            if (weekly) kv.Value.Sort((e1, e2) => e1.GetDatestamp().CompareTo(e2.GetDatestamp())); // sort entries by date if they're grouped by week
            buckets_list.Add((kv.Key, kv.Value.ToArray()));
        }

        // sort buckets by date just in case being put in a dictionary scrambled them
        buckets_list.Sort((t1, t2) => t1.Item1.CompareTo(t2.Item1));

        List<ProjectRequirement> dummy_reqs = [];
        foreach((DateOnly, EffortEntry[]) t in buckets_list)
        {
            ProjectRequirement dummy = new(id: -999999, isfunc: this.isFunctional, desc: "dummy requirement, do not print!!!");
            foreach (EffortEntry entry in t.Item2)
            {
                // Reqs Analysis, Designing, Coding, Testing, Proj Management, Uncategorized
                dummy.AddEffortEntry(
                    t.Item1.ToString(format:"yyyy-MM-dd"),
                    entry.GetEffortByCategory("Reqs Analysis"),
                    entry.GetEffortByCategory("Designing"),
                    entry.GetEffortByCategory("Coding"),
                    entry.GetEffortByCategory("Testing"),
                    entry.GetEffortByCategory("Proj Management"),
                    entry.GetEffortByCategory("Uncategorized")
                    );
            }
            dummy_reqs.Add(dummy);
        }

        return dummy_reqs.ToArray();
    }
    
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
			difference[estimate.Key] = actualTotals[estimate.Key] - estimate.Value;
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