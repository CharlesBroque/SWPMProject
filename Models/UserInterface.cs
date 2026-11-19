using Models;

public class UserInterface
{
    static void Main(string[] args)
    {
        Project project = new Project();
        Console.WriteLine("Welcome to Group 6's Project Software Project Management Tool! Here you will be able to manage your projects information, requirements and efforts.");
        while (true)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("[Main Menu]");
            Console.WriteLine("To get Started, please select an option from the menu below:");
            Console.WriteLine("1. Edit project information");
            Console.WriteLine("2. Edit project requirements");
            Console.WriteLine("3. Manage project effort and progress");
            Console.WriteLine("4. Quit");
            Console.WriteLine("-------------------------");
            
            int choice = ReadInt("Select an option: ");
            
            switch (choice)
            {
                case 1:
                    Console.WriteLine("You selected option 1: Edit project information");
                    EditProjectInformation(project);
                    break;
                case 2:
                    Console.WriteLine("You selected option 2: Edit project requirements");
                    EditProjectRequirements(project);
                    break;
                case 3:
                    Console.WriteLine("You selected option 3: View project effort and progress");
                    EditProjectEffortAndProgress(project);
                    break;
                case 4:
                    Console.WriteLine("You selected option 4: Quit");
                    // Exit the program
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
        
    }

    public static void EditProjectInformation(Project project)
    {
        while (true)
        {
            PrintProjectInformation(project);
            Console.WriteLine("-------------------------");
            Console.WriteLine("[Edit Project Information Menu]");
            Console.WriteLine("1. Edit project description");
            Console.WriteLine("2. Manage project owners and managers");
            Console.WriteLine("3. Manage team members");
            Console.WriteLine("4. Manage project risks");
            Console.WriteLine("5. Return to Main Menu");
            Console.WriteLine("-------------------------");

            int choice = ReadInt("Select an option: "); // Use helper

            switch (choice)
            {
                case 1: // Edit project description
                    Console.WriteLine("You selected option 1: Edit project description.");
                    Console.Write("Enter a new description for the project: ");
                    string newDescription = Console.ReadLine() ?? project.GetDescription();
                    if (string.IsNullOrWhiteSpace(newDescription))
                    {
                        Console.WriteLine("Description cannot be blank!");
                        break; // preserve old description on empty input
                    }
                    project.SetDescription(newDescription);
                    Console.WriteLine("Description updated.");
                    break;

                case 2: // Manage project owners and managers
                    Console.WriteLine("You selected option 2: Manage project owners and managers.");
                    ManageOwnersAndManagers(project);
                    break;

                case 3: //Manage team members
                    Console.WriteLine("You selected option 3: Manage team members");
                    ManageTeamMembers(project);
                    break;

                case 4: // Manage project risks
                    Console.WriteLine("You selected option 4: Manage project risks");
                    ManageProjectRisks(project);
                    break;

                case 5: // return to main menu
                    Console.WriteLine("Returning to Main Menu...");
                    return;

                default: // Invalid option
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void ManageOwnersAndManagers(Project project)
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            PrintNames("Current owners", project.GetOwners());
            PrintNames("Current managers", project.GetManagers());
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Add owner");
            Console.WriteLine("2. Remove owner");
            Console.WriteLine("3. Add manager");
            Console.WriteLine("4. Remove manager");
            Console.WriteLine("5. Return to Project Information Menu");
            Console.WriteLine("-------------------------");

            int subChoice = ReadInt("Select an option: ");

            switch (subChoice)
            {
                case 1: // Add owner
                    AddName("owner", project.AddOwner);
                    break;
                case 2: // Remove owner
                    RemoveName("owner", project.GetOwners(), project.RemoveOwner);
                    break;
                case 3: // Add manager
                    AddName("manager", project.AddManager);
                    break;
                case 4: // Remove manager
                    RemoveName("manager", project.GetManagers(), project.RemoveManager);
                    break;
                case 5: // Return to previous menu
                    Console.WriteLine("Returning to previous menu...");
                    return;
                default: // Invalid input
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void ManageTeamMembers(Project project)
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            PrintNames("Current team members", project.GetMembers());
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Add team member");
            Console.WriteLine("2. Remove team member");
            Console.WriteLine("3. Return to Edit Project Information Menu");
            Console.WriteLine("-------------------------");

            int subChoice = ReadInt("Select an option: ");

            switch (subChoice)
            {
                case 1: // Add team member
                    AddName("team member", project.AddMember);
                    break;
                case 2: // Remove team member
                    RemoveName("team member", project.GetMembers(), project.RemoveMember);
                    break;
                case 3: // Return to previous mennu
                    Console.WriteLine("Returning to previous menu...");
                    return;
                default: // Invalid input
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void AddName(string title, Action<string> add)
    {
        Console.Write($"Enter the name of the {title} to add: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be blank!");
            return;
        }
        add(name);
        Console.WriteLine($"{title} added.");
    }

    private static void RemoveName(string title, List<string> names, Func<string, bool> remove)
    {
        if (names.Count == 0) // if empty
        {
            Console.WriteLine($"There aren't any {title}'s to remove!");
            return;
        }
        
        Console.WriteLine($"Enter the name of the {title} to remove: ");
        string name = Console.ReadLine() ?? "";

        if (remove(name))
        {
            Console.WriteLine($"{title} {name} removed.");
        }
        else
        {
            Console.WriteLine($"{title} {name} was not found.");
        }
    }

    private static void ManageProjectRisks(Project project)
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Current project risks: ");
            List<ProjectRisk> risks = project.GetRisks();

            if (risks.Count == 0)
            {
                Console.WriteLine("(none)");
            }

            foreach (ProjectRisk risk in risks)
            {
                Console.WriteLine(risk.ToStringVerbose());
            }
            
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Add risk");
            Console.WriteLine("2. Edit risk");
            Console.WriteLine("3. Remove risk");
            Console.WriteLine("4. Return to Edit Project Information Menu");
            Console.WriteLine("-------------------------");

            int subChoice = ReadInt("Select an option: ");

            switch (subChoice)
            {
                case 1: // Add risk
                    AddProjectRisk(project);
                    break;
                case 2: // Edit risk
                    EditProjectRisk(project);
                    break;
                case 3: // Remove risk
                    RemoveProjectRisk(project);
                    break;
                case 4: // Return to previous menu
                    Console.WriteLine("Returning to previous menu...");
                    return;
                default: // Invalid input
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void AddProjectRisk(Project project)
    { 
        float probability = ReadFloat("Enter the probability of the risk (0 to 1): ");
        float cost = ReadFloat("Enter the cost of the risk: ");

        Console.Write("Enter the status of the risk: ");
        string status = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(status))
        {
            status = "TBD";
        }
        
        Console.Write("Enter the description of the risk: ");
        string description = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(description))
        {
            description = "No description.";
        }

        try
        {
            int id = project.AddRisk(probability, cost, status, description);
            Console.WriteLine($"Risk R-{id} added.");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Could not add risk: {exception.Message}");
        }
    }
    
    private static void EditProjectRisk(Project project)
    {
        int id = ReadInt("Enter the ID of the risk to edit: ");
        ProjectRisk? risk = project.GetRiskByID(id);

        if (risk == null)
        {
            Console.WriteLine($"No risk found with ID R-{id}.");
            return;
        }

        Console.WriteLine("Leave blank to keep its current value.");
        
        float probability = ReadFloat("Enter the new probability (0 to 1): ");
        float cost = ReadFloat("Enter the new cost of the risk: ");
        Console.Write("Enter the new status: ");
        string status = Console.ReadLine() ?? "";
        Console.Write("Enter the new description: ");
        string description = Console.ReadLine() ?? "";

        try
        {
            project.UpdateRiskByID(id, probability, cost, string.IsNullOrWhiteSpace(status) ? null : status,
                string.IsNullOrWhiteSpace(description) ? null : description);
            Console.WriteLine($"Risk R-{id} updated.");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Could not update risk: {exception.Message}");
        }
    }

    private static void RemoveProjectRisk(Project project)
    {
        int id = ReadInt("Enter the ID number of the risk to remove: ");

        if (project.RemoveRiskByID(id))
        {
            Console.WriteLine($"Risk R-{id} removed.");
        }
        else
        {
            Console.WriteLine($"No risk found with ID {id}.");
        }
    } 
    

    public static void EditProjectRequirements(Project project)
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("[Project Requirements Menu]");
            Console.WriteLine("1. Manage nonfunctional requirements");
            Console.WriteLine("2. Manage functional requirements");
            Console.WriteLine("3. Return to Main Menu");
            Console.WriteLine("-------------------------");

            int choice = ReadInt("Select an option: ");

            switch (choice)
            {
                case 1: // Manage nonfunctional requirements
                    ManageRequirements(project.GetNFRequirements(), project.AddNFRequirement,
                        project.UpdateNFRequirementByID, project.RemoveNFRequirementByID, "non-functional", "NFR");
                    break;
                case 2: // Manage functional requirements
                    ManageRequirements(project.GetFRequirements(), project.AddFRequirement, project.UpdateFRequirementByID,
                        project.RemoveFRequirementByID, "functional", "FR");
                    break;
                case 3: // Return to Main Menu
                    Console.WriteLine("Returning to Main Menu...");
                    return;
                default: // Invalid option
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void ManageRequirements(List<ProjectRequirement> requirements, Func<string, int> addReq,
        Func<int, string, bool> updateReq, Func<int, bool> removeReq, string type, string prefix)
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Current {type} requirements: ");
            if (requirements.Count == 0)
            {
                Console.WriteLine("(none)");
            }
            foreach (ProjectRequirement requirement in requirements)
            {
                Console.WriteLine(requirement);
            }
            
            Console.WriteLine("-------------------------");
            Console.WriteLine($"1. Add {type} requirement");
            Console.WriteLine($"2. Update {type} requirement");
            Console.WriteLine($"3. Remove {type} requirement");
            Console.WriteLine("4. Return to Project Requirements Menu");
            Console.WriteLine("-------------------------");
            
            int subChoice = ReadInt("Select an option: ");
            switch (subChoice)
            {
                case 1: // Add req
                    Console.Write("Enter a description for the new requirement: ");
                    string newDescription = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(newDescription))
                    {
                        Console.WriteLine("Description cannot be blank.");
                        break;
                    }
                    
                    int newID = addReq(newDescription);
                    Console.WriteLine($"{prefix}-{newID} added.");
                    break;
                case 2: // Update req
                    int updateID = ReadInt("Enter the ID number of the requirement to update: ");
                    Console.Write("Enter the new requirement description: ");
                    string updatedDescription = Console.ReadLine() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(updatedDescription))
                    {
                        Console.WriteLine("Description cannot be blank.");
                        break;
                    }

                    if (updateReq(updateID, updatedDescription))
                    {
                        Console.WriteLine($"{prefix}-{updateID} updated.");
                    }
                    else
                    {
                        Console.WriteLine($"Could not update: No such requirement {prefix}-{updateID}.");
                    }
                    break;
                case 3: // Remove req
                    int removeID = ReadInt("Enter the ID number of the requirement to remove: ");

                    if (removeReq(removeID))
                    {
                        Console.WriteLine($"{prefix}-{removeID} removed.");
                    }
                    else
                    {
                        Console.WriteLine($"Could not remove: No such requirement {prefix}-{removeID}.");
                    }
                    break;
                case 4: // Return to Project Req menu
                    Console.WriteLine("Returning to Project Requirements Menu");
                    return;
                default: // Invalid option
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
    
    
    
    public static void EditProjectEffortAndProgress(Project project)
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("[Project Effort Tracking and Progress]");
            Console.WriteLine("1. Log actual person-hours for a requirement");
            Console.WriteLine("2. Enter estimated person-hours for a requirement");
            Console.WriteLine("3. View actual effort totals for each requirement");
            Console.WriteLine("4. View project-wide actual effort totals");
            Console.WriteLine("5. Compare actual and estimated effort for a requirement");
            Console.WriteLine("6. Compare project-wide actual and estimated effort");
            Console.WriteLine("7. Return to Main Menu");
            Console.WriteLine("-------------------------");

            int choice = ReadInt("Select an option: ");
            switch (choice)
            {
                case 1: // Log actual person-hours for a requirement
                    LogRequirementEffort(project);
                    break;
                case 2: // Enter estimated person-hours for a requirement
                    EnterRequirementEstimate(project);
                    break;
                case 3: // View actual effort totals for each requirement
                    ViewRequirementEffort(project);
                    break;
                case 4: // View project-wide actual effort totals
                    ViewProjectEffort(project);
                    break;
                case 5: // Compare actual and estimated effort for a requirement
                    CompareRequirementEffort(project);
                    break;
                case 6: // Compare project-wide actual and estimated effort
                    CompareProjectEffort(project);
                    break;
                case 7: // Return to Main Menu
                    Console.WriteLine("Returning to Main Menu...");
                    return;
                default: // Invalid option
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void LogRequirementEffort(Project project)
    {
        ProjectRequirement? requirement = SelectRequirement(project);
        if (requirement == null) return;

        Console.WriteLine("Enter the date the work was done (yyyy-mm-dd).");
        Console.Write("For a weekly entry, enter the first day of the week: ");
        string date = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(date))
        {
            date = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
        }

        if (!DateOnly.TryParse(date, out _)) // if invalid date/cant parse
        {
            Console.WriteLine("Invalid date. Effort entry was not added.");
            return;
        }

        Console.WriteLine("Enter hours for each activity category.");
        int hoursReqsAnalysis = ReadHourEntry("Requirements Analysis: ");
        int hoursDesigning = ReadHourEntry("Designing: ");
        int hoursCoding = ReadHourEntry("Coding: ");
        int hoursTesting = ReadHourEntry("Testing: ");
        int hoursProjMan = ReadHourEntry("Project Management: ");
        int hoursUncategorized = ReadHourEntry("Uncategorized: ");
        
        requirement.AddEffortEntry(hoursReqsAnalysis, hoursDesigning, hoursCoding, hoursTesting, hoursProjMan, hoursUncategorized);
        EffortEntry[] entries = requirement.GetEffortEntries();

        bool dateSet = entries[entries.Length - 1].SetDatestamp(date);

        if (dateSet)
        {
            Console.WriteLine("Effort entry has been added.");
        }
        else
        {
            requirement.RemoveNewestEntry();
            Console.WriteLine("Invalid date. Effort entry was not added.");
        }
    }

    private static void EnterRequirementEstimate(Project project)
    {
        ProjectRequirement? requirement = SelectRequirement(project);
        
        if (requirement == null) return;
        
        Console.WriteLine("-------------------------");
        Console.WriteLine("Enter estimated person-hours for each category.");
        Console.WriteLine("Entering a new value will replace the previous estimate.");
        Console.WriteLine("-------------------------");

        Dictionary<string, int> currentEstimates = requirement.GetEstimatedEffortHours();
        foreach (KeyValuePair<string, int> estimate in currentEstimates)
        {
            Console.WriteLine($"{estimate.Key}: {estimate.Value} hours");
        }
        Console.WriteLine("-------------------------");

        foreach (string category in currentEstimates.Keys)
        {
            int hours = ReadHourEntry($"Enter estimated hours for {category}: ");
            requirement.SetEstimatedEffortByCategory(category, hours);
        }
        Console.WriteLine("Estimated effort updated successfully.");
    }

    private static void ViewRequirementEffort(Project project)
    {
        Console.WriteLine("Functional requirements: ");
        if (project.GetFRequirements().Count == 0)
        {
            Console.WriteLine("(none)");
        }
        foreach (ProjectRequirement requirement in project.GetFRequirements())
        {
            PrintRequirementActualEffort(requirement);
        }
        
        Console.WriteLine("Non-functional requirements: ");
        if (project.GetNFRequirements().Count == 0)
        {
            Console.WriteLine("(none)");
        }

        foreach (ProjectRequirement requirement in project.GetNFRequirements())
        {
            PrintRequirementActualEffort(requirement);
        }
    }

    private static void PrintRequirementActualEffort(ProjectRequirement requirement)
    {
        Console.WriteLine(requirement);

        Dictionary<string, int> actual = requirement.totalEffort();
        int total = 0;
        foreach (KeyValuePair<string, int> entry in actual)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} hours");
            total += entry.Value;
        }

        Console.WriteLine($"Total: {total} hours");
    }

    private static void ViewProjectEffort(Project project)
    {
        Dictionary<string, int> projectTotals = project.GetProjectEffortTotals();
        int total = 0;
        Console.WriteLine("Project-wide actual effort: ");
        foreach (KeyValuePair<string, int> entry in projectTotals)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}  hours");
            total += entry.Value;
        }

        Console.WriteLine($"Total: {total} hours");
    }

    private static void CompareRequirementEffort(Project project)
    {
        ProjectRequirement? requirement = SelectRequirement(project);
        if (requirement == null) return;

        Dictionary<string, int> actual = requirement.totalEffort();
        Dictionary<string, int> estimated = requirement.GetEstimatedEffortHours();
        Dictionary<string, int> difference = requirement.GetEffortDifference();
        
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Effort comparison for {requirement}");
        Console.WriteLine("Difference = actual effort - estimated effort");
        Console.WriteLine("-------------------------");
        
        int actualTotal = 0;
        int estimatedTotal = 0;
        int differenceTotal = 0;

        foreach (KeyValuePair<string, int> estimate in estimated)
        {
            string category = estimate.Key;
            int estimatedHours = estimate.Value;
            int actualHours = actual[category];
            int differenceHours = difference[category];

            Console.WriteLine($"{category}: ");
            Console.WriteLine($"     Estimated: {estimatedHours} hours");
            Console.WriteLine($"     Actual: {actualHours} hours");
            Console.WriteLine($"     Difference: {differenceHours:+#;-#;0} hours"); // specify positive/negative

            if (differenceHours > 0)
            {
                Console.WriteLine("     Status: Over the estimated amount of time.");
            }
            else if (differenceHours < 0)
            {
                Console.WriteLine("     Status: Under the estimated amount of time.");
            }
            else
            {
                Console.WriteLine("     Status: On the estimated amount of time.");
            }
            actualTotal += actualHours;
            estimatedTotal += estimatedHours;
            differenceTotal += differenceHours;
        }
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Estimated total: {estimatedTotal} hours");
        Console.WriteLine($"Actual total: {estimatedTotal} hours");
        Console.WriteLine($"Difference: {differenceTotal:+#;-#;0} hours");
    }

    private static void CompareProjectEffort(Project project)
    {
        Dictionary<string, int> actual = project.GetProjectEffortTotals();
        Dictionary<string, int> estimated = project.GetProjectEstimatedEffortTotals();
        Dictionary<string, int> difference = project.GetProjectEffortDifference();
        
        Console.WriteLine("-------------------------");
        Console.WriteLine("Project-wide effort comparison");
        Console.WriteLine("Difference = actual effort - estimated effort");
        Console.WriteLine("-------------------------");

        int actualTotal = 0;
        int estimatedTotal = 0;
        int differenceTotal = 0;

        foreach (KeyValuePair<string, int> estimate in estimated)
        {
            string category = estimate.Key;

            int actualHours = actual[category];
            int estimatedHours = estimate.Value;
            int differenceHours = difference[category];

            Console.WriteLine($"{category}: ");
            Console.WriteLine($"     Estimated: {estimatedHours} hours");
            Console.WriteLine($"     Actual: {actualHours} hours");
            Console.WriteLine($"     Difference: {differenceHours:+#;-#;0} hours");
            
            actualTotal += actualHours;
            estimatedTotal += estimatedHours;
            differenceTotal += differenceHours;
        }
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Estimated total: {estimatedTotal} hours");
        Console.WriteLine($"Actual total: {actualTotal} hours");
        Console.WriteLine($"Difference: {differenceTotal:+#;-#;0} hours");
    }
    
    private static ProjectRequirement? SelectRequirement(Project project)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("1. Functional requirement");
        Console.WriteLine("2. Non-functional requirement");
        Console.WriteLine("-------------------------");
        
        int typeChoice = ReadInt("Select the requirement type: ");
        List<ProjectRequirement> requirements;

        switch (typeChoice)
        {
            case 1: // Functional req
                requirements = project.GetFRequirements();
                break;
            case 2: // Non-functional req
                requirements = project.GetNFRequirements();
                break;
            default: // Invalid Input
                Console.WriteLine("Invalid requirement type.");
                return null;
        }

        if (requirements.Count == 0) // if empty
        {
            Console.WriteLine("There are no requirements of this type.");
            return null;
        }
        
        Console.WriteLine("Available requirements: ");
        foreach (ProjectRequirement requirement in requirements)
        {
            Console.WriteLine(requirement);
        }
        int id = ReadInt("Enter the requirement ID: ");
        ProjectRequirement? selectedRequirement = requirements.Find(requirement => requirement.GetID() == id);
        
        if (selectedRequirement == null)
        {
            Console.WriteLine("Requirement not found.");
        }

        return selectedRequirement;
    }
    
    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Integer, null, out int num))
            {
                return num;
            }
            Console.WriteLine("Please enter a valid whole number!");
        }
    }

    private static float ReadFloat(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (float.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out float num) && float.IsFinite(num))
            {
                return num;
            }
            Console.WriteLine("Please enter a valid finite number!");
        }
    }

    private static int ReadHourEntry(string message)
    {
        while (true)
        {
            int hour = ReadInt(message);
            if (hour >= 0) return hour;
            
            Console.WriteLine("Hours cannot be negative!");
        }
    }
    
    private static void PrintNames(string label, List<string> names)
    {
        if (names.Count == 0)
        {
            Console.WriteLine(label + ": (none)");
            return;
        }
        // else
        Console.WriteLine(label + ": " + string.Join(", ", names));
    }
    public static void PrintProjectInformation(Project project)
    {

        Console.WriteLine("-------------------------");
        Console.WriteLine("Current project information:");
        Console.WriteLine("Description: " + project.GetDescription());
        PrintNames("Owners", project.GetOwners());
        PrintNames("Managers", project.GetManagers());
        PrintNames("Members", project.GetMembers());

        Console.WriteLine("Risks:");
        List<ProjectRisk> risks = project.GetRisks();
        if (risks.Count == 0) Console.WriteLine("  (none)");
        foreach (ProjectRisk risk in risks)
        {
            Console.WriteLine(risk.ToStringVerbose());
        }

        Console.WriteLine("Functional requirements:");
        List<ProjectRequirement> fRequirements = project.GetFRequirements();
        if (fRequirements.Count == 0) Console.WriteLine("  (none)");
        foreach (ProjectRequirement fReq in fRequirements)
        {
            Console.WriteLine(fReq.ToString());
        }

        Console.WriteLine("Nonfunctional requirements:");
        List<ProjectRequirement> nfRequirements = project.GetNFRequirements();
        if (nfRequirements.Count == 0) Console.WriteLine("  (none)");
        foreach (ProjectRequirement nfReq in nfRequirements)
        {
            Console.WriteLine(nfReq.ToString());
        }
    }
}
