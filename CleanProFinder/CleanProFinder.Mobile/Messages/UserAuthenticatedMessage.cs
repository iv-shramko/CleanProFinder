using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CleanProFinder.Mobile.Messages;

public class UserAuthenticatedMessage : ValueChangedMessage<bool>
{
    public UserAuthenticatedMessage(bool value) : base(value)
    {
    }
}