namespace WebProgramming.Models
{
    public class WorkingHours
    {
        public int WorkingHoursID { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
