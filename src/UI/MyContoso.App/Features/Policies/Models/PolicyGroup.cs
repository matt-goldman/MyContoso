using System.Collections.ObjectModel;
using Shared;

namespace MyContoso.App.Features.Policies.Models;

/// <summary>
/// Grouping class for Policy by category
/// </summary>
public class PolicyGroup(string category, IEnumerable<Policy> policies) : ObservableCollection<Policy>(policies)
{
    public string Category { get; } = category;
}