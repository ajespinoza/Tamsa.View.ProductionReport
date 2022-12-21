// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BehaviorBinding.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the BehaviorBinding.
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
    using System.Windows.Media;

    /// <summary>
    /// Defines a Command Binding
    /// This inherits from freezable so that it gets inheritance context for DataBinding to work
    /// </summary>
    public class BehaviorBinding : Freezable
    {
        #region Fields

        /// <summary>
        /// Command Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(BehaviorBinding), new FrameworkPropertyMetadata((ICommand)null, new PropertyChangedCallback(OnCommandChanged)));

        /// <summary>
        /// Action Dependency Property
        /// </summary>
        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(Action<object>), typeof(BehaviorBinding), new FrameworkPropertyMetadata((Action<object>)null, new PropertyChangedCallback(OnActionChanged)));

        /// <summary>
        /// CommandParameter Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(BehaviorBinding), new FrameworkPropertyMetadata((object)null, new PropertyChangedCallback(OnCommandParameterChanged)));

        /// <summary>
        /// Event Dependency Property
        /// </summary>
        public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(string), typeof(BehaviorBinding), new FrameworkPropertyMetadata((string)null, new PropertyChangedCallback(OnEventChanged)));

        /// <summary>
        /// Behavior var.
        /// </summary>
        private CommandBehaviorBinding behavior;

        /// <summary>
        /// owner var.
        /// </summary>
        private DependencyObject owner;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Owner of the binding
        /// </summary>
        public DependencyObject Owner
        {
            get
            {
                return this.owner;
            }

            set
            {
                this.owner = value;
                this.ResetEventBinding();
            }
        }

        /// <summary>
        /// Gets or sets the Command property.  
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Action property. 
        /// </summary>
        public Action<object> Action
        {
            get { return (Action<object>)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the CommandParameter property.  
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Event property.  
        /// </summary>
        public string Event
        {
            get { return (string)GetValue(EventProperty); }
            set { SetValue(EventProperty, value); }
        }

        #endregion

        #region Public methods

        #endregion

        #region Internal methods

        /// <summary>
        /// Gets stores the Command Behavior Binding
        /// </summary>
        internal CommandBehaviorBinding Behavior
        {
            get
            {
                if (this.behavior == null)
                {
                    this.behavior = new CommandBehaviorBinding();
                }

                return this.behavior;
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Owner reset.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// The property changed event.
        /// </param>
        public static void OwnerReset(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BehaviorBinding)d).ResetEventBinding();
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Command property.
        /// </summary>
        /// <param name="e">
        /// Property changed event.
        /// </param>
        protected virtual void OnCommandChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Behavior.Command = this.Command;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Action property.
        /// </summary>
        /// <param name="e">
        /// Property chaged object.
        /// </param>
        protected virtual void OnActionChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Behavior.Action = this.Action;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the CommandParameter property.
        /// </summary>
        /// <param name="e">
        /// Property chaged object.
        /// </param>
        protected virtual void OnCommandParameterChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Behavior.CommandParameter = this.CommandParameter;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Event property.
        /// </summary>
        /// <param name="e">
        /// Property chaged object.
        /// </param>
        protected virtual void OnEventChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ResetEventBinding();
        }

        /// <summary>
        /// This is not actually used. This is just a trick so that this object gets WPF Inheritance Context
        /// </summary>
        /// <returns>
        /// The exception
        /// </returns>
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles changes to the Command property.
        /// </summary>
        /// <param name="d">
        /// dependency object.
        /// </param>
        /// <param name="e">
        /// Property changed.
        /// </param>
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BehaviorBinding)d).OnCommandChanged(e);
        }

        /// <summary>
        /// Handles changes to the Action property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        /// Property chaged object.
        /// </param>
        private static void OnActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BehaviorBinding)d).OnActionChanged(e);
        }

        /// <summary>
        /// Handles changes to the CommandParameter property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        ///  Property chaged object.
        /// </param>
        private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BehaviorBinding)d).OnCommandParameterChanged(e);
        }

        /// <summary>
        /// Handles changes to the Event property.
        /// </summary>
        /// <param name="d">
        /// Dependency object.
        /// </param>
        /// <param name="e">
        ///  Property chaged object.
        /// </param>
        private static void OnEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BehaviorBinding)d).OnEventChanged(e);
        }

        /// <summary>
        /// Reset event binding.
        /// </summary>
        private void ResetEventBinding()
        {
            if (this.Owner != null)
            {
                // check if the Event is set. If yes we need to rebind the Command to the new event and unregister the old one
                if (this.Behavior.Event != null && this.Behavior.Owner != null)
                {
                    this.Behavior.Dispose();
                }

                // bind the new event to the command
                this.Behavior.BindEvent(this.Owner, this.Event);
            }
        }

        #endregion
    }
}
