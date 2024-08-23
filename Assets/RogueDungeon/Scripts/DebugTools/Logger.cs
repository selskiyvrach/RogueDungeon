using System;
using System.Text;
using UnityEngine;

namespace RogueDungeon.DebugTools
{
    public interface IDebugName
    {
        string DebugName { get; }
    }

    public static class Logger
    {
        private static readonly StringBuilder Builder = new();
        
        public static void Log(object sender, string message, params object[] arguments) => 
            SendToLogging(CreateResultMessage(sender, message, arguments), LogType.Log);

        public static void LogError(object sender, string message, params object[] arguments) => 
            SendToLogging(CreateResultMessage(sender, message, arguments), LogType.Error);

        public static void LogWarning(object sender, string message, params object[] arguments) => 
            SendToLogging(CreateResultMessage(sender, message, arguments), LogType.Warning);

        private static string CreateResultMessage(object sender, string message, params object[] arguments)
        {
            Builder.Clear();
            Builder.Append(GetHeader(sender));
            Builder.Append('\n');
            Builder.Append(GetFormattedMessage(message, arguments));
            return Builder.ToString();
        }

        private enum LogType
        {
            Log,
            Error,
            Warning
        }

        private static void SendToLogging(string message, LogType type)
        {
            switch (type)
            {
                case LogType.Log: Debug.Log(message);
                    break;
                case LogType.Error: Debug.LogError(message);
                    break;
                case LogType.Warning: Debug.LogWarning(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static string GetFormattedMessage(string message, object[] arguments)
        {
            if (arguments is null || arguments.Length == 0)
                return message;
            FormatArguments(arguments);
            return string.Format(message, arguments);
        }

        private static void FormatArguments(object[] sourceArguments)
        {
            for (var i = 0; i < sourceArguments.Length; i++)
                sourceArguments[i] = GetObjectDisplayName(sourceArguments[i]);
        }

        private static string GetHeader(object sender) => 
            $"[{GetObjectDisplayName(sender)}], frame: {UnityEngine.Time.frameCount}";

        private static string GetObjectDisplayName(object obj)
        {
            if (obj is not IDebugName debugName)
                return obj.ToString();
            
            if (!debugName.DebugName.IsNullOrEmpty()) 
                return debugName.DebugName;
            
            SendToLogging("IDebugName on " + obj + " has no value", LogType.Warning);
            return obj.ToString();
        }
    }
    
}