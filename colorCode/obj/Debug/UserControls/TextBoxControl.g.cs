#pragma checksum "..\..\..\UserControls\TextBoxControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F0160DE541B1F654B48D07C3EF96EB2FAC356C24AE21EFF4A8FA4826DE346E74"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using colorCode;


namespace colorCode {
    
    
    /// <summary>
    /// TextBoxControl
    /// </summary>
    public partial class TextBoxControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\UserControls\TextBoxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal colorCode.TextBoxControl textBoxControl;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\UserControls\TextBoxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtValue;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\UserControls\TextBoxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUp;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\UserControls\TextBoxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDown;
        
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
            System.Uri resourceLocater = new System.Uri("/colorCode;component/usercontrols/textboxcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\TextBoxControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.textBoxControl = ((colorCode.TextBoxControl)(target));
            return;
            case 2:
            this.txtValue = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\..\UserControls\TextBoxControl.xaml"
            this.txtValue.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtValue_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnUp = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\UserControls\TextBoxControl.xaml"
            this.btnUp.Click += new System.Windows.RoutedEventHandler(this.btnUp_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnDown = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\UserControls\TextBoxControl.xaml"
            this.btnDown.Click += new System.Windows.RoutedEventHandler(this.btnDown_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

