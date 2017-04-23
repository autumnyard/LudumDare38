
public class PanelMenu : PanelBase
{

    public void ButtonPlay2()
    {
        Director.Instance.GameBegin(2);
    }
    public void ButtonPlay3()
    {
        Director.Instance.GameBegin(3);
    }

    public void ButtonExit()
    {
        Director.Instance.Exit();
    }
}
