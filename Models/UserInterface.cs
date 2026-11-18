using Models;

public class UserInterface
{
    static void Main(string[] args)
    {
        Project project = new Project();
        Console.WriteLine("Welcome to Group 6's Project Software Project Management Tool! Here you will be able to manage your projects information, requirements and efforts.");
        while (true)
        {
            Console.WriteLine("[Main Menu]");
            Console.WriteLine("-------------------------");
            Console.WriteLine("To get Started, please select an option from the menu below:");
            Console.WriteLine("1. Edit project information");
            Console.WriteLine("2. Edit project requirements");
            Console.WriteLine("3. View project effort and progress");
            Console.WriteLine("4. Quit");
            Console.WriteLine("-------------------------");
            int choice;
            int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice);
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
                    editProjectEffortAndProgress(project);
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
            Console.WriteLine("[Edit Project Information Menu]");
            Console.WriteLine("-------------------------");
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
                    if (newDescription.IsWhiteSpace())
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
                    Console.WriteLine("You selected option 3: Manage team members")
                    ManageTeamMembers(project);
                    break;

                case 4: // Manage project risks
                    Console.WriteLine("You selected option 4: Manage project risks")
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
            Console.WriteLine("5. Return to Edit Project Information Menu");
            Console.WriteLine("-------------------------");

            int subChoice = ReadInt("Select an option: ");

            switch (subChoice)
            {
                case 1: // Add owner
                    AddName("owner", project.AddOwner);
                    break;
                case 2: // Remove owner
                    RemoveName("owner", project.GetOwners(), project.RemoveOwner)
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
                    Console.WriteLine("Invalid choice. Please select a valid option.")
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
                    Addname("team member", project.AddMember);
                    break;
                case 2: // Remove team member
                    RemoveName("team member", project.GetMembers(), project.Removemember);
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
        Console.Write("Enter the name of the " + title + " to add: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be blank!");
            return;
        }
        add(name);
        Console.WriteLine(title + " added.");
    }

    private static void RemoveName(string title, List<string> names, Func<string, bool> remove)
    {
        if (names.Count == 0) // if empty
        {
            Console.WriteLine("There aren't any " + title + "'s to remove!");
            return;
        }
        
        Console.WriteLine("Enter the name of the " + title + " to remove: ");
        string name = Console.ReadLine() ?? "";

        if (remove(name))
        {
            Console.WriteLine(title + name + " removed.");
        }
        else
        {
            Console.WriteLine(title + name + " was not found.");
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
            Console.WriteLine("Risk R-" + id + "added.")
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine("Could not add risk: " + exception.Message);
        }
    }
    
    Project static void EditProjectRisk(Project project)
    {
        int id = ReadInt("Enter the ID of the risk to edit: ");
        ProjectRisk? risk = project.GetRiskByID(id);

        if (risk == null)
        {
            Console.WriteLine("No risk found with ID " + id + ".");
            return;
        }
        
        float probability = ReadFloat("Enter thr new probability (0 to 1): ");
        float cost = ReadFloat("Enter the new cost of the risk: ");
        Console.Write("Enter the new status: ");
        string status = Console.ReadLine() ?? "";
        Console.Write("Enter the new description: ");
        string description = Coonsole.ReadLine() ?? "";

        try
        {
            project.UpdateRiskByID(id, probability, cost, string.IsNullOrWhiteSpace(status) ? null : status,
                string.IsNullOrWhiteSpace(description) ? null : description);
            Console.WriteLine("Risk R-" + id + " updated.");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine("Could not update risk: " + exception.Message);
        }
    }

    private static void RemoveProjectRisk(Project project)
    {
        int id = ReadInt("Enter the ID number of the risk to remove: ");

        if (project.RemoveRiskByID(id))
        {
            Console.WriteLine("Risk R-" + id + " removed.");
        }
        else
        {
            Console.WriteLine("No risk found with ID " + id + ".");
        }
    }
    

    public static void EditProjectRequirements(Project project)
    {
        Console.WriteLine("-------------------------");
        PrintProjectInformation(project);
        Console.WriteLine("-------------------------");
        Console.WriteLine("1. Manage nonfunctional requirements");
        Console.WriteLine("2. Manage functional requirements");
        Console.WriteLine("3. Return to Main Menu");
        Console.WriteLine("-------------------------");
        int choice2;
        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice2);

        switch (choice2)
        {
            case 1:
                Console.WriteLine("You selected option 1: Manage nonfunctional requirements");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Add a nonfunctional requirement");
                Console.WriteLine("2. Update a nonfunctional requirement");
                Console.WriteLine("3. Remove a nonfunctional requirement");
                Console.WriteLine("-------------------------");
                int choice;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice);
                switch (choice)
    {
                    case 1:
                        Console.WriteLine("You selected option 1: Add a nonfunctional requirement.");
                        Console.Write("Enter a description for the new requirement: ");
                        string newRequirementDescription = Console.ReadLine() ?? "No description.";
                        if (newRequirementDescription.IsWhiteSpace()) newRequirementDescription = "No description."; // don't submit blank description
                        project.AddNFRequirement(newRequirementDescription);
                        break;

                    case 2:
                        Console.WriteLine("You selected option 2: Update a nonfunctional requirement");
                        
                        Console.Write("Enter the ID number of the requirement to update: ");
                        int my_id;
                        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out my_id);
                        
                        Console.Write("Enter a new description for the requirement: ");
                        string my_desc = Console.ReadLine() ?? "No description.";

                        if (project.UpdateNFRequirementByID(my_id, my_desc))
                        {
                            Console.WriteLine($"Successfully updated NFR-{my_id}.");
                        }
                        else
                        {
                            Console.WriteLine($"Could not update: No such requirement NFR-{my_id}.");
                        }
                        
                        break;

                    case 3:
                        Console.WriteLine("You selected option 3: Remove a nonfunctional requirement.");
                        Console.Write("Enter the ID number of the requirement to remove: ");
                        int temp_id;
                        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out temp_id);
                        if (project.RemoveNFRequirementByID(temp_id))
                        {
                            Console.WriteLine($"Successfully removed NFR-{temp_id}.");
                        }
                        else
                        {
                            Console.WriteLine($"Removal failed: no such requirement NFR-{temp_id}.");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
    }
                break;

            case 2:
                Console.WriteLine("You selected option 2: Manage functional requirements");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Add a functional requirement");
                Console.WriteLine("2. Update a functional requirement");
                Console.WriteLine("3. Remove a functional requirement");
                Console.WriteLine("-------------------------");
                int choice3;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice3);
                switch (choice3)
                {
                    case 1:
                        Console.WriteLine("You selected option 1: Add a functional requirement.");
                        Console.Write("Enter a description for the new requirement: ");
                        string newRequirementDescription = Console.ReadLine() ?? "No description.";
                        if (newRequirementDescription.IsWhiteSpace()) newRequirementDescription = "No description."; // don't submit blank description
                        project.AddFRequirement(newRequirementDescription);
                        break;
                    
                    case 2:
                        Console.WriteLine("You selected option 2: Update a functional requirement");
                        
                        Console.Write("Enter the ID number of the requirement to update: ");
                        int my_id;
                        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out my_id);
                        
                        Console.Write("Enter a new description for the requirement: ");
                        string my_desc = Console.ReadLine() ?? "No description.";
                        if (my_desc.IsWhiteSpace()) my_desc = "No description."; // don't submit blank description

                        if (project.UpdateFRequirementByID(my_id, my_desc))
                        {
                            Console.WriteLine($"Successfully updated FR-{my_id}.");
                        }
                        else
                        {
                            Console.WriteLine($"Could not update: No such requirement FR-{my_id}.");
                        }
                        
                        break;

                    case 3:
                        Console.WriteLine("You selected option 3: Remove a functional requirement.");
                        Console.Write("Enter the ID of the requirement to remove: ");
                        int temp_id;
                        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out temp_id);
                        if (project.RemoveFRequirementByID(temp_id))
                        {
                            Console.WriteLine($"Successfully removed FR-{temp_id}.");
                        }
                        else
                        {
                            Console.WriteLine($"Removal failed: no such requirement FR-{temp_id}.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;

                }
                break;

            case 3: // return to main menu
                return;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }

    }
    public static void editProjectEffortAndProgress(Project project)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("1. Log person-hours for a requirement");
        Console.WriteLine("2. View effort totals for each requirement");
        Console.WriteLine("3. View project-wide effort totals");
        Console.WriteLine("4. Return to Main Menu");
        Console.WriteLine("-------------------------");
        int choice;
        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice);

        switch (choice)
        {
            case 1:
                Console.WriteLine("You selected option 1: Log person-hours for a requirement");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Log person-hours for a functional requirement");
                Console.WriteLine("2. Log person-hours for a nonfunctional requirement");
                Console.WriteLine("-------------------------");
                int typeChoice;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out typeChoice);
                
                List<ProjectRequirement> requirements;
                if (typeChoice == 1)
                {
                    requirements = project.GetFRequirements();
                }
                else
                {
                    requirements = project.GetNFRequirements();
                }
                foreach (ProjectRequirement req in requirements)
                {
                    Console.WriteLine("  " + req.ToString());
                }

                Console.Write("Enter the ID number of the requirement: ");
                int id;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out id);
                ProjectRequirement? requirement = requirements.Find(r => r.GetID() == id);
                if (requirement == null)
                {
                    Console.WriteLine("Requirement not found.");
                    break;
                }

                Console.WriteLine("Enter the date of the work (yyyy-mm-dd). For a weekly entry, enter the first day of the week:");
                string date = Console.ReadLine() ?? DateTime.Today.ToShortDateString();
                if (date.IsWhiteSpace()) date = DateTime.Today.ToShortDateString(); // set default to today on blank
                
                // validate date here instead of at the end
                if (!DateOnly.TryParse(date, out _))
                {
                    Console.WriteLine("Invalid date. Effort entry was not added.");
                    break;
                }

                Console.WriteLine("Enter hours for Requirements Analysis:");
                int hoursReqsAnalysis;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out hoursReqsAnalysis);
                Console.WriteLine("Enter hours for Designing:");
                int hoursDesigning;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out hoursDesigning);
                Console.WriteLine("Enter hours for Coding:");
                int hoursCoding;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out hoursCoding);
                Console.WriteLine("Enter hours for Testing:");
                int hoursTesting;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out hoursTesting);
                Console.WriteLine("Enter hours for Project Management:");
                int hoursProjManagement;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out hoursProjManagement);
                Console.WriteLine("Enter hours for effort not belonging to any category:");
                int hoursUncategorized;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out hoursUncategorized);

                requirement.AddEffortEntry(hoursReqsAnalysis, hoursDesigning, hoursCoding, hoursTesting, hoursProjManagement, hoursUncategorized);

                // AddEffortEntry uses today's date, so set the date on the newest entry
                EffortEntry[] entries = requirement.GetEffortEntries();
                bool did_it_work = entries[entries.Length - 1].SetDatestamp(date);
                if (did_it_work)
                {
                    Console.WriteLine("Effort entry added.");
                }
                else
                {
                    requirement.RemoveNewestEntry();
                    Console.WriteLine("Invalid date. Effort entry was not added.");
                }
                break;

            case 2:
                Console.WriteLine("You selected option 2: View effort totals for each requirement");
                Console.WriteLine("Functional requirements:");
                if (project.GetFRequirements().Count == 0) Console.WriteLine("  (none)");
                foreach (ProjectRequirement fReq in project.GetFRequirements())
                {
                    Console.WriteLine("  " + fReq.ToString());
                    foreach (KeyValuePair<string, int> kv in fReq.totalEffort())
                    {
                        Console.WriteLine("    " + kv.Key + ": " + kv.Value);
                    }
                }
                Console.WriteLine("Nonfunctional requirements:");
                if (project.GetNFRequirements().Count == 0) Console.WriteLine("  (none)");
                foreach (ProjectRequirement nfReq in project.GetNFRequirements())
                {
                    Console.WriteLine("  " + nfReq.ToString());
                    foreach (KeyValuePair<string, int> kv in nfReq.totalEffort())
                    {
                        Console.WriteLine("    " + kv.Key + ": " + kv.Value);
                    }
                }
                break;

            case 3:
                Console.WriteLine("You selected option 3: View project-wide effort totals");
                Dictionary<string, int> projectTotals = new EffortEntry().GetEffortHours();
                foreach (ProjectRequirement fReq in project.GetFRequirements())
                {
                    foreach (KeyValuePair<string, int> kv in fReq.totalEffort())
                    {
                        projectTotals[kv.Key] += kv.Value;
                    }
                }
                foreach (ProjectRequirement nfReq in project.GetNFRequirements())
                {
                    foreach (KeyValuePair<string, int> kv in nfReq.totalEffort())
                    {
                        projectTotals[kv.Key] += kv.Value;
                    }
                }
                int total = 0;
                foreach (KeyValuePair<string, int> kv in projectTotals)
                {
                    Console.WriteLine("  " + kv.Key + ": " + kv.Value);
                    total += kv.Value;
                }
                Console.WriteLine("  Total: " + total);
                break;

            case 4:
                return;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }
    
    
    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out int num))
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
