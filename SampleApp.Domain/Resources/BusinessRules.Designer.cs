﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SampleApp.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class BusinessRules {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BusinessRules() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SampleApp.Domain.Resources.BusinessRules", typeof(BusinessRules).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An address has to have a country set.
        /// </summary>
        internal static string AddressCountryRequired {
            get {
                return ResourceManager.GetString("AddressCountryRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A task has to have a name set.
        /// </summary>
        internal static string TaskNameRequired {
            get {
                return ResourceManager.GetString("TaskNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A task has to have time set.
        /// </summary>
        internal static string TaskTimeRequired {
            get {
                return ResourceManager.GetString("TaskTimeRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A task has to have a user set.
        /// </summary>
        internal static string TaskUserRequired {
            get {
                return ResourceManager.GetString("TaskUserRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A user has to have at least first or last name set.
        /// </summary>
        internal static string UserNameRequired {
            get {
                return ResourceManager.GetString("UserNameRequired", resourceCulture);
            }
        }
    }
}
