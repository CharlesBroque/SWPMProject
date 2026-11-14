namespace DefaultNamespace;

/// <summary>
/// Project Class: manages the information/data colllection for a project
/// </summary>
public class Project
{
    private string description;
    private List<string> owners;
    private List<string> managers;
    private List<string> members;
    
    private List<ProjectRisk> risks;
    private int lifetimeRiskCount;
    
    private List<ProjectRequirement> fRequirements;
    private int lifetimeFRequirementCount;
    
    private List<ProjectRequirement> nfRequirements;
    private int lifetimeNFrequirementCount;
    
    /// <summary>
    /// Project Constructor: Initializes a new project w/ default values
    /// </summary>
    public Project()
    {
        description = "No description set.";
        
        owners = new List<string>();
        managers = new List<string>();
        members = new List<string>();
        
        risks = new List<ProjectRisk>();
        lifetimeRiskCount = 0;
        
        fRequirements = new List<ProjectRequirement>();
        lifetimeFRequirementCount = 0;
        
        nfRequirements = new List<ProjectRequirement>();
        lifetimeNFrequirementCount = 0;
    }

    /// <summary>
    /// GetDescription: Gets the project description
    /// </summary>
    public string GetDescription()
    {
        return description;
    }
    
    /// <summary>
    /// SetDescription: Updates the project description
    /// </summary>
    public void SetDescription(string description)
    {
        this.description = description;
    }
    
    /// <summary>
    /// UpdateName: Used to update names on existing lists
    /// </summary>
    private static bool UpdateName(List<string> names, string oldName, string newName)
    {
        if (string.IsNullOrWhiteSpace(oldName)) return false;
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("New name cannot be empty.", nameof(newName));
        }
        
        int index = names.FindIndex(n => string.Equals(n, oldName, StringComparison.OrdinalIgnoreCase));
        if (index < 0) return false;
        
        names[index] = newName;
        return true;
    }
    
    // Owner management:
    // (please note owners, managers, and members methods have strong parity. if you change one please change the others.)
    
    /// <summary>
    /// GetOwners: Gets the list of project owners
    /// </summary>
    public List<string> GetOwners()
    {
        return owners;
    }
    
    /// <summary>
    /// AddOwner: Adds a owner to the project (owner list)
    /// </summary>
    public void AddOwner(string name)
    {
        owners.Add(name);
    }

    /// <summary>
    /// UpdateOwner: Updates and existing owner
    /// </summary>
    public bool UpdateOwner(string oldName, string newName)
    {
        return UpdateName(owners, oldName, newName);
    }
    
    /// <summary>
    /// RemoveOwner: removes an owner from the project (owner list)
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
    
    // Manager management: (lol)
    
    /// <summary>
    /// GetManagers: Gets the list of project managers
    /// </summary>
    public List<string> GetManagers()
    {
        return managers;
    }
    
    /// <summary>
    /// AddManager: Adds a manager to the project (manager list)
    /// </summary>
    public void AddManager(string name)
    {
        managers.Add(name);
    }

    /// <summary>
    /// UpdateManger: Updates an existing manager
    /// </summary>
    public bool UpdateManager(string oldName, string newName)
    {
        return UpdateName(managers, oldName, newName);
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
    
    // Member management:
    
    /// <summary>
    /// GetMembers: Gets the list of team members
    /// </summary>
    public List<string> GetMembers()
    {
        return members;
    }
    
    /// <summary>
    /// AddMember: Adds a team member to the project (team member list)
    /// </summary>
    public void AddMember(string name)
    {
        members.Add(name);
    }
    
    /// <summary>
    /// UpdateMember: Updates an existing team member
    /// </summary>
    public bool UpdateMember(string oldName, string newName)
    {
        return UpdateName(members, oldName, newName);
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
    
    // Risk Management:
    
    /// <summary>
    /// GetRisks: Gets the list of project risks
    /// </summary>
    public List<ProjectRisk> GetRisks()
    {
        return risks;
    }
    
    /// <summary>
    /// AddRisk: Adds a project risk and returns its assigned ID no.
    /// </summary>
    public int AddRisk(float probability, float cost, string status, string description)
    {
        int id = ++lifetimeRiskCount;
        ProjectRisk risk = new ProjectRisk(id, probability, cost, status, description);
        risks.Add(risk);
        lifetimeRiskCount = id;
        return id;
    }
    
    /// <summary>
    /// UpdateRisByID: Updates a risk using its ID no.
    /// </summary>
    public bool UpdateRiskByID(int id, float? probability = null, float? cost = null, string? status = null, string? description = null)
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
    /// RemoveRiskByID: Removes a ProjectRisk with the given ID.
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
    
    // Functional Requirements:
    
    /// <summary>
    /// GetFRequirements: Gets the list of functional requirements
    /// </summary>
    public List<ProjectRequirement> GetFRequirements()
    {
        return fRequirements;
    }
    
    /// <summary>
    /// AddFRequirement: Adds a functional requirement and returns its assigned ID
    /// </summary>
    public int AddFRequirement(string desc)
    {
        int id = ++lifetimeFRequirementCount;
        ProjectRequirement fReq = new ProjectRequirement(id, true, desc);
        fRequirements.Add(fReq);
        lifetimeFRequirementCount = id;
        return id;
    }
    
    /// <summary>
    /// UpdateFRequirementByID: Updates a functional requirement using its ID no.
    /// </summary>
    public bool UpdateFRequirementByID(int id, string description)
    {
        ProjectRequirement requirement = fRequirements.Find(req => req.GetID() == id);
        
        if (requirement == null)  return false;
        
        requirement.SetDescription(description);
        return true;
    }
    /// <summary>
    /// RemoveFRequirementByID: Removes functional requirement with given ID.
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
    
    // Non-Functional requirements:
    
    /// <summary>
    /// GetNFRequirements: Gets the list of non-functioal requirements
    /// </summary>
    public List<ProjectRequirement> GetNFRequirements()
    {
        return nfRequirements;
    }
    
    /// <summary>
    /// AddNFRequirement: Adds a non-functional requirement and returns its assigned ID no.
    /// </summary>
    public int AddNFRequirement(string desc)
    {
        int id = ++lifetimeNFrequirementCount;
        ProjectRequirement nfReq = new ProjectRequirement(id, false, desc);
        nfRequirements.Add(nfReq);
        lifetimeNFrequirementCount = id;
        return id;
    }
    
    /// <summary>
    /// UpdateNFRequirementByID: Updates a non-functional requirement using its ID no.
    /// </summary>
    public bool UpdateNFRequirementByID(int id, string description)
    {
        ProjectRequirement requirement = nfRequirements.Find(req => req.GetID() == id);
        
        if (requirement == null) return false;
        
        requirement.SetDescription(description);
        return true;
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

    // Project wide effort calculations here: 
    
    // save to/load from disk? maybe out of scope
}