
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CodeChallenge_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = new[]
            {
                new Employee()
                {
                    Id = 1,
                    ParentId = -1,
                    Position = 0,
                    Title = "Manager A",
                    Allocation = 600
                },
                new Employee()
                {
                    Id = 2,
                    ParentId = 1,
                    Position = 0,
                    Title = "Manager B",
                    Allocation = 600
                },
                new Employee()
                {
                    Id = 3,
                    ParentId = 2,
                    Position = 0,
                    Title = "Developer",
                    Allocation = 2000
                },
                new Employee()
                {
                    Id = 3,
                    ParentId = 2,
                    Position = 1,
                    Title = "QA Tester",
                    Allocation = 1000
                },
                new Employee()
                {
                    Id = 4,
                    ParentId = -1,
                    Position = 1,
                    Title = "Manager C",
                    Allocation = 600
                },
                new Employee()
                {
                    Id = 5,
                    ParentId = 4,
                    Position = 0,
                    Title = "Manager D",
                    Allocation = 600
                },
                new Employee()
                {
                    Id = 6,
                    ParentId = -1,
                    Position = 2,
                    Title = "Manager E",
                    Allocation = 600
                },
                new Employee()
                {
                    Id = 7,
                    ParentId = 6,
                    Position = 0,
                    Title = "Developer",
                    Allocation = 2000
                }
            };
            
            var hierarchy = employees.Hierarchize(
                -1,
                e => e.Id,
                e => e.ParentId,
                e => e.Position
                );

            int companyAllocation = 0;
            string noSubordinates = "";
            foreach (var node in hierarchy)
            {
                int dptAllocation = 0;
                string managerTitle = node.Value.Title.ToString();
                dptAllocation += node.Value.Allocation;
                string subManagerTitle = "";
                int subDptAllocation = 0;

                for (int i = 0; i < node.Children.Count; i++)
                {
                    var subordinateNode = node.Children[i];
                    int subordinateAllocation = subordinateNode.Value.Allocation;
                    dptAllocation += subordinateAllocation;
                    if (subordinateNode.Children.Count > 0)
                    {
                        for (int j = 0; j < subordinateNode.Children.Count; j++)
                        {
                            var subordinateNode2 = subordinateNode.Children[j];
                            int subordinateAllocation2 = subordinateNode2.Value.Allocation;
                            dptAllocation += subordinateAllocation2;
                        }
                        if (subordinateNode.Value.Title.Contains("Manager"))
                        {
                            subManagerTitle = subordinateNode.Value.Title;
                            subDptAllocation = 600;
                                for (int k = 0; k < subordinateNode.Children.Count; k++)
                                {
                                    var subordinateNode2 = subordinateNode.Children[k];
                                    int subordinateAllocation2 = subordinateNode2.Value.Allocation;
                                    subDptAllocation += subordinateAllocation2;
                                }
                        }
                    }
                    else if (subordinateNode.Value.Title.Contains("Manager") && subordinateNode.Children.Count == 0)
                    {
                        subManagerTitle = subordinateNode.Value.Title;
                        subDptAllocation = subordinateNode.Value.Allocation;
                        noSubordinates = subordinateNode.Value.Title + " has no one reportimg to them.";
                    }
                }
                companyAllocation += dptAllocation;
                Console.WriteLine(managerTitle + "'s allocation should be: $" + dptAllocation);
                if (subManagerTitle != "")
                {
                    Console.WriteLine(subManagerTitle + "'s allocation should be: $" + subDptAllocation);
                }
            }
            Console.WriteLine("The department's allocation should be: $" + companyAllocation);
            Console.WriteLine(noSubordinates);
            Console.ReadKey();
        }
    }
}
