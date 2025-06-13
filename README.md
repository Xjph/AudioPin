# AudioPin

Ever been annoyed by Windows audio defaults changing when you plug or unplug devices?

This utility allows a user to "pin" a selection of audio devices in windows with a prioritised list. On any change to audio devices AudioPin should very quickly re-assert your pinned devices setting the highest prioritised available device as default.

Defaults for media and communications can be pinned individually.

## Hidden Auto-Start

Hold shift while checking the "Launch on Startup" checkbox to enable "hidden" auto-start. This will start AudioPin without showing any UI elements, allowing it to run in the background and manage audio devices without user interaction.

## Command-Line Options

- `/min`: Start minimized to the system tray.
- `/hide`: Start completely hidden. Re-launch AudioPin without `/hide` to stop the hidden session and restore the main window.
