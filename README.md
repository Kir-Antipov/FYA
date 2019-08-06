# F*CK YOU ADOBE (a.k.a. FYA)
-------------

## What is going on?

A lot of people encounter the `EXCEPTION_ACCESS_VIOLATION` error when starting **Photoshop** and some other **Adobe** programs. 

Sometimes updating of video card's drivers could help you, sometimes - manually registry editing, but most often this problem is related to the fact that Photoshop doesn't work correctly on the integrated graphics card, which for some reason it chooses by default.

*Looks like a joke*, but Photoshop often chooses the worst of the two video cards :upside_down_face: 

Two things could help you in this case:

 1. Using `Switchable Graphics` to force Photoshop to use a more powerful graphics card.

 2. Disabling the integrated graphics card.

And what could do those who can't resort to any of these points? Refuse Photoshop? Install an older version?

Be that as it may, such questions remain unresolved:

 - https://forums.adobe.com/thread/2400245
 - https://feedback.photoshop.com/photoshop_family/topics/cc-2019-error-crashes-if-one-tries-to-close-out-the-library-help-tab-group

And so on...

Me and my friend came up with a funny solution: during the launch of Photoshop, we can disable the integrated graphics card (thanks to this it will not be able to detect it and will be forced to use the more powerful video card) and then turn it back on. This completely solves the problem of launching Photoshop and other problematic Adobe products without requiring any serious cost!

This program simply automates this excellent crutch-oriented solution, allowing you to run software as it was before (without any unnecessary actions)!

## Fast start

If you have the same problem, follow these simple steps:

 1. Unzip the [latest release](https://github.com/Kir-Antipov/FYA/releases/latest) into some static folder.
 2. Call `FYA.exe` from the console as follows:

```cmd
fya shortcut --path "path_to_your_problem_exe" --name "Integrated graphics card name or its part"
```

Example:

```cmd
fya shortcut --path "C:\Program Files\Adobe\Adobe Photoshop (64 Bit)\Photoshop.exe" --name Intel
```
 3. Copy the shortcut *(it will appear in the active directory)* to wherever you want.

The shortcut looks like a launching application, but in fact it will launch `HFYA.exe`⁰, which will disable the specified graphics card, then it will launch the desired application and turn it back on)

⁰**P.S.** - `HFYA.exe` is a copy of `FYA.exe`, which is compiled as *WinExe* and by default asks for administrator rights for graphics card disabling/enabling (see `NoWindow` configuration)

## More commands

`FYA` can do some more things! To see the complete list of commands just call it with the `help` parameter:

```cmd
fya help
```

```cmd
fya help card
```