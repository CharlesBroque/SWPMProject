
using Models;

public class UserInterface
{
    static void Main(string[] args)
    {
        Project project = new Project();
        Console.WriteLine("Welcome to Group 6's Project Software Project Management Tool! Here you will be able to manage your projects information, requirements and efforts.");
        while (true)
        {
            DisplayMenu();
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

    public static void DisplayMenu()
    {

        Console.WriteLine("-------------------------");
        Console.WriteLine("To get Started, please select an option from the menu below:");
        Console.WriteLine("1. Edit a project information");
        Console.WriteLine("2. Edit project requirements");
        Console.WriteLine("3. View project effort and progress");
        Console.WriteLine("4. Quit");
        Console.WriteLine("-------------------------");
    }

    public static void EditProjectInformation(Project project)
    {
        PrintProjectInformation(project);
        Console.WriteLine("-------------------------");
        Console.WriteLine("Please select an option to edit project information:");
        Console.WriteLine("1. Description");
        Console.WriteLine("2. Project Owner and Manager's name");
        Console.WriteLine("3. Team Members");
        Console.WriteLine("4. Project Risks");
        Console.WriteLine("-------------------------");
        int choice;
        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice);

        switch (choice)
        {
            case 1:
                Console.WriteLine("You selected option 1: Edit project description, Please enter the new description:");
                string newDescription = Console.ReadLine() ?? "";
                
                project.SetDescription(newDescription);
                break;
            case 2:
                Console.WriteLine("You selected option 2: Edit project owner and manager's name");
                PrintNames("Current owners", project.GetOwners());
                PrintNames("Current managers", project.GetManagers());
                Console.WriteLine("1. Add owner");
                Console.WriteLine("2. Remove owner");
                Console.WriteLine("3. Add manager");
                Console.WriteLine("4. Remove manager");
                int subChoice;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out subChoice);

                switch (subChoice)
                {
                    case 1:
                        Console.WriteLine("Enter the name of the owner to add:");
                        string ownerToAdd = Console.ReadLine() ?? "No Name";
                        project.AddOwner(ownerToAdd);
                        break;
                    case 2:
                        Console.WriteLine("Enter the name of the owner to remove:");
                        string ownerToRemove = Console.ReadLine() ?? "No Name";
                        if (project.RemoveOwner(ownerToRemove))
                        {
                            Console.WriteLine($"Owner '{ownerToRemove}' removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Owner '{ownerToRemove}' not found.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter the name of the manager to add:");
                        string managerToAdd = Console.ReadLine() ?? "No Name";
                        project.AddManager(managerToAdd);
                        break;
                    case 4:
                        Console.WriteLine("Enter the name of the manager to remove:");
                        string managerToRemove = Console.ReadLine() ?? "No Name";
                        if (project.RemoveManager(managerToRemove))
                        {
                            Console.WriteLine($"Manager '{managerToRemove}' removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Manager '{managerToRemove}' not found.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
                // Call the method to edit project owner and manager's name here
                break;
            case 3:
                Console.WriteLine("You selected option 3: Edit team members");
                PrintNames("Current team members", project.GetMembers());
                Console.WriteLine("1. Add team member");
                Console.WriteLine("2. Remove team member");         
                int subChoice2;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out subChoice2);

                switch (subChoice2)
                {
                    case 1:
                        Console.WriteLine("Enter the name of the team member to add:");
                        string memberToAdd = Console.ReadLine() ?? "No Name";
                        project.AddMember(memberToAdd);
                        break;
                    case 2:
                        Console.WriteLine("Enter the name of the team member to remove:");
                        string memberToRemove = Console.ReadLine() ?? "No Name";
                        if (project.RemoveMember(memberToRemove))
                        {
                            Console.WriteLine($"Team member '{memberToRemove}' removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Team member '{memberToRemove}' not found.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
                break;
            case 4:
                Console.WriteLine("You selected option 4: Edit project risks");
                List<ProjectRisk> risks = project.GetRisks();
                foreach (ProjectRisk risk in risks)
                {
                    Console.WriteLine($"ID: {risk.GetID()}, Probability: {risk.GetProbability()}, Cost: {risk.GetCost()}, Status: {risk.GetStatus()}, Description: {risk.GetDescription()}");
                }
                Console.WriteLine("1. Add risk");
                Console.WriteLine("2. Edit risk");
                Console.WriteLine("3. Remove risk");
                int subChoice3;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out subChoice3);

                int id;
                switch (subChoice3)
                {
                    case 1:
                        // add risk
                        Console.WriteLine("Enter the probability of the risk (0 to 1):");
                        float probability;
                        float.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, null, out probability);
                        Console.WriteLine("Enter the cost of the risk:");
                        float cost;
                        float.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, null, out cost);
                        Console.WriteLine("Enter the status of the risk:");
                        string status = Console.ReadLine() ?? "TBD";
                        Console.WriteLine("Enter the description of the risk:");
                        string description = Console.ReadLine() ?? "No description.";
                        project.AddRisk(probability, cost, status, description);
                        break;
                    case 2:
                        // edit risk
                        Console.WriteLine("Enter the ID of the risk to edit:");
                        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out id);

                        ProjectRisk? risk = project.GetRiskByID(id);
                        if (risk == null)
                        {
                            Console.WriteLine($"No such risk with ID {id}.");
                            break;
                        }

                        Console.WriteLine("Enter the new probability of the risk (0 to 1):");
                        float.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, null, out probability);
                        Console.WriteLine("Enter the new cost of the risk:");
                        float.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, null, out cost);
                        Console.WriteLine("Enter the new status of the risk:");
                        status = Console.ReadLine() ?? "TBD";
                        Console.WriteLine("Enter the new description of the risk:");
                        description = Console.ReadLine() ?? "No description.";
                        project.UpdateRiskByID(id, probability, cost, status, description);
                        break;
                    case 3:
                        // remove risk
                        Console.WriteLine("Enter the ID of the risk to remove:");
                        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out id);
                        if (project.RemoveRiskByID(id))
                        {
                            Console.WriteLine($"Risk R-{id} removed.");
                        }
                        else
                        {
                            Console.WriteLine($"No such risk with ID {id}.");
                        }
                        break;
                }
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }

    public static void EditProjectRequirements(Project project)
    {
        Console.WriteLine("-------------------------");
        PrintProjectInformation(project);
        Console.WriteLine("Please select an option from the menu below:");
        Console.WriteLine("1. NonFunctional requirements");
        Console.WriteLine("2. Functional requirements");
        int choice2;
        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice2);

        switch (choice2)
        {
            case 1:
                Console.WriteLine("You selected option 1: Edit nonfunctional requirements");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Please select an option from the menu below:");
                Console.WriteLine("1. Add a project requirement");
                Console.WriteLine("2. Update a project requirement");
                Console.WriteLine("3. Remove a project requirement");
                Console.WriteLine("-------------------------");
                int choice;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice);
                switch (choice)
    {
                    case 1:
                        Console.WriteLine("You selected option 1: Add a project requirement:");
                        string newRequirementDescription = Console.ReadLine() ?? "No description.";
                        project.AddNFRequirement(newRequirementDescription);
                        break;
                    case 2:
                        Console.WriteLine("You selected option 2: Update a project requirement");
                        // Call the method to update a project requirement here
                        break;
                    case 3:
                        Console.WriteLine("You selected option 3: Please enter the ID of the requirement to remove:");
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
                Console.WriteLine("You selected option 2: Edit functional requirements");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Please select an option from the menu below:");
                Console.WriteLine("1. Add a project requirement");
                Console.WriteLine("2. Update a project requirement");
                Console.WriteLine("3. Remove a project requirement");
                Console.WriteLine("-------------------------");
                int choice3;
                int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice3);
                switch (choice3)
                {
                    case 1:
                        Console.WriteLine("You selected option 1: Add a project requirement:");
                        string newRequirementDescription = Console.ReadLine() ?? "No description.";
                        project.AddFRequirement(newRequirementDescription);
                        break;
                    case 2:
                        Console.WriteLine("You selected option 2: Update a project requirement");
                        // Call the method to update a project requirement here
                        break;
                    case 3:
                        Console.WriteLine("You selected option 3: Please enter the ID of the requirement to remove:");
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
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }

    }
    public static void editProjectEffortAndProgress(Project project)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("Please select an option from the menu below:");
        Console.WriteLine("1. Log person-hours for a requirement");
        Console.WriteLine("2. View effort totals for each requirement");
        Console.WriteLine("3. View project-wide effort totals");
        Console.WriteLine("-------------------------");
        int choice;
        int.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out choice);

        switch (choice)
        {
            case 1:
                Console.WriteLine("You selected option 1: Log person-hours for a requirement");
                Console.WriteLine("1. Functional requirement");
                Console.WriteLine("2. Nonfunctional requirement");
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
                Console.WriteLine("Enter the ID of the requirement:");
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
                Console.WriteLine("Enter hours for Uncategorized effort:");
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
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
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
            Console.WriteLine("  ID " + risk.GetID() + ": " + risk.GetDescription()
                + " | probability: " + risk.GetProbability()
                + " | cost: " + risk.GetCost()
                + " | status: " + risk.GetStatus());
        }

        Console.WriteLine("Functional requirements:");
        List<ProjectRequirement> fRequirements = project.GetFRequirements();
        if (fRequirements.Count == 0) Console.WriteLine("  (none)");
        foreach (ProjectRequirement fReq in fRequirements)
        {
            Console.WriteLine("  ID " + fReq.GetID() + ": " + fReq.ToString());
        }

        Console.WriteLine("Nonfunctional requirements:");
        List<ProjectRequirement> nfRequirements = project.GetNFRequirements();
        if (nfRequirements.Count == 0) Console.WriteLine("  (none)");
        foreach (ProjectRequirement nfReq in nfRequirements)
        {
            Console.WriteLine("  ID " + nfReq.GetID() + ": " + nfReq.ToString());
        }
    }
}
