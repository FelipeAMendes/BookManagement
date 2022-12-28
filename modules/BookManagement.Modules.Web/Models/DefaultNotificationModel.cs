using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Modules.Web.Models;

public class DefaultNotificationModel
{
    public NotificationType Type { get; set; }
    public IEnumerable<string> Messages { get; set; }

    public bool HasMultipleMessages()
    {
        return Messages.Count() > 1;
    }
}

public enum NotificationType
{
    Success,
    Info,
    Warning,
    Error
}