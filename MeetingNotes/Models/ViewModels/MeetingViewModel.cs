using static Azure.Core.HttpHeader;

namespace MeetingNotes.Models.ViewModels
{
    public class MeetingViewModel
    {
        public int MeetingId { get; set; }

        public DateTime MeetingDate { get; set; }

        public string ManagerFullName { get; set; }

        public string WorkerFullName { get; set; }
        public Notes notes { get; set; }

        //todo za details ne treba nam notes
        //notes prosirujemo sa enumom: regular meeting, public feedback, (tip koji ce worker moc vidit)
    }
}
