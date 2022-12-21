// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBehaviorBinding.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the CommandBehaviorBinding.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Defines the command behavior binding
    /// </summary>
    public class CommandBehaviorBinding : IDisposable
    {
        #region Fields

        /// <summary>
        /// Stores the strategy of how to execute the event handler
        /// </summary>
        private IExecutionStrategy strategy;

        /// <summary>
        /// Command object.
        /// </summary>
        private ICommand command;

        /// <summary>
        /// Action object.
        /// </summary>
        private Action<object> action;

        /// <summary>
        /// Dispose value.
        /// </summary>
        private bool disposed = false;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the owner of the CommandBinding ex: a Button
        /// This property can only be set from the BindEvent Method
        /// </summary>
        public DependencyObject Owner { get; private set; }

        /// <summary>
        /// Gets the event name to hook up to
        /// This property can only be set from the BindEvent Method
        /// </summary>
        public string EventName { get; private set; }

        /// <summary>
        /// Gets the event info of the event
        /// </summary>
        public EventInfo Event { get; private set; }

        /// <summary>
        /// Gets the EventHandler for the binding with the event
        /// </summary>
        public Delegate EventHandler { get; private set; }

        #region Execution

        /// <summary>
        /// Gets or sets a CommandParameter
        /// </summary>
        public object CommandParameter { get; set; }

        /// <summary>
        /// Gets or sets the command to execute when the specified event is raised
        /// </summary>
        public ICommand Command
        {
            get 
            { 
                return this.command; 
            }

            set
            {
                this.command = value;

                // set the execution strategy to execute the command
                this.strategy = new CommandExecutionStrategy { Behavior = this };
            }
        }

        /// <summary>
        /// Gets or sets the Action
        /// </summary>
        public Action<object> Action
        {
            get
            { 
                return this.action; 
            }

            set
            {
                this.action = value;

                // set the execution strategy to execute the action
                this.strategy = new ActionExecutionStrategy { Behavior = this };
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Creates an EventHandler on runtime and registers that handler to the Event specified
        /// </summary>
        /// <param name="owner">
        /// The owner object.
        /// </param>
        /// <param name="eventName">
        /// The event name.
        /// </param>
        public void BindEvent(DependencyObject owner, string eventName)
        {
            this.EventName = eventName;
            this.Owner = owner;
            this.Event = this.Owner.GetType().GetEvent(this.EventName, BindingFlags.Public | BindingFlags.Instance);
            if (this.Event == null)
            {
                throw new InvalidOperationException(String.Format("Could not resolve event name {0}", this.EventName));
            }

            // Create an event handler for the event that will call the ExecuteCommand method
            this.EventHandler = EventHandlerGenerator.CreateDelegate(
                this.Event.EventHandlerType, typeof(CommandBehaviorBinding).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance), this);
            
            // Register the handler to the Event
            this.Event.AddEventHandler(this.Owner, this.EventHandler);
        }

        /// <summary>
        /// Executes the strategy
        /// </summary>
        public void Execute()
        {
            try
            {
                this.strategy.Execute(this.CommandParameter);
            }
            catch
            { }
        }

        #region IDisposable Members

        /// <summary>
        /// Unregisters the EventHandler from the Event
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.Event.RemoveEventHandler(this.Owner, this.EventHandler);
                this.disposed = true;
            }
        }

        #endregion
    }
}
