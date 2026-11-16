
using DefaultNamespace;

public class UserInterface
{
    static void Main(string[] args)
    {
        Project project = new Project();
        Console.WriteLine("Welcome to Group 6's Project Software Project Management Tool! Here you will be able to manage your projects information, requirements and efforts.");
        while (true)
        {
            DisplayMenu();
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("You selected option 1: Edit a project information");
                    EditProjectInformation(project);
                    break;
                case 2:
                    Console.WriteLine("You selected option 2: Edit project requirements");
                    EditProjectRequirements(project);
                    break;
                case 3:
                    Console.WriteLine("You selected option 3: View project effort and progress");
                    // Call the method to view project effort and progress here
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
        Console.WriteLine("1.Description");
        Console.WriteLine("2.Project Owner and Manager's name");
        Console.WriteLine("3.Team Members");
        Console.WriteLine("4.Project Risks");
        Console.WriteLine("-------------------------");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("You selected option 1: Edit project description, Please enter the new description:");
                string newDescription = Console.ReadLine();
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
                int subChoice = int.Parse(Console.ReadLine());
                switch (subChoice)
                {
                    case 1:
                        Console.WriteLine("Enter the name of the owner to add:");
                        string ownerToAdd = Console.ReadLine();
                        project.AddOwner(ownerToAdd);
                        break;
                    case 2:
                        Console.WriteLine("Enter the name of the owner to remove:");
                        string ownerToRemove = Console.ReadLine();
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
                        string managerToAdd = Console.ReadLine();
                        project.AddManager(managerToAdd);
                        break;
                    case 4:
                        Console.WriteLine("Enter the name of the manager to remove:");
                        string managerToRemove = Console.ReadLine();
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
                int subChoice2 = int.Parse(Console.ReadLine());
                switch (subChoice2)
                {
                    case 1:
                        Console.WriteLine("Enter the name of the team member to add:");
                        string memberToAdd = Console.ReadLine();
                        project.AddMember(memberToAdd);
                        break;
                    case 2:
                        Console.WriteLine("Enter the name of the team member to remove:");
                        string memberToRemove = Console.ReadLine();
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
                int subChoice3 = int.Parse(Console.ReadLine());
                int id;
                switch (subChoice3)
                {
                    case 1:
                        Console.WriteLine("Enter the probability of the risk (0 to 1):");
                        float probability = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the cost of the risk:");
                        float cost = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the status of the risk:");
                        string status = Console.ReadLine();
                        Console.WriteLine("Enter the description of the risk:");
                        string description = Console.ReadLine();
                        project.AddRisk(probability, cost, status, description);
                        break;
                    case 2:
                        Console.WriteLine("Enter the ID of the risk to edit:");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new probability of the risk (0 to 1):");
                        probability = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new cost of the risk:");
                        cost = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new status of the risk:");
                        status = Console.ReadLine();
                        Console.WriteLine("Enter the new description of the risk:");
                        description = Console.ReadLine();
                        project.UpdateRiskByID(id, probability, cost, status, description);
                        break;
                    case 3:
                        Console.WriteLine("Enter the ID of the risk to remove:");
                        id = int.Parse(Console.ReadLine());
                        project.RemoveRiskByID(id);
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
        int choice2 = int.Parse(Console.ReadLine());

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
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You selected option 1: Add a project requirement:");
                        string newRequirementDescription = Console.ReadLine();
                        project.AddNFRequirement(newRequirementDescription);
                        break;
                    case 2:
                        Console.WriteLine("You selected option 2: Update a project requirement");
                        // Call the method to update a project requirement here
                        break;
                    case 3:
                        Console.WriteLine("You selected option 3: Please enter the ID of the requirement to remove:");
                        project.RemoveNFRequirementByID(int.Parse(Console.ReadLine()));
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
                int choice3 = int.Parse(Console.ReadLine());
                switch (choice3)
                {
                    case 1:
                        Console.WriteLine("You selected option 1: Add a project requirement:");
                        string newRequirementDescription = Console.ReadLine();
                        project.AddFRequirement(newRequirementDescription);
                        break;
                    case 2:
                        Console.WriteLine("You selected option 2: Update a project requirement");
                        // Call the method to update a project requirement here
                        break;
                    case 3:
                        Console.WriteLine("You selected option 3: Please enter the ID of the requirement to remove:");
                        project.RemoveFRequirementByID(int.Parse(Console.ReadLine()));
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
