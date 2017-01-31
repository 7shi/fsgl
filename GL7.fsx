// (0) PUBLIC DOMAIN
// To the extent possible under law, the person who associated CC0 with this work
// has waived all copyright and related or neighboring rights to this work.

namespace GL7

#nowarn "9"
#r "System"
#r "System.Drawing"
#r "System.Windows.Forms"

open System
open System.Drawing
open System.Runtime.InteropServices
open System.Windows.Forms

[<Struct; StructLayout(LayoutKind.Sequential)>]
type PIXELFORMATDESCRIPTOR =
    val mutable nSize           : int16
    val mutable nVersion        : int16
    val mutable dwFlags         : int
    val mutable iPixelType      : byte
    val mutable cColorBits      : byte
    val mutable cRedBits        : byte
    val mutable cRedShift       : byte
    val mutable cGreenBits      : byte
    val mutable cGreenShift     : byte
    val mutable cBlueBits       : byte
    val mutable cBlueShift      : byte
    val mutable cAlphaBits      : byte
    val mutable cAlphaShift     : byte
    val mutable cAccumBits      : byte
    val mutable cAccumRedBits   : byte
    val mutable cAccumGreenBits : byte
    val mutable cAccumBlueBits  : byte
    val mutable cAccumAlphaBits : byte
    val mutable cDepthBits      : byte
    val mutable cStencilBits    : byte
    val mutable cAuxBuffers     : byte
    val mutable iLayerType      : byte
    val mutable bReserved       : byte
    val mutable dwLayerMask     : int
    val mutable dwVisibleMask   : int
    val mutable dwDamageMask    : int

[<Struct; StructLayout(LayoutKind.Sequential)>]
type BITMAPINFOHEADER =
    val mutable biSize          : int
    val mutable biWidth         : int
    val mutable biHeight        : int
    val mutable biPlanes        : int16
    val mutable biBitCount      : int16
    val mutable biCompression   : int
    val mutable biSizeImage     : int
    val mutable biXPelsPerMeter : int
    val mutable biYPelsPerMeter : int
    val mutable biClrUsed       : int
    val mutable biClrImportant  : int

module Win32 =
    let CS_VREDRAW =  1
    let CS_HREDRAW =  2
    let CS_OWNDC   = 32

    let PFD_DOUBLEBUFFER   =  1
    let PFD_DRAW_TO_WINDOW =  4
    let PFD_DRAW_TO_BITMAP =  8
    let PFD_SUPPORT_GDI    = 16
    let PFD_SUPPORT_OPENGL = 32

    let DIB_PAL_COLORS = 1

    [<DllImport("user32.dll")>]
    extern nativeint GetDC(nativeint hWnd)
    [<DllImport("user32.dll")>]
    extern int ReleaseDC(nativeint hWnd, nativeint hDC)

    [<DllImport("gdi32.dll")>]
    extern int ChoosePixelFormat(nativeint hDC, PIXELFORMATDESCRIPTOR& ppfd)
    [<DllImport("gdi32.dll", SetLastError = true)>]
    extern bool SetPixelFormat(nativeint hDC, int format, PIXELFORMATDESCRIPTOR& ppfd)
    [<DllImport("gdi32.dll")>]
    extern bool SwapBuffers(nativeint hDC)
    [<DllImport("gdi32.dll")>]
    extern nativeint CreateDIBSection(nativeint hdc, BITMAPINFOHEADER& lpbmi, int usage,
                                      nativeint& ppvBits, nativeint hSection, int offset)
    [<DllImport("gdi32.dll")>]
    extern nativeint CreateCompatibleDC(nativeint hdc)
    [<DllImport("gdi32.dll")>]
    extern nativeint SelectObject(nativeint hdc, nativeint h)
    [<DllImport("gdi32.dll")>]
    extern bool DeleteDC(nativeint hdc)
    [<DllImport("gdi32.dll")>]
    extern bool DeleteObject(nativeint ho)

    [<DllImport("opengl32.dll")>]
    extern nativeint wglCreateContext(nativeint hDC)
    [<DllImport("opengl32.dll")>]
    extern bool wglMakeCurrent(nativeint hDC, nativeint hGLRC)
    [<DllImport("opengl32.dll")>]
    extern bool wglDeleteContext(nativeint hGLRC)

type RAII(dtor) =
    interface IDisposable with override x.Dispose() = dtor()

open Win32

type GLForm() =
    inherit Form()

    let mutable hDC   = 0n
    let mutable hGLRC = 0n

    override x.CreateParams =
        x.SetStyle(ControlStyles.Opaque, true)
        let cp = base.CreateParams
        cp.ClassStyle <- cp.ClassStyle ||| CS_VREDRAW ||| CS_HREDRAW ||| CS_OWNDC
        cp

    override x.OnHandleCreated e =
        base.OnHandleCreated e
        let mutable pfd =
            PIXELFORMATDESCRIPTOR(
                nSize        = (Marshal.SizeOf<PIXELFORMATDESCRIPTOR>() |> int16),
                nVersion     = 1s,
                dwFlags      = (PFD_DOUBLEBUFFER ||| PFD_DRAW_TO_WINDOW ||| PFD_SUPPORT_OPENGL),
                cColorBits   = 32uy,
                cDepthBits   = 24uy,
                cStencilBits = 8uy)
        hDC <- GetDC x.Handle
        let format = ChoosePixelFormat(hDC, &pfd)
        if format = 0 then
            failwith "Can not choose format"
        if not <| SetPixelFormat(hDC, format, &pfd) then
            raise <| ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
        hGLRC <- wglCreateContext hDC

    override x.Dispose disposing =
        ignore <| wglDeleteContext hGLRC
        ignore <| ReleaseDC(x.Handle, hDC)
        base.Dispose disposing

    member x.MakeCurrent() =
        ignore <| wglMakeCurrent(hDC, hGLRC)
        new RAII(fun () -> ignore <| wglMakeCurrent(0n, 0n))

    override x.OnPaint e =
        use raii = x.MakeCurrent()
        base.OnPaint e
        ignore <| SwapBuffers hDC

type GLBitmap(width, height) =
    let mutable hBMP  = 0n
    let mutable hDC   = 0n
    let mutable hOld  = 0n
    let mutable hGLRC = 0n

    do
        let mutable bmi =
            BITMAPINFOHEADER(
                biSize        = Marshal.SizeOf<BITMAPINFOHEADER>(),
                biWidth       = width,
                biHeight      = height,
                biPlanes      = 1s,
                biBitCount    = 32s)
        let mutable ppvBits = 0n
        hBMP <- CreateDIBSection(0n, &bmi, DIB_PAL_COLORS, &ppvBits, 0n, 0)
        let hdc = GetDC 0n
        hDC <- CreateCompatibleDC(hdc)
        ignore <| ReleaseDC(0n, hdc)
        hOld <- SelectObject(hDC, hBMP)
        let mutable pfd =
            PIXELFORMATDESCRIPTOR(
                nSize        = (Marshal.SizeOf<PIXELFORMATDESCRIPTOR>() |> int16),
                nVersion     = 1s,
                dwFlags      = (PFD_DRAW_TO_BITMAP ||| PFD_SUPPORT_OPENGL ||| PFD_SUPPORT_GDI),
                cColorBits   = 32uy,
                cDepthBits   = 24uy,
                cStencilBits = 8uy)
        let format = ChoosePixelFormat(hDC, &pfd)
        if format = 0 then
            failwith "Can not choose format"
        if not <| SetPixelFormat(hDC, format, &pfd) then
            raise <| ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
        hGLRC <- wglCreateContext hDC

    interface IDisposable with
        override x.Dispose() =
            ignore <| wglDeleteContext hGLRC
            ignore <| SelectObject(hDC, hOld)
            ignore <| DeleteDC hDC
            ignore <| DeleteObject hBMP

    member x.MakeCurrent() =
        ignore <| wglMakeCurrent(hDC, hGLRC)
        new RAII(fun () -> ignore <| wglMakeCurrent(0n, 0n))

    member x.Width  = width
    member x.Height = height
    member x.Handle = hBMP
