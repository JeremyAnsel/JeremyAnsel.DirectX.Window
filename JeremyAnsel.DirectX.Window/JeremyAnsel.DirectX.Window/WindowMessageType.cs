// <copyright file="WindowMessageType.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    public enum WindowMessageType
    {
        Null = 0x0000,

        Create = 0x0001,

        Destroy = 0x0002,

        Move = 0x0003,

        Size = 0x0005,

        Activate = 0x0006,

        SetFocus = 0x0007,

        KillFocus = 0x0008,

        Enable = 0x000A,

        SetRedraw = 0x000B,

        SetText = 0x000C,

        GetText = 0x000D,

        GetTextLength = 0x000E,

        Paint = 0x000F,

        Close = 0x0010,

        QueryEndSession = 0x0011,

        Quit = 0x0012,

        QueryOpen = 0x0013,

        EraseBackground = 0x0014,

        SystemColorChange = 0x0015,

        EndSession = 0x0016,

        ShowWindow = 0x0018,

        SettingChange = 0x001A,

        DeviceModeChange = 0x001B,

        ActivateApplication = 0x001C,

        FontChange = 0x001D,

        TimeChange = 0x001E,

        CancelMode = 0x001F,

        SetCursor = 0x0020,

        MouseActivate = 0x0021,

        ChildActivate = 0x0022,

        QueueSync = 0x0023,

        GetMinMaxInfo = 0x0024,

        PaintIcon = 0x0026,

        IconEraseBackground = 0x0027,

        NextDialogControl = 0x0028,

        SpoolerStatus = 0x002A,

        DrawItem = 0x002B,

        MeasureItem = 0x002C,

        DeleteItem = 0x002D,

        VirtualKeyToItem = 0x002E,

        CharToItem = 0x002F,

        SetFont = 0x0030,

        GetFont = 0x0031,

        SetHotKey = 0x0032,

        GetHotKey = 0x0033,

        QueryDragIcon = 0x0037,

        CompareItem = 0x0039,

        GetObject = 0x003D,

        Compacting = 0x0041,

        WindowPositionChanging = 0x0046,

        WindowPositionChanged = 0x0047,

        CopyData = 0x004A,

        CancelJournal = 0x004B,

        Notify = 0x004E,

        InputLangChangeRequest = 0x0050,

        InputLangChange = 0x0051,

        TrainingCard = 0x0052,

        Help = 0x0053,

        UserChanged = 0x0054,

        NotifyFormat = 0x0055,

        ContextMenu = 0x007B,

        StyleChanging = 0x007C,

        StyleChanged = 0x007D,

        DisplayChange = 0x007E,

        GetIcon = 0x007F,

        SetIcon = 0x0080,

        NonClientCreate = 0x0081,

        NonClientDestroy = 0x0082,

        NonClientCalcSize = 0x0083,

        NonClientHitTest = 0x0084,

        NonClientPaint = 0x0085,

        NonClientActivate = 0x0086,

        GetDialogCode = 0x0087,

        SyncPaint = 0x0088,

        NonClientMouseMove = 0x00A0,

        NonClientLeftButtonDown = 0x00A1,

        NonClientLeftButtonUp = 0x00A2,

        NonClientLeftButtonDoubleClick = 0x00A3,

        NonClientRightButtonDown = 0x00A4,

        NonClientRightButtonUp = 0x00A5,

        NonClientRightButtonDoubleClick = 0x00A6,

        NonClientMiddleButtonDown = 0x00A7,

        NonClientMiddleButtonUp = 0x00A8,

        NonClientMiddleButtonDoubleClick = 0x00A9,

        NonClientXButtonDown = 0x00AB,

        NonClientXButtonUp = 0x00AC,

        NonClientXButtonDoubleClick = 0x00AD,

        InputDeviceChange = 0x00FE,

        Input = 0x00FF,

        KeyDown = 0x0100,

        KeyUp = 0x0101,

        Char = 0x0102,

        DearChar = 0x0103,

        SystemKeyDown = 0x0104,

        SystemKeyUp = 0x0105,

        SystemChar = 0x0106,

        SystemDeadChar = 0x0107,

        UnicodeChar = 0x0109,

        InitDialog = 0x0110,

        Command = 0x0111,

        SystemCommand = 0x0112,

        Timer = 0x0113,

        HorizontalScroll = 0x0114,

        VerticalScroll = 0x0115,

        InitMenu = 0x0116,

        InitMenuPopup = 0x0117,

        Gesture = 0x0119,

        GestureNotify = 0x011A,

        MenuSelect = 0x011F,

        MenuChar = 0x0120,

        EnterIdle = 0x0121,

        MenuRightButtonUp = 0x0122,

        MenuDrag = 0x0123,

        MenuGetObject = 0x0124,

        UninitMenuPopup = 0x0125,

        MenuCommand = 0x0126,

        MouseMove = 0x0200,

        LeftButtonDown = 0x0201,

        LeftButtonUp = 0x0202,

        LeftButtonDoubleClick = 0x0203,

        RightButtonDown = 0x0204,

        RightButtonUp = 0x0205,

        RightButtonDoubleClick = 0x0206,

        MiddleButtonDown = 0x0207,

        MiddleButtonUp = 0x0208,

        MiddleButtonDoubleClick = 0x0209,

        MouseWheel = 0x020A,

        XButtonDown = 0x020B,

        XButtonUp = 0x020C,

        XButtonDoubleClick = 0x020D,

        MouseHorizontalWheel = 0x020E,

        ParentNotify = 0x0210,

        EnterMenuLoop = 0x0211,

        ExitMenuLoop = 0x0212,

        NextMenu = 0x0213,

        Sizing = 0x0214,

        CaptureChanged = 0x0215,

        Moving = 0x0216,

        PowerBroadcast = 0x0218,

        DeviceChange = 0x0219,

        EnterSizeMove = 0x0231,

        ExitSizeMove = 0x0232,

        FropFiles = 0x0233,

        PointerDeviceChange = 0x238,

        PointerDeviceInRange = 0x239,

        PointerDeviceOutOfRange = 0x23A,

        Touch = 0x0240,

        NonClientPointerUpdate = 0x0241,

        NonClientPointerDown = 0x0242,

        NonClientPointerUp = 0x0243,

        PointerUpdate = 0x0245,

        PointerDown = 0x0246,

        PointerUp = 0x0247,

        PointerEnter = 0x0249,

        PointerLeave = 0x024A,

        PointerActivate = 0x024B,

        PointerCaptureChanged = 0x024C,

        TouchHitTesting = 0x024D,

        PointerWheel = 0x024E,

        PointerHorizontalWheel = 0x024F,

        MpiseHover = 0x02A1,

        MouseLeave = 0x02A3,

        NonClientMouseHover = 0x02A0,

        NonClientMouseLeave = 0x02A2,

        DpiChanged = 0x02E0,

        Cut = 0x0300,

        Copy = 0x0301,

        Paste = 0x0302,

        Clear = 0x0303,

        Undo = 0x0304,

        RenderFormat = 0x0305,

        RenderAllFormats = 0x0306,

        HotKey = 0x0312,

        Print = 0x0317,

        PrintClient = 0x0318,

        ApplicationCommand = 0x0319,

        ThemeChanged = 0x031A,

        GetTitleBarInfoEx = 0x033F,

        User = 0x0400,

        App = 0x8000
    }
}
