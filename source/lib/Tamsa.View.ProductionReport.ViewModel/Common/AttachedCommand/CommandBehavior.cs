// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBehavior.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the CommandBehavior.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;

    /// <summary>
    /// Defines the attached properties to create a CommandBehaviorBinding
    /// </summary>
    public class CommandBehavior
    {
        #region Fields

        /// <summary>
        /// Command Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandBehavior), new FrameworkPropertyMetadata((ICommand)null, new PropertyChangedCallback(OnCommandChanged)));

        /// <summary>
        /// Action Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ActionProperty = DependencyProperty.RegisterAttached("Action", typeof(Action<object>), typeof(CommandBehavior), new FrameworkPropertyMetadata((Action<object>)null, new PropertyChangedCallback(OnActionChanged)));

        /// <summary>
        /// CommandParameter Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(CommandBehavior), new FrameworkPropertyMetadata((object)null, new PropertyChangedCallback(OnCommandParameterChanged)));

        /// <summary>
        /// Event Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty EventProperty = DependencyProperty.RegisterAttached("Event", typeof(string), typeof(CommandBehavior), new FrameworkPropertyMetadata((string)String.Empty, new PropertyChangedCallback(OnEventChanged)));

        /// <summary>
        /// Behavior Attached Dependency Property
        /// </summary>
        private static readonly DependencyProperty BehaviorProperty = DependencyProperty.RegisterAttached("Behavior", typeof(CommandBehaviorBinding), typeof(CommandBehavior), new FrameworkPropertyMetadata((CommandBehaviorBinding)null));

        #endregion

        #region Public methods

        /// <summary>
        /// Sets the Command property. 
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="value">
        /// The command behavior value.
        /// </param>
        public static void SetCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Sets the CommandParameter property. 
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="value">
        /// Dependency property changed.
        /// </param>
        public static void SetCommandParameter(DependencyObject d, object value)
        {
            d.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Sets the Action property.
        /// </summary>
        /// <param name="d">
        /// Dependency property
        /// </param>
        /// <param name="value">
        /// The object value.
        /// </param>
        public static void SetAction(DependencyObject d, Action<object> value)
        {
            d.SetValue(ActionProperty, value);
        }

        /// <summary>
        /// Sets the Event property.  This dependency property 
        /// indicates ....
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="value">
        /// Dependency property changed.
        /// </param>
        public static void SetEvent(DependencyObject d, string value)
        {
            d.SetValue(EventProperty, value);
        }

        /// <summary>
        /// Gets the Command property.  
        /// </summary>
        /// <param name="d">
        /// Dependency property value
        /// </param>
        /// <returns>
        /// The object value.
        /// </returns>
        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        /// <summary>
        /// Gets the CommandParameter property.  
        /// </summary>
        /// <param name="d">
        /// Dependency property value
        /// </param>
        /// <returns>
        /// The object value.
        /// </returns>
        public static object GetCommandParameter(DependencyObject d)
        {
            return (object)d.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// Gets the Action property.  
        /// </summary>
        /// <param name="d">
        /// Dependency object value.
        /// </param>
        /// <returns>
        /// The object value.
        /// </returns>
        public static Action<object> GetAction(DependencyObject d)
        {
            return (Action<object>)d.GetValue(ActionProperty);
        }

        /// <summary>
        /// Gets the Event property.  This dependency property 
        /// indicates ....
        /// </summary>
        /// <param name="d">
        /// Dependency property value
        /// </param>
        /// <returns>
        /// The object value.
        /// </returns>
        public static string GetEvent(DependencyObject d)
        {
            return (string)d.GetValue(EventProperty);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the Behavior property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="value">
        /// The command behavior value.
        /// </param>
        private static void SetBehavior(DependencyObject d, CommandBehaviorBinding value)
        {
            d.SetValue(BehaviorProperty, value);
        }

        /// <summary>
        /// Handles changes to the Command property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// The parameter value.
        /// </param>
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandBehaviorBinding binding = FetchOrCreateBinding(d);
            binding.Command = (ICommand)e.NewValue;
        }

        /// <summary>
        /// Handles changes to the Action property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Dependency property changed.
        /// </param>
        private static void OnActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandBehaviorBinding binding = FetchOrCreateBinding(d);
            binding.Action = (Action<object>)e.NewValue;
        }

        /// <summary>
        /// Handles changes to the CommandParameter property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Dependency property changed.
        /// </param>
        private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandBehaviorBinding binding = FetchOrCreateBinding(d);
            binding.CommandParameter = e.NewValue;
        }

        /// <summary>
        /// Handles changes to the Event property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Dependency property changed.
        /// </param>
        private static void OnEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandBehaviorBinding binding = FetchOrCreateBinding(d);

            // check if the Event is set. If yes we need to rebind the Command to the new event and unregister the old one
            if (binding.Event != null && binding.Owner != null)
            {
                binding.Dispose();
            }

            // bind the new event to the command
            binding.BindEvent(d, e.NewValue.ToString());
        }

        /// <summary>
        /// Gets the Behavior property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <returns>
        /// The command behavior
        /// </returns>
        private static CommandBehaviorBinding GetBehavior(DependencyObject d)
        {
            return (CommandBehaviorBinding)d.GetValue(BehaviorProperty);
        }

        /// <summary>
        /// Tries to get a CommandBehaviorBinding from the element. Creates a new instance if there is not one attached
        /// </summary>
        /// <param name="d">
        /// Deependency object.
        /// </param>
        /// <returns>
        /// The command behavior.
        /// </returns>
        private static CommandBehaviorBinding FetchOrCreateBinding(DependencyObject d)
        {
            CommandBehaviorBinding binding = CommandBehavior.GetBehavior(d);
            if (binding == null)
            {
                binding = new CommandBehaviorBinding();
                CommandBehavior.SetBehavior(d, binding);
            }

            return binding;
        }
        
        #endregion
    }
}
