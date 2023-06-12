using Radzen;
public static class ext{
public static void ShowNotification(this NotificationService notify, string mensaje, NotificationSeverity severity = NotificationSeverity.Success)
    {
        var message = new NotificationMessage
        {
            Severity = severity,
            Summary = mensaje
        };
        notify.Notify(message);
    }
}