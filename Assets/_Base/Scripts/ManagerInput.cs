﻿using UnityEngine;

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
        W, A, S, D,
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

        //CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowLeft], Input.GetKeyDown(KeyCode.LeftArrow));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowRight], Input.GetKeyDown(KeyCode.RightArrow));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowUp], Input.GetKeyDown(KeyCode.UpArrow));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.ArrowDown], Input.GetKeyDown(KeyCode.DownArrow));

        //CallDelegate(OnKeyboard[(int)MyKeyboard.W], Input.GetKey(KeyCode.W));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.A], Input.GetKey(KeyCode.A));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.S], Input.GetKey(KeyCode.S));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.D], Input.GetKey(KeyCode.D));

        CallDelegate(OnKeyboard[(int)MyKeyboard.Space], Input.GetKeyDown(KeyCode.Space));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Enter], Input.GetKeyDown(KeyCode.Return));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Enter], Input.GetKeyDown(KeyCode.KeypadEnter));
        CallDelegate(OnKeyboard[(int)MyKeyboard.Escape], Input.GetKeyDown(KeyCode.Escape));

        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key1], Input.GetKeyDown(KeyCode.Alpha1));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key1], Input.GetKeyDown(KeyCode.Keypad1));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key2], Input.GetKeyDown(KeyCode.Alpha2));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key2], Input.GetKeyDown(KeyCode.Keypad2));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key3], Input.GetKeyDown(KeyCode.Alpha3));
        //CallDelegate(OnKeyboard[(int)MyKeyboard.Key3], Input.GetKeyDown(KeyCode.Keypad3));

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
                Bind(ref OnKeyboard[(int)MyKeyboard.Enter], Director.Instance.GameBegin);
                Bind(ref OnKeyboard[(int)MyKeyboard.Escape], Director.Instance.Exit);
                break;

            case Structs.GameScene.Ingame:
                // TODO: Descomentar todo esto!
                Bind(ref OnKeyboard[(int)MyKeyboard.Space], Director.Instance.DebugHurtPlayer);
                Bind(ref OnKeyboard[(int)MyKeyboard.Escape], Director.Instance.GameEnd);
                //Bind(ref OnKeyboard[(int)MyKeyboard.W], Director.Instance.PlayerMoveForward);
                //Bind(ref OnKeyboard[(int)MyKeyboard.S], Director.Instance.PlayerMoveBackward);
                //Bind(ref OnKeyboard[(int)MyKeyboard.A], Director.Instance.PlayerMoveLeft);
                //Bind(ref OnKeyboard[(int)MyKeyboard.D], Director.Instance.PlayerMoveRight);
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
