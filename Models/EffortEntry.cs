namespace Models;

/// <summary>
/// EffortEntry Class: manages the hours recorded for a project requirement across diff. work categories
/// and an associated date.
/// </summary>
public class EffortEntry
{
    private Dictionary<string, int> effortHours;
    private DateOnly datestamp;

    /// <summary>
    /// EffortEntry Constructor: Initializes a new effort entry with zero hours in each category
    /// and the current date.
    /// </summary>
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

    /// <summary>
    /// GetEffortHours: Gets the hours recorded for all categories.
    /// </summary>
    /// <returns>A dictionary containing the hours for each category.</returns>
    public Dictionary<string, int> GetEffortHours()
    {
        return effortHours;
    }
    
    /// <summary>
    /// GetEffortByCategory: Gets the recorded hours for a specific category.
    /// </summary>
    /// <param name="category_name">Effort category name.</param>
    /// <returns>The number of hours recorded for that category.</returns>
    /// <exception cref="ArgumentException">Thrown if category name is invalid.</exception>
    public int GetEffortByCategory(string category_name)
    {
        if (effortHours.ContainsKey(category_name))
        {
            return effortHours[category_name];
        }
        // else
        throw new ArgumentException("Category_name must be one of: Reqs Analysis, Designing, Coding, Testing, Proj Management, Uncategorized.", paramName: category_name);
    }
    
    /// <summary>
    /// SetEffortByCategory: Sets recorded effort hours by the name of the category.
    /// Throws ArgumentException if the category name is not Reqs Analysis, Designing, Coding, Testing, Proj Management, or Uncategorized.
    /// </summary>
    /// <param name="category_name">The name of the effort category.</param>
    /// <param name="hours">The number of hours to record.</param>
    /// <exception cref="ArgumentException">Thrown if category name is invalid or hours are negative.</exception>
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
    
    // method to update an existing effort entry?
    // UpdateEffort()
    
    /// <summary>
    /// GetDatestamp: Gets the date associated with the effort entry.
    /// </summary>
    public DateOnly GetDatestamp()
    {
        return datestamp;
    }
    
    /// <summary>
    /// SetDatestamp: Sets the datestamp.
    /// Internal use of DateOnly.TryParse means recognition is fuzzy--a variety of formats should work.
    /// </summary>
    /// <param name="date_string">String indicating the date.</param>
    /// <returns>True if the datestamp was set successfully parsed and updated, otherwise false..</returns>
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