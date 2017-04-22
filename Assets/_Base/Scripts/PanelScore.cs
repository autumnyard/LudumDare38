
public class PanelScore : PanelBase
{

    public void ButtonReplay()
    {
        Director.Instance.GameBegin();
    }

    public void ButtonReturn()
    {
        Director.Instance.GameEnd();
    }
}
