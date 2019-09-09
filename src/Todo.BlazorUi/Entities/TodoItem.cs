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
    }
}
