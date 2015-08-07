/*
 The MIT License (MIT)

Copyright (c) 2015 ericcdub

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/

using CapsLock.Properties;
using System;
using System.Windows.Forms;

namespace CapsTray
{

    /// <summary>
    /// This class encapsulates our app's system tray icon and the functionality involved in displaying and updating it
    /// in response to caps lock events
    /// 
    /// The class implements IDisposable as it uses unmanaged resources (.ico images)
    /// so we want to make sure they are cleaned up correctly
    /// </summary>
    class CapsTrayIcon : IDisposable
    {
        NotifyIcon icon;
        bool state;
        MenuItem exitMenuItem;
        public void Dispose()
        {
            icon.Dispose();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CapsTrayIcon"/> class.
        /// </summary>

        public CapsTrayIcon()
        {
            // Instantiate the NotifyIcon object.
            icon = new NotifyIcon();
            state = Control.IsKeyLocked(Keys.CapsLock);
            exitMenuItem = new MenuItem();
        
        }

        /// <summary>
		/// Displays the icon in the system tray.
		/// </summary>
		public void Display()
        {
            // Add the icon to the system tray and allow it react to mouse right-clicks with a context menu		
            icon.Icon = state ? Resources.upper : Resources.lower; // set tray icon based on current caps lock state
            icon.Text = Resources.status + (state ? Resources.on : Resources.off); 
            icon.Visible = true;

            //create a context menu with an "exit" item
            icon.ContextMenu = new System.Windows.Forms.ContextMenu();
            exitMenuItem.Index = 0; // first (and only) menu item
            exitMenuItem.Text = Resources.exit;
            exitMenuItem.Click += new System.EventHandler(exitMenuItem_Click);
            icon.ContextMenu.MenuItems.Add(exitMenuItem);
        }

        // update the icon and desription text of our icon in response to a caps lock state change
        public void updateIcon()
        {
            state = !state;
            icon.Icon = state ? Resources.upper : Resources.lower;
            icon.Text = Resources.status + (state ? Resources.on : Resources.off);


        }

        /// <summary>
        /// Handles the click of the "exit" menu item for our icon
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.EventArgs"/> instance containing the event data.</param>
        void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }


    }
}
