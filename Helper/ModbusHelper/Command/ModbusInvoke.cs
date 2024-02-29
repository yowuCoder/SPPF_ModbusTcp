
using Serilog;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace kl_modbus.Command
{
    public class ModbusInvoke
    {
        private List<ModbusCommand> _command;

        private readonly List<Task> _tasks = new List<Task>();
        public ModbusInvoke(List<ModbusCommand> command)
        {
            _command = command;
        }
        /* public ModbusInvoke()
         {

         }
         public void AddCommand(List<ModbusCommand> command)
         {
             _command = command;
         }
         public void ClearCommand()
         {
             _command.Clear();
         }*/
        public async Task Invoke()
        {
            try
            {

                foreach (var command in _command)
                {
                    if (command == null)
                    {
                        continue;
                    }
                    //await command.Execute();
                    _tasks.Add(command.Execute());

                }
                await Task.WhenAll(_tasks);

            }
            catch (Exception e)
            {

                Log.Error(e.ToString());
                throw;
            }
        }


    }
}
