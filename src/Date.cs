// THIS IS THE REAL STUFF!

namespace Kronometer
{
    /// <summary>
    /// Small class to handle dates easily
    /// </summary>
    public class Date
    {
        public int year { get; set; }
        public Month month { get; set; }
        public int dayOfMonth { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }

        public Date(int year, Month month, int dayOfMonth, int day, int hours, int minutes, int seconds)
        {
            this.year = year;
            this.day = day;
            this.dayOfMonth = dayOfMonth;
            this.month = month;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public Date(Date date)
        {
            year = date.year;
            month = date.month;
            dayOfMonth = date.dayOfMonth;
            day = date.day;
            hours = date.hours;
            minutes = date.minutes;
            seconds = date.seconds;
        }
    }

}
