namespace BE_Movie_Rcm.Modal
{
    public class ListRatingModal
    {
        public int userId {  get; set; }    
        public List<string?> countries {  get; set; }
        public List<int?> genres { get; set; }
    }
}
