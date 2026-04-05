namespace AppointmentBooking.Shared
{
    public enum Specialization
    {
        General = 1,
        Cardiology,
        Orthopedics,
        Neurology,
        Dermatology
    }

    public enum WeekDays
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum Status
    {
        Scheduled = 1,
        Completed,
        Cancelled
    }
}
