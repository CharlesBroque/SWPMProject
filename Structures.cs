// some structures and helper functions to make writing the application easier

class ProjectRisk
{
    int idNumber; // id management to be handled by function in Project
    string status; // statuses include... what do they include?
    float probability;
    float cost;
    float riskValue;
    string description;

    public ProjectRisk(int id, float probability, int cost, string status, string description)
    {
        idNumber = id;
        this.probability = probability;
        this.cost = cost;
        UpdateRiskValue();
        this.description = description;
    }

    // get/set id
    public int GetID()
    {
        return idNumber;
    }
    public void SetID(int id)
    {
        idNumber = id;
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
    public void SetEffortByCategory(string category_name, int hours)
    {
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
        bool did_it_work = DateOnly.TryParse(date_string, result);
        if (did_it_work)
        {
            datestamp = result;
            return true;
        }
        // else
        return false;
    }
}

class ProjectRequirement
{
    int idNumber; // id management to be handled by function in Project
    bool isFunctional;
    string description;
    EffortEntry[] effortEntries;

    public ProjectRequirement(int id, bool isfunc, string desc) {
        idNumber = id;
        isFunctional = isfunc;
        description = desc;
        effortEntries = new EffortEntry[];
    }

    // get all effort entries
    // add effort entry
    // remove effort entry
    // get total effort

    string ToString()
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
    string[] owners;
    string[] managers;
    string[] members;
    ProjectRisk[] risks;
    ProjectRequirement[] fRequirements;
    ProjectRequirement[] nfRequirements;

    // constructor
    // update description
    // add/remove owner
    // add/remove manager
    // add/remove member
    // add/remove risk
    // add/remove frequirement
    // add/remove nfrequirement
    // save to/load from disk? maybe out of scope

}

