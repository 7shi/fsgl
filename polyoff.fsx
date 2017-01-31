(*
 * Copyright (c) 1993-1997, Silicon Graphics, Inc.
 * ALL RIGHTS RESERVED 
 * Permission to use, copy, modify, and distribute this software for 
 * any purpose and without fee is hereby granted, provided that the above
 * copyright notice appear in all copies and that both the copyright notice
 * and this permission notice appear in supporting documentation, and that 
 * the name of Silicon Graphics, Inc. not be used in advertising
 * or publicity pertaining to distribution of the software without specific,
 * written prior permission. 
 *
 * THE MATERIAL EMBODIED ON THIS SOFTWARE IS PROVIDED TO YOU "AS-IS"
 * AND WITHOUT WARRANTY OF ANY KIND, EXPRESS, IMPLIED OR OTHERWISE,
 * INCLUDING WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL SILICON
 * GRAPHICS, INC.  BE LIABLE TO YOU OR ANYONE ELSE FOR ANY DIRECT,
 * SPECIAL, INCIDENTAL, INDIRECT OR CONSEQUENTIAL DAMAGES OF ANY
 * KIND, OR ANY DAMAGES WHATSOEVER, INCLUDING WITHOUT LIMITATION,
 * LOSS OF PROFIT, LOSS OF USE, SAVINGS OR REVENUE, OR THE CLAIMS OF
 * THIRD PARTIES, WHETHER OR NOT SILICON GRAPHICS, INC.  HAS BEEN
 * ADVISED OF THE POSSIBILITY OF SUCH LOSS, HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, ARISING OUT OF OR IN CONNECTION WITH THE
 * POSSESSION, USE OR PERFORMANCE OF THIS SOFTWARE.
 * 
 * US Government Users Restricted Rights 
 * Use, duplication, or disclosure by the Government is subject to
 * restrictions set forth in FAR 52.227.19(c)(2) or subparagraph
 * (c)(1)(ii) of the Rights in Technical Data and Computer Software
 * clause at DFARS 252.227-7013 and/or in similar or successor
 * clauses in the FAR or the DOD or NASA FAR Supplement.
 * Unpublished-- rights reserved under the copyright laws of the
 * United States.  Contractor/manufacturer is Silicon Graphics,
 * Inc., 2011 N.  Shoreline Blvd., Mountain View, CA 94039-7311.
 *
 * OpenGL(R) is a registered trademark of Silicon Graphics, Inc.
 *)

(*
 *  polyoff.c
 *  https://www.opengl.org/archives/resources/code/samples/redbook/polyoff.c
 *  This program demonstrates polygon offset to draw a shaded
 *  polygon and its wireframe counterpart without ugly visual
 *  artifacts ("stitching").
 *)

#load "GL7.fsx"
#load "GL7.GL.fsx"
#load "GL7.GLUT.fsx"

open System
open System.Drawing
open System.IO
open System.Windows.Forms
open GL7
open GL7.GL
open GL7.GLUT

let mutable list  = 0u
let mutable spinx = 0
let mutable spiny = 0
let mutable tdist = 0.0f
let mutable polyfactor = 1.0f
let mutable polyunits  = 1.0f

(*  display() draws two spheres, one with a gray, diffuse material,
 *  the other sphere with a magenta material with a specular highlight.
 *)
let display() =
    let mat_ambient  = [|0.8f; 0.8f; 0.8f; 1.0f|]
    let mat_diffuse  = [|1.0f; 0.0f; 0.5f; 1.0f|]
    let mat_specular = [|1.0f; 1.0f; 1.0f; 1.0f|]
    let gray         = [|0.8f; 0.8f; 0.8f; 1.0f|]
    let black        = [|0.0f; 0.0f; 0.0f; 1.0f|]

    glClear(GL_COLOR_BUFFER_BIT ||| GL_DEPTH_BUFFER_BIT)
    glPushMatrix()
    glTranslatef(0.0f, 0.0f, tdist)
    glRotatef(float32 spinx, 1.0f, 0.0f, 0.0f)
    glRotatef(float32 spiny, 0.0f, 1.0f, 0.0f)

    glMaterialfv(GL_FRONT, GL_AMBIENT_AND_DIFFUSE, gray)
    glMaterialfv(GL_FRONT, GL_SPECULAR, black)
    glMaterialf(GL_FRONT, GL_SHININESS, 0.0f)
    glEnable(GL_LIGHTING)
    glEnable(GL_LIGHT0)
    glEnable(GL_POLYGON_OFFSET_FILL)
    glPolygonOffset(polyfactor, polyunits)
    glCallList(list)
    glDisable(GL_POLYGON_OFFSET_FILL)

    glDisable(GL_LIGHTING)
    glDisable(GL_LIGHT0)
    glColor3f(1.0f, 1.0f, 1.0f)
    glPolygonMode(GL_FRONT_AND_BACK, GL_LINE)
    glCallList(list)
    glPolygonMode(GL_FRONT_AND_BACK, GL_FILL)

    glPopMatrix()
    glFlush()

(*  specify initial properties
 *  create display list with sphere  
 *  initialize lighting and depth buffer
 *)
let gfxinit() =
    let light_ambient  = [|0.0f; 0.0f; 0.0f; 1.0f|]
    let light_diffuse  = [|1.0f; 1.0f; 1.0f; 1.0f|]
    let light_specular = [|1.0f; 1.0f; 1.0f; 1.0f|]
    let light_position = [|1.0f; 1.0f; 1.0f; 0.0f|]

    let global_ambient = [|0.2f; 0.2f; 0.2f; 1.0f|]

    glClearColor(0.0f, 0.0f, 0.0f, 1.0f)

    list <- glGenLists(1)
    glNewList(list, GL_COMPILE)
    glutSolidSphere(1.0, 20, 12)
    glEndList()

    glEnable(GL_DEPTH_TEST)

    glLightfv(GL_LIGHT0, GL_AMBIENT, light_ambient)
    glLightfv(GL_LIGHT0, GL_DIFFUSE, light_diffuse)
    glLightfv(GL_LIGHT0, GL_SPECULAR, light_specular)
    glLightfv(GL_LIGHT0, GL_POSITION, light_position)
    glLightModelfv(GL_LIGHT_MODEL_AMBIENT, global_ambient)

//  call when window is resized
let reshape (f:GLForm) =
    let cr = f.ClientSize
    let width, height = cr.Width, cr.Height
    glViewport(0, 0, width, height)
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    gluPerspective(45.0, float width / float height,
        1.0, 10.0)
    glMatrixMode(GL_MODELVIEW)
    glLoadIdentity()
    gluLookAt(0.0, 0.0, 5.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0)

//  call when mouse button is pressed
let mouse (f:GLForm) button down =
    match button, down with
    | MouseButtons.Left, true ->
        spinx <- (spinx + 5) % 360
        f.Invalidate()
    | MouseButtons.Middle, true ->
        spiny <- (spiny + 5) % 360
        f.Invalidate()
    | MouseButtons.Right, false ->
        f.Close()
    | _ -> ()

let keyboard (f:GLForm) = function
| 't' ->
    if tdist < 4.0f then
        tdist <- tdist + 0.5f
        f.Invalidate()
| 'T' ->
    if tdist > -5.0f then
        tdist <- tdist - 0.5f
        f.Invalidate()
| 'F' ->
    polyfactor <- polyfactor + 0.1f
    printfn "polyfactor is %f" polyfactor
    f.Invalidate()
| 'f' ->
    polyfactor <- polyfactor - 0.1f
    printfn "polyfactor is %f" polyfactor
    f.Invalidate()
| 'U' ->
    polyunits <- polyunits + 1.0f
    printfn "polyunits is %f" polyunits
    f.Invalidate()
| 'u' ->
    polyunits <- polyunits - 1.0f
    printfn "polyunits is %f" polyunits
    f.Invalidate()
| _ -> ()

(*  Main Loop
 *  Open window with initial window size, title bar, 
 *  RGBA display mode, and handle input events.
 *)
[<EntryPoint; STAThread>]
let main args =
    let argv0 = Path.GetFileNameWithoutExtension Application.ExecutablePath
    let f = new GLForm(Text = argv0)
    f.Load.Add <| fun _ ->
        use raii = f.MakeCurrent()
        gfxinit()
        reshape f
    f.Resize.Add <| fun _ ->
        use raii = f.MakeCurrent()
        reshape f
    f.Paint.Add <| fun _ ->
        display()
    f.MouseDown.Add <| fun e ->
        mouse f e.Button true
    f.MouseUp.Add <| fun e ->
        mouse f e.Button false
    f.KeyPress.Add <| fun e ->
        keyboard f e.KeyChar
    Application.Run f
    0
