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
        this.cost = float.Round(cost, 2);
        riskValue = float.Round(cost * probability, 2);
        this.description = description;
    }

    // get/set id
    // get/set status
    // get/set probability
    // get/set cost
    // get/set description

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

    // get array of values
    // set value
    // get/set datestamp
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

