using CommunityToolkit.Mvvm.Messaging.Messages;
using Shared;

namespace MyContoso.App.Messages;

public class LoggedInUserChangedMessage(Employee value) : ValueChangedMessage<Employee>(value);