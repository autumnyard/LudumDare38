
public class PanelScore : PanelBase
{

    public void ButtonReplay()
    {
        Director.Instance.GameBegin(2);
    }

    public void ButtonReplay2()
    {
        Director.Instance.GameBegin(2);
    }

    public void ButtonReplay3()
    {
        Director.Instance.GameBegin(3);
    }

    public void ButtonReturn()
    {
        Director.Instance.GameEnd();
    }
}
