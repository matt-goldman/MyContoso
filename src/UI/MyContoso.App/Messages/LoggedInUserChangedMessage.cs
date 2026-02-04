using CommunityToolkit.Mvvm.Messaging.Messages;
using MyContoso.App.Models;

namespace MyContoso.App.Messages;

public class LoggedInUserChangedMessage(Employee value) : ValueChangedMessage<Employee>(value);
