using CommunityToolkit.Mvvm.Messaging.Messages;
using TODOlist.Models;

namespace TODOlist.Messages;

public class AddItemMessages : ValueChangedMessage<ToDoModel>
{
    public AddItemMessages(ToDoModel value) : base(value)
    {
    }
}