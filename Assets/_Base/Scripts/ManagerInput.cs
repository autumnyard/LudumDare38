using UnityEngine;

public class ManagerInput : MonoBehaviour
{
    #region Variables
    private enum MyMouse
    {
        NONE,
        Left,
        LeftMaintain,
        Middle,
        Right,
        WheelUp,
        WheelDown,
        WheelClick,
        Lateral1,
        Lateral2,
        MaxValues
    }

    private enum MyKeyboard
    {
        NONE,
        Space,
        Enter,
        ControlAny,
        ControlLeft,
        ControlRight,
        ShiftAny,
        ShiftLeft,
        ShiftRight,
        W, A, S, D,
        U, H, J, K,
        Key0,
        Key1,
        Key2,
        Key3,
        ArrowRight,
        ArrowLeft,
        ArrowUp,
        ArrowDown,
        Escape,
        MaxValues
    }

    public delegate void Delegate();
    public Delegate[] OnMouse = new Delegate[(int)MyMouse.MaxValues];
    public Delegate[] OnKeyboard = new Delegate[(int)MyKeyboard.MaxValues];
    #endregion


    #region Monobehaviour
    void Awake()
    {
        Director.Instance.managerInput = this;
    }

    void LateUpdate()
    {
        CheckInput();
    }
    #endregion


    #region Input calling
    private void CheckInput()
    {
        // For efficiency reasons, only use the strictly needed ones
        // We rely on Unity's event delegate callbacks for this
        // They may, or may not be optimal

        //CallDelegate(OnMouse[(int)MyMouse.LeftMaintain], Input.GetMouseButton(0));
        //CallDelegate(OnMouse[(int)MyMouse.Left], Input.GetMouseButtonDown(0));
        //CallDelegate( OnMouse[(int)MyMouse.Wheel_up], Input.GetAxis( "Mouse ScrollWheel" ) );
        //CallDelegate( OnMouse[(int)MyMouse.Wheel_down], Input.GetMouseButton( 2 ) );

        if (!Director.Instance.isPaused)
        {
            // For player 1
            if (Director.Instance.managerEntity.playersScript[0] != null)
            {
                CallDelegate(OnKeyboard[(int)MyKeyboard.W], Input.GetKey(KeyCode.W));
                CallDelegate(OnKeyboard[(int)MyKeyboard.A], Input.GetKey(KeyCode.A));
                CallDelegate(OnKeyboard[(int)MyKeyboard.S], Input.GetKey(KeyCode.S));
                CallDelegate(OnKeyboard[(int)MyKeyboard.D], Input.GetKey(KeyCode.D));
                CallDelegate(OnKeyboard[(int)MyKeyboard.ShiftLeft], Input.GetKey(KeyCode.LeftShift));
                CallDelegate(OnKeyboard[(int)MyKeyboard.ControlLeft], Input.GetKey(KeyCode.LeftControl));

                // Joypad
                CallDelegate(OnKeyboard[(int)MyKeyboard.W], (Input.GetAxis("JoyY1") < 0));
                CallDelegate(OnKeyboard[(int)MyKeyboard.A], (Input.GetAxis("JoyY1") > 0));
                CallDelegate(OnKeyboard[(int)MyKeyboard.S], (Input.GetAxis("JoyX1") < 0));
                CallDelegate(OnKeyboard[(int)MyKeyboard.D], (Input.GetAxis("JoyX1") > 0));
                CallDelegate(OnKeyboard[(int)MyKeyboard.ShiftLeft], (Input.GetButtonDown("JoyButton1")));

            }

            // For player 2
            if (Director.Instance.managerEntity.playersScript[1] != null)
            {
                CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowLeft], Input.GetKey(KeyCode.LeftArrow));
                CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowRight], Input.GetKey(KeyCode.RightArrow));
                CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowUp], Input.GetKey(KeyCode.UpArrow));
                CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowDown], Input.GetKey(KeyCode.DownArrow));
                CallDelegate(OnKeyboard[(int)MyKeyboard.Key0], Input.GetKeyDown(KeyCode.Keypad0));

            }

            // For player 3
            if (Director.Instance.currentGameMode == Structs.GameMode.Multi3players)
            {

                if (Director.Instance.managerEntity.playersScript[2] != null)
                {
                    CallDelegate(OnKeyboard[(int)MyKeyboard.U], Input.GetKey(KeyCode.U));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.J], Input.GetKey(KeyCode.J));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.H], Input.GetKey(KeyCode.H));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.K], Input.GetKey(KeyCode.K));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.ShiftRight], Input.GetKeyDown(KeyCode.RightShift));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.ControlRight], Input.GetKeyDown(KeyCode.RightControl));

                    // Joypad
                    CallDelegate(OnKeyboard[(int)MyKeyboard.U], (Input.GetAxis("JoyY2") < 0));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.J], (Input.GetAxis("JoyY2") > 0));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.H], (Input.GetAxis("JoyX2") < 0));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.K], (Input.GetAxis("JoyX2") > 0));
                    CallDelegate(OnKeyboard[(int)MyKeyboard.ShiftRight], (Input.GetButtonDown("JoyButton2")));
                }
            }
        }

        CallDelegate(OnKeyboard[(int)MyKeyboard.Enter], Input.GetKeyDown(KeyCode.Return));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Enter], Input.GetKeyDown(KeyCode.KeypadEnter));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Escape], Input.GetKeyDown(KeyCode.Escape));

        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key1], Input.GetKeyDown(KeyCode.Alpha1));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key1], Input.GetKeyDown(KeyCode.Keypad1));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Key2], Input.GetKeyDown(KeyCode.Alpha2));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Key2], Input.GetKeyDown(KeyCode.Keypad2));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Key3], Input.GetKeyDown(KeyCode.Alpha3));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Key3], Input.GetKeyDown(KeyCode.Keypad3));

    }

    private void CallDelegate(Delegate action, bool condition = true)
    {
        if (condition)
        {
            if (action != null)
            {
                action();
            }
        }
    }
    #endregion


    #region Input binding
    private void Bind(ref Delegate to, Delegate method)
    {
        Unbind(ref to);
        to += method;
    }

    private void Unbind(ref Delegate from)
    {
        if (from != null)
        {
            from = null;
        }
    }

    private void UnbindAll(ref Delegate[] from)
    {
        for (int i = 0; i < from.Length; i++)
        {
            Unbind(ref from[i]);
        }
    }
    #endregion


    #region Public
    public void SetEvents()
    {
        //UnbindAllEverything( ref OnMouse );
        //UnbindAllEverything( ref OnKeyboard );
        UnbindAll(ref OnMouse);
        UnbindAll(ref OnKeyboard);

        switch (Director.Instance.currentScene)
        {
            case Structs.GameScene.Menu:
                Bind(ref OnKeyboard[(int)MyKeyboard.Enter], Director.Instance.managerUI.panelMenu.ButtonPlay2);
                Bind(ref OnKeyboard[(int)MyKeyboard.Key2], Director.Instance.managerUI.panelMenu.ButtonPlay2);
                Bind(ref OnKeyboard[(int)MyKeyboard.Key3], Director.Instance.managerUI.panelMenu.ButtonPlay3);
                Bind(ref OnKeyboard[(int)MyKeyboard.Escape], Director.Instance.managerUI.panelMenu.ButtonExit);
                break;

            case Structs.GameScene.Score:
                Bind(ref OnKeyboard[(int)MyKeyboard.Enter], Director.Instance.managerUI.panelScore.ButtonReplay);
                Bind(ref OnKeyboard[(int)MyKeyboard.Key2], Director.Instance.managerUI.panelScore.ButtonReplay2);
                Bind(ref OnKeyboard[(int)MyKeyboard.Key3], Director.Instance.managerUI.panelScore.ButtonReplay3);
                Bind(ref OnKeyboard[(int)MyKeyboard.Escape], Director.Instance.managerUI.panelScore.ButtonReturn);
                break;

            case Structs.GameScene.Ingame:
                Bind(ref OnKeyboard[(int)MyKeyboard.Escape], Director.Instance.TogglePause);

                // For player 1
                    Bind(ref OnKeyboard[(int)MyKeyboard.W], Director.Instance.managerEntity.playersScript[0].MoveUp);
                    Bind(ref OnKeyboard[(int)MyKeyboard.S], Director.Instance.managerEntity.playersScript[0].MoveDown);
                    Bind(ref OnKeyboard[(int)MyKeyboard.A], Director.Instance.managerEntity.playersScript[0].MoveLeft);
                    Bind(ref OnKeyboard[(int)MyKeyboard.D], Director.Instance.managerEntity.playersScript[0].MoveRight);
                    Bind(ref OnKeyboard[(int)MyKeyboard.ControlLeft], Director.Instance.managerEntity.playersScript[0].Dash);
                    Bind(ref OnKeyboard[(int)MyKeyboard.ShiftLeft], Director.Instance.managerEntity.playersScript[0].Dash);

                // For player 2
                if (Director.Instance.managerEntity.playersScript[1] != null)
                {
                    Bind(ref OnKeyboard[(int)MyKeyboard.ArrowUp], Director.Instance.managerEntity.playersScript[1].MoveUp);
                    Bind(ref OnKeyboard[(int)MyKeyboard.ArrowDown], Director.Instance.managerEntity.playersScript[1].MoveDown);
                    Bind(ref OnKeyboard[(int)MyKeyboard.ArrowLeft], Director.Instance.managerEntity.playersScript[1].MoveLeft);
                    Bind(ref OnKeyboard[(int)MyKeyboard.ArrowRight], Director.Instance.managerEntity.playersScript[1].MoveRight);
                    Bind(ref OnKeyboard[(int)MyKeyboard.Key0], Director.Instance.managerEntity.playersScript[1].Dash);
                }
                // For player 3
                if (Director.Instance.currentGameMode == Structs.GameMode.Multi3players)
                {
                    if (Director.Instance.managerEntity.playersScript[2] != null)
                    {
                        Bind(ref OnKeyboard[(int)MyKeyboard.U], Director.Instance.managerEntity.playersScript[2].MoveUp);
                        Bind(ref OnKeyboard[(int)MyKeyboard.J], Director.Instance.managerEntity.playersScript[2].MoveDown);
                        Bind(ref OnKeyboard[(int)MyKeyboard.H], Director.Instance.managerEntity.playersScript[2].MoveLeft);
                        Bind(ref OnKeyboard[(int)MyKeyboard.K], Director.Instance.managerEntity.playersScript[2].MoveRight);
                        Bind(ref OnKeyboard[(int)MyKeyboard.ControlRight], Director.Instance.managerEntity.playersScript[2].Dash);
                        Bind(ref OnKeyboard[(int)MyKeyboard.ShiftRight], Director.Instance.managerEntity.playersScript[2].Dash);
                    }
                }
                //Bind(ref OnKeyboard[(int)MyKeyboard.Space], Director.Instance.PlayerJump);
                //Bind(ref OnKeyboard[(int)MyKeyboard.Enter], Director.Instance.GenerateEnemy);
                //Bind(ref OnKeyboard[(int)MyKeyboard.ArrowLeft], Director.Instance.MapPrevious);
                //Bind(ref OnKeyboard[(int)MyKeyboard.ArrowRight], Director.Instance.MapNext);
                break;
        }
    }
    #endregion



    #region DEPRECATED


    //private void UnBind( ref Delegate to, Delegate method )
    //{
    //	if( to != null )
    //	{
    //		to -= method;
    //	}
    //}

    //private void UnbindAll( ref Delegate from )
    //{
    //	if( from != null )
    //	{
    //		foreach( Delegate d in from.GetInvocationList() )
    //		{
    //			UnBind( ref from, d );
    //		}
    //	}
    //}

    //private void UnbindAllEverything( ref Delegate[] froms )
    //{
    //	for( int i = 0; i < froms.Length; i++ )
    //	{
    //		var from = froms[i];
    //		UnbindAll( ref from );
    //	}
    //}
    #endregion
}
