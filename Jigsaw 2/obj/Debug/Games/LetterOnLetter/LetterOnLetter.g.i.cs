﻿#pragma checksum "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C24F739E0784C8642EA38D33D6ED1815A297AAC1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Jigsaw_2.Games;
using Jigsaw_2.Games.LetterOnLetter;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Jigsaw_2.Games.LetterOnLetter {
    
    
    /// <summary>
    /// LetterOnLetter
    /// </summary>
    public partial class LetterOnLetter : Jigsaw_2.Games.GamePage, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LetterOnLetterGrid;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle lol;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Jigsaw 2;component/games/letteronletter/letteronletter.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LetterOnLetterGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 22 "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.startStopSubmitHandler);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lol = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            
            #line 49 "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.undoHandler);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 59 "..\..\..\..\Games\LetterOnLetter\LetterOnLetter.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.feedbackHandler);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

