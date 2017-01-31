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
 *  torus.c
 *  https://www.opengl.org/archives/resources/code/samples/redbook/torus.c
 *  This program demonstrates the creation of a display list.
 *)

#load "GL7.fsx"
#load "GL7.GL.fsx"

open System
open System.Drawing
open System.IO
open System.Windows.Forms
open GL7
open GL7.GL

let mutable theTorus = 0u

// Draw a torus
let torus(numc, numt) =
    let twopi = 2.f *  float32 Math.PI
    for i = 0 to numc - 1 do
        glBegin(GL_QUAD_STRIP)
        for j = 0 to numt do
            for k in [1; 0] do
                let s = float32 ((i + k) % numc) + 0.5f
                let t = float32 (j % numt)
                let x = (1.f+0.1f*cos(s*twopi/float32 numc))*cos(t*twopi/float32 numt)
                let y = (1.f+0.1f*cos(s*twopi/float32 numc))*sin(t*twopi/float32 numt)
                let z =      0.1f*sin(s*twopi/float32 numc)
                glVertex3f(x, y, z)
        glEnd()
    glFlush()

// Create display list with Torus and initialize state
let init() =
    theTorus <- glGenLists 1
    glNewList(theTorus, GL_COMPILE)
    torus(8, 25)
    glEndList()

    glShadeModel(GL_FLAT)
    glClearColor(0.0f, 0.0f, 0.0f, 0.0f)

// Clear window and draw torus
let display() =
    glClear(GL_COLOR_BUFFER_BIT)
    glColor3f(1.0f, 1.0f, 1.0f)
    //glPolygonMode(GL_FRONT_AND_BACK, GL_LINE)
    glCallList(theTorus)
    //glPolygonMode(GL_FRONT_AND_BACK, GL_FILL)
    glFlush()

// Handle window resize
let reshape(f:GLForm) =
    let cs = f.ClientSize
    let w, h = cs.Width, cs.Height
    glViewport(0, 0, w, h)
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    gluPerspective(30., float w/float h, 1.0, 100.0)
    glMatrixMode(GL_MODELVIEW)
    glLoadIdentity()
    gluLookAt(0., 0., 10., 0., 0., 0., 0., 1., 0.)

(* Rotate about x-axis when "x" typed; rotate about y-axis
   when "y" typed; "i" returns torus to original view *)
let keyboard (f:GLForm) = function
| 'x' | 'X' ->
    glRotatef(30.f,1.0f,0.0f,0.0f)
    f.Invalidate()
| 'y' | 'Y' ->
    glRotatef(30.f,0.0f,1.0f,0.0f)
    f.Invalidate()
| 'i' | 'I' ->
    reshape f
    f.Invalidate()
| '\u001b' ->
    f.Close()
| _ -> ()

[<EntryPoint; STAThread>]
let main args =
    let argv0 = Path.GetFileNameWithoutExtension Application.ExecutablePath
    let f = new GLForm(Text = argv0, ClientSize = Size(200, 200))
    f.Load.Add <| fun _ ->
        use raii = f.MakeCurrent()
        init()
        reshape f
    f.Resize.Add <| fun _ ->
        use raii = f.MakeCurrent()
        reshape f
    f.KeyPress.Add <| fun e ->
        use raii = f.MakeCurrent()
        keyboard f e.KeyChar
    f.Paint.Add <| fun _ ->
        display()
    Application.Run f
    0
