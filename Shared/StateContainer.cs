using IF_Vagas.Data;
namespace IF_Vagas.Shared;

public class StateContainer {
    public User? user { get; set; }
    public Project? project { get; set; }
    public void SetUser(User user){
        this.user = user;
    }

    public void SetProject(Project proj){
        project = proj;
    }
}