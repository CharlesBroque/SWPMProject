// some structures and helper functions to make writing the application easier
// TODO: method descriptions

class ProjectRisk
{
    int idNumber; // id management to be handled by function in Project
    string status; // statuses include... what do they include?
    float probability;
    float cost;
    float riskValue;
    string description;

    public ProjectRisk(int id, float probability, float cost, string status, string description)
    {
        idNumber = id;
        this.probability = probability;
        this.cost = cost;
        this.status = status;
        UpdateRiskValue();
        this.description = description;
    }

    // get/set id
    public int GetID()
    {
        return idNumber;
    }
    // get/set status
    public string GetStatus()
    {
        return status;
    }
    public void SetStatus(string status)
    {
        this.status = status;
    }
    // get/set probability
    public float GetProbability()
    {
        return probability;
    }
    public void SetProbability(float probability)
    {
        this.probability = probability;
        UpdateRiskValue();
    }
    // get/set cost
    public float GetCost()
    {
        return cost;
    }
    public void SetCost(float cost)
    {
        this.cost = float.Round(cost, 2);
        UpdateRiskValue();
    }
    // get/update riskValue (riskValue should never be set directly)
    public float GetRiskValue()
    {
        return riskValue;
    }
    void UpdateRiskValue()
    {
        riskValue = float.Round(probability * cost);
    }
    // get/set description
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
        return "R-" + idNumber.ToString() + "(" + status.ToString() + "): " + description;
    }
}

class EffortEntry
{
    Dictionary<string, int> effortHours;
    DateOnly datestamp;

    public EffortEntry()
    {

        effortHours = new Dictionary<string, int>
        {
            { "Reqs Analysis", 0 },
            { "Designing", 0 },
            { "Coding", 0 },
            { "Testing", 0 },
            { "Proj Management", 0 },
            { "Uncategorized", 0 }
        };
        datestamp = DateOnly.FromDateTime(DateTime.Now);
    }

    // get all values
    public Dictionary<string, int> GetEffortHours()
    {
        return effortHours;
    }
    // get value by name
    public int GetEffortByCategory(string category_name)
    {
        if (effortHours.ContainsKey(category_name))
        {
            return effortHours[category_name];
        }
        // else
        throw new ArgumentException("Category_name must be one of: Reqs Analysis, Designing, Coding, Testing, Proj Management, Uncategorized.", paramName: category_name);
    }
    // set value by name
    /// <summary>
    /// Sets effort hours by the name of the category. Throws ArgumentException if the category name is not Reqs Analysis, Designing, Coding, Testing, Proj Management, or Uncategorized.
    /// </summary>
    /// <param name="category_name"></param>
    /// <param name="hours"></param>
    /// <exception cref="ArgumentException"></exception>
    public void SetEffortByCategory(string category_name, int hours)
    {
        // Checks for negative hour entry.
        if (hours < 0)
        {
            throw new ArgumentException("Hours cannot be negative.", paramName: nameof(hours));
        }
        
        if (effortHours.ContainsKey(category_name))
        {
            effortHours[category_name] = hours;
            return;
        }
        // else
        throw new ArgumentException("Category_name must be one of: Reqs Analysis, Designing, Coding, Testing, Proj Management, Uncategorized.", paramName: category_name);
    }
    // get/set datestamp
    public DateOnly GetDatestamp()
    {
        return datestamp;
    }
    /// <summary>
    /// Sets the datestamp. Internal use of DateOnly.TryParse means recognition is fuzzy--a variety of formats should work.
    /// </summary>
    /// <param name="date_string">String indicating the date.</param>
    /// <returns>true if the datestamp was set successfully and false if it failed to parse.</returns>
    public bool SetDatestamp(string date_string)
    {
        DateOnly result;
        bool did_it_work = DateOnly.TryParse(date_string, out result);
        if (did_it_work)
        {
            datestamp = result;
            return true;
        }
        // else
        return false;
    }

    // public string ToString(); // some kind of readout will eventually be needed
}

class ProjectRequirement
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

    public string ToString()
    {
        if (isFunctional) {
            return "FR-" + idNumber.ToString() + ": " + description;
        }
        // else
        return "NFR-" + idNumber.ToString() + ": " + description;
    }
}

class Project
{
    string description;
    List<string> owners;
    List<string> managers;
    List<string> members;
    List<ProjectRisk> risks;
    int lifetimeRiskCount;
    List<ProjectRequirement> fRequirements;
    int lifetimeFRequirementCount;
    List<ProjectRequirement> nfRequirements;
    int lifetimeNFrequirementCount;

    // constructor
    public Project()
    {
        description = "No description set.";
        owners = new List<string>();
        managers = new List<string>();
        members = new List<string>();
        risks = new List<ProjectRisk>();
        fRequirements = new List<ProjectRequirement>();
        nfRequirements = new List<ProjectRequirement>();
        lifetimeRiskCount = 0;
        lifetimeFRequirementCount = 0;
        lifetimeNFrequirementCount = 0;
    }
    // update description
    public void SetDescription(string description)
    {
        this.description = description;
    }
    // get owners (please note owners, managers, and members methods have strong parity. if you change one please change the others.)
    public List<string> GetOwners()
    {
        return owners;
    }
    // add/remove owner
    public void AddOwner(string name)
    {
        owners.Add(name);
    }
    /// <summary>
    /// Removes an owner from the owner list.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>true if owner was successfully found and removed, otherwise false.</returns>
    public bool RemoveOwner(string name)
    {
        bool did_it_work = owners.Remove(name);
        if (did_it_work)
        {
            return true;
        }
        // else
        return false;
    }
    // get managers
    public List<string> GetManagers()
    {
        return managers;
    }
    // add/remove manager
    public void AddManager(string name)
    {
        managers.Add(name);
    }
    /// <summary>
    /// Removes a manager from the manager list.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>true if manager was successfully found and removed, otherwise false.</returns>
    public bool RemoveManager(string name)
    {
        bool did_it_work = managers.Remove(name);
        if (did_it_work)
        {
            return true;
        }
        // else
        return false;
    }
    // get members
    public List<string> GetMembers()
    {
        return members;
    }
    // add/remove member
    public void AddMember(string name)
    {
        members.Add(name);
    }
    /// <summary>
    /// Removes a team member from the member list.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>true if member was successfully found and removed, otherwise false.</returns>
    public bool RemoveMember(string name)
    {
        bool did_it_work = members.Remove(name);
        if (did_it_work)
        {
            return true;
        }
        // else
        return false;
    }
    // get risks
    public List<ProjectRisk> GetRisks()
    {
        return risks;
    }
    // add/remove risk
    public void AddRisk(float probability, float cost, string status, string description)
    {
        int id = ++lifetimeRiskCount;
        ProjectRisk risk = new ProjectRisk(id, probability, cost, status, description);
        risks.Add(risk);
    }
    // update risk
    public bool UpdateRiskByID(int id, float? probability = null, float? cost = null, string status = null, string description = null)
    {
        ProjectRisk risk = risks.Find(risk => risk.GetID() == id);
        if (risk == null) return false;
        if (probability.HasValue) risk.SetProbability(probability.Value);
        if (cost.HasValue) risk.SetCost(cost.Value);
        if (status != null) risk.SetStatus(status);
        if (description != null) risk.SetDescription(description);
        return true;
    }
    /// <summary>
    /// Removes a ProjectRisk with the given ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if the risk is successfully removed, false if no such ID exists.</returns>
    public bool RemoveRiskByID(int id)
    {
        int index = risks.FindIndex(risk => risk.GetID() == id);
        if (index < 0)
        {
            return false;
        }
        // else
        risks.RemoveAt(index);
        return true;
    }
    // get frequirements
    public List<ProjectRequirement> GetFRequirements()
    {
        return fRequirements;
    }
    // add/remove frequirement
    public void AddFRequirement(string desc)
    {
        int id = ++lifetimeFRequirementCount;
        ProjectRequirement fReq = new ProjectRequirement(id, true, desc);
        fRequirements.Add(fReq);
    }
    /// <summary>
    /// Removes functional requirement with given ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if successfully removed, false if no such ID exists.</returns>
    public bool RemoveFRequirementByID(int id)
    {
        int index = fRequirements.FindIndex(fReq => fReq.GetID() == id);
        if (index < 0)
        {
            return false;
        }
        // else
        fRequirements.RemoveAt(index);
        return true;
    }
    // get nfrequirements
    public List<ProjectRequirement> GetNFRequirements()
    {
        return nfRequirements;
    }
    // add/remove nfrequirement
    public void AddNFRequirement(string desc)
    {
        int id = ++lifetimeNFrequirementCount;
        ProjectRequirement nfReq = new ProjectRequirement(id, false, desc);
        nfRequirements.Add(nfReq);
    }
    /// <summary>
    /// Removes nonfunctional requirement by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if successfully removed, false if no such ID exists.</returns>
    public bool RemoveNFRequirementByID(int id)
    {
        int index = nfRequirements.FindIndex(nfReq => nfReq.GetID() == id);
        if (index < 0)
        {
            return false;
        }
        // else
        nfRequirements.RemoveAt(index);
        return true;
    }

    // save to/load from disk? maybe out of scope

}

