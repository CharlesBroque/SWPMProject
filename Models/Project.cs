namespace DefaultNamespace;

public class Project
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