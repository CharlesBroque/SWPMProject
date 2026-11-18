namespace Models;

/// <summary>
/// ProjectRisk Class: manages an individual project risk. 
/// </summary>
public class ProjectRisk
{
    private int idNumber; // id management to be handled by function in Project
    private string status; // statuses include... what do they include?
    private float probability;
    private float cost;
    private float riskValue;
    private string description;

    /// <summary>
    /// ProjectRisk Constructor: Initializes a new project risk.
    /// </summary>
    /// <param name="id">The assigned risk ID.</param>
    /// <param name="probability">The probability of the risk occuring.</param>
    /// <param name="cost">The estimated cost associated with the risk.</param>
    /// <param name="status">The current status of the risk.</param>
    /// <param name="description">A description of the risk.</param>
    public ProjectRisk(int id, float probability, float cost, string status, string description)
    {
        this.idNumber = id;
        this.probability = probability;
        this.cost = cost;
        this.status = status;
        this.description = description;
        
        UpdateRiskValue();
    }

    /// <summary>
    /// GetID: Gets the risk ID.
    /// </summary>
    public int GetID()
    {
        return idNumber;
    }
    
    /// <summary>
    /// GetStatus: Gets the current risk status.
    /// </summary>
    public string GetStatus()
    {
        return status;
    }
    
    /// <summary>
    /// SetStatus: Updates the risk status.
    /// </summary>
    /// <param name="status">The new risk status</param>
    /// <exception cref="ArgumentException">Thrown when the status is null or empty.</exception>
    public void SetStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            throw new ArgumentException("Status can't be null or empty.", nameof(status));
        }
        this.status = status;
    }
    
    /// <summary>
    /// GetProbability: Gets the risk probability.
    /// </summary>
    public float GetProbability()
    {
        return probability;
    }
    
    /// <summary>
    /// setProbability: Updates the risk probability and recalculates risk value.
    /// </summary>
    /// <param name="probability">The new risk probability</param>
    public void SetProbability(float probability)
    {
        this.probability = probability;
        UpdateRiskValue();
    }
    
    /// <summary>
    /// GetCost: Gets the estimated risk cost.
    /// </summary>
    public float GetCost()
    {
        return cost;
    }
    
    /// <summary>
    /// SetCost: Updates the risk cost and recalulates risk value.
    /// </summary>
    /// <param name="cost">The new estimated risk cost</param>
    public void SetCost(float cost)
    {
        this.cost = float.Round(cost, 2);
        UpdateRiskValue();
    }
    
    /// <summary>
    /// GetRiskValue: Gets the calculated risk value.
    /// </summary>
    public float GetRiskValue()
    {
        return riskValue;
    }
    
    /// <summary>
    /// UpdateRiskValue: Recalculates the risk value using probability and cost.
    /// Note: Risk value should never be set directly.
    /// </summary>
    private void UpdateRiskValue()
    {
        riskValue = float.Round(probability * cost);
    }
    
    /// <summary>
    /// GetDescription: Gets the risk description.
    /// </summary>
    public string GetDescription()
    {
        return description;
    }
    
    /// <summary>
    /// SetDescription: Updates the risk description.
    /// </summary>
    /// <param name="description">The new risk description</param>
    /// <exception cref="ArgumentException">Thrown when the description is null or empty.</exception>
    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description can't be empty.", nameof(description));
        }
        this.description = description;
    }

    /// <summary>
    /// ToString: Returns a formatted representation of the risk.
    /// </summary>
    public override string ToString()
    {
        return "R-" + idNumber.ToString() + " (" + status.ToString() + "): " + description;
    }
    public string ToStringVerbose()
    {
        return "R-" + idNumber.ToString() + " (" + status.ToString() + "): " + description + "\t"
        + " | Probability: " + probability
        + " | Cost: " + cost
        + " | Risk Value: " + riskValue;
    }
}