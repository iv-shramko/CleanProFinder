using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CleanProFinder.Mobile.Messages;

public class UserRoleAssignedMessage : ValueChangedMessage<bool>
{
    public UserRoleAssignedMessage(bool value) : base(value)
    {
    }
}