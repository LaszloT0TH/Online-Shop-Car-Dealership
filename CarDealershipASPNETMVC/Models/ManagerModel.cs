namespace CarDealershipASPNETMVC.Models
{
    public class ManagerModel
    {
        public int ManagerId { get; set; }

        public string ManagerFirstName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU

        public string ManagerLastName { get; set; } = null!; // https://www.youtube.com/watch?v=H2sfNnB1QAU
        public string ManagerFullName {
            get { return ManagerId + " " + ManagerFirstName + " " + ManagerLastName; }
        }
    }
}
