namespace DefaultNamespace;

public class EffortEntry
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
    
    // method to update an existing effort entry?
    // UpdateEffort()
    
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