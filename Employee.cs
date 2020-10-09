using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge_CSharp
{
    public class Employee
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public int Allocation { get; set; }
    }
}
