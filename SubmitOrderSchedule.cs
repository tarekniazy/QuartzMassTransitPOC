using MassTransit;
using MassTransit.Scheduling;

public class SubmitOrderSchedule : RecurringSchedule
{
    //public SubmitOrderSchedule()
    //{
    //    CronExpression = "0/1 * * * * ?";

    //}
    public SubmitOrderSchedule(string id, string group, string cronExpression, string description)
    {
        ScheduleId = id;
        ScheduleGroup = group;
        CronExpression = cronExpression;
        Description = description;

        TimeZoneId = TimeZoneInfo.Local.Id;
        StartTime = DateTime.Now;

    }

    public MissedEventPolicy MisfirePolicy { get; protected set; }
    public string TimeZoneId { get; protected set; }
    public DateTimeOffset StartTime { get; protected set; }
    public DateTimeOffset? EndTime { get; protected set; }
    public string ScheduleId { get; private set; }
    public string ScheduleGroup { get; private set; }
    public string CronExpression { get; protected set; }
    public string Description { get; protected set; }
}
