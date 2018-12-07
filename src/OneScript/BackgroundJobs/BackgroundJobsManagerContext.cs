﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using ScriptEngine;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

namespace OneScript.WebHost.BackgroundJobs
{
    [ContextClass("МенеджерФоновыхЗаданий", "BackgroundJobsManager")]
    public class BackgroundJobsManagerContext : AutoContext<BackgroundJobsManagerContext>
    {
        private static RuntimeEnvironment _globalEnv;

        public BackgroundJobsManagerContext(RuntimeEnvironment env)
        {
            _globalEnv = env;
        }

        /// <summary>
        /// Выполняет заданный метод в фоновом режиме. Метод должен располагаться в общем модуле и быть экспортным.
        /// Параметры метода в текущей версии не поддерживаются.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [ContextMethod("Выполнить", "Execute")]
        public BackgroundJobContext Execute(string method)
        {
            var callAddr = method.Split(new[]{'.'}, 2);
            if (callAddr.Length != 2)
                throw RuntimeException.InvalidArgumentValue(method);

            var jobId = BackgroundJob.Enqueue(
                () => PerformAction(callAddr[0], callAddr[1])
            );

            return new BackgroundJobContext(jobId);
        }

        [ContextMethod("ОжидатьЗавершения")]
        public void WaitForCompletion()
        {
            throw new NotImplementedException();
        }

        // должен быть public, т.к. hangfire не умеет вызывать private
        public static void PerformAction(string module, string method)
        {
            var machine = MachineInstance.Current;
            _globalEnv.LoadMemory(machine);

            var scriptObject = (IRuntimeContextInstance)_globalEnv.GetGlobalProperty(module);
            var methodId = scriptObject.FindMethod(method);
            scriptObject.CallAsProcedure(methodId, new IValue[0]);
        }
    }
}
