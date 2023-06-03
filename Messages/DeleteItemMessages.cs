using CommunityToolkit.Mvvm.Messaging.Messages;
using TODOlist.Models;

namespace TODOlist.Messages;

public class DeleteItemMessages : ValueChangedMessage<ToDoModel>
{
    public DeleteItemMessages(ToDoModel value) : base(value)
    {
    }
}