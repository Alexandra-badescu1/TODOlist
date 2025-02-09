using SQLite;

namespace TODOlist.Models;

[Table("ToDoModel")]
public class ToDoModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    private string name;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    private int val = 0;
    public int Val {
        get
        {
            return val;
        }
        set 
        {
            val = value;
        }
    }

    private bool isCompleted;
    public bool IsCompleted
    {
        get => isCompleted;
        set {
            isCompleted = value;
        }
    }
}