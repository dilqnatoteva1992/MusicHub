namespace MusicHub.Data.Models
{
    public class SongPerformer
    {
        public int SongId { get; set; }

        public int PerformerId { get; set; }

        #region Navigation Properties

        public Song Song { get; set; }

        public Performer Performer { get; set; }

        #endregion Navigation Properties
    }
}
