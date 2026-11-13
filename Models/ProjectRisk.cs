namespace DefaultNamespace;

public class ProjectRisk
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