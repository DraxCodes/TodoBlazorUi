using System;

namespace Todo.BlazorUi.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

        public TodoItem() { }

        public TodoItem(string description)
        {
            Description = description;
        }

        public TodoItem(string description, string name)
        {
            Description = description;
            Name = name;
        }

        public TodoItem(string description, string name, DateTime? dueDate)
        {
            Description = description;
            Name = name;
            DueDate = dueDate;
        }
    }
}
