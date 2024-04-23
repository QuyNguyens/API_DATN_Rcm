using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Movie_Rcm.Modal
{
    public class AccessTimeResponseModal
    {
        public List<string>? country { get; set; }
        public List<int?>? accessTime { get; set; }
    }
}
